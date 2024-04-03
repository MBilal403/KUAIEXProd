using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Remittance_Currency_Limit_Log
    {
        [Key]
        public int Id { get; set; }
        public int Remittance_Currency_Limit_Id { get; set; }
        public string Operation { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<DateTime> DateTime { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public Nullable<decimal> DD_Rate { get; set; }
        public Nullable<decimal> TT_Rate { get; set; }
        public Nullable<decimal> TT_Min_Rate { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> Updatedby { get; set; }
        public Nullable<Guid> UID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public int Comparison_Id { get; set; }
    }
}
