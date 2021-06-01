namespace HumbleVerifierLibrary
{
    using System.Threading.Tasks;

    using Nethereum.Contracts;
    using Nethereum.Contracts.ContractHandlers;
    using Nethereum.Web3;

    public class ChainTools
    {
        public ChainTools(IHttpClient httpClient, Web3 web3, bool useLocalAbi)
        {
            this.HttpClient = httpClient;
            this.Web3 = web3;
            this.UseLocalAbi = useLocalAbi;
        }

        public IHttpClient HttpClient { get; }

        public Web3 Web3 { get; }

        public bool UseLocalAbi { get; }

        public async Task<U> GetFunction<T, U>(string address)
            where T : FunctionMessage, new()
        {
            var t = new T();
            IContractQueryHandler<T> queryHandler2 = this.Web3.Eth.GetContractQueryHandler<T>();
            U tokenOwner = await queryHandler2.QueryAsync<U>(address, t).ConfigureAwait(false);

            return tokenOwner;
        }
    }
}