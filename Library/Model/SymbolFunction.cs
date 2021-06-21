namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("symbol", "string")]
    public class SymbolFunction : FunctionMessage
    {
    }
}