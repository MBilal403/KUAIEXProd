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
    
    public class Customer_Mst
    {
        public int Customer_Id { get; set; }
        public string GUID { get; set; }
        public Nullable<int> Agency_Id { get; set; }
        public Nullable<int> Agency_Branch_Id { get; set; }
        public string English_Name1 { get; set; }
        public string English_Name2 { get; set; }
        public string English_Name3 { get; set; }
        public string Arabic_Name1 { get; set; }
        public string Arabic_Name2 { get; set; }
        public string Arabic_Name3 { get; set; }
        public string Customer_Type { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<System.DateTime> Birth_Date { get; set; }
        public Nullable<int> Identification_Type { get; set; }
        public string Identification_Number { get; set; }
        public string Identification_Remarks { get; set; }
        public Nullable<System.DateTime> Identification_Issued_Date { get; set; }
        public Nullable<System.DateTime> Identification_Expiry_Date { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public string Tel_Number1 { get; set; }
        public string Mob_Number1 { get; set; }
        public string Fax_Number1 { get; set; }
        public string EMail_Address1 { get; set; }
        public string EMail_Address2 { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
        public Nullable<int> Updated_User { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<int> customer_id_old { get; set; }
        public string RemitterLocalAdrFlatNo { get; set; }
        public string RemitterLocalAdrUnitNo { get; set; }
        public string RemitterLocalAdrFloorNo { get; set; }
        public string RemitterLocalAdrBldgNo { get; set; }
        public string RemitterLocalAdrJadah { get; set; }
        public string RemitterLocalAdrBlockNo { get; set; }
        public string RemitterLocalAdrStreet { get; set; }
        public Nullable<int> RemitterLocalAdrArea { get; set; }
        public Nullable<int> RemitterLocalAdrCountry { get; set; }
        public string BirthPlace { get; set; }
        public Nullable<decimal> MonthlyLimit { get; set; }
        public Nullable<decimal> YearlyLimit { get; set; }
        public Nullable<int> BirthCountryId { get; set; }
        public string PEP_Status { get; set; }
        public Nullable<decimal> MonthlyIncome { get; set; }
        public Nullable<decimal> MonthlyOtherIncome { get; set; }
        public string OtherIncomeDetails { get; set; }
        public Nullable<decimal> YearlyIncome { get; set; }
        public Nullable<decimal> AssetVolume { get; set; }
        public string AssetsDetails { get; set; }
        public string PEP_Details { get; set; }
        public Nullable<int> Residency_Type { get; set; }
        public string IsBannedList { get; set; }
        public Nullable<System.DateTime> BannedListCheckedOn { get; set; }
        public string BannedListCleared { get; set; }
        public Nullable<System.DateTime> BannedListClearedOn { get; set; }
        public Nullable<int> BannedListClearedBy { get; set; }
        public Nullable<decimal> ComplianceLimit { get; set; }
        public string ComplianceComments { get; set; }
        public Nullable<int> ComplianceTxnCount { get; set; }
        public Nullable<System.DateTime> ComplianceLimitExpiry { get; set; }
        public string RiskType { get; set; }
        public Nullable<System.DateTime> RiskDate { get; set; }
    }
}
