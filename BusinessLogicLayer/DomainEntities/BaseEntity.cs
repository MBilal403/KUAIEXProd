using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DomainEntities
{
   public  class BaseEntity
    {
        public int Id { get; set; }
        public Guid UID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
