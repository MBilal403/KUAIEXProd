using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DAL;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.BankServices;
using KuaiexDashboard.Services.BankServices.Impl;
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
        private readonly ICountryService _countryService;
        private readonly IBankService _bankService;
        public BanksController()
        {
            _countryService = new CountryService();
            _bankService = new BankService();
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
        
        [HttpGet]
        public ActionResult LoadGrid(JqueryDatatableParam param, int countryId)
        {
            try
            {
                PagedResult<GetBanks_Result> list = _bankService.GetAllWithPagination(param, countryId);

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
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {

              
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        } 
        public ActionResult Detail(Guid UID)
        {
            string status = "error";
            try
            {
                GetBankDetailsById_Result bank_Mst = _bankService.GetByUID(UID);
                status = JsonConvert.SerializeObject(bank_Mst);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult GetLimitDetail(Guid UID)
        {
            string status = "error";
            try
            {
                Bank_Mst bank_Mst = _bankService.GetLimitDetailByUId(UID);
                status = JsonConvert.SerializeObject(bank_Mst);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult UpdateLimits(Guid? UID,decimal AmountLimit, int NumberOfTransaction)
        {
            string status = "error";
            try
            {
              status = _bankService.UpdateLimits(UID, AmountLimit, NumberOfTransaction);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }

        public ActionResult ChangeState(Guid UID)
        {
            string status = "error";
            try
            {
                status = _bankService.ChangeState(UID);
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
                status = _bankService.SynchronizeRecords();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
            }
            return Content(status.ToString());
        }

    }
}