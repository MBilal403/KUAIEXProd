﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.DTO.Customer
{
    public class CustomerDTO
    {
        public int Customer_Id { get; set; }
        public Nullable<int> Agency_Id { get; set; }
        public Nullable<int> Agency_Branch_Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Identification_Type { get; set; }
        public string Identification_Number { get; set; }
        public Nullable<System.DateTime> Identification_Expiry_Date { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public string Mobile_No { get; set; }
        public string Email_Address { get; set; }
        public string Area { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Login_Id { get; set; }
        public string Password { get; set; }
        public Nullable<int> Security_Question_Id_1 { get; set; }
        public string Security_Answer_1 { get; set; }
        public Nullable<int> Security_Question_Id_2 { get; set; }
        public string Security_Answer_2 { get; set; }
        public Nullable<int> Security_Question_Id_3 { get; set; }
        public string Security_Answer_3 { get; set; }
        public string Device_Key { get; set; }
        public Nullable<System.Guid> UID { get; set; }
        public Nullable<System.Guid> UID_Token { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedIp { get; set; }
        public Nullable<int> IsBlocked { get; set; }
        public Nullable<int> Block_Count { get; set; }
        public Nullable<int> InvalidTryCount { get; set; }
        public string Civil_Id_Front { get; set; }
        public string Civil_Id_Back { get; set; }
        public Nullable<bool> Pep_Status { get; set; }
        public string Pep_Description { get; set; }
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
        public Nullable<decimal> Yearly_Trans_Limit { get; set; }
        public Nullable<decimal> Compliance_Limit { get; set; }
        public Nullable<int> Compliance_Trans_Count { get; set; }
        public Nullable<System.DateTime> Compliance_Limit_Expiry { get; set; }
        public string Compliance_Comments { get; set; }
        public Nullable<int> IsVerified { get; set; }
        public Nullable<bool> IsReviwed { get; set; }
        public Nullable<int> Prod_Remitter_Id { get; set; }
        public string Employer { get; set; }
        public Nullable<int> Is_Profile_Completed { get; set; }
        public string Identification_Type_Description { get; set; }

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