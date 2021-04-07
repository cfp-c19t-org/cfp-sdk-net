using c19t.SDK.CFP.Helpers;
using c19t.SDK.CFP.Helpers.Keccak;
using c19t.SDK.CFP.Interfaces;
using c19t.SDK.CFP.Models;
using System;

namespace c19t.SDK.CFP.Generators
{
    class CfpGenerator : ICfpGenerator
    {
        private readonly Keccak256 _keccak;

        public CfpGenerator()
        {
            _keccak = new Keccak256();
        }

        public CfpModel GenerateCfpBy0000Procedure(string personalCode, string idNumber)
        {
            string cfpKey = CreateCfpKey("0000", personalCode, idNumber);
            string hash = $"0x{_keccak.Hash(cfpKey)}";

            return new CfpModel(cfpKey, hash);
        }

        public CfpModel GenerateCfpBy0001Procedure(string personalCode, string firstname, string lastname)
        {
            string personalData = $"{firstname}{StringConstants.Separator}{lastname}";

            string cfpKey = CreateCfpKey("0001", personalCode, personalData);
            string hash = $"0x{_keccak.Hash(cfpKey)}";

            return new CfpModel(cfpKey, hash);
        }

        public CfpModel GenerateCfpBy0002Procedure(string personalCode, string firstname, string lastname, DateTime dateOfBirth)
        {
            string personalData = $"{firstname}{StringConstants.Separator}{lastname}{StringConstants.Separator}{dateOfBirth:yyyy-MM-dd}";

            string cfpKey = CreateCfpKey("0002", personalCode, personalData);
            string hash = $"0x{_keccak.Hash(cfpKey)}";

            return new CfpModel(cfpKey, hash);
        }

        public CfpModel GenerateCfpBy0003Procedure(string personalCode, string firstname, string lastname, DateTime dateOfBirth, string idNumber)
        {
            string personalData = $"{firstname}{StringConstants.Separator}{lastname}{StringConstants.Separator}{dateOfBirth:yyyy-MM-dd}{StringConstants.Separator}{idNumber}";

            string cfpKey = CreateCfpKey("0003", personalCode, personalData);
            string hash = $"0x{_keccak.Hash(cfpKey)}";

            return new CfpModel(cfpKey, hash);
        }

        public CfpModel GenerateCfpBy0004Procedure(string personalCode, string firstname, string lastname, DateTime dateOfBirth, string taxId)
        {
            string personalData = $"{firstname}{StringConstants.Separator}{lastname}{StringConstants.Separator}{dateOfBirth:yyyy-MM-dd}{StringConstants.Separator}{taxId}";

            string cfpKey = CreateCfpKey("0004", personalCode, personalData);
            string hash = $"0x{_keccak.Hash(cfpKey)}";

            return new CfpModel(cfpKey, hash);
        }

        #region Private helpers

        private string CreateCfpKey(string procedureCode, string personalCode, string personalData)
        {
            return $"{personalCode}{StringConstants.Separator}{personalData}{StringConstants.Separator}{procedureCode}";
        }

        #endregion
    }
}
