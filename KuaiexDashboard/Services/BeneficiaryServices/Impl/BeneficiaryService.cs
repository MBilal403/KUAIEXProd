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
            _beneficiaryRepository = new GenericRepository<Beneficiary>();
            _bank_Branch_MstRepository = new GenericRepository<Bank_Branch_Mst>();
            _remittance_SubType_MstRepository = new GenericRepository<Remittance_SubType_Mst>();
            _relationship_LookupRepository = new GenericRepository<Relationship_Lookup>();
            _source_Of_Income_LookupRepository = new GenericRepository<Source_Of_Income_Lookup>();
            _bank_MstRepository = new GenericRepository<Bank_Mst>();
        }

        public string AddBeneficiary(BeneficiaryDTO beneficiaryDto)
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
                beneficiaryDto.UID = Guid.NewGuid();
                var Ben = AutoMapper.Mapper.Map<Beneficiary>(beneficiaryDto);
                Kuaiex_Prod objKuaiex_Prod = new Kuaiex_Prod();

                _beneficiaryRepository.Insert(Ben);

                //if (beneficiaryId > 0)
                //{
                //    status = "success";
                //}*/
            }

            return MsgKeys.CreatedSuccessfully;
        }
        public List<Bank_Branch_Mst> GetGetBankBranches(int bankId)
        {
            return _bank_Branch_MstRepository.GetAll(x => x.Bank_Id == bankId, x => x.Bank_Branch_Id, x => x.Branch_Code, x => x.English_Name);

        }
        public List<Relationship_Lookup> GetRelationshipLookupList()
        {
            return _relationship_LookupRepository.GetAll(x => x.Status == 1, null);
        }
        public List<Remittance_SubType_Mst> GetRemittanceSubtypes(int Remittance_Type_Id, int Bank_Id)
        {
            return _remittance_SubType_MstRepository.GetAll(x => x.Remittance_Type_Id == Remittance_Type_Id && x.Bank_Id == Bank_Id,

                x => x.Remittance_SubType, x => x.Remittance_SubType_Id);
        }
        public List<Source_Of_Income_Lookup> GetSourceOfIncomeLookupList()
        {
            return _source_Of_Income_LookupRepository.GetAll(x => x.Record_Status == "A", null);
        }
        public List<Bank_Mst> GetBanksByCountry(int CountryId)
        {
            return _bank_MstRepository.GetAll(x => x.Country_Id == CountryId && x.Record_Status == "A", x => x.Bank_Id, x => x.English_Name, x => x.Bank_Code);
        }

        public List<BeneficiaryDTO> GetAllBeneficiary()
        {
            var joins = new List<JoinInfo>
            {
                 new JoinInfo { JoinType = "INNER", TargetTable = "Customer", JoinCondition = "Beneficiary.Customer_Id = Customer.Customer_Id" },
                 new JoinInfo { JoinType = "INNER", TargetTable = "Country", JoinCondition = "Beneficiary.Country_Id = Country.Id" },
                 new JoinInfo { JoinType = "INNER", TargetTable = "Currency", JoinCondition = "Beneficiary.Currency_Id = Country.Id" }
            };
            var columns = new StringBuilder();
            columns.Append(new ObjectInspector<Beneficiary>().ObjectInspect(
                x => x.UID,
                x => x.Bank_Id,
                x => x.FullName,
                x => x.Birth_Date,
                x => x.Address_Line3,
                x => x.Branch_Address_Line3,
                x => x.Bank_Name
                ));
            columns.Append(", ");
            columns.Append(new ObjectInspector<Customer>().ObjectInspect(
               x => x.Name
               ));
            columns.Append(", ");
            columns.Append(new ObjectInspector<Country>().ObjectInspect(
               x => x.Name
              ));
            columns.Append(", ");
            columns.Append(new ObjectInspector<Currency>().ObjectInspect(
               x => x.Name
              ));



            return _beneficiaryRepository.GetAllWithJoins<BeneficiaryDTO>(joins,null, columns.ToString());
        }

    }
}