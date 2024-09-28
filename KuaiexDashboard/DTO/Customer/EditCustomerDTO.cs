using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.DTO.Customer
{
    public class EditCustomerDTO
    {
        public string Name { get; set; }
        public int Customer_Id { get; set; }
        public int? Gender { get; set; }
        public string Source_Of_Income { get; set; }
        public string Email_Address { get; set; }
        public int? Identification_Type { get; set; }
        public string Identification_Number { get; set; }
        public DateTime? Identification_Expiry_Date { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public string Mobile_No { get; set; }
        public string Area { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Flat { get; set; }
        public Guid UID { get; set; }
        public string Civil_Id_Front { get; set; }
        public string Civil_Id_Back { get; set; }
        public string Identification_Additional_Detail { get; set; }
        public Nullable<int> Residence_Type { get; set; }
        public string Telephone_No { get; set; }
        public string Birth_Place { get; set; }
        public Nullable<int> Birth_Country { get; set; }
        public Nullable<decimal> Monthly_Income { get; set; }
        public Nullable<int> Expected_Monthly_Trans_Count { get; set; }
        public Nullable<decimal> Other_Income { get; set; }
        public string Other_Income_Detail { get; set; }
        public Nullable<decimal> Monthly_Trans_Limit { get; set; }
        public Nullable<int> IsVerified { get; set; }
        public Nullable<bool> IsReviwed { get; set; }
        public string Employer { get; set; }
        public Nullable<int> Is_Profile_Completed { get; set; }
        public string Identification_Type_Description { get; set; }
        public Nullable<bool> pepcheckbox15 { get; set; }
        public Nullable<bool> pepcheckbox16 { get; set; }
        public Nullable<bool> pepcheckbox17 { get; set; }
        public Nullable<bool> checkbox1 { get; set; }
        public Nullable<bool> checkbox2 { get; set; }
        public Nullable<bool> checkbox3 { get; set; }
        public Nullable<bool> checkbox4 { get; set; }
        public Nullable<bool> checkbox5 { get; set; }
        public Nullable<bool> checkbox6 { get; set; }
        public Nullable<bool> checkbox7 { get; set; }
        public Nullable<bool> checkbox8 { get; set; }
        public Nullable<bool> checkbox9 { get; set; }
        public Nullable<bool> checkbox10 { get; set; }
        public Nullable<bool> checkbox11 { get; set; }
        public Nullable<bool> checkbox12 { get; set; }
        public Nullable<bool> checkbox13 { get; set; }
        public Nullable<bool> checkbox14 { get; set; }

        public string other_Detail { get; set; }
    }
}