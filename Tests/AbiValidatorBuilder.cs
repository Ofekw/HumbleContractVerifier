﻿namespace HumbleVerifierTests
{
    using System.IO.Abstractions;

    using HumbleVerifierLibrary;

    using Nethereum.Web3;

    using NSubstitute;

    using IHttpClient = HumbleVerifierLibrary.IHttpClient;

    public class AbiValidatorBuilder
    {
        private readonly IHttpClient httpClient;

        private readonly Web3 web3;

        public AbiValidatorBuilder()
        {
            this.httpClient = Substitute.For<IHttpClient>();
            this.web3 = new Web3("http://foo");
        }

        public IFileSystem FileSystem { get; }

        public AbiValidator Build()
        {
            var chainTools = new ChainTools(this.httpClient, this.web3, false, 2.1);
            return new AbiValidator(chainTools, "key");
        }
    }
}