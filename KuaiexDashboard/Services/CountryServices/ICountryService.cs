using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.CountryServices
{
    public interface ICountryService
    {
        Country GetCountryByName(string countryName);
        int? GetCountryIdByCountryName(string countryName);
        List<GetCountryList_Result> GetCountryList();
        PagedResult<GetCountryList_Result> GetCountryList(JqueryDatatableParam param);
        Country GetCountryByUID(Guid countryId);
        List<Country> GetAllCountries();
        string UpdateCountry(Country country);
        string AddCountry(Country country);
        List<GetCountryList_Result> GetActiveCountryList();



    }
}