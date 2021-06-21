namespace HumbleVerifierLibrary
{
    using System.Numerics;
    using Nethereum.ABI.FunctionEncoding.Attributes;

    [FunctionOutput]
    public class PoolInfoOutputDTO : IFunctionOutputDTO
    {
        [Parameter("address", "lpToken")]
        public virtual string Address { get; set; }

        [Parameter("uint256", "allocPoint", 2)]
        public virtual BigInteger AllocPoint { get; set; }

        [Parameter("uint256", "lastRewardBlock", 3)]
        public virtual BigInteger LastRewardBlock { get; set; }

        [Parameter("uint256", "accWishPerShare", 4)]
        public virtual BigInteger AmountPerShare { get; set; }

        [Parameter("uint16", "depositFeeBP", 5)]
        public virtual int DepositFeeBP { get; set; }

        [Parameter("uint16", "withdrawFeeBP", 6)]
        public virtual int WithdrawalFeeBP { get; set; }
    }
}
