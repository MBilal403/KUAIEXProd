using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KuaiexDashboard.DAL;

namespace KuaiexDashboard.Controllers
{
    public class AuthenticationController : Controller
    {
        private UsersDAL objUsersDAL;

        public AuthenticationController()
        {
            objUsersDAL = new UsersDAL();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToActionPermanent("Login");
        }

        [HttpPost]
        public ActionResult UserLogin(Users objUser)
        {
            if (ModelState.IsValid)
            {
                Users authenticatedUser = objUsersDAL.AuthenticateUser(objUser);

                if (authenticatedUser != null)
                {
                    Session["Id"] = authenticatedUser.Id.ToString();
                    Session["UserName"] = authenticatedUser.UserName.ToString();
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult RegisterUser(Users newUser)
        {
            if (ModelState.IsValid)
            {                
                bool registrationSuccess = objUsersDAL.RegisterUser(newUser);
                if (registrationSuccess)
                {
                   return RedirectToAction("Login");
                }
                else
                {
                   ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                }
            }
            return View("Register");
        }

        public Users SiteUser
        {
            set
            {
                Session["ActiveUser"] = value;
            }
            get
            {
                Users user = new Users();
                user = Session["ActiveUser"] as Users;
                return user;
            }
        }

        public List<string> UserPermissions
        {
            set
            {
                Session["ActiveUserPermissions"] = value;
            }
            get
            {
                List<string> permissions = new List<string>();
                permissions = Session["ActiveUserPermissions"] as List<string>;
                return permissions;
            }
        }
    }
}
