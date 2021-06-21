namespace HumbleVerifierLibrary
{
    using System.Collections.Generic;
    using System.Numerics;

    public interface ISwapBase
    {
        BigInteger amountIn { get; set; }
        BigInteger amountOutMin { get; set; }
        IList<string> path { get; set; }
        string to { get; set; }
        BigInteger deadline { get; set; }
    }
}