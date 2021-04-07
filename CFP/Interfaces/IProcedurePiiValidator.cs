using c19t.SDK.CFP.Models;

namespace c19t.SDK.CFP.Interfaces
{
    interface IProcedurePiiValidator
    {
        /// <summary>
        /// Validate required pii for 0000 procedure. Required data is: id number
        /// </summary>
        /// <param name="data"> personal data </param>
        /// <returns> validation state </returns>
        bool Validate0000Pii(CfpPii data);

        /// <summary>
        /// Validate required pii for 0001 procedure. Required data is: first name, last name
        /// </summary>
        /// <param name="data"> personal data </param>
        /// <returns> validation state </returns>
        bool Validate0001Pii(CfpPii data);

        /// <summary>
        /// Validate required pii for 0002 procedure. Required data is: first name, last name, date of birth
        /// </summary>
        /// <param name="data"> personal data </param>
        /// <returns> validation state </returns>
        bool Validate0002Pii(CfpPii data);

        /// <summary>
        /// Validate required pii for 0003 procedure. Required data is: first name, last name, date of birth, id number
        /// </summary>
        /// <param name="data"> personal data </param>
        /// <returns> validation state </returns>
        bool Validate0003Pii(CfpPii data);

        /// <summary>
        /// Validate required pii for 0004 procedure. Required data is: first name, last name, date of birth, tax id/number
        /// </summary>
        /// <param name="data"> personal data </param>
        /// <returns> validation state </returns>
        bool Validate0004Pii(CfpPii data);

        /// <summary>
        /// Validates required pii per procedure
        /// </summary>
        /// <param name="data"> personal data </param>
        /// <param name="procedure"> procedure code </param>
        /// <returns></returns>
        bool ValidatePii(CfpPii data, CfpProcedure procedure);
    }
}
