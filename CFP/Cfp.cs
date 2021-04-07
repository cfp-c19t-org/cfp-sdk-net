using c19t.SDK.CFP.Generators;
using c19t.SDK.CFP.Interfaces;
using c19t.SDK.CFP.Models;
using c19t.SDK.CFP.Models.Exceptions;
using c19t.SDK.CFP.Validators;

namespace c19t.SDK.CFP
{
    public class Cfp
    {
        private readonly IProcedurePiiValidator _validator;
        private readonly ICfpGenerator _generator;

        public Cfp()
        {
            _validator = new ProcedurePiiValidator();
            _generator = new CfpGenerator();
        }

        /// <summary>
        /// Generate the digital id for a new cfp
        /// </summary>
        /// <param name="personalData"> personally identifiable information </param>
        /// <param name="procedure"> procedure code; if not provided the procedure with the least personal data is used </param>
        /// <returns> cleartext CFP (attention, contains PII!) and hash </returns>
        public CfpModel CreateDigitalIdentity(CfpPii personalData, string personalCode, CfpProcedure? procedure = null)
        {
            // 1. Autodetect procedure
            procedure = procedure ?? this.AutodetectProcedure(personalData);

            // 2. Validate pii
            if (!_validator.ValidatePii(personalData, procedure.Value))
            {
                throw new CfpPiiValidationException($"PII validation for procedure {procedure} failed!");
            }

            // 3. Generate cfp
            switch (procedure)
            {
                case CfpProcedure.FirstnameLastnameProcedure0001:
                    return _generator.GenerateCfpBy0001Procedure(personalCode, personalData.Firstname, personalData.Lastname);
                case CfpProcedure.FirstnameLastnameDoBProcedure0002:
                    return _generator.GenerateCfpBy0002Procedure(personalCode, personalData.Firstname, personalData.Lastname, personalData.DateOfBirth.Value);
                case CfpProcedure.FirstnameLastnameDoBIdNumberProcedure0003:
                    return _generator.GenerateCfpBy0003Procedure(personalCode, personalData.Firstname, personalData.Lastname, personalData.DateOfBirth.Value, personalData.IdNumber);
                case CfpProcedure.FirstnameLastnameDoBTaxIdProcedure0004:
                    return _generator.GenerateCfpBy0004Procedure(personalCode, personalData.Firstname, personalData.Lastname, personalData.DateOfBirth.Value, personalData.TaxId);

                // default case = generic procedure
                case CfpProcedure.GenericProcedure0000:
                default:
                    return _generator.GenerateCfpBy0000Procedure(personalCode, personalData.IdNumber);
            }
        }

        /// <summary>
        /// Autodetect CFP procedure by using least necessary pii
        /// </summary>
        /// <param name="pii"> personal data </param>
        /// <returns> CFP procedure code </returns>
        private CfpProcedure AutodetectProcedure(CfpPii pii)
        {
            if (!string.IsNullOrWhiteSpace(pii.IdNumber))
            {
                return CfpProcedure.GenericProcedure0000;
            }
            else if (!string.IsNullOrWhiteSpace(pii.Firstname)
                && !string.IsNullOrWhiteSpace(pii.Lastname))
            {
                return CfpProcedure.FirstnameLastnameProcedure0001;
            }
            else if (!string.IsNullOrWhiteSpace(pii.Firstname)
                && !string.IsNullOrWhiteSpace(pii.Lastname)
                && pii.DateOfBirth.HasValue && pii.DateOfBirth != default)
            {
                return CfpProcedure.FirstnameLastnameDoBProcedure0002;
            }
            else if (!string.IsNullOrWhiteSpace(pii.Firstname)
                && !string.IsNullOrWhiteSpace(pii.Lastname)
                && pii.DateOfBirth.HasValue && pii.DateOfBirth != default
                && !string.IsNullOrWhiteSpace(pii.IdNumber))
            {
                return CfpProcedure.FirstnameLastnameDoBIdNumberProcedure0003;
            }
            else if (!string.IsNullOrWhiteSpace(pii.Firstname)
                && !string.IsNullOrWhiteSpace(pii.Lastname)
                && pii.DateOfBirth.HasValue && pii.DateOfBirth != default
                && !string.IsNullOrWhiteSpace(pii.TaxId))
            {
                return CfpProcedure.FirstnameLastnameDoBTaxIdProcedure0004;
            }
            else
            {
                throw new CfpAutodetectionException("Autodetection of procedure failed due to insufficient pii provided!");
            }
        }
    }
}
