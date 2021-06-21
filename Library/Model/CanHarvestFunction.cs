namespace HumbleUtilities
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("canHarvest", "bool")]
    public class CanHarvestFunction : FunctionMessage
    {
        [Parameter("uint256", "_pid", 1)]
        public BigInteger PoolId { get; set; }

        [Parameter("address", "user", 2)]
        public string User { get; set; }
    }
}