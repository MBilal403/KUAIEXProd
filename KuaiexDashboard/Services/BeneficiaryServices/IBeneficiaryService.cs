using DataAccessLayer.Entities;
using KuaiexDashboard.DTO.Beneficiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.BeneficiaryServices
{
    interface IBeneficiaryService
    {
        string AddBeneficiary(BeneficiaryDTO beneficiaryDto);
        List<Bank_Branch_Mst> GetGetBankBranches(int bankId);
        List<Remittance_SubType_Mst> GetRemittanceSubtypes(int Remittance_Type_Id, int Bank_Id);
        List<Relationship_Lookup> GetRelationshipLookupList();
        List<Source_Of_Income_Lookup> GetSourceOfIncomeLookupList();
        List<Bank_Mst> GetBanksByCountry(int CountryId);
        List<BeneficiaryDTO> GetAllBeneficiary();



    }
}
