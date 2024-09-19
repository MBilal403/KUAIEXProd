using System;

namespace DataAccessLayer.DomainEntities
{

    public class UserType_Lookup
    {
        public int UserTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
