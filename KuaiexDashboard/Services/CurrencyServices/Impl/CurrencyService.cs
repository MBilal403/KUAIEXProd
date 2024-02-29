using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository.Impl;
using KuaiexDashboard.Repository;
using System;


namespace KuaiexDashboard.Services.CurrencyServices.Impl
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency> _currencyRepository;
        public CurrencyService()
        {
            _currencyRepository = new GenericRepository<Currency>();
        }

        public string AddCurrency(Currency currency)
        {
            int currencyId = currency.Id;
           Currency currencyExist = _currencyRepository.FindBy(x => x.Id == currencyId);
            if (currencyExist != null)
            {
                return MsgKeys.DuplicateValueExist;
            }
            currency.CreatedOn = DateTime.Now;
            currency.UpdatedOn = DateTime.Now;
            currency.IsBaseCurrency = 0;
            currency.Display_Order = 1;
            currency.TT_Min_Rate = currency.DD_Rate;
            currency.TT_Rate = currency.DD_Rate;
            currency.UID = Guid.NewGuid();

            currency.Status =  currency.Status != null ? "A" : "N";

            if (_currencyRepository.Insert(currency) > 0)
            {
                return MsgKeys.CreatedSuccessfully;
            }
            return MsgKeys.Error;
        }

        public PagedResult<Currency> GetAllCurrency(JqueryDatatableParam param)
        {
            PagedResult<Currency> getCurrencyList_Results = _currencyRepository.GetPagedDataFromSP<Currency>("GetAllCurrencyWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
            return getCurrencyList_Results;
        }

        public Currency GetCurrency(Guid UID)
        {
            Currency currency = _currencyRepository.FindBy(x => x.UID == UID);
            return currency;
        }

        public string UpdateCurrency(Currency currency)
        {
            int currencyId = currency.Id;
            Currency currencyExist = _currencyRepository.FindBy(x => x.Id == currencyId);
            if (currencyExist != null)
            {
                currency.UpdatedOn = DateTime.Now;
                currency.TT_Min_Rate = currency.DD_Rate;
                currency.TT_Rate = currency.DD_Rate;
                currency.UpdatedOn = DateTime.Now;
                currency.Status = currency.Status != null ? "A" : "N";
                _currencyRepository.Update(currency, $" Id = {currency.Id} ");
                return MsgKeys.UpdatedSuccessfully;
            }
            return MsgKeys.Error;
        }
    }
}