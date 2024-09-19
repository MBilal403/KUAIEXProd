using System;

namespace DataAccessLayer.DomainEntities
{
    public  class BaseEntity
    {
        public Nullable<Guid> UID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }
}
