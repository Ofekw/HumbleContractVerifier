namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("delay", "uint265")]
    public class TimelockDelayFunction : FunctionMessage
    {
    }
}