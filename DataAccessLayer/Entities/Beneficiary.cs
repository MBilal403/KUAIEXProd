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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Beneficiary_Id { get; set; }
        public int? Customer_Id { get; set; }
        public string Arabic_Name { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public int? Country_Id { get; set; }
        public int? Currency_Id { get; set; }
        public DateTime? Birth_Date { get; set; }
        public string Remittance_Purpose { get; set; }
        public int? Remittance_Type_Id { get; set; }
        public string Remittance_Instruction { get; set; }
        public string Status { get; set; }
        public DateTime? BannedListClearedOn { get; set; }
        public string BannedListCleared { get; set; }
        public string Phone_No { get; set; }
        public string Tel_Number { get; set; }
        public string Mobile_No { get; set; }
        public string Fax_No { get; set; }
        public string Email_Address1 { get; set; }
        public string Email_Address2 { get; set; }
        public int? Identification_Type { get; set; }
        public string Identification_No { get; set; }
        public string Identification_Remarks { get; set; }
        public DateTime? Identification_Issue_Date { get; set; }
        public DateTime? Identification_Expiry_Date { get; set; }
        public int? Bank_Id { get; set; }
        public int? Branch_Id { get; set; }
        public string Bank_Account_No { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Name { get; set; }
        public int? Branch_City_Id { get; set; }
        public string Branch_City_Name { get; set; }
        public string Branch_Address_Line1 { get; set; }
        public string Branch_Address_Line2 { get; set; }
        public string Branch_Address_Line3 { get; set; }
        public string Branch_Number { get; set; }
        public string Branch_Phone_Number { get; set; }
        public string Branch_Fax_Number { get; set; }
        public int? Destination_Country_Id { get; set; }
        public int? Destination_City_Id { get; set; }
        public string DD_Beneficiary_Name { get; set; }
        public string Bank_Account_type { get; set; }
        public int? Gender { get; set; }
        public string Remittance_Remarks { get; set; }
        public string Bank_Code { get; set; }
        public int? Routing_Bank_Id { get; set; }
        public int? Routing_Bank_Branch_Id { get; set; }
        public int? Remittance_Subtype_Id { get; set; }
        public string Birth_Place { get; set; }
        public string IsBannedList { get; set; }
        public DateTime? BannedListCreatedOn { get; set; }
        public int? BannedListClearedBy { get; set; }
        public string TransFastInfo { get; set; }
        public string PEPStatus { get; set; }
        public string PEPDetails { get; set; }
        public DateTime? PEPCheckedOn { get; set; }
        public int? PEPClearedBy { get; set; }
        public DateTime? PEPClearedOn { get; set; }
        public string PEPCleared { get; set; }
        public Guid? UID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public string Remittance_Purpose_Detail { get; set; }
        public string Remitter_Relation { get; set; }
        public string Remitter_Relation_Detail { get; set; }
        public string Source_Of_Income { get; set; }
        public string Source_Of_Income_Detail { get; set; }
        public int? Nationality_Id { get; set; }
        public string Bank_Account_Title { get; set; }
        public int? Prod_Beneficiary_Id { get; set; }
        public string SwiftCode { get; set; }
    }
}
