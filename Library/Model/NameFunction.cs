namespace HumbleUtilities
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("name", "string")]
    public class NameFunction : FunctionMessage
    {
    }
}