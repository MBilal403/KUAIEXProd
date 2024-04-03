using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository.Impl;
using KuaiexDashboard.Repository;
using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;
using DataAccessLayer.ProdEntities;
using KuaiexDashboard.DTO.Beneficiary;
using DataAccessLayer.ProcedureResults;


namespace KuaiexDashboard.Services.CurrencyServices.Impl
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Remittance_Currency_Limit> _currencyLimitRepository;
        private readonly IRepository<Currency_Mst> _currencyProdRepository;
        private readonly IRepository<Remittance_Currency_Limit_Log> _logRepository;
        private readonly IRepository<Comparison_Lookup> _comparisonRepository;

        public CurrencyService()
        {
            _currencyRepository = new GenericRepository<Currency>(DatabasesName.KUAIEXEntities);
            _currencyLimitRepository = new GenericRepository<Remittance_Currency_Limit>(DatabasesName.KUAIEXEntities);
            _logRepository = new GenericRepository<Remittance_Currency_Limit_Log>(DatabasesName.KUAIEXEntities);
            _comparisonRepository = new GenericRepository<Comparison_Lookup>(DatabasesName.KUAIEXEntities);
            _currencyProdRepository = new GenericRepository<Currency_Mst>(DatabasesName.KUAIEXProdEntities);
        }

        public string AddCurrency(Currency currency)
        {
            try
            {
                int currencyId = currency.Id;
                Currency currencyExist = _currencyRepository.FindBy(x => x.Id == currencyId);
                if (currencyExist != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                currency.CreatedOn = DateTime.Now;
                currency.IsBaseCurrency = 0;
                currency.Display_Order = 1;
                currency.TT_Min_Rate = currency.DD_Rate;
                currency.TT_Rate = currency.DD_Rate;
                currency.UID = Guid.NewGuid();

                currency.Status = currency.Status != null ? "A" : "N";

                if (_currencyRepository.Insert(currency) > 0)
                {

                    return MsgKeys.CreatedSuccessfully;
                }
                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public string AddCurrencylimit(Remittance_Currency_Limit remittance_Currency_Limit)
        {
            try
            {
                int currencyId = remittance_Currency_Limit.Currency_Id;
                decimal amount = remittance_Currency_Limit.Amount;
                decimal rate = remittance_Currency_Limit.DD_Rate;
                Remittance_Currency_Limit remittance_Currency_LimitExist = _currencyLimitRepository.FindBy(x => x.Currency_Id == currencyId && x.Amount == amount);
                if (remittance_Currency_LimitExist != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                remittance_Currency_Limit.CreatedOn = DateTime.Now;
                remittance_Currency_Limit.TT_Min_Rate = remittance_Currency_Limit.DD_Rate;
                remittance_Currency_Limit.TT_Rate = remittance_Currency_Limit.DD_Rate;
                remittance_Currency_Limit.UID = Guid.NewGuid();

                int insertResult = _currencyLimitRepository.Insert(remittance_Currency_Limit);
                if (insertResult > 0)
                {
                    Remittance_Currency_Limit_Log _Limit_Log = AutoMapper.Mapper.Map<Remittance_Currency_Limit_Log>(remittance_Currency_Limit);
                    _Limit_Log.Operation = "insert";
                    _Limit_Log.User_Id = 1;
                    _Limit_Log.Remittance_Currency_Limit_Id = insertResult;
                    _logRepository.Insert(_Limit_Log);
                    return MsgKeys.CreatedSuccessfully;
                }

                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }


        public string DeleteCurrencylimit(Guid UID)
        {
            try
            {
                Remittance_Currency_Limit remittance_Currency_Limit = _currencyLimitRepository.FindBy(x => x.UID == UID);
                Remittance_Currency_Limit_Log _Limit_Log = AutoMapper.Mapper.Map<Remittance_Currency_Limit_Log>(remittance_Currency_Limit);
                _Limit_Log.Operation = "deleted";
                _Limit_Log.User_Id = 1;
                if (_logRepository.Insert(_Limit_Log) > 0)
                {
                    _currencyLimitRepository.Delete(UID);
                    return MsgKeys.DeletedSuccessfully;
                }
                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public PagedResult<Currency> GetAllCurrency(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<Currency> getCurrencyList_Results = _currencyRepository.GetPagedDataFromSP<Currency>("GetAllCurrencyWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
                return getCurrencyList_Results;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<Currency> getAllCurrencies()
        {
            try
            {
                List<Currency> currencies = _currencyRepository.GetAll(x => x.Status == "A", x => x.Id, x => x.Name, x => x.Code);
                return currencies;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<RemittanceCurrencyLimitResult> GetAllCurrencylimitByCurrencyId(int Id)
        {
            try
            {
                var data = _currencyLimitRepository.GetDataFromSP<RemittanceCurrencyLimitResult>("GetRemittanceCurrencyLimits", Id);
                return data;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public Currency GetCurrency(Guid UID)
        {
            try
            {
                Currency currency = _currencyRepository.FindBy(x => x.UID == UID);
                return currency;
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
                List<Currency_Mst> prodCurrencies = _currencyProdRepository.GetAll(null, x => x.Currency_Id, x => x.Currency_Code, x => x.English_Name, x => x.Record_Status);

                foreach (var item in prodCurrencies)
                {
                    string CurrencyName = Strings.EscapeSingleQuotes(item.English_Name.Trim());
                    string CurrencyCode = item.Currency_Code.Trim();

                    if (!_currencyRepository.Any(x => x.Name == CurrencyName && x.Code == CurrencyCode))
                    {
                        Currency currency = new Currency();
                        currency.Name = item.English_Name.Trim();
                        currency.Code = item.Currency_Code.Trim();
                        currency.Status = item.Record_Status;
                        currency.Display_Order = 1;
                        currency.Prod_Currency_Id = item.Currency_Id;
                        currency.UID = Guid.NewGuid();
                        currency.CreatedOn = DateTime.Now;
                        if (_currencyRepository.Insert(currency) > 0)
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

        public string UpdateCurrency(Currency currency)
        {
            try
            {
                int currencyId = currency.Id;
                Currency currencyExist = _currencyRepository.FindBy(x => x.Id == currencyId);
                if (currencyExist != null)
                {
                    currencyExist.UpdatedOn = DateTime.Now;
                    currencyExist.DD_Rate = currency.DD_Rate;
                    currencyExist.TT_Min_Rate = currency.DD_Rate;
                    currencyExist.TT_Rate = currency.DD_Rate;
                    currencyExist.Status = currency.Status != null ? "A" : "N";
                    if(_currencyRepository.Update(currencyExist, $" Id = {currency.Id} "))
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

        public List<Comparison_Lookup> GelAllComprison()
        {
            try
            {
                List<Comparison_Lookup> comparisons = _comparisonRepository.GetAll();
                return comparisons;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}