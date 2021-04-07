using c19t.SDK.CFP.Models;
using c19t.SDK.CFP.Models.Exceptions;
using c19t.SDK.CFP.Tests.Helpers;
using Xunit;

namespace c19t.SDK.CFP.Tests.Tests.Procedures
{
    public class Procedure0000
    {
        private readonly Cfp _cfp;

        public Procedure0000()
        {
            _cfp = new Cfp();
        }


        [Fact]
        public void TestDynamic()
        {
            string personalCode = CfpHelper.GeneratePersonalCode();

            var pii = new CfpPii()
            {
                IdNumber = "P123456"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.GenericProcedure0000);

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
                IdNumber = "P123456"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.GenericProcedure0000);

            Assert.True(cfp.Hash.Equals("0xea88a0a23ff881994d7c20bf3529900e4e89e4c46a52914fb95ded7c57978601"));
        }

        [Fact]
        public void TestInvalidPii()
        {
            string personalCode = "a1b2c3d4";

            var pii = new CfpPii()
            {
                Firstname = "Invalid"
            };

            Assert.Throws<CfpPiiValidationException>(() => _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.GenericProcedure0000));
        }
    }
}
