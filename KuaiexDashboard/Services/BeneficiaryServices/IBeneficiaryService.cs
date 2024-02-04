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
        IEnumerable<Bank_Branch_Mst> GetGetBankBranches(int bankId);
        IEnumerable<Remittance_SubType_Mst> GetRemittanceSubtypes(int Remittance_Type_Id, int Bank_Id);
    }
}
