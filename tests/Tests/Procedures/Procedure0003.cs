using c19t.SDK.CFP.Models;
using c19t.SDK.CFP.Models.Exceptions;
using c19t.SDK.CFP.Tests.Helpers;
using System;
using Xunit;

namespace c19t.SDK.CFP.Tests.Tests.Procedures
{
    public class Procedure0003
    {
        private readonly Cfp _cfp;

        public Procedure0003()
        {
            _cfp = new Cfp();
        }


        [Fact]
        public void TestDynamic()
        {
            string personalCode = CfpHelper.GeneratePersonalCode();

            var pii = new CfpPii()
            {
                Firstname = "Max",
                Lastname = "Mustermann",
                DateOfBirth = new DateTime(1990, 03, 12),
                IdNumber = "P123456"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBIdNumberProcedure0003);

            Assert.Contains(pii.Firstname, cfp.ClearTextCfp);
            Assert.Contains(pii.Lastname, cfp.ClearTextCfp);
            Assert.Contains(pii.DateOfBirth.Value.ToString("yyyy-MM-dd"), cfp.ClearTextCfp);
            Assert.Contains(pii.IdNumber, cfp.ClearTextCfp);
            Assert.Contains(personalCode, cfp.ClearTextCfp);
            // TODO: check hash
        }

        [Fact]
        public void TestFixed()
        {
            string personalCode = "a1b2c3d4";

            var pii = new CfpPii()
            {
                Firstname = "Max",
                Lastname = "Mustermann",
                DateOfBirth = new DateTime(1990, 10, 30),
                IdNumber = "P123456"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBIdNumberProcedure0003);

            Assert.True(cfp.Hash.Equals("0x03418e21c49fbe27c14c0fd3f13e22766cf47a5f63b733a8e8986a62419fa5f9"));
        }

        [Fact]
        public void TestInvalidPii()
        {
            string personalCode = "a1b2c3d4";

            var pii = new CfpPii()
            {
                IdNumber = "Invalid",
                Firstname = "Max",
                Lastname = "Mustermann"
            };

            Assert.Throws<CfpPiiValidationException>(() => _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBIdNumberProcedure0003));
        }
    }
}
