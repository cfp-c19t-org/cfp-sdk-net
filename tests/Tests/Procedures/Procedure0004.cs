using c19t.SDK.CFP.Models;
using c19t.SDK.CFP.Models.Exceptions;
using c19t.SDK.CFP.Tests.Helpers;
using System;
using Xunit;

namespace c19t.SDK.CFP.Tests.Tests.Procedures
{
    public class Procedure0004
    {
        private readonly Cfp _cfp;

        public Procedure0004()
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
                TaxId = "TAX-987654321-asdfghjkl"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBTaxIdProcedure0004);

            Assert.Contains(pii.Firstname, cfp.ClearTextCfp);
            Assert.Contains(pii.Lastname, cfp.ClearTextCfp);
            Assert.Contains(pii.DateOfBirth.Value.ToString("yyyy-MM-dd"), cfp.ClearTextCfp);
            Assert.Contains(pii.TaxId, cfp.ClearTextCfp);
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
                TaxId = "TAX-123456-asdfghjkl"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBTaxIdProcedure0004);

            Assert.True(cfp.Hash.Equals("0x62e62649155747ca8260079d351aa2b5c15855eaac7270960d3d7b3885ac69aa"));
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

            Assert.Throws<CfpPiiValidationException>(() => _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameDoBTaxIdProcedure0004));
        }
    }
}
