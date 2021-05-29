namespace HumbleVerifierLibrary
{
    using System;
    using System.Net.Http;

    using Nethereum.Web3;

    public static class Utilities
    {
        public static ChainTools GetChainTools(int chainId)
        {
            var innerClient = new HttpClient();
            Web3 web3;
            IHttpClient client = new HumbleHttpClient(innerClient);
            bool useLocalAbi = false;
            switch (chainId)
            {
                case 56:
                    innerClient.BaseAddress = new Uri("https://api.bscscan.com/");
                    web3 = new Web3("https://bsc-dataseed3.binance.org/");
                    break;
                default:
                    web3 = new Web3("https://rpc-mainnet.maticvigil.com/");
                    useLocalAbi = true;
                    break;
            }

            var tools = new ChainTools(client, web3, useLocalAbi);
            return tools;
        }
    }
}