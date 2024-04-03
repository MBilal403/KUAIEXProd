using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Comparison_Lookup
    {
        [Key]
        public int Id { get; set; }
        public string Operator { get; set; }
        public string Message { get; set; }
    }
}
