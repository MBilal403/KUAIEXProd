using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Terms_and_Privacy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> Content_Type { get; set; }
        public string Title { get; set; }
    }
}
