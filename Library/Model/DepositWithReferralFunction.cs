namespace HumbleUtilities
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("deposit", "bool")]
    public class DepositWithReferralFunction : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public BigInteger PoolId { get; set; }

        [Parameter("uint256", "_amount", 2)]
        public BigInteger Amount { get; set; }

        [Parameter("address", "_referrer)", 3)]
        public string Referrer { get; set; }
    }
}