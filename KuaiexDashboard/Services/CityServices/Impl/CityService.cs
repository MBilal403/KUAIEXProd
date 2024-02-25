using DataAccessLayer.Entities;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.CityServices.Impl
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;
        public CityService()
        {
            _cityRepository = new GenericRepository<City>();
        }
        public List<City> GetActiveCities()
        {
            List<City> listCities = _cityRepository.GetDataFromSP<City>("GetCityList");
            return listCities;
        }

    }
}