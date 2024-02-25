using KuaiexDashboard.DAL;
using KuaiexDashboard.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class UserManagementController : Controller
    {
        private UsersDAL usersDAL = new UsersDAL(); 

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
                Users existingUser = usersDAL.GetUserByUserName(objUser.UserName);

                if (existingUser != null)
                {
                    status = "exist";
                }
                else
                {
                    if(objUser.Status!= null)
                    {
                        objUser.Status = 0;
                    }
                    else
                    {
                        objUser.Status = 1;
                    }
                    objUser.UID = Guid.NewGuid();
                    

                    bool success = usersDAL.RegisterUser(objUser);

                    if (success)
                    {
                        status = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                status = "error";
            }

            return Content(status);
        }



        public ActionResult LoadUserType()
        {
            string status = "0:{choose}";

            try
            {
                List<UserType_Lookup> lstUserType = usersDAL.GetUserTypes();
                status = JsonConvert.SerializeObject(lstUserType);
            }
            catch (Exception ex)
            {
                status = "error";
                
            }

            return Content(status);
        }


        public ActionResult LoadGrid()
        {
            string status = "error";

            try
            {
                List<GetUsersList_Result> userList = usersDAL.GetUsersList();
                status = JsonConvert.SerializeObject(userList);
            }
            catch (Exception ex)
            {
                status = "error";
                
            }

            return Content(status);
        }

      

        public ActionResult Edit(Guid UID)
        {
            string status = "error";

            try
            {
                Users obj = usersDAL.GetUserByUID(UID);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                status = "Error";
                
            }

            return Content(status);
        }


        [HttpPost]
        public ActionResult EditUser(Users objUser)
        {
            string status = "";

            try
            {
                Users existingUser = usersDAL.GetUserByUID(objUser.UID);

                if (existingUser != null)
                {
                    existingUser.Name = objUser.Name;
                    existingUser.Password = objUser.Password;
                    existingUser.ContactNo = objUser.ContactNo;
                    existingUser.Email = objUser.Email;
                    if(existingUser.Status == 1)
                    {
                        objUser.Status = 0;
                    }
                    else
                    {
                        objUser.Status = existingUser.Status;
                    }

                    bool success = usersDAL.UpdateUser(existingUser);

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
                status = "error";
               
            }

            return Content(status);
        }
    }
}
