namespace HumbleVerifierTests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AbiToolsTests
    {
        [TestMethod]
        public void Aladdin_WithdrawParameterDetected()
        {
            List<string> scaryMethods = new AbiToolsBuilder().Build().GetMethodsWithPossibleWithdrawalFee(SampleJson.AladdinMasterchefABI);
            scaryMethods.Should().HaveCount(2);
            scaryMethods[0].Should().Contain("add(uint256 _allocPoint, address _lpToken, uint16 _depositFeeBP, uint16 _withdrawFeeBP, bool _withUpdate)");
        }

        [TestMethod]
        public void Slion_NoWithdrawParameterDetected()
        {
            new AbiToolsBuilder().Build().GetMethodsWithPossibleWithdrawalFee(SampleJson.SlionMasterchefABI).Should().BeEmpty();
        }

        [TestMethod]
        public void GetPoolInfoSignature_Correct()
        {
            string poolInfoSig = new AbiToolsBuilder().Build().GetPoolInfoSignature(SampleJson.PoolInfoExtraArgs);
            poolInfoSig.Should().Be("poolInfo returns(address lpToken, uint256 allocPoint, uint256 lastRewardBlock, uint256 accPZapPerShare, uint16 depositFeeBP, uint256 harvestInterval)");
        }
    }
}
