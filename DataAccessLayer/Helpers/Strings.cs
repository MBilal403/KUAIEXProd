using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helpers
{
    public static  class Strings
    {
       public static string EscapeSingleQuotes(string input)
        {
            // Replace each single quote with two single quotes
            return input.Replace("'", "''");
        }
    }
}
