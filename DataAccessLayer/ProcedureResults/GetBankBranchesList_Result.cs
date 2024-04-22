using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetBankBranchesList_Result
    {
        public int Bank_Branch_Id { get; set; }
        public string English_Name { get; set; }
        public string CountryName { get; set; }
        public string FullAddress { get; set; }
        public string Record_Status { get; set; }
        public string EMail_Address1 { get; set; }
        public string BankName { get; set; }
    }
}
