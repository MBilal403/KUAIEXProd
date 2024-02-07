using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.DTO
{
    public class Remittance_TrnDetailDTO
    {
        public int Remittance_Id { get; set; }
        public DateTime Remittance_Date { get; set; }
        public string Remitter_Name { get; set; }
        public string Address_Line3 { get; set; }
        public string Beneficiary_Name { get; set; }
        public string Country_Name { get; set; }
        public string Identification_Number { get; set; }
        public string DD_Number { get; set; }
        public string Bank_Name { get; set; }
        public string Customer_Name { get; set; }
    }
}