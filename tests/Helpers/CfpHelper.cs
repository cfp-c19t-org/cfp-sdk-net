using System;
using System.Linq;

namespace c19t.SDK.CFP.Tests.Helpers
{
    class CfpHelper
    {
        public static string GeneratePersonalCode()
        {
            var random = new Random();

            // leave out characters that might look the same 1/l - 0/o
            return new string(Enumerable.Repeat("abcdefghijkmnopqrstuvwxyz123456789", 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
