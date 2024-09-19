using System;
namespace DataAccessLayer.DomainEntities
{ 
    public class CustomerChatMain_Result
    {
        public string Name { get; set; }
        public Nullable<System.Guid> UID { get; set; }
        public string ChatOn { get; set; }
        public string NickName { get; set; }
    }
}
