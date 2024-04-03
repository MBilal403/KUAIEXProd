using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<int> Display_Order { get; set; }
        public Nullable<decimal> DD_Rate { get; set; }
        public Nullable<decimal> TT_Rate { get; set; }
        public Nullable<decimal> TT_Min_Rate { get; set; }
        public string Status { get; set; }
        public Nullable<int> IsBaseCurrency { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public Guid UID { get; set; }
        public int Prod_Currency_Id { get; set; }

    }
}
