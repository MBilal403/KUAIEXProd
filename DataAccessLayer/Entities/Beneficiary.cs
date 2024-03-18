using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Beneficiary
    {
        [Key]
        public int Beneficiary_Id { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public string FullName { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public Nullable<DateTime> Birth_Date { get; set; }
        public string Remittance_Purpose { get; set; }
        public Nullable<int> Remittance_Type_Id { get; set; }
        public string Remittance_Instruction { get; set; }
        public string Phone_No { get; set; }
        public string Mobile_No { get; set; }
        public string Fax_No { get; set; }
        public string Email_Address1 { get; set; }
        public string Email_Address2 { get; set; }
        public Nullable<int> Identification_Type { get; set; }
        public string Identification_No { get; set; }
        public string Identification_Remarks { get; set; }
        public Nullable<DateTime> Identification_Issue_Date { get; set; }
        public Nullable<DateTime> Identification_Expiry_Date { get; set; }
        public Nullable<int> Bank_Id { get; set; }
        public Nullable<int> Branch_Id { get; set; }
        public string Bank_Account_No { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Name { get; set; }
        public Nullable<int> Branch_City_Id { get; set; }
        public string Branch_City_Name { get; set; }
        public string Branch_Address_Line1 { get; set; }
        public string Branch_Address_Line2 { get; set; }
        public string Branch_Address_Line3 { get; set; }
        public string Branch_Number { get; set; }
        public string Branch_Phone_Number { get; set; }
        public string Branch_Fax_Number { get; set; }
        public Nullable<int> Destination_Country_Id { get; set; }
        public Nullable<int> Destination_City_Id { get; set; }
        public string DD_Beneficiary_Name { get; set; }
        public string Bank_Account_type { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Remittance_Remarks { get; set; }
        public string Bank_Code { get; set; }
        public Nullable<int> Routing_Bank_Id { get; set; }
        public Nullable<int> Routing_Bank_Branch_Id { get; set; }
        public Nullable<int> Remittance_Subtype_Id { get; set; }
        public string Birth_Place { get; set; }
        public char IsBannedList { get; set; }
        public Nullable<DateTime> BannedListCreatedOn { get; set; }
        public Nullable<int> BannedListClearedBy { get; set; }
        public string TransFastInfo { get; set; }
        public Guid UID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public string Remittance_Purpose_Detail { get; set; }
        public string Remitter_Relation { get; set; }
        public string Remitter_Relation_Detail { get; set; }
        public string Source_Of_Income { get; set; }
        public string Source_Of_Income_Detail { get; set; }
        public Nullable<int> Nationality_Id { get; set; }
        public string Bank_Account_Title { get; set; }
        public Nullable<int> Prod_Beneficiary_Id { get; set; }
    }
}
