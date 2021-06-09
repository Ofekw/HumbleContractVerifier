namespace HumbleVerifierTests
{
    using HumbleVerifierLibrary;

    using Nethereum.Web3;

    using NSubstitute;

    using IHttpClient = HumbleVerifierLibrary.IHttpClient;

    public class AbiToolsBuilder
    {
        private readonly IHttpClient httpClient;

        private readonly Web3 web3;

        public AbiToolsBuilder()
        {
            this.httpClient = Substitute.For<IHttpClient>();
            this.web3 = new Web3("http://foo");
        }

        public AbiTools Build()
        {
            var chainTools = new ChainTools(this.httpClient, this.web3, 2.1, "POLYGON");
            return new AbiTools(chainTools, "key");
        }
    }
}