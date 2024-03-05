using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Relationship_Lookup
    {
        [Key]
        public int Relationship_Id { get; set; }
        public Nullable<int> Status { get; set; }
        public string Name { get; set; }
    }
}
