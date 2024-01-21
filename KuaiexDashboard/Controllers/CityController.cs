using DataAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    public class CityController : Controller
    {
        CityDAL objCityDal = new CityDAL();
        // GET: City
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
                if (objCity != null)
                {
                    City obj = objCityDal.GetCityByName(objCity.Name);
                    objCity.Country_Id = objCity.Country_Id;

                    if (obj != null)
                    {
                        status = "exist";
                    }
                    else
                    {
                        if (objCity.Status != null)
                            objCity.Status = 1;
                        else
                            objCity.Status = 0;

                        objCity.UID = Guid.NewGuid();
                        Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                        objCity.Prod_City_Id = objKuaiex_Prod.GetCityIdByCityName(objCity.Name);

                        objCityDal.AddCity(objCity);
                        status = "success";
                    }
                }
                else
                {
                    status = "error: objCity is null";
                }
            }
            catch (Exception ex)
            {
                status = "error: " + ex.Message;
            }

            return Content(status);
        }


        public ActionResult LoadGrid()
        {
            string status = "error";
            //if (IsAdminUser)
            //{
            List<GetCityList_Result> list = objCityDal.GetActiveCities();
            status = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            //}
            return Content(status);
        }
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                City obj = objCityDal.GetCityByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
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
                City obj = objCityDal.GetCityByUID(objcity.UID);
                obj.Name = objcity.Name;

                if (objcity.Prod_City_Id == null || objcity.Prod_City_Id <= 0)
                {
                    Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                    objcity.Prod_City_Id = objKuaiex_Prod.GetCityIdByCityName(obj.Name);
                }

                if (objcity.Status != null)
                {
                    objcity.Status = 1;
                }
                else
                {
                    objcity.Status = 0;
                }
                obj.Status = objcity.Status;
                objCityDal.EditCity(objcity);
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