using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoExample.App_Classes;

namespace ToDoExample.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u,string remember)
        {
            MembershipUser user = Membership.GetUser(u.UserName);
            bool result = Membership.ValidateUser(u.UserName, u.Password);
            var current_user_role = Roles.GetRolesForUser(u.UserName);
            if (result)
            {
                if (current_user_role[0].ToString() == "User")
                {
                    if (remember == "on")
                    {
                        FormsAuthentication.RedirectFromLoginPage(u.UserName, true);
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(u.UserName, false);
                    }
                    return RedirectToAction("ToDoList", "Home");
                }
                else
                {
                    if (remember == "on")
                    {
                        FormsAuthentication.RedirectFromLoginPage(u.UserName, true);
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(u.UserName, false);
                    }
                    return RedirectToAction("ToDoList", "Admin");
                }
            }
            else
            {
                if (user != null)
                {
                    if (!user.IsApproved)
                    {
                        ViewBag.Message = "Unverified User";
                    }
                    else if (user.IsLockedOut)
                    {
                        ViewBag.Message = "Account Locked";
                    }
                    else
                    {
                        ViewBag.Message = "Username or Password Incorrect !!!";
                    }
                }
                else
                {
                    ViewBag.Message = "Username or Password Incorrect !!!";
                }
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User _user)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(_user.UserName, _user.Password, _user.Email, _user.SecretQuestion, _user.SecretAnswer, true, out status);
            string message = "";
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    message += "Invalid User Name.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    message += "Invalid Password.";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    message += "Invalid Secret Question.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    message += "Invalid Secret Answer.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    message += "Invalid E-Mail.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    message += "Used User Name Entered.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    message += "Used Mail Address Entered.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    message += "User Block Error.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    message += "Invalid User Key Error.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    message += "Used User Key Error.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    message += "Member Management Provider Error";
                    break;
                default:
                    break;
            }

            ViewBag.Mesaj = message;
            if (status == MembershipCreateStatus.Success)
            {
                Roles.AddUserToRole(_user.UserName, "User");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
    }
}