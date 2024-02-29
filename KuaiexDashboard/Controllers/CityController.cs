using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Repository.Impl;
using KuaiexDashboard.Services.CityServices;
using KuaiexDashboard.Services.CityServices.Impl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController()
        {
            _cityService = new CityService();
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
                //var result = db.GetUsersList();
                List<GetCountryList_Result> lstCityCountry = objCityDal.GetActiveCountryList();
                status = JsonConvert.SerializeObject(lstCityCountry);
            }
            catch (Exception ex)
            {

                status = "error";
            }
            return Content(status);
        }
        public ActionResult AddCity(City objCity)
        {
            string status = "error";
            try
            {
                status = _cityService.AddCity(objCity);
            }
            catch (Exception ex)
            {
                status = "error: " + ex.Message;
            }

            return Content(status);
        }

        [HttpGet]
        public ActionResult LoadGrid(JqueryDatatableParam param)
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
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                City obj = objCityDal.GetCityByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                status = "Error";
            }
            return Content(status);
        }
        public ActionResult EditCity(City objcity)
        {
            string status = "";
            try
            {
                status = _cityService.UpdateCity(objcity);
            }
            catch (Exception ex)
            {
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
        }

    }
}