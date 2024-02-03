using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DomainEntities
{
    public class Individual_KYC : BaseEntity
    {
        public int Question_Id { get; set; }
        public bool Answer { get; set; }
        public int Customer_Id { get; set; }
        public string Details { get; set; }
    }
}
