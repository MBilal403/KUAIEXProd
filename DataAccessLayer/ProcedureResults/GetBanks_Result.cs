using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetBanks_Result
    {
        public int Bank_Id { get; set; }
        public string English_Name { get; set; }
        public Guid UID { get; set; }
        public string CountryName { get; set; }
        public string CurrencyName { get; set; }
        public string Record_Status { get; set; }
        public string FullAddress { get; set; }
        public int NumberOfTransaction { get; set; }
        public decimal AmountLimit { get; set; }
    }
}
