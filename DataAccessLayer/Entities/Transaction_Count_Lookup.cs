using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Transaction_Count_Lookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
