using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.CountryServices.Impl
{
    public class CountryService : ICountryService
    {
        private readonly CountryDAL objCountryDal;
        private readonly Kuaiex_Prod objKuaiex_Prod;
        private readonly IRepository<Country> _countryRepository;
        public CountryService()
        {
            objCountryDal = new CountryDAL();
            objKuaiex_Prod = new Kuaiex_Prod();
            _countryRepository = new GenericRepository<Country>();
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
                    return MsgKeys.Error;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                throw new Exception(MsgKeys.SomethingWentWrong);
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
                    objCountry.UpdatedOn = DateTime.Now;
                    objCountry.Id = existingCountry.Id;
                    objCountry.CreatedOn = existingCountry.CreatedOn;
                    objCountry.UID = existingCountry.UID;
                    objCountry.Prod_Country_Id = existingCountry.Prod_Country_Id;
                    objCountry.Status = objCountry.Status != null ? "A" : "N";
                    if (_countryRepository.Update(objCountry, $" Id = {objCountry.Id} ") > 0)
                    {
                        return MsgKeys.UpdatedSuccessfully;
                    }
                    return MsgKeys.Error;
                }
                return MsgKeys.Error;

                /*    if (obj.Prod_Country_Id == null || obj.Prod_Country_Id <= 0)
                      {
                          Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                          obj.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(obj.Name);
                      }*/
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong);
            }
        }
    }
}