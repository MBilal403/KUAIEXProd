
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Filters;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using KuaiexDashboard.Services.CurrencyServices;
using KuaiexDashboard.Services.CurrencyServices.Impl;
using DataAccessLayer.Recources;
using Serilog;
using System.Collections.Generic;
using DataAccessLayer.ProcedureResults;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class CurrencyController : Controller
    {

        private readonly ICurrencyService _currencyService;

        public CurrencyController()
        {

            _currencyService = new CurrencyService();
        }

        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCurrency(Currency objCurrency)
        {
            string status = "error";
            try
            {
                status = _currencyService.AddCurrency(objCurrency);
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
                PagedResult<Currency> list = _currencyService.GetAllCurrency(param);

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
                return Json(MsgKeys.Error, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadCurrencyLimits(Guid UID)
        {
            string status = "error";
            Currency currency = _currencyService.GetCurrency(UID);
            var list = _currencyService.GetAllCurrencylimitByCurrencyId(currency.Id);
            status = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            return Content(status);
        }
        public ActionResult LoadComparisons()
        {
            string status = "error";
            List<Comparison_Lookup> list = _currencyService.GelAllComprison();
            status = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            return Content(status);
        }

        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                Currency currency = _currencyService.GetCurrency(UID);
                status = JsonConvert.SerializeObject(currency);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult EditCurrency(Currency currency)
        {
            string status = "";
            try
            {
                status = _currencyService.UpdateCurrency(currency);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult Limits(Guid UID)
        {
            Currency currency = _currencyService.GetCurrency(UID);
            ViewBag.CurrencyInfo = $" {currency.Name} - {currency.Code} ";
            return View();

        }
        public ActionResult Delete(Guid UID)
        {
            string status = "";
            try
            {
                status = _currencyService.DeleteCurrencylimit(UID);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult AddLimits(Remittance_Currency_Limit remittance_Currency_Limit, Guid UID)
        {
            string status = "";
            try
            {
                Currency currency = _currencyService.GetCurrency(UID);
                remittance_Currency_Limit.Currency_Id = currency.Id;
                status = _currencyService.AddCurrencylimit(remittance_Currency_Limit);
                return Content(status);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult GetAllCurrencylimit(int id)
        {
            string status = "error";
            try
            {
                var list = _currencyService.GetAllCurrencylimitByCurrencyId(id);
                status = Newtonsoft.Json.JsonConvert.SerializeObject(list);
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
                status = _currencyService.SynchronizeRecords();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
            }
            return Content(status.ToString());
        }

    }
}