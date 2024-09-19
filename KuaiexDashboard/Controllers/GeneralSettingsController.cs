
using DataAccessLayer.DomainEntities;
using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.GeneralSettingsServices;
using KuaiexDashboard.Services.GeneralSettingsServices.Impl;
using KuaiexDashboard.Services.RelationshipServices;
using KuaiexDashboard.Services.RelationshipServices.Impl;
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
        private readonly IGeneralSettingsService _generalSettingsService;
        private readonly IRelationshipService _relationshipService;
        public GeneralSettingsController()
        {
            _relationshipService = new RelationshipService();
            _generalSettingsService = new GeneralSettingsService();
        }

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
                List<GetTermsConditions_Result> result = _generalSettingsService.GetTermsConditions();
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
                Terms_and_Privacy obj = _generalSettingsService.GetTermsAndPrivacyById(objterms.Id);
                if (obj != null)
                {
                    obj.Title = objterms.Title;
                    obj.Description = objterms.Description;
                    obj.Content_Type = 2;
                    _generalSettingsService.UpdateTermsAndPrivacy(obj);
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
                List<GetPrivacyPolicy_Result> obj = _generalSettingsService.GetPrivacyPolicy();
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
                Terms_and_Privacy obj = _generalSettingsService.GetTermsAndPrivacyById(objterms.Id);
                if (obj != null)
                {
                    obj.Title = objterms.Title;
                    obj.Description = objterms.Description;
                    _generalSettingsService.UpdatePrivacyPolicy(objterms);

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
                List<GetContactUs_Result> obj = _generalSettingsService.GetContactUs();
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
                ContactUs obj = _generalSettingsService.GetContactUsById(objContactUs.Id);
                if (obj != null)
                {
                    obj.ContactNo = objContactUs.ContactNo;
                    obj.Email = objContactUs.Email;
                    obj.Address = objContactUs.Address;
                    obj.CustomerService = objContactUs.CustomerService;
                    _generalSettingsService.UpdateContactUs(objContactUs);

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
                FAQs obj = _generalSettingsService.GetFAQsByQuestion(objFAQS.Question);

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
                    _generalSettingsService.AddFAQs(objFAQS);
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
                List<GetFAQSList_Result> result = _generalSettingsService.GetFAQSList();
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
                FAQs obj = _generalSettingsService.GetFAQById(Id);
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
                FAQs obj = _generalSettingsService.GetFAQById(objFaQs.Id);
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
                _generalSettingsService.UpdateFAQ(objFaQs);
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
                List<GetCustomerQueries_Result> result = _generalSettingsService.GetCustomerQueries();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult Relationships()
        {
            return View();
        }

        public ActionResult AddRelationship(Relationship_Lookup relationship_Lookup)
        {
            string status = "error";
            try
            {

                status = _relationshipService.AddRelationship(relationship_Lookup);

            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);


        }

        public ActionResult LoadGridRelationship()
        {
            string status = "error";
            try
            {
                List<Relationship_Lookup> list = _relationshipService.GetActiveRelationships();

                status = JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult GetRelationship(int Id)
        {
            string status = "error";
            try
            {
                Relationship_Lookup obj = _relationshipService.GetRelatioshipById(Id);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult EditRelationship(Relationship_Lookup objRelationship)
        {
            string status = "";
            try
            {
                if (ModelState.IsValid)
                {
                    status = _relationshipService.UpdateRelationship(objRelationship);
                }
                else
                {
                    throw new InvalidOperationException(MsgKeys.InvalidInputParameters);
                }
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