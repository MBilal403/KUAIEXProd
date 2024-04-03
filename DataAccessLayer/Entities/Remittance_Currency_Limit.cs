using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Remittance_Currency_Limit
    {
        [Key]
        public int Id { get; set; }
        public int Currency_Id { get; set; }
        public decimal DD_Rate { get; set; }
        public decimal TT_Rate { get; set; }
        public decimal TT_Min_Rate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int Updatedby { get; set; }
        public Guid UID { get; set; }
        public decimal Amount { get; set; }
        public int Comparison_Id { get; set; }
    }
}
