using System;


namespace DataAccessLayer.DomainEntities
{
    public class Bank_Account_Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
