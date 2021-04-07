using c19t.SDK.CFP.Models;
using c19t.SDK.CFP.Models.Exceptions;
using c19t.SDK.CFP.Tests.Helpers;
using System;
using Xunit;

namespace c19t.SDK.CFP.Tests.Tests.Procedures
{
    public class Procedure0002
    {
        private readonly Cfp _cfp;

        public Procedure0002()
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
                DateOfBirth = new DateTime(1990, 01, 04)
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBProcedure0002);

            Assert.Contains(pii.Firstname, cfp.ClearTextCfp);
            Assert.Contains(pii.Lastname, cfp.ClearTextCfp);
            Assert.Contains(pii.DateOfBirth.Value.ToString("yyyy-MM-dd"), cfp.ClearTextCfp);
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
                DateOfBirth = new DateTime(1990, 10, 30)
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBProcedure0002);

            Assert.True(cfp.Hash.Equals("0x40dc9033a05ea01b08c20effc12cee08aff6984b052fc895a568f815f15ec0b5"));
        }

        [Fact]
        public void TestInvalidPii()
        {
            string personalCode = "a1b2c3d4";

            var pii = new CfpPii()
            {
                IdNumber = "Invalid",
                Firstname = "Max"
            };

            Assert.Throws<CfpPiiValidationException>(() => _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBProcedure0002));
        }
    }
}
