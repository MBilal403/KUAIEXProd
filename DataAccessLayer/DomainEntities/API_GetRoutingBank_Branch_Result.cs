using System;

namespace DataAccessLayer.DomainEntities
{

    public partial class API_GetRoutingBank_Branch_Result
    {
        public int Bank_Branch_Id { get; set; }
        public Nullable<int> Bank_Id { get; set; }
        public string Branch_Code { get; set; }
        public string English_Name { get; set; }
        public string Arabic_Name { get; set; }
        public Nullable<int> City_Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public Nullable<decimal> Upper_Limit { get; set; }
        public string Account_Reference { get; set; }
        public string Reference_Location { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public string Tel_Number1 { get; set; }
        public string Fax_Number1 { get; set; }
        public string EMail_Address1 { get; set; }
        public string Contact_Person1 { get; set; }
        public string Contact_Title1 { get; set; }
        public string Contact_Person2 { get; set; }
        public string Contact_Title2 { get; set; }
        public string Draft_Status { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
        public Nullable<int> Updated_User { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<int> Bank_SubAgent_Id { get; set; }
    }
}
