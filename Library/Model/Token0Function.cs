namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("token0", "address")]
    public class Token0Function : FunctionMessage
    {
    }
}