using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DomainEntities
{
    public class Remittance_Purpose_Lookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Record_Status { get; set; }
        public int Display_Order { get; set; }
    }
}
