namespace HumbleVerifierLibrary
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("approve")]
    public class ApproveAllowanceFunction : FunctionMessage
    {
        [Parameter("address", "spender", 1)]
        public string Spender { get; set; }

        [Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }
    }
}