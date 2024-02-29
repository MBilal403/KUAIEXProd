using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.DTO.Beneficiary
{
    public class LoadBeneficiaryDTO
    {
        public int Beneficiary_Id { get; set; }
        public int Customer_Id { get; set; }
        public string FullName { get; set; }
        public string Customer_Name { get; set; }
        public string Address_Line3 { get; set; }
        public string Country_Name { get; set; }
        public string Currency_Name { get; set; }
        public string Birth_Date { get; set; }
        public string Bank_Name { get; set; }
        public Guid UID { get; set; }
    }
}