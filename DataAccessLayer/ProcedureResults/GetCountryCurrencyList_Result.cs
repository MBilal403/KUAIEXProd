using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetCountryCurrencyList_Result
    {
        public Nullable<int> Id { get; set; }
        public string Country_Name { get; set; }
        public string Currency_Name { get; set; }
        public Nullable<decimal> CommissionRate1 { get; set; }
        public Nullable<decimal> CommissionRate2 { get; set; }
        public Nullable<decimal> AmountLimit { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<Guid> UID { get; set; }
    }
}
