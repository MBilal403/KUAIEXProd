﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Bank_Mst
    {
        [Key]
        public int Bank_Id { get; set; }
        public string English_Name { get; set; }
        public string Arabic_Name { get; set; }
        public string Short_English_Name { get; set; }
        public string Short_Arabic_Name { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public Nullable<int> NumberOfTransactionMonthly { get; set; }
        public Nullable<decimal> Upper_Limit { get; set; }
        public Nullable<decimal> TransactionAmountFC { get; set; }
        public Nullable<decimal> TransactionAmountKWD { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
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
        public Nullable<int> Updated_User { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<int> HO_Branch1_Id { get; set; }
        public string Dispatch_Status { get; set; }
        public string Dispatch_Header { get; set; }
        public string Dispatch_Type { get; set; }
        public string Column_Separator { get; set; }
        public string Bank_Code { get; set; }
        public string Remittance_Sequence_Prefix { get; set; }
        public Nullable<int> Common_Branch_Id { get; set; }
        public string Column_Demarkation_Char { get; set; }
        public Nullable<int> IsDefaultBank { get; set; }
        public Guid UID { get; set; }
        public int Prod_Bank_Id { get; set; }
        public decimal AmountLimit { get; set; }
        public int NumberOfTransaction { get; set; }
        public int PriorityValue { get; set; }
        public Nullable<bool> DirectTransaction { get; set; }

    }
}
