using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.CountryServices.Impl;
using KuaiexDashboard.Services.CountryServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KuaiexDashboard.Services.CountryCurrencyServices;
using KuaiexDashboard.Services.CountryCurrencyServices.Impl;
using KuaiexDashboard.Services.CurrencyServices;
using KuaiexDashboard.Services.CurrencyServices.Impl;
using Serilog;
using System.Web.Http.Results;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class CountryCurrencyController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICountryCurrencyService _countryCurrencyService;
        private readonly ICurrencyService _currencyService;

        public CountryCurrencyController()
        {
            _countryService = new CountryService();
            _countryCurrencyService = new CountryCurrencyService();
            _currencyService = new CurrencyService();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCountryCurrency(CountryCurrency objCountryCurrency)
        {
            string status = "error";
            try
            {
                status = _countryCurrencyService.AddCountryCurrency(objCountryCurrency);
            }
            catch (Exception ex)
            {

                status = "error";
            }
            return Content(status);
        }

        public ActionResult LoadCountry()
        {
            string status = "0:{choose}";
            try
            {
                List<Country> lstCountry = _countryService.GetAllCountries();
                status = JsonConvert.SerializeObject(lstCountry);
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

                List<Currency> lstcurrency = _currencyService.getAllCurrencies();
                status = JsonConvert.SerializeObject(lstcurrency);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }

        public ActionResult LoadGrid(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<GetCountryCurrencyList_Result> list = _countryCurrencyService.GetAllCountryCurrency(param);

                var result = new
                {
                    draw = param.sEcho,
                    recordsTotal = list.TotalSize,
                    recordsFiltered = list.FilterRecored,
                    data = list.Data
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
            }
            return Json("erroe", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                CountryCurrency countryCurrency = _countryCurrencyService.GetCountryCurrency(UID);
                status = JsonConvert.SerializeObject(countryCurrency);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "Error";
            }
            return Content(status);
        }

        public ActionResult EditCountryCurrency(CountryCurrency countryCurrency)
        {
            string status = "";
            try
            {
                status = _countryCurrencyService.UpdateCountryCurrency(countryCurrency);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult SynchronizeRecords()
        {
            int status = 0;
            try
            {
                status = _countryCurrencyService.SynchronizeRecords();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
            }
            return Content(status.ToString());
        }

    }
}
