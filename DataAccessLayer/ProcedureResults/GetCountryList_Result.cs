using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetCountryList_Result
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Nationality { get; set; }
        public string Under_Review_Status { get; set; }
        public string High_Risk_Status { get; set; }
        public string Alpha_2_Code { get; set; }
        public string Alpha_3_Code { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
    }
}
