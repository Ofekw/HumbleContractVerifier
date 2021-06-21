namespace HumbleUtilities
{
    using System.Collections.Generic;
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("getAmountsOut", "uint256[]")]
    public class GetAmountsOutFunction : FunctionMessage
    {
        [Parameter("uint256", "amountIn", 1)]
        public BigInteger amountIn { get; set; }


        [Parameter("address[]", "path", 3)]
        public IList<string> path { get; set; }
    }
}