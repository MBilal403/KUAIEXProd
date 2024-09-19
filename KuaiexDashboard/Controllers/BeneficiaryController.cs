using DataAccessLayer.DomainEntities;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using KuaiexDashboard.DTO.Beneficiary;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.BeneficiaryServices;
using KuaiexDashboard.Services.BeneficiaryServices.Impl;
using KuaiexDashboard.Services.CountryCurrencyServices;
using KuaiexDashboard.Services.CountryCurrencyServices.Impl;
using KuaiexDashboard.Services.CountryServices;
using KuaiexDashboard.Services.CountryServices.Impl;
using KuaiexDashboard.Services.RemitterServices;
using KuaiexDashboard.Services.RemitterServices.Impl;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class BeneficiaryController : Controller
    {
        IBeneficiaryService _beneficiaryService;
        IRemitterService _remitterService;
        ICountryService _countryService;
        ICountryCurrencyService _countryCurrencyService;
        BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();

        public BeneficiaryController()
        {
            _beneficiaryService = new BeneficiaryService();
            _remitterService = new RemitterService();
            _countryService = new CountryService();
            _countryCurrencyService = new CountryCurrencyService();
        }
        public ActionResult Index(Guid UID)
        {

            var user = _remitterService.GetCustomerByUID(UID);
            if (user != null)
            {
                ViewBag.UserName = $" {user.Identification_Number} - {user.Name} ";
                return View();
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Authentication");
            }
        }

        public ActionResult LoadCountry()
        {
            string status = "0:{choose}";
            try
            {
                List<GetCountryList_Result> lstCountries = _countryService.GetCountryList();
                status = JsonConvert.SerializeObject(lstCountries);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }


            return Content(status);
        }
        public ActionResult LoadCurrency(int CountryId)
        {
            string status = "0:{choose}";

            try
            {
                List<Currency_Result> lstcurrency = _countryCurrencyService.GetCurrencyByCountry(CountryId);

                status = JsonConvert.SerializeObject(lstcurrency);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }


            return Content(status);
        }
        public ActionResult LoadNationality()
        {
            string status = "0:{choose}";

            try
            {
                List<GetCountryList_Result> lstCountries = _countryService.GetCountryList();

                status = JsonConvert.SerializeObject(lstCountries);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                status = JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }


            return Content(status);
        }
        public ActionResult LoadGrid(Guid CUID)
        {
            string status = "error";
            try
            {
                var customer = _remitterService.GetCustomerByUID(CUID);
                var lstbeneficiary = _beneficiaryService.GetBeneficiariesByCustomerID(customer.Customer_Id);
                status = Newtonsoft.Json.JsonConvert.SerializeObject(lstbeneficiary);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
        public ActionResult Add(Guid UID)
        {
            var user = _remitterService.GetCustomerByUID(UID);
            if (user != null)
            {
                ViewBag.UserName = $" {user.Identification_Number} -  {user.Name} ";
                return View();
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Authentication");
            }
        }
        public ActionResult EditBene(Guid UID, Guid CUID)
        {
            var user = _remitterService.GetCustomerByUID(CUID);
            if (user != null)
            {
                ViewBag.UserName = $" {user.Identification_Number} -  {user.Name} ";
                return View("Add");
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Authentication");
            }
        }

        public ActionResult AddBeneficiary(BeneficiaryDTO objBeneficiary)
        {
            string status = "error";

            try
            {
                var customer = _remitterService.GetCustomerByUID(objBeneficiary.UID);
                if (customer != null)
                {
                    objBeneficiary.Customer_Id = customer.Customer_Id;
                    status = _beneficiaryService.AddBeneficiary(objBeneficiary);
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                BeneficiaryDTO beneficiaryDto = _beneficiaryService.GetBeneficiaryByUID(UID);
                status = JsonConvert.SerializeObject(beneficiaryDto);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
        public ActionResult EditBeneficiary(BeneficiaryDTO objBeneficiary)
        {
            string status = "";
            try
            {

                status = _beneficiaryService.Updatebeneficiary(objBeneficiary);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
       /* public ActionResult SynchronizeRecords()
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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
            }

            return Content(status.ToString());
        }*/
    }
}