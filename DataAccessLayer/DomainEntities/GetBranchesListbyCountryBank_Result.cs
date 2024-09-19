using System;

namespace DataAccessLayer.DomainEntities
{

    public class GetBranchesListbyCountryBank_Result
    {
        public string English_Name { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string EMail_Address1 { get; set; }
        public string Address_Line3 { get; set; }
        public string Country_Name { get; set; }
        public string City_Name { get; set; }
        public string Bank_Name { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public string Currency { get; set; }
    }
}
