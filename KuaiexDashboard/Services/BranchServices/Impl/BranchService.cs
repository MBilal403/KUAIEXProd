using Bank_Branch_Mst = DataAccessLayer.Entities.Bank_Branch_Mst;
using Bank_Mst = DataAccessLayer.Entities.Bank_Mst;
using DataAccessLayer.Helpers;
using Bank_Branch_Mst_Prod = DataAccessLayer.ProdEntities.Bank_Branch_Mst;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using DataAccessLayer.ProdEntities;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Impl;
using DataAccessLayer.ProcedureResults;

namespace KuaiexDashboard.Services.BranchServices.Impl
{
    public class BranchService : IBranchService
    {
        public readonly IRepository<Bank_Branch_Mst> _branchRepository;
        public readonly IRepository<Bank_Branch_Mst_Prod> _branchProdRepository;
        public readonly IRepository<City> _cityRepository;
        public readonly IRepository<Country> _countryRepository;
        public readonly IRepository<Bank_Mst> _bankRepository;

        public BranchService()
        {
            _branchRepository = new GenericRepository<Bank_Branch_Mst>(DatabasesName.KUAIEXEntities);
            _cityRepository = new GenericRepository<City>(DatabasesName.KUAIEXEntities);
            _countryRepository = new GenericRepository<Country>(DatabasesName.KUAIEXEntities);
            _bankRepository = new GenericRepository<Bank_Mst>(DatabasesName.KUAIEXEntities);
            _branchProdRepository = new GenericRepository<Bank_Branch_Mst_Prod>(DatabasesName.KUAIEXProdEntities);

        }

        public PagedResult<GetBankBranchesList_Result> GetAllBankBranches(JqueryDatatableParam param, int countryId, int bankId)
        {
            try
            {
                param.sSearch = string.Join("|", param.sSearch ?? "", countryId.ToString() ?? "", bankId.ToString() ?? "");
                PagedResult<GetBankBranchesList_Result> list = _branchRepository.GetPagedDataFromSP<GetBankBranchesList_Result>("GetBankBranchesWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
                return list;
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
                List<Bank_Branch_Mst_Prod> prodBankBranches = _branchProdRepository.GetAll(x => x.City_Id > 0 && x.Bank_Id > 0 && x.Country_Id > 0);
                List<Bank_Branch_Mst> bankBranches = _branchRepository.GetAll();
                List<City> cities = _cityRepository.GetAll(null, x => x.Id, x => x.Prod_City_Id, x => x.Prod_City_Ids);
                List<Country> countries = _countryRepository.GetAll(null, x => x.Id, x => x.Prod_Country_Id, x => x.Prod_Country_Ids);
                List<Bank_Mst> banks = _bankRepository.GetAll(null, x => x.Bank_Id, x => x.Prod_Bank_Id);

                foreach (var prodBankBranch in prodBankBranches)
                {
                    var matchingCountry = countries.FirstOrDefault(c => c.Prod_Country_Id == prodBankBranch.Country_Id);
                    var matchingBank = banks.FirstOrDefault(c => c.Prod_Bank_Id == prodBankBranch.Bank_Id);
                    var matchingCity = cities.FirstOrDefault(c => c.Prod_City_Id == prodBankBranch.City_Id);

                    if (matchingCity != null)
                    {
                        prodBankBranch.City_Id = matchingCity.Id;
                    }
                    else
                    {
                        foreach (var pendingCity in cities.Where(x => x.Prod_City_Ids != null))
                        {
                            int[] cityIdsInt = pendingCity.Prod_City_Ids?.Split(',').Select(int.Parse).ToArray();
                            if (cityIdsInt.Contains(prodBankBranch.City_Id ?? 0))
                            {
                                prodBankBranch.City_Id = cityIdsInt[0];
                                break;
                            }
                        }
                    }
                    if (matchingCountry != null)
                    {
                        prodBankBranch.Country_Id = matchingCountry.Id;
                        prodBankBranch.Bank_Id = matchingBank.Bank_Id;
                    }
                    else
                    {
                        foreach (var pendingCountry in countries.Where(x => x.Prod_Country_Ids != null))
                        {
                            int[] countryIdsInt = pendingCountry.Prod_Country_Ids?.Split(',').Select(int.Parse).ToArray();
                            if (countryIdsInt.Contains(prodBankBranch.Country_Id ?? 0))
                            {
                                prodBankBranch.Country_Id = countryIdsInt[0];
                                break;
                            }
                        }
                    }
                }

                var missingBankBranches = prodBankBranches
                            .Where(pbb => !bankBranches.Any(bb => bb.Prod_Bank_Branch_Id == pbb.Bank_Branch_Id))
                          .ToList();



                foreach (var item in missingBankBranches)
                {
                    Bank_Branch_Mst temp = new Bank_Branch_Mst();
                    temp.Bank_Id = item.Bank_Id;
                    temp.Branch_Code = item.Branch_Code;
                    temp.English_Name = item.English_Name;
                    temp.Arabic_Name = item.Arabic_Name;
                    temp.City_Id = item.City_Id;
                    temp.Country_Id = item.Country_Id;
                    temp.Currency_Id = item.Currency_Id;
                    temp.Upper_Limit = item.Upper_Limit;
                    temp.Account_Reference = item.Account_Reference;
                    temp.Reference_Location = item.Reference_Location;
                    temp.Address_Line1 = item.Address_Line1;
                    temp.Address_Line2 = item.Address_Line2;
                    temp.Address_Line3 = item.Address_Line3;
                    temp.Tel_Number1 = item.Tel_Number1;
                    temp.Fax_Number1 = item.Fax_Number1;
                    temp.EMail_Address1 = item.EMail_Address1;
                    temp.Contact_Person1 = item.Contact_Person1;
                    temp.Contact_Title1 = item.Contact_Title1;
                    temp.Contact_Person2 = item.Contact_Person2;
                    temp.Contact_Title2 = item.Contact_Title2;
                    temp.Draft_Status = item.Draft_Status;
                    temp.Record_Status = item.Record_Status;
                    temp.Option_Status = item.Option_Status;
                    temp.Updated_Date = item.Updated_Date;
                    temp.Updated_User = item.Updated_User;
                    temp.Bank_SubAgent_Id = item.Bank_SubAgent_Id;
                    temp.Prod_Bank_Branch_Id = item.Bank_Branch_Id;


                    if (_branchRepository.Insert(temp) > 0)
                    {
                        count++;
                    }
                    else
                    {
                        throw new Exception(MsgKeys.SomethingWentWrong);
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