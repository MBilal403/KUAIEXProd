using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using DataAccessLayer.DomainEntities;
using System.Web;

namespace KuaiexDashboard.Services.GeneralSettingsServices
{
    public interface IGeneralSettingsService
    {
        List<GetTermsConditions_Result> GetTermsConditions();
        Terms_and_Privacy GetTermsAndPrivacyById(int id);
        bool UpdateTermsAndPrivacy(Terms_and_Privacy updatedTermsAndPrivacy);
        List<GetPrivacyPolicy_Result> GetPrivacyPolicy();
        List<GetContactUs_Result> GetContactUs();
        ContactUs GetContactUsById(int contactUsId);
        bool UpdateContactUs(ContactUs objContactUs);
        FAQs GetFAQsByQuestion(string question);
        bool AddFAQs(FAQs objFaq);
        List<GetFAQSList_Result> GetFAQSList();
        FAQs GetFAQById(int id);
        bool UpdateFAQ(FAQs objFAQs);
        List<GetCustomerQueries_Result> GetCustomerQueries();
        bool UpdatePrivacyPolicy(Terms_and_Privacy obj);
    }
}