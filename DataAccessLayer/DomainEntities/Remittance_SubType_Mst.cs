using System;
using System.Collections.Generic;

namespace DataAccessLayer.DomainEntities
{

    public class Remittance_SubType_Mst
    {
        public int Remittance_SubType_Id { get; set; }
        public int Remittance_Type_Id { get; set; }
        public string Remittance_SubType { get; set; }
        public int Bank_Id { get; set; }
        public string English_Name { get; set; }
        public string Txn_Code1 { get; set; }
        public string Txn_Code2 { get; set; }
        public string Txn_Code3 { get; set; }
        public string Record_Status { get; set; }
        public string TT_Number_Prefix { get; set; }
    }
}
