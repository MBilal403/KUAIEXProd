using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DAL;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.BankServices;
using KuaiexDashboard.Services.BankServices.Impl;
using KuaiexDashboard.Services.BranchServices;
using KuaiexDashboard.Services.BranchServices.Impl;
using KuaiexDashboard.Services.CityServices;
using KuaiexDashboard.Services.CityServices.Impl;
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
    public class BranchesController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IBankService _bankService;
        private readonly ICountryService _countryService;
        public BranchesController()
        {
            _branchService = new BranchService();
            _bankService = new BankService();
            _countryService = new CountryService();
        }

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
        public ActionResult LoadBanks(int countryId)
        {
            string status = "0:{choose}";

            try
            {
                List<Bank_Mst> banks = _bankService.GetBanksByCountryId(countryId);
                status = JsonConvert.SerializeObject(banks);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }


            return Content(status);
        }

        [HttpGet]
        public ActionResult LoadGrid(JqueryDatatableParam param, int countryId, int bankId)
        {
            try
            {
                PagedResult<GetBankBranchesList_Result> list = _branchService.GetAllBankBranches(param, countryId, bankId);
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

        public ActionResult SynchronizeRecords()
        {
            int status = 0;
            try
            {
                status = _branchService.SynchronizeRecords();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
            }
            return Content(status.ToString());
        }

    }
}