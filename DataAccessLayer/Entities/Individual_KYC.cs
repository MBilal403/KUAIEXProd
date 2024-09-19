using DataAccessLayer.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Individual_KYC : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> Question_Id { get; set; }
        public bool Answer { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public string Details { get; set; }
    }
}
