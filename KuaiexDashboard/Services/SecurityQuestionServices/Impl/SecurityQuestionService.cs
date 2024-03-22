using DataAccessLayer.Entities;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.SecurityQuestionServices.Impl
{
    public class SecurityQuestionService : ISecurityQuestionService
    {
        private readonly IRepository<SecurityQuestions> _securityQuestionRepository;
        public SecurityQuestionService()
        {
            _securityQuestionRepository = new GenericRepository<SecurityQuestions>(DatabasesName.KUAIEXEntities);
        }
        public string AddSecurityQuestion(SecurityQuestions securityQuestions)
        {
            try
            {
                SecurityQuestions obj = GetSecurityQuestionByQuestion(securityQuestions.Question);
                if (obj != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                else
                {
                    if (securityQuestions.Status != null)
                        securityQuestions.Status = "Y";
                    else
                        securityQuestions.Status = "N";

                    securityQuestions.UID = Guid.NewGuid();
                    if (_securityQuestionRepository.Insert(securityQuestions) > 0)
                        return MsgKeys.CreatedSuccessfully;
                    return MsgKeys.Error;
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public string EditSecurityQuestion(SecurityQuestions securityQuestion)
        {
            try
            {
                throw new Exception(MsgKeys.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public List<SecurityQuestions> GetAllSecurityQuestions()
        {
            try
            {
                throw new Exception(MsgKeys.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public SecurityQuestions GetSecurityQuestionByQuestion(string securityQuestion)
        {
            try
            {
                throw new Exception(MsgKeys.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }

        public SecurityQuestions GetSecurityQuestionByUID(Guid uid)
        {
            try
            {
                throw new Exception(MsgKeys.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}