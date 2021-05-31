namespace HumbleVerifierConsole
{
    using CommandLine;

    public class Options
    {
        [Value(0, Required = true, HelpText = "Address of the token (or masterchef in the case of -m) contract. E.g. 0xe9e7cea3dedca5984780bafc599bd69add087d56")]
        public string ContractAddress { get; set; }

        [Value(1, Required = false, Default = 56, HelpText = "Specify the chain ID. 1=Ethereum, 56=BSC, 137=Polygon, 250=Fantom, 43114=Avalanche.")]
        public int ChainId { get; set; }

        [Option('m', "mconly", Default = false, HelpText = "Validate Masterchef contract only")]
        public bool MasterchefOnly { get; set; }

        [Option('a', "apikey", Required = true, HelpText = "ApiKey for calling BscScan. Get yours at https://bscscan.com/myapikey")]
        public string ApiKey { get; set; }
    }
}
