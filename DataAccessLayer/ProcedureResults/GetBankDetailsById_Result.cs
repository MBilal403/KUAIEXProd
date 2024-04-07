using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class GetBankDetailsById_Result
    {
        public string English_Name { get; set; }
        public string Arabic_Name { get; set; }
        public string Short_English_Name { get; set; }
        public string Short_Arabic_Name { get; set; }
        public string CountryName { get; set; }
        public string CurrencyName { get; set; }
        public Nullable<decimal> Upper_Limit { get; set; }
        public string Full_Address { get; set; }
        public string Tel_Number1 { get; set; }
        public string Fax_Number1 { get; set; }
        public string EMail_Address1 { get; set; }
        public string Prefix_Draft { get; set; }
        public string Sufix_Draft { get; set; }
        public string Reimbursement_Text_Status { get; set; }
        public string Contact_Person1 { get; set; }
        public string Contact_Title1 { get; set; }
        public string Mob_Number1 { get; set; }
        public string Contact_Person2 { get; set; }
        public string Contact_Title2 { get; set; }
        public string Mob_Number2 { get; set; }
        public string Remarks { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
        public int HO_Branch1_Id { get; set; }
        public string Dispatch_Status { get; set; }
        public string Dispatch_Header { get; set; }
        public string Dispatch_Type { get; set; }
        public string Bank_Code { get; set; }
        public string Remittance_Sequence_Prefix { get; set; }
        public int Common_Branch_Id { get; set; }
    
    }
}
