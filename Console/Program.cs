namespace HumbleVerifierConsole
{
    using System.Threading.Tasks;
    using CommandLine;
    using HumbleVerifierLibrary;

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(options =>
                {
                    ChainTools chainTools = Utilities.GetChainTools(options.ChainId);
                    var abiValidator = new AbiTools(chainTools, options.ApiKey);
                    var launchPad = new Runner(chainTools, abiValidator);
                    return launchPad.Run(options.ContractAddress, options.MasterchefOnly);
                });
        }
    }
}
