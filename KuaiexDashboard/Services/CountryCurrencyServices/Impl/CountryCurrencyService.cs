using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;

namespace KuaiexDashboard.Services.CountryCurrencyServices.Impl
{
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private readonly IRepository<CountryCurrency> _countryCurrencyRepository;
        public CountryCurrencyService()
        {
            _countryCurrencyRepository = new GenericRepository<CountryCurrency>();
        }

        public string AddCountryCurrency(CountryCurrency countryCurrency)
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
            return MsgKeys.Error;
        }

        public PagedResult<GetCountryCurrencyList_Result> GetAllCountryCurrency(JqueryDatatableParam param)
        {
            PagedResult<GetCountryCurrencyList_Result> getCountryCurrencyList_Results = _countryCurrencyRepository.GetPagedDataFromSP<GetCountryCurrencyList_Result>("GetAllCountryCurrencyWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
            return getCountryCurrencyList_Results;
        }

        public CountryCurrency GetCountryCurrency(Guid UID)
        {
            CountryCurrency currency = _countryCurrencyRepository.FindBy(x => x.UID == UID);
            return currency;
        }

        public string UpdateCountryCurrency(CountryCurrency countryCurrency)
        {
            int countryId = countryCurrency.Country_Id;
            int displayOrder = countryCurrency.DisplayOrder;
            CountryCurrency existingCurrency = _countryCurrencyRepository.FindBy(x => x.Country_Id == countryId && x.DisplayOrder == displayOrder);
            if (existingCurrency != null)
            {
                countryCurrency.UpdatedOn = DateTime.Now;
                countryCurrency.UID = existingCurrency.UID;
                countryCurrency.CreatedOn = existingCurrency.CreatedOn;

                _countryCurrencyRepository.Update(countryCurrency, $" Id = {countryCurrency.Id} ");
                return MsgKeys.UpdatedSuccessfully;
            }
            return MsgKeys.Error;
        }
    }
}