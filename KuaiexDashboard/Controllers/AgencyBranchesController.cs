using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using KuaiexDashboard.DAL;

namespace KuaiexDashboard.Controllers
{
    public class AgencyBranchesController : Controller
    {
        AgencyBranchesDAL objAgencyBranchDal = new AgencyBranchesDAL();
        // GET: AgencyBranches
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAgencyBarnches(BranchesInfo objBranchesinfo)
        {
            string status = "error";
            //if (Authenticated)
            //{
            try
            {
                BranchesInfo obj = objAgencyBranchDal.GetBranchesInfoByName(objBranchesinfo.Name);
                if (obj != null)
                {
                    status = "exist";
                }
                else
                {
                    if (objBranchesinfo.Status != null)
                        objBranchesinfo.Status = 1;
                    else
                        objBranchesinfo.Status = 0;
                   objAgencyBranchDal.AddBranchesInfo(objBranchesinfo);

                        status = "success";
                   
                }
            }
            catch (Exception ex)
            {
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
            
            List<BranchesInfo> lstbranches = objAgencyBranchDal.GetBrancheskInfoList();
            status = Newtonsoft.Json.JsonConvert.SerializeObject(lstbranches);
            //}
            return Content(status);
        }
        public ActionResult Edit(int Id)
        {
            string status = "error";
            try
            {
                
                BranchesInfo obj = objAgencyBranchDal.GetBranchesInfoById(Id);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return Content(status);
        }
        public ActionResult EditAgencyBarnches(BranchesInfo objbranchesInfo)
        {
            string status = "";
            try
            {
               
                BranchesInfo obj = objAgencyBranchDal.GetBranchesInfoById(objbranchesInfo.Id);
                obj.Name = objbranchesInfo.Name;
                obj.ContactNo = objbranchesInfo.ContactNo;
                obj.Address = objbranchesInfo.Address;
                obj.Longitude = objbranchesInfo.Longitude;
                obj.Latitude = objbranchesInfo.Latitude;

                if (objbranchesInfo.Status != null)
                {
                    objbranchesInfo.Status = 1;
                }
                else
                {
                    objbranchesInfo.Status = 0;
                }
                obj.Status = objbranchesInfo.Status;
                objAgencyBranchDal.EditBranchesInfo(objbranchesInfo);
            }
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
    }
}