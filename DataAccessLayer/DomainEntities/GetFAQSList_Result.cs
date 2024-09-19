using System;
namespace DataAccessLayer.DomainEntities
{
    public class GetFAQSList_Result
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Status { get; set; }
    }
}
