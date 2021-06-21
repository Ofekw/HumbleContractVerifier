﻿namespace HumbleUtilities
{
    using System.Numerics;
    using Nethereum.ABI.FunctionEncoding.Attributes;

    [FunctionOutput]
    public class GetReservesOutputDTO : IFunctionOutputDTO
    {
        [Parameter("uint112", "_reserve0")]
        public virtual BigInteger Reserve0 { get; set; }

        [Parameter("uint112", "_reserve1", 2)]
        public virtual BigInteger Reserve1 { get; set; }

        [Parameter("uint32", "_blockTimestampLast", 3)]
        public virtual uint BlockTimestampLast { get; set; }
    }
}
