using System;
using System.Collections.Generic;
using System.Text;

namespace HumbleVerifierLibrary.Contracts
{
    using Newtonsoft.Json.Linq;

    public class VerifierResponseDto
    {
        public string MasterChefAddress { get; set; } = string.Empty;

        public bool OwnerOfTokenIsAContract { get; set; } = true;

        public double StartBlock { get; set; } = 0;

        public DateTime EstimatedLaunchTime { get; set; } = DateTime.MinValue;

        public IList<string> DangerousContractMethods { get; set; } = new List<string>();

        public IList<PoolDto> Pools { get; set; } = new List<PoolDto>();

        public JToken TokenSourceCode { get; set; }

        public JToken MasterChefSourceCode { get; set; }

        public JToken TimelockSourceCode { get; set; }
    }
}
