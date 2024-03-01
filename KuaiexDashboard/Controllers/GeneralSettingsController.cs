
using DataAccessLayer;
using KuaiexDashboard.Filters;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class GeneralSettingsController : Controller
    {
        GeneralSettingsDAL objGeneralSettingsDal = new GeneralSettingsDAL();
        // GET: GeneralSettings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TermsConditions()
        {
            return View();
        }

        public ActionResult LoadTermsConditions()
        {
            string status = "error";
            try
            {
                List<GetTermsConditions_Result> result = objGeneralSettingsDal.GetTermsConditions();
                status = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult updateTermsConditions(Terms_and_Privacy objterms)
        {
            string status = "";
            try
            {
                Terms_and_Privacy obj = objGeneralSettingsDal.GetTermsAndPrivacyById(objterms.Id);
                if (obj != null)
                {
                    obj.Title = objterms.Title;
                    obj.Description = objterms.Description;
                    obj.Content_Type = 2;
                    objGeneralSettingsDal.UpdateTermsAndPrivacy(obj);
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult LoadPrivacyPolicy()
        {
            string status = "error";
            try
            {
                List<GetPrivacyPolicy_Result> obj = objGeneralSettingsDal.GetPrivacyPolicy();
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult updatePrivacyPolicy(Terms_and_Privacy objterms)
        {
            string status = "";
            try
            {
                Terms_and_Privacy obj = objGeneralSettingsDal.GetTermsAndPrivacyById(objterms.Id);
                if (obj != null)
                {
                    obj.Title = objterms.Title;
                    obj.Description = objterms.Description;
                    objGeneralSettingsDal.UpdatePrivacyPolicy(objterms);

                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult LoadContactUs()
        {
            string status = "error";
            try
            {
                List<GetContactUs_Result> obj = objGeneralSettingsDal.GetContactUs();
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult updateContactUs(ContactUs objContactUs)
        {
            string status = "";
            try
            {
                ContactUs obj = objGeneralSettingsDal.GetContactUsById(objContactUs.Id);
                if (obj != null)
                {
                    obj.ContactNo = objContactUs.ContactNo;
                    obj.Email = objContactUs.Email;
                    obj.Address = objContactUs.Address;
                    obj.CustomerService = objContactUs.CustomerService;
                    objGeneralSettingsDal.UpdateContactUs(objContactUs);

                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult FAQS()
        {
            return View();
        }
        public ActionResult AddFAQS(FAQs objFAQS)
        {
            string status = "error";
            //if (Authenticated)
            //{
            try
            {
                FAQs obj = objGeneralSettingsDal.GetFAQsByQuestion(objFAQS.Question);

                if (obj != null)
                {
                    status = "exist";
                }
                else
                {
                    if (objFAQS.Status != null)
                        objFAQS.Status = "Y";
                    else
                        objFAQS.Status = "N";
                    objGeneralSettingsDal.AddFAQs(objFAQS);
                    status = "success";
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult LoadGrid()
        {
            string status = "error";
            try
            {
                //if (IsAdminUser)
                //{
                List<GetFAQSList_Result> result = objGeneralSettingsDal.GetFAQSList();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                //}
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult Edit(int Id)
        {
            string status = "error";
            try
            {
                FAQs obj = objGeneralSettingsDal.GetFAQById(Id);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }

        public ActionResult EditFAQS(FAQs objFaQs)
        {
            string status = "";
            try
            {
                FAQs obj = objGeneralSettingsDal.GetFAQById(objFaQs.Id);
                obj.Question = objFaQs.Question;
                obj.Answer = objFaQs.Answer;
                if (objFaQs.Status != null)
                {
                    objFaQs.Status = "Y";
                }
                else
                {
                    objFaQs.Status = "N";
                }
                obj.Status = objFaQs.Status;
                objGeneralSettingsDal.UpdateFAQ(objFaQs);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult CustomerQuries()
        {
            return View();
        }
        public ActionResult LoadGridCustomerQuries()
        {
            string status = "error";
            try
            {
                List<GetCustomerQueries_Result> result = objGeneralSettingsDal.GetCustomerQueries();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
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