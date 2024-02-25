using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.CountryServices
{
    public interface ICountryService
    {
        Country GetCountryByName(string countryName);
        int GetCountryIdByCountryName(string countryName);
        List<GetCountryList_Result> GetCountryList();
        Country GetCountryByUID(Guid countryId);
        List<Country> GetAllCountries();
        void UpdateCountry(Country country);
        string AddCountry(Country country);



    }
}