using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProdEntities
{
    public class City_Mst
    {
        [Key]
        public int City_Id { get; set; }
        public int Country_Id { get; set; }
        public string English_Name { get; set; }
        public string Arabic_Name { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
    }
}
