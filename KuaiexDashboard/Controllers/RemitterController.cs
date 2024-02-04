﻿using BusinessLogicLayer.DomainEntities;
using DataAccessLayer;
using DataAccessLayer.Entities;
using KuaiexDashboard.DTO.Customer;
using KuaiexDashboard.Services.RemitterServices;
using KuaiexDashboard.Services.RemitterServices.Impl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    public class RemitterController : Controller
    {
        IRemitterService _remitterService;
        public RemitterController()
        {
            _remitterService = new RemitterService();
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
            catch (Exception ex)
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
                RemitterDAL objRemitterDAL = new RemitterDAL();

                List<GetCountryList_Result> lstCountries = objRemitterDAL.GetCountryList();

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
        public ActionResult LoadGrid()
        {
            string status = "error";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<Customer> result = objRemitterDal.GetRemitterList();

                status = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadSecurityQuestions(int Customer_Id)
        {
            string status = "error";

            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                List<Customer_Security_Questions> SecurityQuestions = objRemitterDal.GetCustomerSecurityQuestions(Customer_Id);

                status = JsonConvert.SerializeObject(SecurityQuestions);
            }
            catch (Exception ex)
            {
                status = "Error";
            }

            return Content(status);
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerDTO addCustomerDto)
        {
            string status = "error";
            try
            {
                status = _remitterService.CreateRemitter(addCustomerDto);
            }
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();

                Customer obj = objRemitterDal.GetCustomerByUID(UID);

                if (obj != null)
                {
                    status = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    status = "Customer not found";
                }
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
        public ActionResult EditCustomer(Customer objcustomer)
        {
            string status = "";
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                Customer obj = objRemitterDal.GetCustomerByUID(objcustomer.UID);

                obj.Name = objcustomer.Name;
                obj.Identification_Type = objcustomer.Identification_Type;
                obj.Identification_Number = objcustomer.Identification_Number;
                obj.Nationality = objcustomer.Nationality;
                obj.Date_Of_Birth = objcustomer.Date_Of_Birth;
                obj.Identification_Expiry_Date = objcustomer.Identification_Expiry_Date;
                obj.Occupation = objcustomer.Occupation;

                obj.IsBlocked = objcustomer.IsBlocked;
                obj.InvalidTryCount = objcustomer.InvalidTryCount;

                obj.Email_Address = objcustomer.Email_Address;
                obj.Mobile_No = objcustomer.Mobile_No;
                obj.Area = objcustomer.Area;
                obj.Block = objcustomer.Block;
                obj.Street = objcustomer.Street;
                obj.Building = objcustomer.Building;
                obj.Floor = objcustomer.Floor;
                obj.Flat = objcustomer.Flat;
                obj.Identification_Additional_Detail = objcustomer.Identification_Additional_Detail;
                obj.Residence_Type = objcustomer.Residence_Type;
                obj.Telephone_No = objcustomer.Telephone_No;
                obj.Birth_Place = objcustomer.Birth_Place;
                obj.Birth_Country = objcustomer.Birth_Country;
                obj.Expected_Monthly_Trans_Count = objcustomer.Expected_Monthly_Trans_Count;
                obj.Other_Income = objcustomer.Other_Income;
                obj.Login_Id = objcustomer.Login_Id;
                obj.Password = objcustomer.Password;
                obj.Pep_Description = objcustomer.Pep_Description;
                obj.Monthly_Income = objcustomer.Monthly_Income;
                obj.Monthly_Trans_Limit = objcustomer.Monthly_Trans_Limit;
                obj.Yearly_Trans_Limit = objcustomer.Yearly_Trans_Limit;
                obj.Compliance_Limit = objcustomer.Compliance_Limit;
                obj.Compliance_Trans_Count = objcustomer.Compliance_Trans_Count;
                obj.Compliance_Comments = objcustomer.Compliance_Comments;
                obj.Compliance_Limit_Expiry = objcustomer.Compliance_Limit_Expiry;

                if (obj.Prod_Remitter_Id == null || obj.Prod_Remitter_Id <= 0)
                {
                    Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                    obj.Prod_Remitter_Id = objKuaiex_Prod.GetRemittanceIdByIdentificationNumber(obj.Identification_Number);
                }

                if (objcustomer.IsReviwed != null)
                {
                    objcustomer.IsReviwed = true;
                }
                else
                {
                    objcustomer.IsReviwed = false;
                }
                obj.IsReviwed = objcustomer.IsReviwed;

                if (objcustomer.IsVerified != null)
                    objcustomer.IsVerified = 1;
                else
                    objcustomer.IsVerified = 0;
                obj.IsVerified = objcustomer.IsVerified;

                objRemitterDal.UpdateRemitter(obj);

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