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

namespace KuaiexDashboard.Services.BeneficiaryServices.Impl
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IRepository<Beneficiary> _beneficiaryRepository;
        private readonly IRepository<Bank_Branch_Mst> _bank_Branch_MstRepository;
        private readonly IRepository<Remittance_SubType_Mst> _remittance_SubType_MstRepository;
        public BeneficiaryService()
        {
            _beneficiaryRepository = new GenericRepository<Beneficiary>();
            _bank_Branch_MstRepository = new GenericRepository<Bank_Branch_Mst>();
            _remittance_SubType_MstRepository = new GenericRepository<Remittance_SubType_Mst>();
        }

        
        public string AddBeneficiary(BeneficiaryDTO beneficiaryDto)
        {
            Beneficiary beneficiary = new Beneficiary();
            BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();
            int existenceStatus = objBeneficiaryDAL.CheckBeneficiaryExistence(beneficiaryDto.FullName);
            if (existenceStatus == 1)
            {
              return  MsgKeys.DuplicateValueExist;
            }
            else
            {
                beneficiaryDto.UID = Guid.NewGuid();

                Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();

                _beneficiaryRepository.Insert(beneficiary);

                // objBeneficiary.Prod_Beneficiary_Id = objKuaiex_Prod.GetBeneficiaryIdByIdentificationNumber(objBeneficiary.Identification_No);
                //  int beneficiaryId = objBeneficiaryDAL.AddBeneficiary(objBeneficiary);
                /*
                                if (beneficiaryId > 0)
                                {
                                    status = "success";
                                }*/
            }

            return MsgKeys.CreatedSuccessfully;
        }

        public IEnumerable<Bank_Branch_Mst> GetGetBankBranches(int bankId)
        {
           return _bank_Branch_MstRepository.GetAll(x => x.Bank_Id == bankId , "Bank_Branch_Id", "Branch_Code", "English_Name");

        }

        public IEnumerable<Remittance_SubType_Mst> GetRemittanceSubtypes(int Remittance_Type_Id, int Bank_Id)
        {
          return _remittance_SubType_MstRepository.GetAll(x => x.Remittance_Type_Id == Remittance_Type_Id && x.Bank_Id == Bank_Id, "Remittance_SubType_Id", "Remittance_SubType");
        }
    }
}