using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.UserServices;
using KuaiexDashboard.Services.UserServices.Impl;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLayer.DomainEntities;


namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class UserManagementController : Controller
    {
        private readonly IUserService userService;

        public UserManagementController()
        {
            userService = new UserService();
        }

        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Users objUser)
        {
            string status = "error";

            try
            {
                Users existingUser = userService.GetUserByUserName(objUser.UserName);

                if (existingUser != null)
                {
                    status = "exist";
                }
                else
                {
                    if (objUser.Status != null)
                    {
                        objUser.Status = 0;
                    }
                    else
                    {
                        objUser.Status = 1;
                    }
                    objUser.UID = Guid.NewGuid();


                    bool success = userService.RegisterUser(objUser);

                    if (success)
                    {
                        status = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }



        public ActionResult LoadUserType()
        {
            string status = "0:{choose}";

            try
            {
                List<UserType_Lookup> lstUserType = userService.GetUserTypes();
                status = JsonConvert.SerializeObject(lstUserType);
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
                List<GetUsersList_Result> userList = userService.GetUsersList();
                status = JsonConvert.SerializeObject(userList);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }

        public ActionResult Edit(Guid UID)
        {
            string status = "error";

            try
            {
                Users obj = userService.GetUserByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }


        [HttpPost]
        public ActionResult EditUser(Users objUser)
        {
            string status = "";

            try
            {
                Users existingUser = userService.GetUserByUID(objUser.UID);

                if (existingUser != null)
                {
                    existingUser.Name = objUser.Name;
                    existingUser.Password = objUser.Password;
                    existingUser.ContactNo = objUser.ContactNo;
                    existingUser.Email = objUser.Email;
                    if (existingUser.Status == 1)
                    {
                        objUser.Status = 0;
                    }
                    else
                    {
                        objUser.Status = existingUser.Status;
                    }

                    bool success = userService.UpdateUser(existingUser);

                    if (success)
                    {
                        status = "success";
                    }
                    else
                    {
                        status = "error";
                    }
                }
                else
                {
                    status = "error";
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
