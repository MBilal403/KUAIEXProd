//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KuaiexDashboard
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Status { get; set; }
        public string Alpha_2_Code { get; set; }
        public string Alpha_3_Code { get; set; }
        public string Remittance_Status { get; set; }
        public Nullable<int> City_Id { get; set; }
        public string High_Risk_Status { get; set; }
        public string Country_Dialing_Code { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public Nullable<System.Guid> UID { get; set; }
        public Nullable<decimal> Comission { get; set; }
        public Nullable<int> Prod_Country_Id { get; set; }
    }
}
