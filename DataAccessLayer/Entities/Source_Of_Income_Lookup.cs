using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Source_Of_Income_Lookup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Record_Status { get; set; }
        public Nullable<int> Display_Order { get; set; }
    }
}
