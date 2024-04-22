using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.ProdEntities;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;

namespace KuaiexDashboard.Services.CountryServices.Impl
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Country_Mst> _countryProdRepository;
        public CountryService()
        {
            _countryRepository = new GenericRepository<Country>(DatabasesName.KUAIEXEntities);
            _countryProdRepository = new GenericRepository<Country_Mst>(DatabasesName.KUAIEXProdEntities);
        }

        public string AddCountry(Country objCountry)
        {
            try
            {
                int? existingCountry = GetCountryIdByCountryName(objCountry.Name);

                if (existingCountry != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                else
                {
                    objCountry.Status = objCountry.Status != null ? "A" : "N";
                    objCountry.Remittance_Status = "Y";
                    objCountry.High_Risk_Status = "A";
                    objCountry.CreatedOn = DateTime.Now;
                    objCountry.CreatedIp = "127.0.0.1";
                    objCountry.UID = Guid.NewGuid();
                    // objCountry.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(objCountry.Name);

                    if (_countryRepository.Insert(objCountry) > 0)
                    {
                        return MsgKeys.CreatedSuccessfully;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.CreatedFailed);
                    }
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }

        public List<GetCountryList_Result> GetActiveCountryList()
        {
            try
            {
                List<GetCountryList_Result> obj = _countryRepository.GetDataFromSP<GetCountryList_Result>("GetActiveCountryList");
                return obj;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }

        public List<Country> GetAllCountries()
        {
            try
            {
                List<Country> countries = _countryRepository.GetAll(x => x.Status == "A", x => x.Id, x => x.Name);
                return countries;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }

        public Country GetCountryByName(string countryName)
        {
            throw new NotImplementedException();
        }

        public Country GetCountryByUID(Guid UID)
        {
            try
            {
                Country existingCountry = _countryRepository.FindBy(x => x.UID == UID);
                return existingCountry;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }

        public int? GetCountryIdByCountryName(string countryName)
        {
            try
            {
                Country country = _countryRepository.FindBy(x => x.Name == countryName);
                if (country != null)
                {
                    return country.Id;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }

        }

        public List<GetCountryList_Result> GetCountryList()
        {
            try
            {
                List<GetCountryList_Result> obj = _countryRepository.GetDataFromSP<GetCountryList_Result>("GetCountryList");
                return obj;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }
        public PagedResult<GetCountryList_Result> GetCountryList(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<GetCountryList_Result> obj = _countryRepository.GetPagedDataFromSP<GetCountryList_Result>("GetAllCountriesWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);

                return obj;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }

        public int SynchronizeRecords()
        {
            int count = 0;
            try
            {
                List<Country_Mst> prodCountries = _countryProdRepository.GetAll();

                foreach (var item in prodCountries)
                {
                    string CountryName = Strings.EscapeSingleQuotes(item.English_Name);
                    string NationalityName = item.English_Nationality;
                  
                    if (!_countryRepository.Any(x => x.Name == CountryName && x.Nationality == NationalityName))
                    {
                        Country country = new Country();
                        var pendingCountries = prodCountries.Where(x=> x.English_Name == item.English_Name && x.English_Nationality == item.English_Nationality).ToList();
                        if (pendingCountries.Count > 1)
                        {
                            country.Prod_Country_Ids = string.Join(",", pendingCountries.Select(x => x.Country_Id.ToString()));
                        }
                        country.Name = item.English_Name;
                        country.Nationality = item.English_Nationality; 
                        country.Status = item.Record_Status;
                        country.Alpha_2_Code = item.Country_Code;
                        country.Remittance_Status = item.Remittance_Status;
                        country.City_Id = item.Any_City_Id;
                        country.High_Risk_Status = item.High_Risk_Status;
                        country.Under_Review_Status = item.Under_Review_Status;
                        country.Prod_Country_Id = item.Country_Id;
                        country.UID = Guid.NewGuid();
                        country.CreatedOn = DateTime.Now;
                        if (_countryRepository.Insert(country) > 0)
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

        public string UpdateCountry(Country objCountry)
        {
            try
            {
                Guid guid = objCountry.UID;
                Country existingCountry = _countryRepository.FindBy(x => x.UID == guid);

                if (existingCountry != null)
                {
                    existingCountry.UpdatedOn = DateTime.Now;
                    existingCountry.Alpha_2_Code = objCountry.Alpha_2_Code;
                    existingCountry.Alpha_3_Code = objCountry.Alpha_3_Code;
                    existingCountry.Country_Dialing_Code = objCountry.Country_Dialing_Code;
                    existingCountry.Status = objCountry.Status != null ? "A" : "N";
                    existingCountry.Under_Review_Status = objCountry.Under_Review_Status != null ? "A" : "N";
                    existingCountry.High_Risk_Status = objCountry.High_Risk_Status != null ? "A" : "N";
                    if (_countryRepository.Update(existingCountry, $" Id = {existingCountry.Id} ") )
                    {
                        return MsgKeys.UpdatedSuccessfully;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.UpdationFailed);
                    }
                }
                else
                {
                    throw new Exception(MsgKeys.ObjectNotExists);
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw;
            }
        }


    }
}