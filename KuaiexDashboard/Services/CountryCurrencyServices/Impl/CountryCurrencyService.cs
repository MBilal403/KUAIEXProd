using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace KuaiexDashboard.Services.CountryCurrencyServices.Impl
{
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private readonly IRepository<CountryCurrency> _countryCurrencyRepository;
        public CountryCurrencyService()
        {
            _countryCurrencyRepository = new GenericRepository<CountryCurrency>(DatabasesName.KUAIEXEntities);
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
                countryCurrency.UpdatedOn = DateTime.Now;
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
    }
}