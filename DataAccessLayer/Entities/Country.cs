using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Nationality { get; set; }
        public string Status { get; set; }
        [Required]
        public string Alpha_2_Code { get; set; }
        public string Alpha_3_Code { get; set; }
        public string Remittance_Status { get; set; }
        public Nullable<int> City_Id { get; set; }
        public string High_Risk_Status { get; set; }
        public string Under_Review_Status { get; set; }
        public string Country_Dialing_Code { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public Guid UID { get; set; }
        public Nullable<int> Prod_Country_Id { get; set; }
    }
}
