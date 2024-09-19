using System;

namespace DataAccessLayer.DomainEntities
{

    public partial class Customer_Chat
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Nullable<System.Guid> UID { get; set; }
        public Nullable<int> Message_Type { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<int> App_Type { get; set; }
        public Nullable<int> Agency_Id { get; set; }
        public Nullable<int> Agency_Branch_Id { get; set; }
    }
}
