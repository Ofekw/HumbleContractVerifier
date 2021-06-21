namespace HumbleUtilities
{
    using System.Collections.Generic;
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("swapExactTokensForETHSupportingFeeOnTransferTokens", "bool")]
    public class SwapExactTokensForETH : FunctionMessage, ISwapBase
    {
        [Parameter("uint256", "amountIn", 1)]
        public BigInteger amountIn { get; set; }

        [Parameter("uint256", "amountOutMin", 2)]
        public BigInteger amountOutMin { get; set; }

        [Parameter("address[]", "path", 3)]
        public IList<string> path { get; set; }

        [Parameter("address", "to", 4)]
        public string to { get; set; }

        [Parameter("uint256", "deadline", 5)]
        public BigInteger deadline { get; set; }
    }
}