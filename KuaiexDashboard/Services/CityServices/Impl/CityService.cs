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
            try
            {
                City existingCity = GetCityIdByCityName(objCity.Name, objCity.Country_Id);
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
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong ,ex);
            }
            return MsgKeys.Error;
        }
        private City GetCityIdByCityName(string cityName, int Country_Id)
        {
            try
            {
                City city = _cityRepository.FindBy(x => x.Name == cityName && x.Country_Id == Country_Id);
                if (city != null)
                {
                    return city;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }


        public List<City> GetActiveCities()
        {
            try
            {
                List<City> listCities = _cityRepository.GetDataFromSP<City>("GetCityList");
                return listCities;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
        public PagedResult<GetCityList_Result> GetActiveCities(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<GetCityList_Result> list = _cityRepository.GetPagedDataFromSP<GetCityList_Result>("GetCitiesWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
                return list;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
          
        }

        public string UpdateCity(City objCity)
        {
            try
            {
                City existingCity = GetCityIdByCityName(objCity.Name, objCity.Country_Id);
                if (existingCity != null)
                {
                    existingCity.Country_Id = objCity.Country_Id;
                    existingCity.UpdatedOn = DateTime.Now;
                    //objCity.Prod_City_Id = objKuaiex_Prod.GetCityIdByCityName(objCity.Name);

                    _cityRepository.Update(existingCity, $" UID = '{objCity.UID}' ");
                    return MsgKeys.UpdatedSuccessfully;
                }

                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }

        public City GetCityByUID(Guid uid)
        {
            try
            {
                City city = _cityRepository.FindBy(x => x.UID == uid);
                return city;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}