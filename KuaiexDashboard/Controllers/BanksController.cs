using DataAccessLayer;
using KuaiexDashboard.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    public class BanksController : Controller
    {
        BankMstDAL objBankDal = new BankMstDAL();
        // GET: Banks
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult LoadCountry()
        {
            string status = "0:{choose}";

            try
            {
                BeneficiaryDAL objBeneficiaryDAL = new BeneficiaryDAL();

                List<GetCountryList_Result> lstCountries = objBeneficiaryDAL.GetCountryList();

                status = JsonConvert.SerializeObject(lstCountries);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadCurrency()
        {
            string status = "0:{choose}";
            try
            {
                
                List<Currency> lstCurrency = objBankDal.GetCurrency();

                if (lstCurrency != null)
                {
                    status = JsonConvert.SerializeObject(lstCurrency);
                }
                else
                {
                    status = "error";
                }
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        public ActionResult LoadGrid(int countryId)
       {
          string status = "0:{choose}";
    
       try
       {
                
                List<GetBanksListByCountry_Result> banks = objBankDal.GetBanksListByCountry(countryId);

           status = JsonConvert.SerializeObject(banks);
       }
       catch (Exception ex)
       {
        status = "error";
       }

    return Content(status);
}
        public ActionResult LoadGrid1()
        {
            string status = "0:{choose}";

            try
            {
               
                List<GetBanksList_Result> bankslist = objBankDal.GetBanksList();

                status = JsonConvert.SerializeObject(bankslist);
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }
        //public ActionResult AddBranchess(Bank_Branch_Mst objBank)
        //{
        //    string status = "error";
        //    //if (Authenticated)
        //    //{
        //    try
        //    {
        //        Bank_Mst obj = db.Bank_Mst.Where(x => x.English_Name == objBank.English_Name).FirstOrDefault();

        //        if (obj != null)
        //        {
        //            status = "exist";
        //        }
        //        else
        //        {
        //            if (objBank.Record_Status != null)
        //                objBank.Record_Status = "Active";
        //            else
        //                objBank.Record_Status = "In Active";

        //            objBank.UID = Guid.NewGuid();

        //            db.Bank_Mst.Add(objBank);

        //            if (db.SaveChanges() > 0)
        //            {
        //                status = "success";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status = "error";
        //    }
        //    //}
        //    return Content(status);
        //}
        public ActionResult Edit(Guid UID)
        {
            string status = "error";
            try
            {
                
                Bank_Mst obj = objBankDal.GetBankByUID(UID);

                if (obj != null)
                {
                    status = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    status = "Bank not found";
                }
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return Content(status);
        }

        public ActionResult EditBank(Bank_Mst objBank)
        {
            string status = "error";
            try
            {
                
                
                status = objBankDal.UpdateBank(objBank);
            }
            catch (Exception ex)
            {
                status = "error";     
            }

            return Content(status);
        }

    }
}