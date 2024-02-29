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

        public List<Country> GetAllCountries()
        {
           List<Country> countries = _countryRepository.GetAll(x => x.Status == "A", x=>x.Id, x => x.Name);
            return countries;
        }

        public Country GetCountryByName(string countryName)
        {
            throw new NotImplementedException();
        }

        public Country GetCountryByUID(Guid countryId)
        {
            throw new NotImplementedException();
        }

        public int? GetCountryIdByCountryName(string countryName)
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

        public List<GetCountryList_Result> GetCountryList()
        {
            List<GetCountryList_Result> obj = _countryRepository.GetDataFromSP<GetCountryList_Result>("GetCountryList");
            return obj;
        }
        public PagedResult<GetCountryList_Result> GetCountryList(JqueryDatatableParam param)
        {
            PagedResult<GetCountryList_Result> obj = _countryRepository.GetPagedDataFromSP<GetCountryList_Result>("GetAllCountriesWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
           
            return obj;
        }

        public void UpdateCountry(Country objCountry)
        {
            Country obj = objCountryDal.GetCountryByUID(objCountry.UID);
            objCountry.Id = obj.Id;
            objCountry.Name = obj.Name;
            obj.Comission = objCountry.Comission;
            obj.Nationality = objCountry.Nationality;
            obj.Alpha_2_Code = objCountry.Alpha_2_Code;
            obj.Alpha_3_Code = objCountry.Alpha_3_Code;
            obj.City_Id = objCountry.City_Id;
            objCountry.Remittance_Status = obj.Remittance_Status;
            objCountry.High_Risk_Status = obj.High_Risk_Status;
            objCountry.CreatedIp = obj.CreatedIp;
            objCountry.UpdatedIp = obj.UpdatedIp;
            obj.CreatedOn = DateTime.Now;
            obj.Country_Dialing_Code = objCountry.Country_Dialing_Code;

            /*    if (obj.Prod_Country_Id == null || obj.Prod_Country_Id <= 0)
                  {
                      Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                      obj.Prod_Country_Id = objKuaiex_Prod.GetCountryIdByCountryName(obj.Name);
                  }*/

            if (objCountry.Status != null)
            {
                objCountry.Status = "A";
            }
            else
            {
                objCountry.Status = "N";
            }
            obj.Status = objCountry.Status;

            _countryRepository.Update(obj, $" Id = {obj.Id} ");


        }
    }
}