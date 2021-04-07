namespace c19t.SDK.CFP.Models
{
    public class CfpModel
    {
        public string ClearTextCfp { get; internal set; }

        public string Hash { get; internal set; }

        public CfpProcedure Procedure { get; internal set; }

        public CfpModel(string clearTextCfp, string hash, CfpProcedure procedure)
        {
            ClearTextCfp = clearTextCfp;
            Hash = hash;
            Procedure = procedure;
        }
    }
}
