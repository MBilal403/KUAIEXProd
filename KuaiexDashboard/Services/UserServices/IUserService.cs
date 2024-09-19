using System;
using System.Collections.Generic;
using DataAccessLayer.DomainEntities;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.UserServices
{
    public interface IUserService
    {
        List<GetUsersList_Result> GetUsersList();
        Users GetUserByUserName(string userName);
        bool RegisterUser(Users user);
        Users AuthenticateUser(Users objUser);
        List<UserType_Lookup> GetUserTypes();
        Users GetUserByUID(Guid? uid);
        bool UpdateUser(Users user);
        
    }
}