using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Recources;
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

                 if(_countryRepository.Insert(objCountry) > 0)
                {
                    return MsgKeys.CreatedSuccessfully;
                }
                 return MsgKeys.Error;
            }
        }

        public List<Country> GetAllCountries()
        {
            throw new NotImplementedException();
        }

        public Country GetCountryByName(string countryName)
        {
            throw new NotImplementedException();
        }

        public Country GetCountryByUID(Guid countryId)
        {
            throw new NotImplementedException();
        }

        public int GetCountryIdByCountryName(string countryName)
        {
           Country country = _countryRepository.FindBy(x=> x.Name == countryName);
           return country.Id;
            
        }

        public List<GetCountryList_Result> GetCountryList()
        {
            List<GetCountryList_Result> obj =  _countryRepository.GetDataFromSP<GetCountryList_Result>("GetCountryList");
            return obj;
        }

        public void UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}