namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("MINIMUM_DELAY", "uint265")]
    public class MinimumDelayFunction : FunctionMessage
    {
    }
}