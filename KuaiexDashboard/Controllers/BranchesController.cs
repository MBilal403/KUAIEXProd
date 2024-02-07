using DataAccessLayer;
using DataAccessLayer.Entities;
using KuaiexDashboard.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace KuaiexDashboard.Controllers
{
    public class BranchesController : Controller
    {

        // GET: Branches
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadCountry()
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
        public ActionResult LoadCurrency()
        {
            string status = "0:{choose}";
            try
            {
                BankMstDAL currencyDAL = new BankMstDAL();
                List<Currency> lstCurrency = currencyDAL.GetCurrency();

                if (lstCurrency != null)
                {
                    status = JsonConvert.SerializeObject(lstCurrency);
                }
                else
                {
                    status = "error";
                }
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadBanks(int countryId)
        {
            string status = "0:{choose}";

            try
            {
                BankMstDAL objBankDal = new BankMstDAL();
                List<GetBanksListByCountry_Result> banks = objBankDal.GetBanksListByCountry(countryId);

                status = JsonConvert.SerializeObject(banks);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadCity()
        {
            string status = "0:{choose}";

            try
            {
                BranchesDAL cityDAL = new BranchesDAL();
                List<GetCityList_Result> lstCity = cityDAL.GetCityList();

                status = JsonConvert.SerializeObject(lstCity);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid(int Country, int Bank)
        {
            string status = "error";
            try
            {
                BranchesDAL objBranchDAL = new BranchesDAL();
                var result = objBranchDAL.GetBranchesListbyCountryBank(Country, Bank);
                status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid1()
        {
            string status = "error";
            try
            {
                BranchesDAL objBranchDAL = new BranchesDAL();
                var result = objBranchDAL.GetBranchesList();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult AddBank(Bank_Mst objBank)
        {
            string status = "error";

            try
            {
                BranchesDAL objBranchDal = new BranchesDAL();
                Bank_Mst existingBank = objBranchDal.GetBankByName(objBank.English_Name);
                if (existingBank != null)
                {
                    status = "exist";
                }
                else
                {
                    objBank.Record_Status = objBank.Record_Status != null ? "Active" : "In Active";
                    objBank.UID = Guid.NewGuid();
                    if (objBranchDal.AddBank(objBank))
                    {
                        status = "success";
                    }
                }
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
                BranchesDAL objBranchDal = new BranchesDAL();
                Bank_Mst obj = objBranchDal.GetBankByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return Content(status);
        }
        public ActionResult EditBank(Bank_Mst objBank)
        {
            string status = "";
            try
            {
                BranchesDAL objBranchDal = new BranchesDAL();
                Bank_Mst obj = objBranchDal.GetBankByUID(objBank.UID);

                obj.English_Name = objBank.English_Name;

                obj.Arabic_Name = objBank.Arabic_Name;
                obj.Short_English_Name = objBank.Short_English_Name;
                obj.Short_Arabic_Name = objBank.Short_Arabic_Name;

                obj.Country_Id = objBank.Country_Id;
                obj.Currency_Id = objBank.Currency_Id;
                obj.Upper_Limit = objBank.Upper_Limit;

                obj.Address_Line1 = objBank.Address_Line1;
                obj.Address_Line2 = objBank.Address_Line2;
                obj.Address_Line3 = objBank.Address_Line3;

                if (objBank.Record_Status != null)
                {
                    objBank.Record_Status = "Active";
                }
                else
                {
                    objBank.Record_Status = "In_Active";
                }
                obj.Record_Status = objBank.Record_Status;
                objBranchDal.UpdateBank(objBank);
                status = "Success";
            }
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
    }
}