using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DTO.Beneficiary;
using KuaiexDashboard.DTO.Customer;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KuaiexDashboard.Services.RemitterServices.Impl
{
    public class RemitterService : IRemitterService
    {
        IRepository<Customer> _customerRepository;
        IRepository<Customer_Security_Questions> _customerSecurityQuestionRepository;
        IRepository<Individual_KYC> _individual_KYCRepository;
        public RemitterService()
        {
            _customerRepository = new GenericRepository<Customer>();
            _individual_KYCRepository = new GenericRepository<Individual_KYC>();
            _customerSecurityQuestionRepository = new GenericRepository<Customer_Security_Questions>();
        }
        public string CreateRemitter(CustomerDTO customerDto)
        {
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                Customer customer = new Customer();
                List<Customer_Security_Questions> securityQuestions;
                List<Individual_KYC> individual_KYCs;

                Customer existingCustomer = objRemitterDal.GetCustomerByIdentificationAndName(customerDto.Identification_Number, customerDto.Name);

                if (existingCustomer != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                else
                {

                    customer.Agency_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Agency_Id"].ToString());
                    customer.Agency_Branch_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Agency_Branch_Id"].ToString());
                    customer.Name = customerDto.Name;
                    customer.Employer = customerDto.Employer;
                    // customer.Gender = 0;
                    customer.Identification_Type = customerDto.Identification_Type;
                    customer.Identification_Number = customerDto.Identification_Number;
                    customer.Identification_Expiry_Date = customerDto.Identification_Expiry_Date;
                    customer.Date_Of_Birth = customerDto.Date_Of_Birth;
                    customer.Occupation = customerDto.Occupation;
                    customer.IsBlocked = 0;
                    customer.Block_Count = 0;
                    customer.InvalidTryCount = 0;
                    customer.Nationality = string.IsNullOrEmpty(customerDto.Nationality) ? null : customerDto.Nationality;
                    customer.Mobile_No = customerDto.Mobile_No;
                    customer.Area = customerDto.Area;
                    customer.Block = customerDto.Block;
                    customer.Street = customerDto.Street;
                    customer.Building = customerDto.Building;
                    customer.Floor = customerDto.Floor;
                    customer.Flat = customerDto.Flat;
                    customer.Login_Id = customerDto.Identification_Number;
                    //  customer.Device_Key = "e537487483sj";
                    customer.UID = Guid.NewGuid();
                    customer.CreatedBy = 1;
                    customer.UID_Token = Guid.NewGuid();
                    customer.CreatedOn = DateTime.Now;
                    customer.CreatedIp = "127.0.0.1";
                    customer.UpdatedBy = 1;
                    customer.UpdatedOn = DateTime.Now;
                    customer.UpdatedIp = "127.0.0.1";
                    customer.Civil_Id_Front = customerDto.Civil_Id_Front;
                    customer.Civil_Id_Back = customerDto.Civil_Id_Back;
                    // customer.Pep_Status = false;
                    //  customer.Pep_Description = "";
                    customer.Identification_Additional_Detail = customerDto.Identification_Additional_Detail;
                    customer.Residence_Type = customerDto.Residence_Type;
                    customer.Telephone_No = customerDto.Telephone_No;
                    customer.Birth_Place = customerDto.Birth_Place;
                    customer.Birth_Country = customerDto.Birth_Country;
                    customer.Monthly_Income = customerDto.Monthly_Income;
                    customer.Expected_Monthly_Trans_Count = customerDto.Expected_Monthly_Trans_Count;
                    customer.Other_Income = customerDto.Other_Income;
                    customer.Other_Income_Detail = customerDto.Other_Income_Detail;
                    customer.Monthly_Trans_Limit = customerDto.Monthly_Trans_Limit;
                    customer.Yearly_Trans_Limit = customerDto.Yearly_Trans_Limit;
                    customer.Compliance_Limit = customerDto.Compliance_Limit;
                    customer.Compliance_Trans_Count = customerDto.Compliance_Trans_Count;
                    customer.Compliance_Limit_Expiry = customerDto.Compliance_Limit_Expiry;
                    customer.Compliance_Comments = customerDto.Compliance_Comments;
                    customer.IsReviwed = customerDto.IsReviwed == null ? false : true;
                    // customer.Prod_Remitter_Id = 100;
                    customer.Is_Profile_Completed = 1;

                    //customer.Prod_Remitter_Id = objKuaiex_Prod.GetRemittanceIdByIdentificationNumber(customerDto.Identification_Number);
                    customerDto.Customer_Id = _customerRepository.Insert(customer);

                    securityQuestions = new List<Customer_Security_Questions>
                    {
                        new Customer_Security_Questions { Customer_Id = customerDto.Customer_Id, Question_Id = customerDto.Security_Question_Id_1, Answer = customerDto.Security_Answer_1 },
                        new Customer_Security_Questions { Customer_Id = customerDto.Customer_Id, Question_Id = customerDto.Security_Question_Id_2, Answer = customerDto.Security_Answer_2 },
                        new Customer_Security_Questions { Customer_Id = customerDto.Customer_Id, Question_Id = customerDto.Security_Question_Id_3, Answer = customerDto.Security_Answer_3 }
                    };

                    foreach (var securityQuestion in securityQuestions)
                    {
                        _customerSecurityQuestionRepository.Insert(securityQuestion);
                    }

                    string check = "on";

                    individual_KYCs = new List<Individual_KYC>()
                    {
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 1, Answer = customerDto.checkbox1 == check, CreatedOn = DateTime.Now },
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 2, Answer = customerDto.checkbox2 == check,CreatedOn = DateTime.Now },
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 3, Answer = customerDto.checkbox3 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 4, Answer = customerDto.checkbox4 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 5, Answer = customerDto.checkbox5 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 6, Answer = customerDto.checkbox6 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 7, Answer = customerDto.checkbox7 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 8, Answer = customerDto.checkbox8 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 9, Answer = customerDto.checkbox9 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 10, Answer = customerDto.checkbox10 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 11, Answer = customerDto.checkbox11 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 12, Answer = customerDto.checkbox12 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 13, Answer = customerDto.checkbox13 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 14, Answer = customerDto.checkbox14 == check ,CreatedOn = DateTime.Now , Details = customerDto.other_Detail},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 15, Answer = customerDto.pepcheckbox15 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 16, Answer = customerDto.pepcheckbox16 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 17, Answer = customerDto.pepcheckbox17 == check ,CreatedOn = DateTime.Now}
                    };

                    foreach (var individual_KYC in individual_KYCs)
                    {
                        _individual_KYCRepository.Insert(individual_KYC);
                    }


                    return MsgKeys.CreatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }

        public string EditRemitter(CustomerDTO customerDto)
        {
            try
            {
                RemitterDAL objRemitterDal = new RemitterDAL();
                Customer customer = objRemitterDal.GetCustomerByUID(customerDto.UID);
                List<Customer_Security_Questions> securityQuestions;
                List<Individual_KYC> individual_KYCs;
                if (customer != null)
                {
                    customer.Name = customerDto.Name;
                    customer.Occupation = customerDto.Occupation;
                    customer.Employer = customerDto.Employer;
                    customer.IsReviwed = customerDto.IsReviwed == null ? false : true;
                    customer.Area = customerDto.Area;
                    customer.Block = customerDto.Block;
                    customer.Street = customerDto.Street;
                    customer.Building = customerDto.Building;
                    customer.Floor = customerDto.Floor;
                    customer.Flat = customerDto.Flat;
                    customer.Identification_Type = customerDto.Identification_Type;
                    customer.Identification_Number = customerDto.Identification_Number;
                    customer.Identification_Expiry_Date = customerDto.Identification_Expiry_Date;
                    customer.Identification_Additional_Detail = customerDto.Identification_Additional_Detail;
                    customer.Residence_Type = customerDto.Residence_Type;
                    customer.Mobile_No = customerDto.Mobile_No;
                    customer.Telephone_No = customerDto.Telephone_No;
                    customer.Nationality = customerDto.Nationality;
                    customer.Date_Of_Birth = customerDto.Date_Of_Birth;
                    customer.Birth_Place = customerDto.Birth_Place;
                    customer.Birth_Country = customerDto.Birth_Country;
                    customer.Expected_Monthly_Trans_Count = customerDto.Expected_Monthly_Trans_Count;
                    customer.Monthly_Income = customerDto.Monthly_Income;
                    customer.Other_Income = customerDto.Other_Income;
                    customer.Other_Income_Detail = customerDto.Other_Income_Detail;
                    customer.Monthly_Trans_Limit = customerDto.Monthly_Trans_Limit;
                    customer.Yearly_Trans_Limit = customerDto.Yearly_Trans_Limit;
                    customer.Compliance_Limit = customerDto.Compliance_Limit;
                    customer.Compliance_Trans_Count = customerDto.Compliance_Trans_Count;
                    customer.Compliance_Limit_Expiry = customerDto.Compliance_Limit_Expiry;
                    customer.Compliance_Comments = customerDto.Compliance_Comments;
                    customer.Civil_Id_Front = customerDto.Civil_Id_Front;
                    customer.Civil_Id_Back = customerDto.Civil_Id_Back;
                    customer.UpdatedOn = DateTime.Now;
                    /////////////////////////////

                }

                // customer.Prod_Remitter_Id = objKuaiex_Prod.GetRemittanceIdByIdentificationNumber(customerDto.Identification_Number);
                // Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();

                _customerRepository.Update(customer, $"  Customer_Id = {customer.Customer_Id} ");

                customerDto.Customer_Id = customer.Customer_Id;

                securityQuestions = new List<Customer_Security_Questions>
                    {
                        new Customer_Security_Questions { Customer_Id = customerDto.Customer_Id, Question_Id = customerDto.Security_Question_Id_1, Answer = customerDto.Security_Answer_1 },
                        new Customer_Security_Questions { Customer_Id = customerDto.Customer_Id, Question_Id = customerDto.Security_Question_Id_2, Answer = customerDto.Security_Answer_2 },
                        new Customer_Security_Questions { Customer_Id = customerDto.Customer_Id, Question_Id = customerDto.Security_Question_Id_3, Answer = customerDto.Security_Answer_3 }
                    };

                foreach (var securityQuestion in securityQuestions)
                {
                    _customerSecurityQuestionRepository.Update(securityQuestion, $" Customer_Id = {customerDto.Customer_Id} AND Question_Id = {securityQuestion.Question_Id} ");
                }

                string check = "on";

                individual_KYCs = new List<Individual_KYC>()
                    {
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 1, Answer = customerDto.checkbox1 == check, CreatedOn = DateTime.Now },
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 2, Answer = customerDto.checkbox2 == check, CreatedOn = DateTime.Now },
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 3, Answer = customerDto.checkbox3 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 4, Answer = customerDto.checkbox4 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 5, Answer = customerDto.checkbox5 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 6, Answer = customerDto.checkbox6 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 7, Answer = customerDto.checkbox7 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 8, Answer = customerDto.checkbox8 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 9, Answer = customerDto.checkbox9 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 10, Answer = customerDto.checkbox10 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 11, Answer = customerDto.checkbox11 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 12, Answer = customerDto.checkbox12 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 13, Answer = customerDto.checkbox13 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 14, Answer = customerDto.checkbox14 == check ,CreatedOn = DateTime.Now , Details = customerDto.other_Detail},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 15, Answer = customerDto.pepcheckbox15 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 16, Answer = customerDto.pepcheckbox16 == check ,CreatedOn = DateTime.Now},
                        new Individual_KYC {Customer_Id = customerDto.Customer_Id, Question_Id = 17, Answer = customerDto.pepcheckbox17 == check ,CreatedOn = DateTime.Now}
                    };

                foreach (var individual_KYC in individual_KYCs)
                {
                    _individual_KYCRepository.Update(individual_KYC, $" Customer_Id = {customerDto.Customer_Id} AND Question_Id = {individual_KYC.Question_Id} ");
                }

                return MsgKeys.UpdatedSuccessfully;

            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }

        public EditCustomerDTO GetCustomerByUID(Guid customerId)
        {
            try
            {
                EditCustomerDTO customerDTO = null;
                RemitterDAL objRemitterDal = new RemitterDAL();
                string LocalStoragePath = ConfigurationManager.AppSettings["LocalStoragePath"].ToString();
                Customer obj = objRemitterDal.GetCustomerByUID(customerId);
                customerDTO = AutoMapper.Mapper.Map<EditCustomerDTO>(obj);
                customerDTO.Civil_Id_Front = string.Concat(LocalStoragePath, customerDTO.Civil_Id_Front);
                customerDTO.Civil_Id_Back = string.Concat(LocalStoragePath, customerDTO.Civil_Id_Back);
                return customerDTO;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<Individual_KYC> GetCustomerLoadKYCIndividuals(int id)
        {
            try
            {
                return _individual_KYCRepository.GetAll(x => x.Customer_Id == id, null);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<Customer_Security_Questions> GetCustomerSecurityQuestions(int id)
        {
            try
            {
                return _customerSecurityQuestionRepository.GetAll(x => x.Customer_Id == id, null);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public PagedResult<GetRemitterList_Result> GetRemitterList(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<GetRemitterList_Result> obj = _customerRepository.GetPagedDataFromSP<GetRemitterList_Result>("GetRemitterListWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);

                return obj;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}
