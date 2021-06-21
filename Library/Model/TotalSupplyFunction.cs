namespace HumbleUtilities
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunction : FunctionMessage
    {
    }
}