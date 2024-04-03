using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProdEntities
{
    public class CountryCurrency
    {
        public int Country_Id { get; set; }
        public int Currency_Id { get; set; }
        public int RECID { get; set; }
        public int Display_Order { get; set; }

        // Skip for this time 
        // public int Transit_Account_Id { get; set; }
    }
}
