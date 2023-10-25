using c19t.SDK.CFP.Models;
using System;

namespace c19t.SDK.CFP.Interfaces
{
    interface ICfpGenerator
    {
        CfpModel GenerateCfpBy0000Procedure(string personalCode, string idNumber);

        CfpModel GenerateCfpBy0001Procedure(string personalCode, string firstname, string lastname);

        CfpModel GenerateCfpBy0002Procedure(string personalCode, string firstname, string lastname, DateTime dateOfBirth);

        CfpModel GenerateCfpBy0003Procedure(string personalCode, string firstname, string lastname, DateTime dateOfBirth, string idNumber);

        CfpModel GenerateCfpBy0004Procedure(string personalCode, string firstname, string lastname, DateTime dateOfBirth, string taxId);

        CfpModel GenerateCfpBy0005Procedure(string personalCode, string idNumber, DateTime dateOfBirth);

    }
}
