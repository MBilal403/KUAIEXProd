using Bank_Mst_Prod = DataAccessLayer.ProdEntities.Bank_Mst;
using Bank_Mst = DataAccessLayer.Entities.Bank_Mst;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProdEntities;
using DataAccessLayer.Recources;
using KuaiexDashboard.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Entities;
using KuaiexDashboard.Repository.Impl;
using DataAccessLayer.Repository;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace KuaiexDashboard.Services.BankServices.Impl
{
    public class BankService : IBankService
    {
        private readonly IRepository<Bank_Mst> _bank_MstRepository;
        private readonly IRepository<Bank_Mst_Prod> _bank_MstProdRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Currency> _currencyRepository;
        public BankService()
        {
            _bank_MstRepository = new GenericRepository<Bank_Mst>(DatabasesName.KUAIEXEntities);
            _countryRepository = new GenericRepository<Country>(DatabasesName.KUAIEXEntities);
            _currencyRepository = new GenericRepository<Currency>(DatabasesName.KUAIEXEntities);
            _bank_MstProdRepository = new GenericRepository<Bank_Mst_Prod>(DatabasesName.KUAIEXProdEntities);
        }

        public string ChangeState(Guid UID)
        {
            try
            {
                Bank_Mst existingLimits = _bank_MstRepository.FindBy(x => x.UID == UID);
                if (existingLimits != null)
                {
                    existingLimits.Updated_Date = DateTime.Now;
                    existingLimits.Record_Status = existingLimits.Record_Status == "A" ? "N" : "A";
                    if (_bank_MstRepository.Update(existingLimits, $" UID = '{existingLimits.UID}' "))
                    {
                        return MsgKeys.UpdatedSuccessfully;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.UpdationFailed);
                    }
                }

                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public PagedResult<GetBanks_Result> GetAllWithPagination(JqueryDatatableParam param, int countryId)
        {
            try
            {
                param.sSearch = string.Join("|", param.sSearch ?? "", countryId.ToString() ?? "");
                PagedResult<GetBanks_Result> list = _bank_MstRepository.GetPagedDataFromSP<GetBanks_Result>("GetBankWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
                return list;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public GetBankDetailsById_Result GetByUID(Guid uid)
        {
            try
            {
                Bank_Mst bank_Mst = _bank_MstRepository.FindBy(x => x.UID == uid);
                GetBankDetailsById_Result getBankDetailsById_Result = _bank_MstRepository.GetDataFromSP<GetBankDetailsById_Result>("GetBankDetailsById", bank_Mst.Bank_Id).First();
                return getBankDetailsById_Result;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public Bank_Mst GetLimitDetailByUId(Guid uid)
        {
            try
            {
                Bank_Mst bank_Mst = _bank_MstRepository.FindBy(x => x.UID == uid);
                return bank_Mst;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public int SynchronizeRecords()
        {
            int count = 0;
            try
            {
                List<Bank_Mst_Prod> prodBanks = _bank_MstProdRepository.GetAll().Where(x => x.Currency_Id > 0 && x.Country_Id > 0).ToList();
                List<Country> countries = _countryRepository.GetAll(null, x => x.Prod_Country_Id, x => x.Id);
                List<Currency> currencies = _currencyRepository.GetAll(null, x => x.Prod_Currency_Id, x => x.Id);

                prodBanks = prodBanks
               .Select(prodCountryCurrency =>
               {
                   var matchingCountry = countries.FirstOrDefault(c => c.Prod_Country_Id == prodCountryCurrency.Country_Id);
                   var matchingCurrencies = currencies.FirstOrDefault(c => c.Prod_Currency_Id == prodCountryCurrency.Currency_Id);
                   if (matchingCountry != null && matchingCurrencies != null)
                   {
                       prodCountryCurrency.Country_Id = matchingCountry.Id;
                       prodCountryCurrency.Currency_Id = matchingCurrencies.Id;
                   }
                   return prodCountryCurrency;
               })
               .ToList();

                foreach (var item in prodBanks)
                {
                    string bankName = Strings.EscapeSingleQuotes(item.English_Name);
                    int countryId = item.Country_Id;
                    int currencyId = item.Currency_Id;
                    if (!_bank_MstRepository.Any(x => x.English_Name == bankName && x.Country_Id == countryId && x.Currency_Id == currencyId))
                    {
                        Bank_Mst bank_Mst = new Bank_Mst();
                        bank_Mst.English_Name = item.English_Name;
                        bank_Mst.Arabic_Name = item.Arabic_Name;
                        bank_Mst.Short_English_Name = item.Short_English_Name;
                        bank_Mst.Short_Arabic_Name = item.Short_Arabic_Name;
                        bank_Mst.Country_Id = item.Country_Id;
                        bank_Mst.Currency_Id = item.Currency_Id;
                        bank_Mst.Upper_Limit = item.Upper_Limit;
                        bank_Mst.Address_Line1 = item.Address_Line1;
                        bank_Mst.Address_Line2 = item.Address_Line2;
                        bank_Mst.Address_Line3 = item.Address_Line3;
                        bank_Mst.Tel_Number1 = item.Tel_Number1;
                        bank_Mst.Fax_Number1 = item.Fax_Number1;
                        bank_Mst.EMail_Address1 = item.EMail_Address1;
                        bank_Mst.Prefix_Draft = item.Prefix_Draft;
                        bank_Mst.Sufix_Draft = item.Sufix_Draft;
                        bank_Mst.Reimbursement_Text_Status = item.Reimbursement_Text_Status;
                        bank_Mst.Contact_Person1 = item.Contact_Person1;
                        bank_Mst.Contact_Title1 = item.Contact_Title1;
                        bank_Mst.Contact_Person2 = item.Contact_Person2;
                        bank_Mst.Contact_Title2 = item.Contact_Title2;
                        bank_Mst.Mob_Number2 = item.Mob_Number2;
                        bank_Mst.Remarks = item.Remarks;
                        bank_Mst.Record_Status = item.Record_Status;
                        bank_Mst.Option_Status = item.Option_Status;
                        bank_Mst.Updated_Date = item.Updated_Date;
                        bank_Mst.Updated_User = item.Updated_User;
                        bank_Mst.HO_Branch1_Id = item.HO_Branch1_Id;
                        bank_Mst.Dispatch_Status = item.Dispatch_Status;
                        bank_Mst.Dispatch_Header = item.Dispatch_Header;
                        bank_Mst.Dispatch_Type = item.Dispatch_Type;
                        bank_Mst.Column_Separator = item.Column_Separator;
                        bank_Mst.Bank_Code = item.Bank_Code;
                        bank_Mst.Remittance_Sequence_Prefix = item.Remittance_Sequence_Prefix;
                        bank_Mst.Column_Demarkation_Char = item.Column_Demarkation_Char;
                        bank_Mst.UID = Guid.NewGuid();
                        bank_Mst.Prod_Bank_Id = item.Bank_Id;
                        bank_Mst.AmountLimit = 0;
                        bank_Mst.NumberOfTransaction = 0;
                        if (_bank_MstRepository.Insert(bank_Mst) > 0)
                        {
                            count++;
                        }
                        else
                        {
                            throw new Exception(MsgKeys.SomethingWentWrong);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
            return count;
        }

        public string UpdateLimits(Guid? UID, decimal AmountLimit, int NumberOfTransaction)
        {
            try
            {
                if (UID != null)
                {
                    Bank_Mst existingLimits = _bank_MstRepository.FindBy(x => x.UID == UID);
                    if (existingLimits != null)
                    {
                        existingLimits.AmountLimit = AmountLimit;
                        existingLimits.NumberOfTransaction = NumberOfTransaction;
                        existingLimits.Updated_Date = DateTime.Now;
                        if (_bank_MstRepository.Update(existingLimits, $" UID = '{existingLimits.UID}' "))
                        {
                            return MsgKeys.UpdatedSuccessfully;
                        }
                        else
                        {
                            throw new Exception(MsgKeys.UpdationFailed);
                        }
                    }
                }
                else
                {
                    string query = "UPDATE Bank_Mst " +
                       "SET AmountLimit = @AmountLimit, NumberOfTransaction = @NumberOfTransaction ";

                    SqlParameter[] parameters = {
                        new SqlParameter("@AmountLimit", AmountLimit),
                        new SqlParameter("@NumberOfTransaction", NumberOfTransaction)
                    };


                    bool success = _bank_MstRepository.ExecuteQuery(query, parameters);
                    if (success)
                    {
                        return MsgKeys.UpdatedSuccessfully;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.UpdationFailed);
                    }
                }


                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}