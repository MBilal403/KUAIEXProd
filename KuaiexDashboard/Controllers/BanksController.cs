using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using KuaiexDashboard.DAL;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.CountryServices;
using KuaiexDashboard.Services.CountryServices.Impl;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class BanksController : Controller
    {
        BankMstDAL objBankDal = new BankMstDAL();
        private readonly ICountryService _countryService;
        public BanksController()
        {
            _countryService = new CountryService();
        }

        // GET: Banks
        public ActionResult Index()
        {
            return View();
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
        public ActionResult LoadCurrency()
        {
            string status = "0:{choose}";
            try
            {

                List<Currency> lstCurrency = objBankDal.GetCurrency();

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
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid(int countryId)
        {
            string status = "0:{choose}";

            try
            {

                List<GetBanksListByCountry_Result> banks = objBankDal.GetBanksListByCountry(countryId);

                status = JsonConvert.SerializeObject(banks);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid1()
        {
            string status = "0:{choose}";

            try
            {

                List<GetBanksList_Result> bankslist = objBankDal.GetBanksList();

                status = JsonConvert.SerializeObject(bankslist);
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

                Bank_Mst obj = objBankDal.GetBankByUID(UID);

                if (obj != null)
                {
                    status = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    status = "Bank not found";
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult EditBank(Bank_Mst objBank)
        {
            string status = "error";
            try
            {
                status = objBankDal.UpdateBank(objBank);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }

    }
}