using BusinessLogicLayer.DomainEntities;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using KuaiexDashboard.DTO.Beneficiary;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.BeneficiaryServices;
using KuaiexDashboard.Services.BeneficiaryServices.Impl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class BeneficiaryController : Controller
    {
        IBeneficiaryService _beneficiaryService;
        BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();

        public BeneficiaryController()
        {
            _beneficiaryService = new BeneficiaryService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadCountry()
        {
            string status = "0:{choose}";

            try
            {
                List<GetCountryList_Result> lstCountries = objBeneficiaryDAL.GetCountryList();
                status = JsonConvert.SerializeObject(lstCountries);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadCurrency(int CountryId)
        {
            string status = "0:{choose}";

            try
            {
                List<Currency_Result> lstcurrency = objBeneficiaryDAL.GetCurrencyByCountry(CountryId);
                status = JsonConvert.SerializeObject(lstcurrency);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadNationality()
        {
            string status = "0:{choose}";

            try
            {
                BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();

                List<GetCountryList_Result> lstCountries = objBeneficiaryDAL.GetCountryList();

                status = JsonConvert.SerializeObject(lstCountries);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadRemittancePurpose()
        {
            string status = "0:{choose}";

            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                List<Remittance_Purpose_Lookup> lstRemittancePurpose = objBeneficiaryDal.GetRemittancePurposeList();
                status = JsonConvert.SerializeObject(lstRemittancePurpose);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadRemitterRelation()
        {
            string status = "0:{choose}";

            try
            {
                IEnumerable<Relationship_Lookup> lstRelationshipLookup = _beneficiaryService.GetRelationshipLookupList();

                status = JsonConvert.SerializeObject(lstRelationshipLookup);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadSourceOfIncome()
        {
            string status = "0:{choose}";
            try
            {
                List<Source_Of_Income_Lookup> lstSourceOfIncome = _beneficiaryService.GetSourceOfIncomeLookupList();

                status = JsonConvert.SerializeObject(lstSourceOfIncome);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadRemittanceType()
        {
            string status = "0:{choose}";

            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                List<Remittance_Type_Mst> lstRemittanceType = objBeneficiaryDal.GetRemittanceTypeList();
                status = JsonConvert.SerializeObject(lstRemittanceType);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadBank(int CountryId)
        {
            string status = "0:{choose}";
            try
            {
                List<Bank_Mst> lstBanks = _beneficiaryService.GetBanksByCountry(CountryId);
                status = JsonConvert.SerializeObject(lstBanks);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadBankAcoountType()
        {
            string status = "0:{choose}";
            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                List<Bank_Account_Type> lstRemittanceType = objBeneficiaryDal.GetBankAccountTypes();
                status = JsonConvert.SerializeObject(lstRemittanceType);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadRemittanceSubType(int Remittance_Type_Id, int Bank_Id)
        {
            string status = "0:{choose}";

            try
            {
                var data = _beneficiaryService.GetRemittanceSubtypes(Remittance_Type_Id, Bank_Id);
                /* BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL(); 
                 List<Remittance_SubType_Mst> lstRemittanceSubtypes = objBeneficiaryDal.GetRemittanceSubtypes(Remittance_Type_Id, Bank_Id);*/
                status = JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadIdentificationType()
        {
            string status = "0:{choose}";
            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                List<IdentificationTypeLookup> lstIdentificationTypes = objBeneficiaryDal.GetIdentificationTypes();
                status = JsonConvert.SerializeObject(lstIdentificationTypes);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadBranchCity()
        {
            string status = "0:{choose}";

            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                List<City> lstBranchCities = objBeneficiaryDal.GetBranchCities();
                status = JsonConvert.SerializeObject(lstBranchCities);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadBranch(int bankId)
        {
            string status = "0:{choose}";
            try
            {
                var data = _beneficiaryService.GetGetBankBranches(bankId);


                //BeneficiaryDAL objBeneficiary = new BeneficiaryDAL();
                //List<Bank_Branch_Mst> lstBankBranches = objBeneficiary.GetBankBranches(bankId);
                status = JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid(Guid CUID)
        {
            string status = "error";
            BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
            var lstbeneficiary = objBeneficiaryDal.GetBeneficiaryByUID(CUID);
            status = Newtonsoft.Json.JsonConvert.SerializeObject(lstbeneficiary);
            return Content(status);
        }
        public ActionResult LoadBanksByCountry(int countryId)
        {
            string status = "error";
            try
            {
                BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();
                List<API_GetBank_MstByCountry_Id_Result> lstBanks = objBeneficiaryDAL.GetBanksByCountryId(countryId);
                status = JsonConvert.SerializeObject(lstBanks);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadBankBranches(int bankId)
        {
            string status = "error";
            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                List<API_GetRoutingBank_Branch_Result> lstBankBranches = objBeneficiaryDal.GetBankBranchesByBankId(bankId);
                status = JsonConvert.SerializeObject(lstBankBranches);
            }
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
        public ActionResult AddBeneficiary(BeneficiaryDTO objBeneficiary)
        {
            string status = "error";

            try
            {
                status = _beneficiaryService.AddBeneficiary(objBeneficiary);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();
                Beneficiary obj = objBeneficiaryDal.GetBeneficiaryByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return Content(status);
        }
        public ActionResult EditBeneficiary(Beneficiary objBeneficiary)
        {
            string status = "";
            try
            {
                BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();
                Beneficiary obj = objBeneficiaryDAL.GetBeneficiaryByUID(objBeneficiary.UID);

                obj.FullName = objBeneficiary.FullName;
                obj.Customer_Id = objBeneficiary.Customer_Id;
                obj.Country_Id = objBeneficiary.Country_Id;
                obj.Currency_Id = objBeneficiary.Currency_Id;
                obj.Address_Line1 = objBeneficiary.Address_Line1;
                obj.Address_Line2 = objBeneficiary.Address_Line2;
                obj.Address_Line3 = objBeneficiary.Address_Line3;
                obj.Remittance_Purpose = objBeneficiary.Remittance_Purpose;
                obj.Birth_Date = objBeneficiary.Birth_Date;
                obj.Remittance_Type_Id = objBeneficiary.Remittance_Type_Id;
                obj.Remittance_Instruction = objBeneficiary.Remittance_Instruction;
                obj.Phone_No = objBeneficiary.Phone_No;
                obj.Mobile_No = objBeneficiary.Mobile_No;
                obj.Fax_No = objBeneficiary.Fax_No;
                obj.Email_Address1 = objBeneficiary.Email_Address1;
                obj.Email_Address2 = objBeneficiary.Email_Address2;
                obj.Identification_Type = objBeneficiary.Identification_Type;
                obj.Identification_No = objBeneficiary.Identification_No;
                obj.Identification_Remarks = objBeneficiary.Identification_Remarks;
                obj.Identification_Issue_Date = objBeneficiary.Identification_Issue_Date;
                obj.Identification_Expiry_Date = objBeneficiary.Identification_Expiry_Date;
                obj.Bank_Id = objBeneficiary.Bank_Id;
                obj.Branch_Id = objBeneficiary.Branch_Id;
                obj.Bank_Account_No = objBeneficiary.Bank_Account_No;
                obj.Bank_Name = objBeneficiary.Bank_Name;
                obj.Branch_Name = objBeneficiary.Branch_Name;
                obj.Branch_City_Id = objBeneficiary.Branch_City_Id;
                obj.Branch_City_Name = objBeneficiary.Branch_City_Name;
                obj.Branch_Address_Line1 = objBeneficiary.Branch_Address_Line1;
                obj.Branch_Address_Line2 = objBeneficiary.Branch_Address_Line2;
                obj.Branch_Address_Line3 = objBeneficiary.Branch_Address_Line3;
                obj.Branch_Number = objBeneficiary.Branch_Number;
                obj.Branch_Phone_Number = objBeneficiary.Branch_Phone_Number;
                obj.Branch_Fax_Number = objBeneficiary.Branch_Fax_Number;
                obj.Destination_Country_Id = objBeneficiary.Destination_Country_Id;
                // obj.Remitter_Relation_Id = objBeneficiary.Remitter_Relation_Id;
                obj.DD_Beneficiary_Name = objBeneficiary.DD_Beneficiary_Name;
                obj.Bank_Account_type = objBeneficiary.Bank_Account_type;
                obj.Remittance_Remarks = objBeneficiary.Remittance_Remarks;
                obj.Bank_Code = objBeneficiary.Bank_Code;
                obj.Routing_Bank_Id = objBeneficiary.Routing_Bank_Id;
                obj.Routing_Bank_Branch_Id = objBeneficiary.Routing_Bank_Branch_Id;
                obj.Remittance_Subtype_Id = objBeneficiary.Remittance_Subtype_Id;
                obj.Birth_Place = objBeneficiary.Birth_Place;
                obj.TransFastInfo = objBeneficiary.TransFastInfo;

                if (obj.Prod_Beneficiary_Id == null || obj.Prod_Beneficiary_Id <= 0)
                {
                    Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                    obj.Prod_Beneficiary_Id = objKuaiex_Prod.GetBeneficiaryIdByIdentificationNumber(obj.Identification_No);
                }

                //if (objBeneficiary. != null)
                //{
                //    objBeneficiary.Status = 1;
                //}
                //else
                //{
                //    objBeneficiary.Status = 0;
                //}
                //obj.Status = objBeneficiary.Status;
                objBeneficiaryDAL.EditBeneficiary(obj);

                status = "success";

            }
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
        public ActionResult SynchronizeRecords()
        {
            int status = 0;
            int Counter = 0;
            try
            {
                BeneficiaryDAL objBeneficiaryDal = new BeneficiaryDAL();

                List<Beneficiary> lst = objBeneficiaryDal.GetAllBeneficiary();

                foreach (var item in lst)
                {
                    Beneficiary obj = objBeneficiaryDal.GetBeneficiaryByUID(item.UID);
                    if (obj.Prod_Beneficiary_Id == null || obj.Prod_Beneficiary_Id <= 0)
                    {
                        Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                        obj.Prod_Beneficiary_Id = objKuaiex_Prod.GetRemittanceIdByIdentificationNumber(obj.Identification_No);

                        if (obj.Prod_Beneficiary_Id > 0)
                        {
                            Counter++;
                            objBeneficiaryDal.EditBeneficiary(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = Counter;
            }
            return Content(status.ToString());
        }
    }
}