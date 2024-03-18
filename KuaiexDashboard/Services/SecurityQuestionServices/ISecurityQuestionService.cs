using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.SecurityQuestionServices
{
    internal interface ISecurityQuestionService
    {
        string AddSecurityQuestion(SecurityQuestions securityQuestions);
        SecurityQuestions GetSecurityQuestionByQuestion(string securityQuestion);
        List<SecurityQuestions> GetAllSecurityQuestions();
        SecurityQuestions GetSecurityQuestionByUID(Guid uid);
        string EditSecurityQuestion(SecurityQuestions securityQuestion);

    }
}
