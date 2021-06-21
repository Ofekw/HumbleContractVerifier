namespace HumbleUtilities
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("userInfo", "bool")]
    public class UserInfoFunction : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public BigInteger PoolId { get; set; }

        [Parameter("address", "_address", 2)]
        public string Address { get; set; }
    }
}