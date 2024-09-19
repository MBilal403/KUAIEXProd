using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DomainEntities;


namespace KuaiexDashboard.Services.AgencyBranchesServices
{
    internal interface IAgencyBranchesService
    {
        BranchesInfo GetBranchesInfoByName(string name);
        bool AddBranchesInfo(BranchesInfo objBranchesInfo);
        List<BranchesInfo> GetBrancheskInfoList();
        BranchesInfo GetBranchesInfoById(int branchId);
        bool EditBranchesInfo(BranchesInfo objBranchesInfo);
    }
}
