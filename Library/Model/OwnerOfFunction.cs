namespace HumbleUtilities
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    // Function: SwapExactTokensForETH(uint256 amountIn, uint256 amountOutMin, address[] path, address to, uint256 deadline)
    // [Function("swapExactTokensForETHSupportingFeeOnTransferTokens", "bool")]

    [Function("owner", "address")]
    public class OwnerOfFunction : FunctionMessage
    {
    }
}
