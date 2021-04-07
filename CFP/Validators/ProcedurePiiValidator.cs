using c19t.SDK.CFP.Interfaces;
using c19t.SDK.CFP.Models;

namespace c19t.SDK.CFP.Validators
{
    /// <summary>
    /// Validates the required pii for each procedure
    /// </summary>
    class ProcedurePiiValidator : IProcedurePiiValidator
    {
        public bool Validate0000Pii(CfpPii data)
        {
            return !string.IsNullOrWhiteSpace(data.IdNumber);
        }

        public bool Validate0001Pii(CfpPii data)
        {
            return !string.IsNullOrWhiteSpace(data.Firstname)
                && !string.IsNullOrWhiteSpace(data.Lastname);
        }

        public bool Validate0002Pii(CfpPii data)
        {
            return !string.IsNullOrWhiteSpace(data.Firstname)
                && !string.IsNullOrWhiteSpace(data.Lastname)
                && data.DateOfBirth.HasValue
                && data.DateOfBirth.Value != default;
        }

        public bool Validate0003Pii(CfpPii data)
        {
            return !string.IsNullOrWhiteSpace(data.Firstname)
                && !string.IsNullOrWhiteSpace(data.Lastname)
                && data.DateOfBirth.HasValue
                && data.DateOfBirth.Value != default
                && !string.IsNullOrWhiteSpace(data.IdNumber);
        }

        public bool Validate0004Pii(CfpPii data)
        {
            return !string.IsNullOrWhiteSpace(data.Firstname)
                && !string.IsNullOrWhiteSpace(data.Lastname)
                && data.DateOfBirth.HasValue
                && data.DateOfBirth.Value != default
                && !string.IsNullOrWhiteSpace(data.TaxId);
        }

        public bool ValidatePii(CfpPii data, CfpProcedure procedure)
        {
            switch (procedure)
            {
                case CfpProcedure.FirstnameLastnameProcedure0001:
                    return Validate0001Pii(data);
                case CfpProcedure.FirstnameLastnameDoBProcedure0002:
                    return Validate0002Pii(data);
                case CfpProcedure.FirstnameLastnameDoBIdNumberProcedure0003:
                    return Validate0003Pii(data);
                case CfpProcedure.FirstnameLastnameDoBTaxIdProcedure0004:
                    return Validate0004Pii(data);
                case CfpProcedure.GenericProcedure0000:
                default:
                    return Validate0000Pii(data);
            }
        }
    }
}
