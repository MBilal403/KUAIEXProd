using BusinessLogicLayer.DomainEntities;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DTO.Customer;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.CountryServices;
using KuaiexDashboard.Services.CountryServices.Impl;
using KuaiexDashboard.Services.RemitterServices;
using KuaiexDashboard.Services.RemitterServices.Impl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class RemitterController : Controller
    {
        private readonly IRemitterService _remitterService;
        private readonly ICountryService _countryService;
        public RemitterController()
        {
            _remitterService = new RemitterService();
            _countryService = new CountryService();
        }
        // GET: Remittance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadIdentificationType()
        {
            string status = "0:{choose}";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<IdentificationTypeLookup> lstIdentification = objRemitterDal.GetAllIdentificationTypes();

                status = JsonConvert.SerializeObject(lstIdentification);
            }
            catch (Exception)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadResidencyType()
        {
            string status = "0:{choose}";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<Residency_Type> lstResidencyTypes = objRemitterDal.GetResidencyTypes();

                status = JsonConvert.SerializeObject(lstResidencyTypes);
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
                List<GetCountryList_Result> lstCountries = _countryService.GetCountryList();

                status = JsonConvert.SerializeObject(lstCountries);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadCity()
        {
            string status = "0:{choose}";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<City> lstCities = objRemitterDal.GetActiveCities();

                status = JsonConvert.SerializeObject(lstCities);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadQuestions()
        {
            string status = "0:{choose}";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<SecurityQuestions> lstSecurityQuestions = objRemitterDal.GetAllSecurityQuestions();

                status = JsonConvert.SerializeObject(lstSecurityQuestions);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadExpectTrancationsCount()
        {
            string status = "0:{choose}";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<Transaction_Count_Lookup> lstTransactionCounts = objRemitterDal.GetAllTransactionCountLookup();

                status = JsonConvert.SerializeObject(lstTransactionCounts);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid(JqueryDatatableParam param)
        {
            PagedResult<GetRemitterList_Result> list = _remitterService.GetRemitterList(param);

            var result = new
            {
                draw = param.sEcho,
                recordsTotal = list.TotalSize,
                recordsFiltered = list.FilterRecored,
                data = list.Data
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadSecurityQuestions(int Customer_Id)
        {
            string status = "error";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                //  List<Customer_Security_Questions> SecurityQuestions = objRemitterDal.GetCustomerSecurityQuestions(Customer_Id);
                List<Customer_Security_Questions> SecurityQuestions = _remitterService.GetCustomerSecurityQuestions(Customer_Id);

                status = JsonConvert.SerializeObject(SecurityQuestions);
            }
            catch (Exception ex)
            {
                status = "Error";
            }

            return Content(status);
        }

        public ActionResult LoadKYCIndividuals(int Customer_Id)
        {
            string status = "error";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                //  List<Customer_Security_Questions> SecurityQuestions = objRemitterDal.GetCustomerSecurityQuestions(Customer_Id);
                List<Individual_KYC> individual_KYCs = _remitterService.GetCustomerLoadKYCIndividuals(Customer_Id);

                status = JsonConvert.SerializeObject(individual_KYCs);
            }
            catch (Exception)
            {
                status = "Error";
            }

            return Content(status);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(string Civil_Id_Back, string Civil_Id_Front, CustomerDTO addCustomerDto)
        {
            string status = "error";
            try
            {
                addCustomerDto.Civil_Id_Front = Civil_Id_Front == "null" ? null : Civil_Id_Front;
                addCustomerDto.Civil_Id_Back = Civil_Id_Back == "null" ? null : Civil_Id_Back;
                status = _remitterService.CreateRemitter(addCustomerDto);
            }
            catch (Exception)
            {
                status = "error";
            }
            return Content(status);
        }
        [HttpPost]
        public ActionResult AddCustomerFiles(string CivilId)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file1 = Request.Files["Civil_Id_Front"];
                    HttpPostedFileBase file2 = Request.Files["Civil_Id_Back"];
                    string uniqueFileName1 = default, uniqueFileName2 = default;

                    if (file1 != null || file2 != null)
                    {
                        string uploadDirectory = ConfigurationManager.AppSettings["LocalStoragePath"].ToString();

                        if (!Directory.Exists(uploadDirectory))
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }
                        if (file1 != null)
                        {
                            string fileExtension1 = Path.GetExtension(file1.FileName);
                            uniqueFileName1 = $"{CivilId}F{fileExtension1}";
                            string filePath1 = Path.Combine(uploadDirectory, uniqueFileName1);

                            if (System.IO.File.Exists(filePath1))
                            {
                                System.IO.File.Delete(filePath1);
                            }
                            file1.SaveAs(filePath1);
                        }

                        if (file2 != null)
                        {
                            string fileExtension2 = Path.GetExtension(file2.FileName);
                            uniqueFileName2 = $"{CivilId}R{fileExtension2}";
                            string filePath2 = Path.Combine(uploadDirectory, uniqueFileName2);
                            if (System.IO.File.Exists(filePath2))
                            {
                                System.IO.File.Delete(filePath2);
                            }
                            file2.SaveAs(filePath2);
                        }
                        return Json(new
                        {
                            success = true,
                            message = "Files uploaded successfully.",
                            Civil_Id_Front = uniqueFileName1,
                            Civil_Id_Back = uniqueFileName2,
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Exception :" + ex.Message });
            }
            return Json(new { success = false, message = "No files were selected." });
        }
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                EditCustomerDTO obj = _remitterService.GetCustomerByUID(UID);

                if (obj != null)
                {
                    status = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    status = "Customer not found";
                }
            }
            catch (Exception)
            {
                status = "Error";
            }
            return Content(status);
        }
        public ActionResult UnBlockCustomer(Customer objcustomer)
        {
            string status = "";
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                Customer obj = objRemitterDal.GetCustomerByUID(objcustomer.UID);

                if (obj != null)
                {
                    obj.IsBlocked = objcustomer.IsBlocked;
                    obj.InvalidTryCount = objcustomer.InvalidTryCount;

                    objRemitterDal.UnblockCustomer(obj.UID);

                    status = "success";
                }
                else
                {
                    status = "Customer not found";
                }
            }
            catch (Exception)
            {
                status = "error";
            }
            return Content(status);
        }
        public ActionResult EditCustomer(string Civil_Id_Back, string Civil_Id_Front, CustomerDTO editCustomerDto)
        {
            string status = "";
            try
            {
                editCustomerDto.Civil_Id_Front = Civil_Id_Front == "null" ? null : Civil_Id_Front;
                editCustomerDto.Civil_Id_Back = Civil_Id_Back == "null" ? null : Civil_Id_Back;
                status = _remitterService.EditRemitter(editCustomerDto);

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
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<Customer> lst = objRemitterDal.GetAllCustomers();

                foreach (var item in lst)
                {
                    Customer obj = objRemitterDal.GetCustomerByUID(item.UID);

                    if (obj.Prod_Remitter_Id == null || obj.Prod_Remitter_Id <= 0)
                    {
                        Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                        obj.Prod_Remitter_Id = objKuaiex_Prod.GetRemittanceIdByIdentificationNumber(obj.Identification_Number);

                        if (obj.Prod_Remitter_Id > 0)
                        {
                            Counter++;
                            objRemitterDal.UpdateRemitter(obj);
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