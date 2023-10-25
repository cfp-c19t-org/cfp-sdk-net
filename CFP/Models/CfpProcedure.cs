namespace c19t.SDK.CFP.Models
{
    public enum CfpProcedure
    {
        /// <summary>
        /// Generic procedure for e.g. passport id
        /// </summary>
        GenericProcedure0000 = 0,

        /// <summary>
        /// Procedure to create CFP with firstname and lastname
        /// </summary>
        FirstnameLastnameProcedure0001 = 1,

        /// <summary>
        /// Procedure to create CFP with firstname, lastname and date of birth
        /// </summary>
        FirstnameLastnameDoBProcedure0002 = 2,

        /// <summary>
        /// Procedure to create CFP with firstname, lastname, date of birth and id number
        /// </summary>
        FirstnameLastnameDoBIdNumberProcedure0003 = 3,

        /// <summary>
        /// Procedure to create CFP with firstname, lastname, date of birth and tax number
        /// </summary>
        FirstnameLastnameDoBTaxIdProcedure0004 = 4,

        /// <summary>
        /// Procedure to create CFP with digitalId and start date
        /// </summary>
        DigitalIDStartDateProcedure0005 = 5,
    }
}
