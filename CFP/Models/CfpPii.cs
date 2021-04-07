using System;

namespace c19t.SDK.CFP.Models
{
    public class CfpPii
    {
        /// <summary>
        /// First name of the person
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Last name of the person
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Number of the persons ID (e.g. Passport, Health insurance card, drivers licence, ...)
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// Tax id / number of the person
        /// </summary>
        public string TaxId { get; set; }

        /// <summary>
        /// The persons date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }
}
