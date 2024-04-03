using DataAccessLayer.Entities;
using CountryCurrencyProd = DataAccessLayer.ProdEntities.CountryCurrency;
using CountryCurrency = DataAccessLayer.Entities.CountryCurrency;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.ProdEntities;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace KuaiexDashboard.Services.CountryCurrencyServices.Impl
{
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private readonly IRepository<CountryCurrency> _countryCurrencyRepository;
        private readonly IRepository<CountryCurrencyProd> _countryCurrencyProdRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Currency> _currencyRepository;
        public CountryCurrencyService()
        {
            _countryCurrencyRepository = new GenericRepository<CountryCurrency>(DatabasesName.KUAIEXEntities);
            _countryRepository = new GenericRepository<Country>(DatabasesName.KUAIEXEntities);
            _currencyRepository = new GenericRepository<Currency>(DatabasesName.KUAIEXEntities);
            _countryCurrencyProdRepository = new GenericRepository<CountryCurrencyProd>(DatabasesName.KUAIEXProdEntities);
        }

        public string AddCountryCurrency(CountryCurrency countryCurrency)
        {
            try
            {
                int countryId = countryCurrency.Country_Id;
                int displayOrder = countryCurrency.DisplayOrder;
                CountryCurrency currency = _countryCurrencyRepository.FindBy(x => x.Country_Id == countryId && x.DisplayOrder == displayOrder);
                if (currency != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                countryCurrency.CreatedOn = DateTime.Now;
                countryCurrency.UID = Guid.NewGuid();
                if (_countryCurrencyRepository.Insert(countryCurrency) > 0)
                {
                    return MsgKeys.CreatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
            return MsgKeys.Error;
        }

        public PagedResult<GetCountryCurrencyList_Result> GetAllCountryCurrency(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<GetCountryCurrencyList_Result> getCountryCurrencyList_Results = _countryCurrencyRepository.GetPagedDataFromSP<GetCountryCurrencyList_Result>("GetAllCountryCurrencyWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
                return getCountryCurrencyList_Results;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public CountryCurrency GetCountryCurrency(Guid UID)
        {
            try
            {
                CountryCurrency currency = _countryCurrencyRepository.FindBy(x => x.UID == UID);
                return currency;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<Currency_Result> GetCurrencyByCountry(int id)
        {
            try
            {
                List<Currency_Result> currency = _countryCurrencyRepository.GetDataFromSP<Currency_Result>("GetCurrencyByCountry", id);
                return currency;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public string UpdateCountryCurrency(CountryCurrency countryCurrency)
        {
            try
            {
                int countryId = countryCurrency.Country_Id;
                int displayOrder = countryCurrency.DisplayOrder;
                CountryCurrency existingCurrency = _countryCurrencyRepository.FindBy(x => x.Country_Id == countryId && x.DisplayOrder == displayOrder);
                if (existingCurrency != null)
                {
                    existingCurrency.CommissionRate1 = countryCurrency.CommissionRate1;
                    existingCurrency.CommissionRate2 = countryCurrency.CommissionRate2;
                    existingCurrency.DisplayOrder = countryCurrency.DisplayOrder;
                    existingCurrency.AmountLimit = countryCurrency.AmountLimit;
                    existingCurrency.UpdatedOn = DateTime.Now;

                    _countryCurrencyRepository.Update(existingCurrency, $" Id = {countryCurrency.Id} ");
                    return MsgKeys.UpdatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
            return MsgKeys.Error;

        }
        public int SynchronizeRecords()
        {
            int count = 0;
            try
            {
                List<CountryCurrencyProd> prodCountryCurrencies = _countryCurrencyProdRepository.GetAll().Where(x => x.Currency_Id > 0 && x.Country_Id > 0).ToList();
                List<CountryCurrency> countryCurrencies = _countryCurrencyRepository.GetAll();
                List<Country> countries = _countryRepository.GetAll(null, x => x.Prod_Country_Id, x => x.Id);
                List<Currency> currencies = _currencyRepository.GetAll(null, x => x.Prod_Currency_Id, x => x.Id);

                prodCountryCurrencies = prodCountryCurrencies
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

                List<CountryCurrency> countryCurrency = new List<CountryCurrency>();


                var missingCountryCurrencies = prodCountryCurrencies
                              .Where(ccp => !countryCurrencies.Any(cc => cc.Country_Id == ccp.Country_Id && cc.Currency_Id == ccp.Currency_Id))
                            .ToList();

                foreach (var item in missingCountryCurrencies)
                {
                    CountryCurrency temp = new CountryCurrency();
                    temp.CreatedOn = DateTime.Now;
                    temp.UID = Guid.NewGuid();
                    temp.Country_Id = item.Country_Id;
                    temp.Currency_Id = item.Currency_Id;
                    temp.Prod_CountryCurrency_Id = item.RECID;
                    temp.DisplayOrder = item.Display_Order;

                    if (_countryCurrencyRepository.Insert(temp) > 0)
                    {
                        count++;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.SomethingWentWrong);
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
    }
}