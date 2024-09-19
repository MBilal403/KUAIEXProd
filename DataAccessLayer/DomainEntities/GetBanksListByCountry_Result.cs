using System;

namespace DataAccessLayer.DomainEntities
{

    public class GetBanksListByCountry_Result
    {
        public int Bank_Id { get; set; }
        public string English_Name { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public string Country_Name { get; set; }
        public string Currency { get; set; }
    }
}
