using c19t.SDK.CFP.Models;
using c19t.SDK.CFP.Models.Exceptions;
using c19t.SDK.CFP.Tests.Helpers;
using Xunit;

namespace c19t.SDK.CFP.Tests.Tests.Procedures
{
    public class Procedure0001
    {
        private readonly Cfp _cfp;

        public Procedure0001()
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
                Lastname = "Mustermann"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameProcedure0001);

            Assert.Contains(pii.Firstname, cfp.ClearTextCfp);
            Assert.Contains(pii.Lastname, cfp.ClearTextCfp);
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
                Lastname = "Mustermann"
            };

            var cfp = _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameProcedure0001);

            Assert.True(cfp.Hash.Equals("0x199b5daa8b45b4267f9178600265718d276796bfc4289a212588e0e83371adb0"));
        }

        [Fact]
        public void TestInvalidPii()
        {
            string personalCode = "a1b2c3d4";

            var pii = new CfpPii()
            {
                IdNumber = "Invalid"
            };

            Assert.Throws<CfpPiiValidationException>(() => _cfp.CreateDigitalIdentity(pii, personalCode, CfpProcedure.FirstnameLastnameProcedure0001));
        }
    }
}
