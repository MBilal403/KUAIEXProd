using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProdEntities
{
    public class Country_Mst
    {
        [Key]
        public int Country_Id { get; set; }
        public string English_Name { get; set; }
        public string Arabic_Name { get; set; }
        public string English_Nationality { get; set; }
        public string Arabic_Nationality { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
        public string Country_Code { get; set; }
        public string Remittance_Status { get; set; }
        public int Any_City_Id { get; set; }
        public string High_Risk_Status { get; set; }
        public string Under_Review_Status { get; set; }
    }
}
