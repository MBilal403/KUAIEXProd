using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Country_Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.Guid> UID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> Prod_City_Id { get; set; }
        public string Prod_City_Ids { get; set; }
    }
}
