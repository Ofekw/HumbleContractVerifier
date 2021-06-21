namespace HumbleUtilities
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;

    [FunctionOutput]
    public class UserInfo : IFunctionOutputDTO
    {
        [Parameter("uint256", "amount", 1)]
        public BigInteger Amount { get; set; } = 0;

        [Parameter("uint256", "rewardDebt", 2)]
        public BigInteger RewardDebt { get; set; } = 0;

        [Parameter("uint256", "rewardLocked", 3)]
        public BigInteger RewardLocked { get; set; } = 0;

        [Parameter("uint256", "lastClaimTime", 4)]
        public long LastClaimTime { get; set; } = 0;

    }
}