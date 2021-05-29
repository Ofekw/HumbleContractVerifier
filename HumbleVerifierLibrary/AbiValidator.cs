namespace HumbleVerifierLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class AbiValidator
    {
        protected readonly ChainTools chainTools;

        public string APIKey { get; }

        public const string LocalAbiFileName = "MasterchefAbi.json";

        public AbiValidator(ChainTools chainTools, string apiKey)
        {
            this.APIKey = apiKey;
            this.chainTools = chainTools;
        }


        public List<string> GetMethodsWithPossibleWithdrawalFee(string abi)
        {
            var scaryMethods = new List<string>();
            JToken abiToken = FetchAbiToken(abi);
            IEnumerable<JToken> scarySet = (abiToken as JArray).Where(
                                                                      x =>
                                                                      {
                                                                          JToken inputs = x["inputs"];
                                                                          if (inputs != null)
                                                                          {
                                                                              IEnumerable<JToken> method = (inputs as JArray).Where(
                                                                               parameter => (parameter["name"] != null)
                                                                                            && (parameter["name"]
                                                                                                .ToString()
                                                                                                .IndexOf("withdraw", StringComparison.OrdinalIgnoreCase)
                                                                                                != -1)
                                                                                            && (parameter["name"].ToString().IndexOf("fee", StringComparison.OrdinalIgnoreCase)
                                                                                                != -1));
                                                                              return method.Any();
                                                                          }

                                                                          return false;
                                                                      });

            foreach (JToken scary in scarySet)
            {
                scaryMethods.Add(ReconstructMethodSignature(scary));
            }

            return scaryMethods;
        }

        /// <summary>
        /// Return the collection of functions 
        /// </summary>
        /// <param name="abiToken">Full abi parsed into JToken</param>
        /// <param name="functionName">Name of function to search for (case-insensitive)</param>
        /// <returns>Collection of functions discovered</returns>
        public static IEnumerable<JToken> GetFunctionsByName(JToken abiToken, string functionName)
        {
            return (abiToken as JArray).Where(
                                              x =>
                                              {
                                                  JToken name = x["name"];
                                                  return (x["type"]?.ToString() == "function")
                                                         && (name != null)
                                                         && (name.ToString().IndexOf(functionName, StringComparison.OrdinalIgnoreCase) != -1);
                                              });
        }

        public async Task<string> FetchAbiFromApiAsync(string address)
        {
            string abi;
            HttpResponseMessage response =
                await this.chainTools.HttpClient.GetAsync($"https://api.bscscan.com/api?module=contract&action=getabi&address={address}&apikey={this.APIKey}");

            string body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNullOrEmpty();
            var jObject = JsonConvert.DeserializeObject<JObject>(body);
            jObject.Should().NotBeNull();
            jObject["status"].ToString().Should().Be("1");
            abi = jObject["result"].ToString();
            abi.Should().NotBeNullOrEmpty();
            return abi;
        }

        public static JToken FetchAbiToken(string abi)
        {
            string abiUnescaped = Regex.Unescape(abi);
            var abiToken = JsonConvert.DeserializeObject<JToken>(abiUnescaped);
            return abiToken;
        }

        private static string ReconstructMethodOutputsSignature(JToken methodData)
        {
            var sig = new StringBuilder().Append((object?)methodData["name"]).Append(" returns");
            return ReconstructSignatureFromParams(sig, methodData["outputs"]);
        }

        private static string ReconstructMethodSignature(JToken methodData)
        {
            var sig = new StringBuilder().Append((object?)methodData["name"]);
            return ReconstructSignatureFromParams(sig, methodData["inputs"]);
        }

        private static string ReconstructSignatureFromParams(StringBuilder sig, JToken inputs)
        {
            sig.Append("(");
            bool isFirst = true;
            foreach (JToken input in inputs as JArray)
            {
                if (!isFirst)
                {
                    sig.Append(", ");
                }

                sig.Append((object?)input["type"]).Append(" ").Append((object?)input["name"]);
                isFirst = false;
            }

            sig.Append(")");
            return sig.ToString();
        }

        public string GetPoolInfoSignature(string masterchefAbi)
        {
            JToken abiToken = FetchAbiToken(masterchefAbi);
            JToken poolInfo = GetFunctionsByName(abiToken, "poolInfo").FirstOrDefault();

            return poolInfo != null ? ReconstructMethodOutputsSignature(poolInfo) : "PoolInfo method not found";
        }
    }
}