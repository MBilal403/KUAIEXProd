using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.DTO.Customer
{
    public class CustomerDTO
    {
        public int Customer_Id { get; set; }
        public int? Agency_Id { get; set; }
        public int? Agency_Branch_Id { get; set; }
        public string Name { get; set; }
        public string Arabic_Name { get; set; }
        public int? Gender { get; set; }
        public int? Identification_Type { get; set; }
        public string Identification_Number { get; set; }
        public DateTime? Identification_Expiry_Date { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public string Mobile_No { get; set; }
        public string Email_Address { get; set; }
        public string Area { get; set; }
        public string House_Number { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string City { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Login_Id { get; set; }
        public string Password { get; set; }
        public int? Security_Question_Id_1 { get; set; }
        public string Security_Answer_1 { get; set; }
        public int? Security_Question_Id_2 { get; set; }
        public string Security_Answer_2 { get; set; }
        public int? Security_Question_Id_3 { get; set; }
        public string Security_Answer_3 { get; set; }
        public string Device_Key { get; set; }
        public Guid? UID { get; set; }
        public Guid? UID_Token { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public int? PEPClearedBy { get; set; }
        public string Status { get; set; }
        public int? IsBlocked { get; set; }
        public DateTime? PEPCheckedOn { get; set; }
        public DateTime? PEPClearedOn { get; set; }
        public DateTime? RiskDate { get; set; }
        public string PEPCleared { get; set; }
        public string RiskType { get; set; }
        public string IsBannedList { get; set; }
        public int? BannedListClearedBy { get; set; }
        public DateTime? BannedListClearedOn { get; set; }
        public string BannedListCleared { get; set; }
        public DateTime? BannedListCheckedOn { get; set; }
        public int? Block_Count { get; set; }
        public int? InvalidTryCount { get; set; }
        public string Civil_Id_Front { get; set; }
        public string Civil_Id_Back { get; set; }
        public HttpPostedFileBase CivilIdFrontImage { get; set; }
        public HttpPostedFileBase CivilIdBackImage { get; set; }
        public string Source_Of_Income { get; set; }
        public int? Risk_Status { get; set; }
        public string Place_Of_Employment { get; set; }
        public string Pep_Status { get; set; }
        public string Pep_Description { get; set; }
        public string Identification_Additional_Detail { get; set; }
        public int? Residence_Type { get; set; }
        public string Telephone_No { get; set; }
        public string Birth_Place { get; set; }
        public int? Birth_Country { get; set; }
        public decimal? Monthly_Income { get; set; }
        public int? Expected_Monthly_Trans_Count { get; set; }
        public decimal? Other_Income { get; set; }
        public string Other_Income_Detail { get; set; }
        public decimal? Monthly_Trans_Limit { get; set; }
        public decimal? Yearly_Trans_Limit { get; set; }
        public decimal? Compliance_Limit { get; set; }
        public int? Compliance_Trans_Count { get; set; }
        public DateTime? Compliance_Limit_Expiry { get; set; }
        public string Compliance_Comments { get; set; }
        public int? IsVerified { get; set; }
        public bool? IsReviwed { get; set; }
        public int? Prod_Remitter_Id { get; set; }
        public string Employer { get; set; }
        public int? Is_Profile_Completed { get; set; }
        public string pepcheckbox15 { get; set; }
        public string pepcheckbox16 { get; set; }
        public string pepcheckbox17 { get; set; }
        public string checkbox1 { get; set; }
        public string checkbox2 { get; set; }
        public string checkbox3 { get; set; }
        public string checkbox4 { get; set; }
        public string checkbox5 { get; set; }
        public string checkbox6 { get; set; }
        public string checkbox7 { get; set; }
        public string checkbox8 { get; set; }
        public string checkbox9 { get; set; }
        public string checkbox10 { get; set; }
        public string checkbox11 { get; set; }
        public string checkbox12 { get; set; }
        public string checkbox13 { get; set; }
        public string checkbox14 { get; set; }
        public string other_Detail { get; set; }
    }
}