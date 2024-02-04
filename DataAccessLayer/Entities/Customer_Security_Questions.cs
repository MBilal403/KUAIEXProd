using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Customer_Security_Questions
    {
        [Key]
        public Nullable<int> Id { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public Nullable<int> Question_Id { get; set; }
        public string Answer { get; set; }
    }
}
