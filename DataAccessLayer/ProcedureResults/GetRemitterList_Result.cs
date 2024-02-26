using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetRemitterList_Result
    {
        public Nullable<System.Guid> UID { get; set; }
        public string Name { get; set; }
        public string Identification_Number { get; set; }
        public string Email_Address { get; set; }
        public string Occupation { get; set; }
        public Nullable<System.DateTime> Identification_Expiry_Date { get; set; }
        public bool IsReviwed { get; set; }
        public string Description { get; set; }
        public int IsBlocked { get; set; }
    }
}
