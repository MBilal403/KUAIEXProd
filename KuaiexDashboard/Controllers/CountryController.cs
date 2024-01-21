using DataAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    public class CountryController : Controller
    {
        CountryDAL objCountryDal = new CountryDAL();
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
                Country existingCountry = objCountryDal.GetCountryByName(objCountry.Name);

                if (existingCountry != null)
                {
                    status = "exist";
                }
                else
                {
                    if (objCountry.Status != null)
                        objCountry.Status = "A";
                    else
                        objCountry.Status = "N";
                    objCountry.Remittance_Status = "Y";
                    objCountry.High_Risk_Status = "A";
                    objCountry.CreatedIp = "127.0.0.1";
                    objCountry.UpdatedIp = "127.0.0.1";
                    objCountry.UID = Guid.NewGuid();
                    Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                    objCountry.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(objCountry.Name);
                    
                    objCountryDal.AddCountry(objCountry);

                    status = "success";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = "error";
            }

            return Content(status);
        }

        public ActionResult LoadCity()
        {
            string status = "0:{choose}";
            try
            {
                List<City> lstCity = objCountryDal.GetActiveCities();
                status = JsonConvert.SerializeObject(lstCity);
            }
            catch (Exception ex)
            {

                status = "error";
            }
            return Content(status);
        }

        public ActionResult LoadGrid()
        {
            string status = "error";
            //if (IsAdminUser)
            //{
            List<GetCountryList_Result> list = objCountryDal.GetCountryList();
            status = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            //}
            return Content(status);
        }

        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                Country obj = objCountryDal.GetCountryByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return Content(status);
        }

        public ActionResult EditCountry(Country objCountry)
        {
            string status = "";
            try
            {
                Country obj = objCountryDal.GetCountryByUID(objCountry.UID);
                objCountry.Id = obj.Id;
                objCountry.Name = obj.Name;
                obj.Comission = objCountry.Comission;
                obj.Nationality = objCountry.Nationality;
                obj.Alpha_2_Code = objCountry.Alpha_2_Code;
                obj.Alpha_3_Code = objCountry.Alpha_3_Code;
                objCountry.Remittance_Status = obj.Remittance_Status;
                objCountry.High_Risk_Status = obj.High_Risk_Status;
                objCountry.CreatedIp = obj.CreatedIp;
                objCountry.UpdatedIp = obj.UpdatedIp;
                obj.Country_Dialing_Code = objCountry.Country_Dialing_Code;
                if (obj.Prod_Country_Id == null || obj.Prod_Country_Id <= 0)
                {
                    Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                    obj.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(obj.Name);
                }

                if (objCountry.Status != null)
                {
                    objCountry.Status = "A";
                }
                else
                {
                    objCountry.Status = "N";
                }
                obj.Status = objCountry.Status;

                // Save the changes to the database
                objCountryDal.UpdateCountry(obj);

                status = "success";
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
                status = Counter;
            }
            return Content(status.ToString());
        }
    }
}