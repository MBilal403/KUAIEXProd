using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.CityServices
{
    public interface ICityService
    {
        List<City> GetActiveCities();
        string AddCity(City objCity);
        PagedResult<GetCityList_Result> GetActiveCities(JqueryDatatableParam param, int countryId);
        string UpdateCity(City objCity);
        City GetCityByUID(Guid uid);
        int SynchronizeRecords();
    }
}