namespace HumbleUtilities
{
    using System.Collections.Generic;
    using System.Numerics;

    using Nethereum.ABI.FunctionEncoding.Attributes;

    [FunctionOutput]
    public class AmountsOutDTO : IFunctionOutputDTO
    {
        [Parameter("uint256[]", "amounts", 1)]
        public IList<BigInteger> amounts { get; set; }
    }
}