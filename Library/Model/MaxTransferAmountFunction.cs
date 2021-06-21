namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("maxTransferAmount", "uint256")]
    public class MaxTransferAmountFunction : FunctionMessage
    {
    }
}