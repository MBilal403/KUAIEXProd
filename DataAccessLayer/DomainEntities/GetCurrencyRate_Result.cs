using System;
namespace DataAccessLayer.DomainEntities
{
    public class GetCurrencyRate_Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> DD_Rate { get; set; }
    }
}
