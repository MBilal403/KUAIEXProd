using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.ProdEntities;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DTO.Beneficiary;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KuaiexDashboard.Services.CityServices.Impl
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<City_Mst> _cityProdRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Remittance_Currency_Limit> _remittance_Currency_LimitRepository;

        public CityService()
        {
            _cityRepository = new GenericRepository<City>(DatabasesName.KUAIEXEntities);
            _countryRepository = new GenericRepository<Country>(DatabasesName.KUAIEXEntities);
            _cityProdRepository = new GenericRepository<City_Mst>(DatabasesName.KUAIEXProdEntities);
            _remittance_Currency_LimitRepository = new GenericRepository<Remittance_Currency_Limit>(DatabasesName.KUAIEXEntities);
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
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
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

        public List<City> GetKuwaitActiveCities(int countryId)
        {
            try
            {
                List<City> listCities = _cityRepository.GetAll(x => x.Status == 1 && x.Country_Id == countryId, x => x.Id, x => x.Name);
                return listCities;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
        public PagedResult<GetCityList_Result> GetActiveCities(JqueryDatatableParam param, int countryId)
        {
            try
            {
                param.sSearch = string.Join("|", param.sSearch ?? "", countryId.ToString() ?? "");
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
                    existingCity.Status = objCity.Status != null ? 1 : 0;
                    existingCity.UpdatedOn = DateTime.Now;
                    if (_cityRepository.Update(existingCity, $" UID = '{objCity.UID}' "))
                    {
                        return MsgKeys.UpdatedSuccessfully;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.UpdationFailed);
                    }
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

        public int SynchronizeRecords()
        {
            int count = 0;
            try
            {
                List<City_Mst> prodCities = _cityProdRepository.GetAll();
                List<Country> countries = _countryRepository.GetAll(null, x => x.Id, x => x.Prod_Country_Id);

                foreach (var item in prodCities)
                {
                    string CityName = Strings.EscapeSingleQuotes(item.English_Name);
                    int CountryId = item.Country_Id;
                    Country country = countries.Find(x => x.Prod_Country_Id == CountryId);
                    if (country == null)
                    {
                        foreach (var pendingCountry in countries.Where(x => x.Prod_Country_Ids != null))
                        {
                            int[] countryIdsInt = pendingCountry.Prod_Country_Ids?.Split(',').Select(int.Parse).ToArray();

                            if (countryIdsInt.Contains(CountryId))
                            {
                                item.Country_Id = countryIdsInt[0];
                                break;
                            }
                        }
                    }
                    int countryId = item.Country_Id;
                    if (!_cityRepository.Any(x => x.Name == CityName && x.Country_Id == countryId))
                    {
                        City city = new City();
                        var pendingCities = prodCities.Where(x => x.English_Name == item.English_Name && x.Country_Id == item.Country_Id).ToList();
                        if (pendingCities.Count > 1)
                        {
                            city.Prod_City_Ids = string.Join(",", pendingCities.Select(x => x.City_Id.ToString()));
                        }
                        city.Name = item.English_Name;
                        city.Status = item.Record_Status == "A" ? 1 : 0;
                        city.Prod_City_Id = item.City_Id;
                        city.Country_Id = countryId;
                        city.UID = Guid.NewGuid();
                        city.CreatedOn = DateTime.Now;
                        if (_cityRepository.Insert(city) > 0)
                        {
                            count++;
                        }
                        else
                        {
                            throw new Exception(MsgKeys.SomethingWentWrong);
                        }
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