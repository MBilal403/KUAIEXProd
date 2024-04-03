using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProdEntities
{
    public class Currency_Mst
    {
        [Key]
        public int Currency_Id { get; set; }
        public string Currency_Code { get; set; }
        public string English_Name { get; set;}
        public string Record_Status { get; set;}
    }
}
