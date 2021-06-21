namespace HumbleUtilities
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("poolLength", "uint256")]
    public class PoolLengthFunction : FunctionMessage
    {
    }
}