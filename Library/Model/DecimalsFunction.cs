namespace HumbleUtilities
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("decimals", "uint8")]
    public class DecimalsFunction : FunctionMessage
    {
    }
}