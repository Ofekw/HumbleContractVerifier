namespace HumbleVerifierLibrary
{
    using System.Threading.Tasks;

    using Nethereum.Contracts;
    using Nethereum.Contracts.ContractHandlers;
    using Nethereum.Web3;

    public class ChainTools
    {
        /// <summary>
        /// Create a ChainTools object
        /// </summary>
        /// <param name="httpClient">Http Client for making web requests</param>
        /// <param name="web3">Web3 client for interacting with blockchain</param>
        /// <param name="avgBlockTimeSec">The average amount of time to mine a block on this chain.</param>
        /// <param name="networkName">Name of the Ethereum network</param>
        public ChainTools(IHttpClient httpClient, IWeb3 web3, double avgBlockTimeSec, string networkName)
        {
            this.HttpClient = httpClient;
            this.Web3 = web3;
            this.AvgBlockTimeSec = avgBlockTimeSec;
            this.NetworkName = networkName;
        }

        /// <summary>
        /// Http Client for making web requests
        /// </summary>
        public IHttpClient HttpClient { get; }

        /// <summary>
        /// Web3 client for interacting with blockchain
        /// </summary>
        public IWeb3 Web3 { get; }

        /// <summary>
        /// The average amount of time to mine a block on this chain in seconds.
        /// </summary>
        public double AvgBlockTimeSec { get; }

        /// <summary>
        /// The name of the Ethereum chain. ETH, BSC, POLYGON, FANTOM
        /// </summary>
        public string NetworkName { get; }

        /// <summary>
        /// Query a blockchain function
        /// </summary>
        /// <typeparam name="T">The function message to query.</typeparam>
        /// <typeparam name="U">The type of the result to return.</typeparam>
        /// <param name="address">Address of the contract to query.</param>
        /// <returns>The typed result from querying the contract.</returns>
        public async Task<U> QueryContract<T, U>(string address)
            where T : FunctionMessage, new()
        {
            var t = new T();
            IContractQueryHandler<T> queryHandler2 = this.Web3.Eth.GetContractQueryHandler<T>();
            U result = await queryHandler2.QueryAsync<U>(address, t).ConfigureAwait(false);

            return result;
        }
    }
}