
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

    }
}