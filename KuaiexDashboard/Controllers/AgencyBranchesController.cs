using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.DomainEntities;
using Serilog;
using KuaiexDashboard.Services.AgencyBranchesServices;
using KuaiexDashboard.Services.AgencyBranchesServices.Impl;

namespace KuaiexDashboard.Controllers
{
    public class AgencyBranchesController : Controller
    {
        private readonly IAgencyBranchesService _agencyBranchesService;

        public AgencyBranchesController()
        {
            _agencyBranchesService = new AgencyBranchesService();
        }
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
                BranchesInfo obj = _agencyBranchesService.GetBranchesInfoByName(objBranchesinfo.Name);
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
                    _agencyBranchesService.AddBranchesInfo(objBranchesinfo);

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

            List<BranchesInfo> lstbranches = _agencyBranchesService.GetBrancheskInfoList();
            status = JsonConvert.SerializeObject(lstbranches);
            //}
            return Content(status);
        }
        public ActionResult Edit(int Id)
        {
            string status = "error";
            try
            {
                
                BranchesInfo obj = _agencyBranchesService.GetBranchesInfoById(Id);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult EditAgencyBarnches(BranchesInfo objbranchesInfo)
        {
            string status = "";
            try
            {
               
                BranchesInfo obj = _agencyBranchesService.GetBranchesInfoById(objbranchesInfo.Id);
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
                _agencyBranchesService.EditBranchesInfo(objbranchesInfo);
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