using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Repository.Impl;
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
using System.Web.Http.Results;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        public CityController()
        {
            _cityService = new CityService();
            _countryService = new CountryService();
        }

        CityDAL objCityDal = new CityDAL();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadCountry()
        {
            string status = "0:{choose}";
            try
            {
                List<GetCountryList_Result> lstCityCountry = _countryService.GetActiveCountryList();
                status = JsonConvert.SerializeObject(lstCityCountry);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult AddCity(City objCity)
        {
            string status = "error";
            try
            {
                if (ModelState.IsValid)
                {
                    status = _cityService.AddCity(objCity);
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

        [HttpGet]
        public ActionResult LoadGrid(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<GetCityList_Result> list = _cityService.GetActiveCities(param);

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
                City obj = _cityService.GetCityByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult EditCity(City objcity)
        {
            string status = "";
            try
            {
                if (ModelState.IsValid)
                {
                    status = _cityService.UpdateCity(objcity);
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

        /*    public ActionResult SynchronizeRecords()
            {
                int status = 0;
                int Counter = 0;
                try
                {
                    List<City> lst = objCityDal.GetAllCities();

                    foreach (var item in lst)
                    {
                        City obj = objCityDal.GetCityByUID(item.UID);
                        if (obj.Prod_City_Id == null || obj.Prod_City_Id <= 0)
                        {
                            Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                            obj.Prod_City_Id = objKuaiex_Prod.GetCityIdByCityName(obj.Name);

                            if (obj.Prod_City_Id > 0)
                            {
                                objCityDal.UpdateCity_ProdCityId(obj.Prod_City_Id, item.Id);
                                Counter++;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    status = Counter;
                }
                return Content(status.ToString());
            }*/

    }
}