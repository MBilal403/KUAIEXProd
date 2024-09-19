using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using KuaiexDashboard.Filters;
using Serilog;
using DataAccessLayer.Entities;
namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class SecurityQuestionsController : Controller
    {
        SequrityQuesDAL objsecDal = new SequrityQuesDAL();
        // GET: SecurityQuestions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQuestions(SecurityQuestions objQuestion)
        {
            string status = "error";
            //if (Authenticated)
            //{
            try
            {
                SecurityQuestions obj = objsecDal.GetSecurityQuestionByQuestion(objQuestion.Question);
                if (obj != null)
                {
                    status = "exist";
                }
                else
                {
                    if (objQuestion.Status != null)
                        objQuestion.Status = "Y";
                    else
                        objQuestion.Status = "N";

                    objQuestion.UID = Guid.NewGuid();
                    objsecDal.AddSecurityQuestion(objQuestion);
                    status = "success";
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            //}
            return Content(status);
        }
        public ActionResult LoadGrid()
        {
            string status = "error";
            //if (IsAdminUser)
            //{
            List<SecurityQuestions> result = objsecDal.GetAllSecurityQuestions();
            status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            //}
            return Content(status);
        }
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                SecurityQuestions obj = objsecDal.GetSecurityQuestionByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult EditQuestions(SecurityQuestions objQuestion)
        {
            string status = "";
            try
            {
                SecurityQuestions obj = objsecDal.GetSecurityQuestionByUID(objQuestion.UID);
                obj.Question = objQuestion.Question;
                objQuestion.Id = obj.Id;
                if (objQuestion.Status != null)
                {
                    objQuestion.Status = "Y";
                }
                else
                {
                    objQuestion.Status = "N";
                }
                obj.Status = objQuestion.Status;
                objsecDal.EditSecurityQuestion(objQuestion);
                status = "Success";
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
    }
}