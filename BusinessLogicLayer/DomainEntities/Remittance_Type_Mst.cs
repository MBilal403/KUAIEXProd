namespace KuaiexDashboard
{
    using System;
    using System.Collections.Generic;
    
    public class Remittance_Type_Mst
    {
        public int Remittance_Type_Id { get; set; }
        public string English_Name { get; set; }
        public string Arabic_Name { get; set; }
        public Nullable<int> Display_Order { get; set; }
        public string Remittance_Master_Type { get; set; }
        public string Voucher_Sub_Type { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
        public Nullable<int> Updated_User { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
    }
}
