namespace HumbleUtilities
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("deposit", "bool")]
    public class DepositFunction : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public BigInteger PoolId { get; set; }

        [Parameter("uint256", "_amount", 2)]
        public BigInteger Amount { get; set; }
    }
}