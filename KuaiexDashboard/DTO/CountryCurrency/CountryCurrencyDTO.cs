using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.DTO.CountryCurrency
{
    public class CountryCurrencyDTO
    {
        public int Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public Nullable<decimal> CommissionRate1 { get; set; }
        public Nullable<decimal> CommissionRate2 { get; set; }
        public Nullable<decimal> AmountLimit { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatdIp { get; set; }
        public Guid UID { get; set; }
    }
}