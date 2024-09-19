using System;
using System.Collections.Generic;
using DataAccessLayer.DomainEntities;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using KuaiexDashboard.Services.UserServices;
using KuaiexDashboard.Services.UserServices.Impl;
using Serilog;

namespace KuaiexDashboard.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService userService;

        public AuthenticationController()
        {
            userService = new UserService();
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
                Users authenticatedUser = userService.AuthenticateUser(objUser);

                if (authenticatedUser != null)
                {
                    Log.Information("User logged in: {UserId}", authenticatedUser.Id);
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
                bool registrationSuccess = userService.RegisterUser(newUser);
                if (registrationSuccess)
                {
                    Log.Information("User Sign up in: {UserEmail}", newUser.Email);
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
