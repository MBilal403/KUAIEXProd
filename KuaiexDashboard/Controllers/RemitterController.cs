using BusinessLogicLayer.DomainEntities;
using DataAccessLayer;
using DataAccessLayer.Services.RemitterService;
using DataAccessLayer.Services.RemitterServices.Impl;
using KuaiexDashboard.DTO.Customer;
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
        public ActionResult AddCustomer(AddCustomerDTO addCustomerDto)
        {
            string status = "error";
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                Customer customer = new Customer();
                List<Customer_Security_Questions> securityQuestions;
                List<Individual_KYC> individual_KYC;

                
                Customer existingCustomer = objRemitterDal.GetCustomerByIdentificationAndName(addCustomerDto.Identification_Number, addCustomerDto.Name);

                if (existingCustomer != null)
                {
                    status = "exist";
                }
                else
                {
                    Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();

                    customer.Agency_Id = 1;
                    customer.Agency_Branch_Id = 2;
                    customer.Name = addCustomerDto.Name;
                    customer.Gender = 0; // Mand
                    customer.Identification_Type = addCustomerDto.Identification_Type;
                    customer.Identification_Number = addCustomerDto.Identification_Number;
                    customer.Identification_Expiry_Date = addCustomerDto.Identification_Expiry_Date;
                    customer.Date_Of_Birth = addCustomerDto.Date_Of_Birth;
                    customer.Occupation = addCustomerDto.Occupation;
                    customer.Nationality = addCustomerDto.Nationality;
                    customer.Mobile_No = addCustomerDto.Mobile_No;
                    customer.Email_Address = "";// optional
                    customer.Area = addCustomerDto.Area;
                    customer.Block = addCustomerDto.Block;
                    customer.Street = addCustomerDto.Street;
                    customer.Building = addCustomerDto.Building;
                    customer.Floor = addCustomerDto.Floor;
                    customer.Flat = addCustomerDto.Flat;
                    customer.Login_Id = "33784939944";
                    customer.Password = addCustomerDto.Password;
                    customer.Device_Key = "e537487483sj";
                    customer.UID = Guid.NewGuid();
                    customer.CreatedBy = 1;
                    customer.UID_Token = Guid.NewGuid();
                    customer.CreatedOn = DateTime.Now;
                    customer.CreatedIp = "127.0.0.1";
                    customer.UpdatedBy = 1;
                    customer.UpdatedOn = DateTime.Now;
                    customer.UpdatedIp = "127.0.0.1";
                    customer.IsBlocked = 0;
                    customer.Block_Count = 0;
                    customer.InvalidTryCount = 0;
                    customer.Civil_Id_Front = "";
                    customer.Civil_Id_Back = "";
                    customer.Pep_Status = false;
                    customer.Pep_Description = "";// missing 
                    customer.Identification_Additional_Detail = addCustomerDto.Identification_Additional_Detail;
                    customer.Residence_Type = addCustomerDto.Residence_Type;
                    customer.Telephone_No = addCustomerDto.Telephone_No;
                    customer.Birth_Place = addCustomerDto.Birth_Place;
                    customer.Birth_Country = addCustomerDto.Birth_Country;
                    customer.Monthly_Income = addCustomerDto.Monthly_Income;
                    customer.Expected_Monthly_Trans_Count = addCustomerDto.Expected_Monthly_Trans_Count;
                    customer.Other_Income = addCustomerDto.Other_Income;
                    customer.Other_Income_Detail = addCustomerDto.Other_Income_Detail;
                    customer.Monthly_Trans_Limit = addCustomerDto.Monthly_Trans_Limit;
                    customer.Yearly_Trans_Limit = addCustomerDto.Yearly_Trans_Limit;
                    customer.Compliance_Limit = addCustomerDto.Compliance_Limit;
                    customer.Compliance_Trans_Count = addCustomerDto.Compliance_Trans_Count;
                    customer.Compliance_Limit_Expiry = addCustomerDto.Compliance_Limit_Expiry;
                    customer.Compliance_Comments = addCustomerDto.Compliance_Comments;
                    customer.IsVerified = 1; // missing
                    customer.IsReviwed = false; // missing
                    customer.Prod_Remitter_Id = 100;
                    customer.Employer = ""; // Employer
                    customer.Is_Profile_Completed = 0; // Missing in Both 

                    status = _remitterService.CreateRemitter(customer);

                   //customer.Prod_Remitter_Id = objKuaiex_Prod.GetRemittanceIdByIdentificationNumber(addCustomerDto.Identification_Number);

                    customer.Security_Answer_1 = "";
                    customer.Security_Answer_2 = "";
                    customer.Security_Answer_3 = "";
       
                  //  objRemitterDal.AddCustomer(customer);

                    securityQuestions = new List<Customer_Security_Questions>
                    {
                        new Customer_Security_Questions { Customer_Id = addCustomerDto.Customer_Id, Question_Id =addCustomerDto.Security_Question_Id_1, Answer =addCustomerDto.Security_Answer_1 },
                        new Customer_Security_Questions { Customer_Id = addCustomerDto.Customer_Id, Question_Id = addCustomerDto.Security_Question_Id_2, Answer = addCustomerDto.Security_Answer_2 },
                        new Customer_Security_Questions { Customer_Id = addCustomerDto.Customer_Id, Question_Id = addCustomerDto.Security_Question_Id_3, Answer = addCustomerDto.Security_Answer_3 }
                    };

                    objRemitterDal.AddCustomerSecurityQuestions(securityQuestions);

                    string check = "on";

                    individual_KYC = new List<Individual_KYC>()
                    {
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 1, Answer = addCustomerDto.checkbox1 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 2, Answer = addCustomerDto.checkbox2 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 3, Answer = addCustomerDto.checkbox3 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 4, Answer = addCustomerDto.checkbox4 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 5, Answer = addCustomerDto.checkbox5 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 6, Answer = addCustomerDto.checkbox6 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 7, Answer = addCustomerDto.checkbox7 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 8, Answer = addCustomerDto.checkbox8 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 9, Answer = addCustomerDto.checkbox9 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 10, Answer = addCustomerDto.checkbox10 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 11, Answer = addCustomerDto.checkbox11 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 12, Answer = addCustomerDto.checkbox12 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 13, Answer = addCustomerDto.checkbox13 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 14, Answer = addCustomerDto.checkbox14 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 15, Answer = addCustomerDto.pepcheckbox15 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 16, Answer = addCustomerDto.pepcheckbox16 == check },
                        new Individual_KYC {Customer_Id = addCustomerDto.Customer_Id, Question_Id = 17, Answer = addCustomerDto.pepcheckbox17 == check }
                    };

                    objRemitterDal.AddCustomerKYC(individual_KYC);

                    status = "success";
                }

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