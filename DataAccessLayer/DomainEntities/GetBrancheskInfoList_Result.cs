using System;
namespace DataAccessLayer.DomainEntities
{
    public class GetBrancheskInfoList_Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
