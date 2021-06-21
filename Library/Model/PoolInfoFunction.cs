namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("poolInfo")]
    public class PoolInfoFunction : FunctionMessage
    {
        [Parameter("uint256", "input")]
        public int PoolNum { get; set; }
    }
}