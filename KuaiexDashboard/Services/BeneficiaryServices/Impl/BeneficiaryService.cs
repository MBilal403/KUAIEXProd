using DataAccessLayer.Recources;
using KuaiexDashboard.DTO.Beneficiary;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using KuaiexDashboard.Services.BeneficiaryServices;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Repository;
using System.Text;
using System.Collections;

namespace KuaiexDashboard.Services.BeneficiaryServices.Impl
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IRepository<Beneficiary> _beneficiaryRepository;
        private readonly IRepository<Bank_Branch_Mst> _bank_Branch_MstRepository;
        private readonly IRepository<Remittance_SubType_Mst> _remittance_SubType_MstRepository;
        private readonly IRepository<Relationship_Lookup> _relationship_LookupRepository;
        private readonly IRepository<Source_Of_Income_Lookup> _source_Of_Income_LookupRepository;
        private readonly IRepository<Bank_Mst> _bank_MstRepository;
        public BeneficiaryService()
        {
            _beneficiaryRepository = new GenericRepository<Beneficiary>(DatabasesName.KUAIEXEntities);
            _bank_Branch_MstRepository = new GenericRepository<Bank_Branch_Mst>(DatabasesName.KUAIEXEntities);
            _remittance_SubType_MstRepository = new GenericRepository<Remittance_SubType_Mst>(DatabasesName.KUAIEXEntities);
            _relationship_LookupRepository = new GenericRepository<Relationship_Lookup>(DatabasesName.KUAIEXEntities);
            _source_Of_Income_LookupRepository = new GenericRepository<Source_Of_Income_Lookup>(DatabasesName.KUAIEXEntities);
            _bank_MstRepository = new GenericRepository<Bank_Mst>(DatabasesName.KUAIEXEntities);
        }

        public string AddBeneficiary(BeneficiaryDTO beneficiaryDto)
        {
            try
            {
                Beneficiary beneficiary = new Beneficiary();
                BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();
                int existenceStatus = objBeneficiaryDAL.CheckBeneficiaryExistence(beneficiaryDto.FullName);

                if (existenceStatus == 1)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                else
                {
                    beneficiary = AutoMapper.Mapper.Map<Beneficiary>(beneficiaryDto);
                    beneficiary.UID = Guid.NewGuid();
                    beneficiary.CreatedOn = DateTime.Now;
                    beneficiary.IsBannedList = default;
                    if (beneficiaryDto.Remittance_Purpose != null)
                    {
                        beneficiary.Remittance_Purpose = string.Join(",", beneficiaryDto.Remittance_Purpose.Reverse().Select(x => x.ToString()));

                    }
                    if (beneficiaryDto.Source_Of_Income != null)
                    {
                        beneficiary.Source_Of_Income = string.Join(",", beneficiaryDto.Source_Of_Income.Reverse().Select(x => x.ToString()));

                    }
                    if (beneficiaryDto.Remitter_Relation != null)
                    {
                        beneficiary.Remitter_Relation = string.Join(",", beneficiaryDto.Remitter_Relation.Reverse().Select(x => x.ToString()));

                    }

                    //Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                    // beneficiary.Prod_Beneficiary_Id = objKuaiex_Prod.GetBeneficiaryIdByIdentificationNumber(beneficiary.Identification_No);
                    if (_beneficiaryRepository.Insert(beneficiary) > 0)
                    {
                        return MsgKeys.CreatedSuccessfully;
                    }
                    return "error";
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }


        }
        public List<Bank_Branch_Mst> GetGetBankBranches(int bankId)
        {
            try
            {
                return _bank_Branch_MstRepository.GetAll(x => x.Bank_Id == bankId, x => x.Bank_Branch_Id, x => x.Branch_Code, x => x.English_Name);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }
        public List<Relationship_Lookup> GetRelationshipLookupList()
        {
            try
            {
                return _relationship_LookupRepository.GetAll(x => x.Status == 1, null);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
        public List<Remittance_SubType_Mst> GetRemittanceSubtypes(int Remittance_Type_Id, int Bank_Id)
        {
            try
            {
                return _remittance_SubType_MstRepository.GetAll(x => x.Remittance_Type_Id == Remittance_Type_Id && x.Bank_Id == Bank_Id,

                        x => x.Remittance_SubType, x => x.Remittance_SubType_Id);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
        public List<Source_Of_Income_Lookup> GetSourceOfIncomeLookupList()
        {
            try
            {
                return _source_Of_Income_LookupRepository.GetAll(x => x.Record_Status == "A", null);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
        public List<Bank_Mst> GetBanksByCountry(int CountryId)
        {
            try
            {
                return _bank_MstRepository.GetAll(x => x.Country_Id == CountryId && x.Record_Status == "A", x => x.Bank_Id, x => x.English_Name, x => x.Bank_Code);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<LoadBeneficiaryDTO> GetBeneficiariesByCustomerID(int CustomerId)
        {
            try
            {
                List<LoadBeneficiaryDTO> beneficiaryDTOs = _beneficiaryRepository.GetDataFromSP<LoadBeneficiaryDTO>("GetBeneficiariesByCustomerID", CustomerId);
                return beneficiaryDTOs;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public BeneficiaryDTO GetBeneficiaryByUID(Guid UID)
        {
            try
            {
                Beneficiary beneficiary = _beneficiaryRepository.FindBy(x => x.UID == UID);
                var beneficiarydto = AutoMapper.Mapper.Map<BeneficiaryDTO>(beneficiary);
                if (!string.IsNullOrEmpty(beneficiary.Source_Of_Income))
                {
                    beneficiarydto.Source_Of_Income = Array.ConvertAll(beneficiary.Source_Of_Income.Split(','), s => string.IsNullOrEmpty(s) ? (int?)null : int.Parse(s));

                }
                if (!string.IsNullOrEmpty(beneficiary.Remitter_Relation))
                {
                    beneficiarydto.Remitter_Relation = Array.ConvertAll(beneficiary.Remitter_Relation.Split(','), s => string.IsNullOrEmpty(s) ? (int?)null : int.Parse(s));

                }
                if (!string.IsNullOrEmpty(beneficiary.Remittance_Purpose))
                {
                    beneficiarydto.Remittance_Purpose = Array.ConvertAll(beneficiary.Remittance_Purpose.Split(','), s => string.IsNullOrEmpty(s) ? (int?)null : int.Parse(s));

                }

                return beneficiarydto;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public string Updatebeneficiary(BeneficiaryDTO beneficiary)
        {
            try
            {
                Guid beneficiaryUID = beneficiary.UID;
                Beneficiary existingBeneficiary = _beneficiaryRepository.FindBy(x => x.UID == beneficiaryUID);

                if (existingBeneficiary != null)
                {
                    existingBeneficiary.FullName = beneficiary.FullName;
                    existingBeneficiary.Country_Id = beneficiary.Country_Id;
                    existingBeneficiary.Currency_Id = beneficiary.Currency_Id;
                    existingBeneficiary.Nationality_Id = beneficiary.Nationality_Id;
                    existingBeneficiary.Remittance_Purpose = string.Join(",", beneficiary.Remittance_Purpose.Select(x => x.ToString()));
                    existingBeneficiary.Source_Of_Income = string.Join(",", beneficiary.Source_Of_Income.Select(x => x.ToString()));
                    existingBeneficiary.Remitter_Relation = string.Join(",", beneficiary.Remitter_Relation.Select(x => x.ToString()));
                    existingBeneficiary.Remittance_Type_Id = beneficiary.Remittance_Type_Id;
                    existingBeneficiary.Mobile_No = beneficiary.Mobile_No;
                    existingBeneficiary.Bank_Id = beneficiary.Bank_Id;
                    existingBeneficiary.Bank_Code = beneficiary.Bank_Code;
                    existingBeneficiary.Branch_Id = beneficiary.Branch_Id;
                    existingBeneficiary.Branch_Number = beneficiary.Branch_Number;
                    existingBeneficiary.Bank_Account_No = beneficiary.Bank_Account_No;
                    existingBeneficiary.Remittance_Purpose_Detail = beneficiary.Remittance_Purpose_Detail;
                    existingBeneficiary.Remitter_Relation_Detail = beneficiary.Remitter_Relation_Detail;
                    existingBeneficiary.Source_Of_Income_Detail = beneficiary.Source_Of_Income_Detail;
                    existingBeneficiary.Remittance_Subtype_Id = beneficiary.Remittance_Subtype_Id;
                    existingBeneficiary.UpdatedOn = DateTime.Now;



                    /*     if (obj.Prod_Beneficiary_Id == null || obj.Prod_Beneficiary_Id <= 0)
                         {
                             Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();
                             obj.Prod_Beneficiary_Id = objKuaiex_Prod.GetBeneficiaryIdByIdentificationNumber(obj.Identification_No);
                         }*/
                    // obj.Remitter_Relation_Id = objBeneficiary.Remitter_Relation_Id;

                    if (_beneficiaryRepository.Update(existingBeneficiary, $" Beneficiary_Id = {existingBeneficiary.Beneficiary_Id} "))
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