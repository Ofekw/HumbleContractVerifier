namespace HumbleVerifierLibrary
{
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("owner", "address")]
    public class OwnerOfFunction : FunctionMessage
    {
    }

    [Function("symbol", "string")]
    public class SymbolFunction : FunctionMessage
    {
    }

    [Function("pendingAdmin", "address")]
    public class PendingAdminFunction : FunctionMessage
    {
    }

    [Function("delay", "uint265")]
    public class TimelockDelayFunction : FunctionMessage
    {
    }

    [Function("MINIMUM_DELAY", "uint265")]
    public class MinimumDelayFunction : FunctionMessage
    {
    }

    [Function("startBlock", "uint256")]
    public class StartBlockFunction : FunctionMessage
    {
    }

    [Function("poolLength", "uint256")]
    public class PoolLengthFunction : FunctionMessage
    {
    }

    [Function("poolInfo")]
    public class PoolInfoFunction : FunctionMessage
    {
        [Parameter("uint256", "input")]
        public int PoolNum { get; set; }
    }

    [Function("token0", "address")]
    public class Token0Function : FunctionMessage
    {
    }

    [Function("token1", "address")]
    public class Token1Function : FunctionMessage
    {
    }

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
