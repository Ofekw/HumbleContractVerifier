namespace HumbleVerifierTests
{
    using FluentAssertions;
    using HumbleVerifierLibrary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Nethereum.Web3;
    using NSubstitute;

    [TestClass]
    public class ChainToolsTests
    {
        [TestMethod]
        public void ChainTools_BSC()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var web3 = new Web3("http://foo");
            var ct = new ChainTools(httpClient, web3, 4.2, "BSC");
            ct.AvgBlockTimeSec.Should().Be(4.2);
            ct.NetworkName.Should().Be("BSC");
            ct.HttpClient.Should().Be(httpClient);
            ct.Web3.Should().Be(web3);
        }
    }
}
