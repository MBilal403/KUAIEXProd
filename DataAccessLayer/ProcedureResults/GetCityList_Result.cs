using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetCityList_Result
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string CountryName { get; set; }
    }
}
