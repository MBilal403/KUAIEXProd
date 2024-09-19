using System;

namespace DataAccessLayer.DomainEntities
{
    public class GetUsersList_Result
    {
        public Nullable<System.Guid> UID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public int Status { get; set; }
        public string UserType { get; set; }
    }
}
