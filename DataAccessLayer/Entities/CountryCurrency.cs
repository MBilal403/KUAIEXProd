using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CountryCurrency
    {
        [Key]
        public int Id { get; set; }
        public int Country_Id { get; set; }
        public int Currency_Id { get; set; }
        public decimal CommissionRate1 { get; set; }
        public decimal CommissionRate2 { get; set; }
        public decimal AmountLimit { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatdIp { get; set; }
        public Nullable<Guid> UID { get; set; }
    }
}
