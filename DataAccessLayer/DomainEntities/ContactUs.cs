using System;

namespace DataAccessLayer.DomainEntities
{

    public class ContactUs
    {
        public int Id { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> Status { get; set; }
        public string CustomerService { get; set; }
    }
}
