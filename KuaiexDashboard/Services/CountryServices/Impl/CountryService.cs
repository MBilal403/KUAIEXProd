using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;

namespace KuaiexDashboard.Services.CountryServices.Impl
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryService()
        {
            _countryRepository = new GenericRepository<Country>(DatabasesName.KUAIEXEntities);
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
                    objCountry.UpdatedIp = "127.0.0.1";
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

        public string UpdateCountry(Country objCountry)
        {
            try
            {
                Guid guid = objCountry.UID ?? Guid.NewGuid();
                Country existingCountry = _countryRepository.FindBy(x => x.UID == guid);

                if (existingCountry != null)
                {
                    existingCountry.UpdatedOn = DateTime.Now;
                    existingCountry.Alpha_2_Code = objCountry.Alpha_2_Code;
                    existingCountry.Alpha_3_Code = objCountry.Alpha_3_Code;
                    existingCountry.Country_Dialing_Code = objCountry.Country_Dialing_Code;
                    existingCountry.City_Id = objCountry.City_Id;
                    existingCountry.Comission = objCountry.Comission;
                    existingCountry.Status = objCountry.Status != null ? "A" : "N";
                    if (_countryRepository.Update(existingCountry, $" Id = {existingCountry.Id} ") > 0)
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

        /*    if (obj.Prod_Country_Id == null || obj.Prod_Country_Id <= 0)
              {
                  Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                  obj.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(obj.Name);
              }*/
    }
}