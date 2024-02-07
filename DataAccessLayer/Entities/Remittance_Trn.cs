using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Remittance_Trn
    {
        [Key]
        public int Remittance_Id { get; set; }
        public Nullable<int> Agency_Id { get; set; }
        public Nullable<int> Agency_Branch_Id { get; set; }
        public Nullable<int> Agency_Branch_Remittance_Id { get; set; }
        public string GUID { get; set; }
        public Nullable<System.DateTime> Remittance_Date { get; set; }
        public Nullable<int> Voucher_Id { get; set; }
        public string DD_Number { get; set; }
        public string Reference_Number { get; set; }
        public Nullable<int> Remittance_Type_Id { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public Nullable<int> City_Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Bank_Id { get; set; }
        public Nullable<int> Bank_Branch_Id { get; set; }
        public Nullable<int> Identification_Type { get; set; }
        public string Identification_Number { get; set; }
        public Nullable<System.DateTime> Identification_Exp_Date { get; set; }
        public string Remitter_Name { get; set; }
        public string Remitter_Mob_Number1 { get; set; }
        public string Remitter_EMail1 { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public string Tel_Number1 { get; set; }
        public string Mob_Number1 { get; set; }
        public string Instructions { get; set; }
        public Nullable<int> Currency_Id_FC { get; set; }
        public Nullable<int> Currency_Id_LC { get; set; }
        public Nullable<decimal> Amount_FC { get; set; }
        public Nullable<decimal> Amount_LC { get; set; }
        public Nullable<decimal> Amount_BC { get; set; }
        public Nullable<decimal> Conversion_Rate_FC { get; set; }
        public Nullable<decimal> Conversion_Rate_BC { get; set; }
        public Nullable<decimal> Commission_LC { get; set; }
        public Nullable<decimal> Commission_BC { get; set; }
        public Nullable<decimal> Std_Commission_LC { get; set; }
        public Nullable<decimal> Std_Commission_BC { get; set; }
        public Nullable<decimal> Other_Amount_LC { get; set; }
        public Nullable<decimal> Other_Amount_BC { get; set; }
        public Nullable<decimal> Other_Charges_LC { get; set; }
        public Nullable<decimal> Other_Charges_BC { get; set; }
        public Nullable<decimal> Cost_Amount { get; set; }
        public Nullable<int> Purpose_Id { get; set; }
        public string Payment_Type { get; set; }
        public Nullable<System.DateTime> Reconciled_Date { get; set; }
        public Nullable<decimal> Reconciled_Amount { get; set; }
        public string Reconciled_Status { get; set; }
        public string Reconciled_Remarks { get; set; }
        public string Remittance_Operation { get; set; }
        public Nullable<int> Beneficiary_Id { get; set; }
        public string Beneficiary_Name { get; set; }
        public string DD_Beneficiary_Name { get; set; }
        public string Beneficiary_Account_Number { get; set; }
        public Nullable<int> Beneficiary_Bank_Id { get; set; }
        public Nullable<int> Beneficiary_Branch_Id { get; set; }
        public string Beneficiary_Branch_Code { get; set; }
        public Nullable<int> Beneficiary_Identification_Type { get; set; }
        public string Beneficiary_Identification_Number { get; set; }
        public Nullable<System.DateTime> Beneficiary_Identification_Exp_Date { get; set; }
        public string Beneficiary_Address_Line1 { get; set; }
        public string Beneficiary_Address_Line2 { get; set; }
        public string Beneficiary_Address_Line3 { get; set; }
        public string Beneficiary_Tel_Number1 { get; set; }
        public string Beneficiary_Mob_Number1 { get; set; }
        public string Beneficiary_Fax_Number1 { get; set; }
        public string Beneficiary_EMail1 { get; set; }
        public Nullable<int> Beneficiary_Country_Id { get; set; }
        public Nullable<int> Beneficiary_City_Id { get; set; }
        public string Secret_Code { get; set; }
        public Nullable<int> Paying_User_Id { get; set; }
        public Nullable<int> Paying_Agency_Id { get; set; }
        public Nullable<int> Paying_Agency_Branch_Id { get; set; }
        public Nullable<decimal> Cheque_Amount { get; set; }
        public Nullable<decimal> Cash_Amount { get; set; }
        public Nullable<decimal> Card1_Amount { get; set; }
        public Nullable<decimal> Card2_Amount { get; set; }
        public Nullable<System.DateTime> Paid_Date { get; set; }
        public string Paid_Status { get; set; }
        public string Duplicate_Draft_Status { get; set; }
        public string Print_Status { get; set; }
        public string Cancelled_Status { get; set; }
        public string Record_Status { get; set; }
        public string Option_Status { get; set; }
        public Nullable<int> Updated_User { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<decimal> Tax1_Amount_BC { get; set; }
        public Nullable<decimal> Tax2_Amount_BC { get; set; }
        public Nullable<decimal> Tax3_Amount_BC { get; set; }
        public string Posting_Status { get; set; }
        public string Beneficiary_Bank_Name { get; set; }
        public string Beneficiary_Bank_Code { get; set; }
        public string Beneficiary_Bank_Branch_Name { get; set; }
        public string Beneficiary_Bank_Branch_Address_Line1 { get; set; }
        public string Beneficiary_Bank_Branch_Address_Line2 { get; set; }
        public string Beneficiary_Bank_Branch_Address_Line3 { get; set; }
        public Nullable<decimal> Total_Amount_LC { get; set; }
        public string Dispatch_Status { get; set; }
        public Nullable<System.DateTime> Dispatch_Date { get; set; }
        public Nullable<int> Dispatch_Id { get; set; }
        public string Bank_Account_Type { get; set; }
        public string IsBannedList { get; set; }
        public string CBK_Dispatch_Status { get; set; }
        public string Old_Reference_No { get; set; }
        public Nullable<int> Inserted_User { get; set; }
        public Nullable<System.DateTime> Inserted_Date { get; set; }
        public Nullable<int> Remittance_SubType_Id { get; set; }
        public string Unblocked { get; set; }
        public Nullable<int> UnblockedBy { get; set; }
        public Nullable<System.DateTime> UnblockedDate { get; set; }
        public Nullable<int> CBK_Dispatched_By { get; set; }
        public Nullable<System.DateTime> CBK_Dispatch_Date { get; set; }
        public string RemitterLocalAdrFlatNo { get; set; }
        public string RemitterLocalAdrUnitNo { get; set; }
        public string RemitterLocalAdrFloorNo { get; set; }
        public string RemitterLocalAdrBldgNo { get; set; }
        public string RemitterLocalAdrJadah { get; set; }
        public string RemitterLocalAdrBlockNo { get; set; }
        public string RemitterLocalAdrStreet { get; set; }
        public Nullable<int> RemitterLocalAdrArea { get; set; }
        public Nullable<int> RemitterLocalAdrCountry { get; set; }
        public Nullable<System.DateTime> RemitterBirthDate { get; set; }
        public string RemitterBirthPlace { get; set; }
        public Nullable<System.DateTime> BeneficiaryBirthDate { get; set; }
        public string BeneficiaryBirthPlace { get; set; }
        public Nullable<int> IncomeSource { get; set; }
        public string OL_Block_Status { get; set; }
        public Nullable<int> OL_Block_ClearedBy { get; set; }
        public Nullable<System.DateTime> OL_Block_ClearedDate { get; set; }
        public string IncomeSourceDesc { get; set; }
        public string RemittancePurposeDesc { get; set; }
        public string TransFastDetails { get; set; }
        public string PrefDispatch { get; set; }
    }
}
