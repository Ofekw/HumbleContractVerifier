namespace HumbleVerifierConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ConsoleTables;
    using HumbleVerifierLibrary;
    using HumbleVerifierLibrary.Contracts;
    using Nethereum.ABI.FunctionEncoding;
    using Nethereum.Contracts;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Runner
    {
        private readonly ChainTools chainTools;

        private readonly AbiValidator abiValidator;

        private readonly VerifierResponseDto verifierResponseDto;

        public Runner(ChainTools chainTools, AbiValidator abiValidator)
        {
            this.chainTools = chainTools;
            this.abiValidator = abiValidator;
            this.verifierResponseDto = new VerifierResponseDto();
        }

        /// <summary>Run the logic</summary>
        /// <param name="contractAddress">Address of the contract to check. If masterchefonly, this
        /// is masterchef. Otherwise this is token address</param>
        /// <param name="isMasterchefOnly">Validate masterchef only</param>
        /// <returns>Async task</returns>
        public async Task Run(string contractAddress, bool isMasterchefOnly)
        {
            string masterChef = contractAddress;
            string tokenName = "TokenName";
            string tokenAddress = "TokenAddress";

            if (!isMasterchefOnly)
            {
                tokenAddress = contractAddress;
                tokenName = await this.chainTools.GetFunction<SymbolFunction, string>(contractAddress);
                masterChef = await this.chainTools.GetFunction<OwnerOfFunction, string>(contractAddress); // should be masterchef
                this.verifierResponseDto.MasterChefAddress = masterChef;
                Console.WriteLine($"Token Name: {tokenName}");
                Console.WriteLine($"Token Address: {tokenAddress}");
            }

            Console.WriteLine($"MasterChef Address: {masterChef}");
            JToken masterchefSource = await GetSourceCode(masterChef, this.chainTools, this.abiValidator.APIKey);

            if (!this.chainTools.UseLocalAbi && !IsContract(masterchefSource))
            {
                Console.WriteLine($"!!! Owner of token {tokenName} is not a contract !!!");
                this.verifierResponseDto.OwnerOfTokenIsAContract = false;
                return;
            }

            int startBlock = await this.chainTools.GetFunction<StartBlockFunction, int>(masterChef);
            this.verifierResponseDto.StartBlock = startBlock;
            Console.WriteLine($"StartBlock: {startBlock}");

            if (this.chainTools.UseLocalAbi)
            {
                Console.WriteLine("TO BE IMPLEMENTED: Estimate launch date");
            }
            else
            {
                DateTime estLaunch = await GetLaunchDate(startBlock, this.chainTools, this.abiValidator.APIKey);
                this.verifierResponseDto.EstimatedLaunchTime = estLaunch;
                Console.WriteLine("Estimated launch date (PST): " + (estLaunch == DateTime.MinValue ? "Already launched" : estLaunch.ToString("G")));
            }

            JToken timeLockSource = JToken.FromObject(string.Empty);

            try
            {
                timeLockSource = await this.ProcessTimelock(masterChef);
            }
            catch (Exception e)
            {
                Console.WriteLine("Issue processing timelock: " + e.Message);
            }

            try
            {
                string masterchefAbi = await this.abiValidator.FetchAbiFromApiAsync(masterChef);

                if (masterchefAbi == null)
                {
                    return;
                }

                List<string> methodsWithPossibleWithdrawFee = this.abiValidator.GetMethodsWithPossibleWithdrawalFee(masterchefAbi);

                if (methodsWithPossibleWithdrawFee.Any())
                {
                    Console.WriteLine("!!! WARNING: Methods with possible withdrawal fee detected !!!");

                    foreach (string method in methodsWithPossibleWithdrawFee)
                    {
                        Console.WriteLine(method);
                        this.verifierResponseDto.DangerousContractMethods.Add(method);
                    }
                }

                Console.WriteLine("Pool info:");

                JArray abiArray = JArray.Parse(masterchefAbi);
                JToken poolInfoFunctionBlock = AbiValidator.GetFunctionsByName(abiArray, "poolInfo").FirstOrDefault();

                if (poolInfoFunctionBlock != null)
                {
                    string functionName = poolInfoFunctionBlock["name"].ToString();

                    Contract contract = this.chainTools.Web3.Eth.GetContract(masterchefAbi, masterChef);
                    Function poolInfoFunc = contract.GetFunction(functionName);

                    var poolInfoOutputs = new List<string> { "pair" };

                    foreach (JToken output in poolInfoFunctionBlock["outputs"])
                    {
                        string outputName = output["name"].ToString();
                        poolInfoOutputs.Add(outputName);
                    }

                    int poolLength = await this.chainTools.GetFunction<PoolLengthFunction, int>(masterChef);
                    var table = new ConsoleTable(poolInfoOutputs.ToArray());

                    for (int i = 0; i < poolLength; i++)
                    {
                        var dto = new PoolDto();
                        List<ParameterOutput> result = await poolInfoFunc.CallDecodingToDefaultAsync(i);

                        string pairString;
                        string token0 = null;

                        try
                        {
                            token0 = await this.chainTools.GetFunction<Token0Function, string>(result[0].Result.ToString());
                        }
                        catch
                        {
                        }

                        if (!string.IsNullOrWhiteSpace(token0))
                        {
                            string token1 = await this.chainTools.GetFunction<Token1Function, string>(result[0].Result.ToString());
                            string token0Name = await this.chainTools.GetFunction<SymbolFunction, string>(token0);
                            string token1Name = await this.chainTools.GetFunction<SymbolFunction, string>(token1);
                            dto.TokenOneSymbol = token0Name;
                            dto.TokenTwoSymbol = token1Name;
                            pairString = $"{token0Name}-{token1Name}";
                        }
                        else
                        {
                            pairString = await this.chainTools.GetFunction<SymbolFunction, string>(result[0].Result.ToString());
                        }

                        var results = new List<string> { pairString };
                        string[] poolOutputs = result.Select(x => x.Result.ToString()).ToArray();
                        results.AddRange(poolOutputs);
                        table.AddRow(results.ToArray());
                    }

                    table.Write(Format.MarkDown);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error fetching pool info: " + e);
            }

            if (this.chainTools.UseLocalAbi)
            {
                Console.WriteLine("TO BE IMPLEMENTED: Fetch contract source code");
            }
            else
            {
                string path = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), tokenName);
                var dir = new DirectoryInfo(path);

                if (dir.Exists)
                {
                    dir.Delete(true);
                }

                dir.Create();

                Console.WriteLine($"Saving contracts to: {dir.FullName}");

                var writer = new ContractWriter(new FileSystem());
                JToken tokenContract = Deserializer.SafeUnencodeJson((await GetSourceCode(contractAddress, this.chainTools, this.abiValidator.APIKey)).ToString(), out ContractType contractType);
                writer.WriteToDisk(tokenContract, contractType, tokenName, dir.FullName);
                JToken masterchefContract = Deserializer.SafeUnencodeJson(masterchefSource.ToString(), out contractType);
                writer.WriteToDisk(masterchefContract, contractType, "MasterChef", dir.FullName);
                if (IsContract(timeLockSource))
                {
                    JToken timelockContract = Deserializer.SafeUnencodeJson(timeLockSource.ToString(), out contractType);
                    writer.WriteToDisk(timelockContract, contractType, "Timelock", dir.FullName);
                }
            }
        }

        private static bool IsContract(JToken fetchedSourceCode)
        {
            return (fetchedSourceCode.ToString() != string.Empty) && !fetchedSourceCode.ToString().Contains("Contract source code not verified");
        }

        private static async Task<DateTime> GetLaunchDate(int block, ChainTools chainTools, string apiKey)
        {
            HttpResponseMessage response = await chainTools.HttpClient.GetAsync($"api?module=block&action=getblockcountdown&blockno={block}&apikey={apiKey}");
            string body = await response.Content.ReadAsStringAsync();
            var jObject = JsonConvert.DeserializeObject<JObject>(body);

            JToken result = jObject["result"];

            if (result.ToString().Contains("already pass"))
            {
                return DateTime.MinValue;
            }

            int remainingBlock = int.Parse(result["RemainingBlock"].ToString());
            double estimatedTimeInSec = remainingBlock * 3;
            DateTime estimatedLaunch = DateTime.Now.AddSeconds(estimatedTimeInSec);

            return estimatedLaunch;
        }

        private static async Task<JToken> GetSourceCode(string address, ChainTools chainTools, string apiKey)
        {
            HttpResponseMessage response = await chainTools.HttpClient.GetAsync($"api?module=contract&action=getsourcecode&address={address}&apikey={apiKey}");
            string body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<JToken>(body)["result"] as JArray;
            return result[0]["SourceCode"];
        }

        private async Task<JToken> ProcessTimelock(string masterChef)
        {
            string timeLock = await this.chainTools.GetFunction<OwnerOfFunction, string>(masterChef); // owner of masterchef should be timelock
            JToken timeLockSource = await GetSourceCode(timeLock, this.chainTools, this.abiValidator.APIKey);

            Console.WriteLine($"TimeLock Address: {timeLock}");

            if (IsContract(timeLockSource) || this.chainTools.UseLocalAbi)
            {
                string pendingAdmin = await this.chainTools.GetFunction<PendingAdminFunction, string>(timeLock);
                Console.WriteLine($"TimeLock Pending Admin: {pendingAdmin}");
                int delay = await this.chainTools.GetFunction<TimelockDelayFunction, int>(timeLock);
                int minDelay = await this.chainTools.GetFunction<MinimumDelayFunction, int>(timeLock);

                Console.WriteLine($"TimeLock delay: {Math.Round(delay / 60.0 / 60.0, 2)} hours. Minimum delay: {Math.Round(minDelay / 60.0 / 60.0, 2)} hours");
            }
            else
            {
                Console.WriteLine("!!! WARNING: Timelock is not a contract !!!");
            }

            return timeLockSource;
        }
    }
}
