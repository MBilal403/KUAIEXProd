using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;

namespace KuaiexDashboard.Services.CityServices.Impl
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;
        public CityService()
        {
            _cityRepository = new GenericRepository<City>();
        }

        public string AddCity(City objCity)
        {
            int? existingCity = GetCityIdByCityName(objCity.Name , objCity.Country_Id);
            if (existingCity != null)
            {
                return MsgKeys.DuplicateValueExist;
            }
            else
            {
                objCity.Country_Id = objCity.Country_Id;
                objCity.Status = objCity.Status != null ? 1 : 0;
                objCity.UID = Guid.NewGuid();
                objCity.CreatedOn = DateTime.Now;

                // Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                //objCity.Prod_City_Id = objKuaiex_Prod.GetCityIdByCityName(objCity.Name);

                if (_cityRepository.Insert(objCity) > 0)
                {
                    return MsgKeys.CreatedSuccessfully;
                }
                return MsgKeys.Error;
            }
        }
        private int? GetCityIdByCityName(string cityName,int Country_Id)
        {
            City city = _cityRepository.FindBy(x => x.Name == cityName && x.Country_Id == Country_Id);
            if (city != null)
            {
                return city.Id;
            }
            else
            {
                return null;
            }
        }


        public List<City> GetActiveCities()
        {
            List<City> listCities = _cityRepository.GetDataFromSP<City>("GetCityList");
            return listCities;
        }
        public PagedResult<GetCityList_Result> GetActiveCities(JqueryDatatableParam param)
        {
            PagedResult<GetCityList_Result> list = _cityRepository.GetPagedDataFromSP<GetCityList_Result>("GetCitiesWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
            return list;
        }

        public string UpdateCity(City objCity)
        {
            int? existingCity = GetCityIdByCityName(objCity.Name, objCity.Country_Id);
            if (existingCity != null)
            {
                objCity.Country_Id = objCity.Country_Id;
                objCity.Status = objCity.Status != null ? 1 : 0;
                objCity.UpdatedOn = DateTime.Now;
                //objCity.Prod_City_Id = objKuaiex_Prod.GetCityIdByCityName(objCity.Name);

                _cityRepository.Update(objCity, $" UID = '{objCity.UID}' ");
                return MsgKeys.UpdatedSuccessfully;
            }

            return MsgKeys.Error;

        }

        public City GetCityByUID(Guid uid)
        {
            City city = _cityRepository.FindBy(x => x.UID == uid);
            return city;
        }
    }
}