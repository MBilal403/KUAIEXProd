using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Remittance_SubType_Mst
    {
        [Key]
        public Nullable<int> Remittance_SubType_Id { get; set; }
        public Nullable<int> Remittance_Type_Id { get; set; }
        public string Remittance_SubType { get; set; }
        public Nullable<int> Bank_Id { get; set; }
        public string English_Name { get; set; }
        public string Txn_Code1 { get; set; }
        public string Txn_Code2 { get; set; }
        public string Txn_Code3 { get; set; }
        public Nullable<char> Record_Status { get; set; }
        public string TT_Number_Prefix { get; set; }
    }
}
