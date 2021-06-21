namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("startBlock", "uint256")]
    public class StartBlockFunction : FunctionMessage
    {
    }
}