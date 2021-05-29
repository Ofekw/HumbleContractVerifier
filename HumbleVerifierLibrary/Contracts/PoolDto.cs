using System;
using System.Collections.Generic;
using System.Text;

namespace HumbleVerifierLibrary.Contracts
{
    public class PoolDto
    {
        public string TokenOneSymbol { get; set; } = string.Empty;
        public string TokenTwoSymbol { get; set; } = string.Empty;
        public IDictionary<string,string> PoolDetailsRaw { get; set; }
    }
}
