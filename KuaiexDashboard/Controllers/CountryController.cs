using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.CityServices;
using KuaiexDashboard.Services.CityServices.Impl;
using KuaiexDashboard.Services.CountryServices;
using KuaiexDashboard.Services.CountryServices.Impl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;
using Serilog.Core;
using DataAccessLayer.Recources;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class CountryController : Controller
    {
        CountryDAL objCountryDal = new CountryDAL();
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public CountryController()
        {
            _countryService = new CountryService();
            _cityService = new CityService();
        }

        // GET: Country
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCountry(Country objCountry)
        {
            string status = "error";
            try
            {
                if (ModelState.IsValid)
                {
                    status = _countryService.AddCountry(objCountry);
                }
                else
                {
                    throw new InvalidOperationException(MsgKeys.InvalidInputParameters);
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }

        public ActionResult LoadCity()
        {
            string status = "0:{choose}";
            try
            {
                List<City> lstCity = _cityService.GetActiveCities();
                status = JsonConvert.SerializeObject(lstCity);
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
                PagedResult<GetCountryList_Result> list = _countryService.GetCountryList(param);

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
                return Json("error");
            }

        }

        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                Country obj = _countryService.GetCountryByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "Error";
            }
            return Content(status);
        }

        public ActionResult EditCountry(Country objCountry)
        {
            string status = "";
            try
            {
                if (ModelState.IsValid)
                {
                    status = _countryService.UpdateCountry(objCountry);
                }
                else
                {
                    throw new InvalidOperationException(MsgKeys.InvalidInputParameters);
                }
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
            int Counter = 0;
            try
            {
                List<Country> lst = objCountryDal.GetAllCountries();
                foreach (var item in lst)
                {
                    Country obj = objCountryDal.GetCountryByUID(item.UID);
                    if (obj.Prod_Country_Id <= 0)
                    {
                        Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                        obj.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(obj.Name);

                        if (obj.Prod_Country_Id > 0)
                        {
                            objCountryDal.UpdateCountry_ProdCountryId(obj.Prod_Country_Id, item.Id);
                            Counter++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = Counter;
            }
            return Content(status.ToString());
        }
    }
}