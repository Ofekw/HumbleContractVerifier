namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("token1", "address")]
    public class Token1Function : FunctionMessage
    {
    }
}