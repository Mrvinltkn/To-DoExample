using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using ToDoExample;
using ToDoExample.Controllers;
using ToDoExample.Models;
using System.Collections.Generic;
using ToDoExample.App_Classes;
using System.Web.Security;

namespace ToDoExample.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Register()
        {
            // Arrange
            LoginController controller = new LoginController();
            User model = new User
            {
                UserName = "Merve",
                Password = "741852",
                Email="dnm@dnm.com"
            };
            ToDoEntities td = new ToDoEntities();
            ViewResult result = controller.Register(model) as ViewResult;
            MembershipUser user = Membership.GetUser(model.UserName);
            var Expecting = td.aspnet_Users.Find(user.ProviderUserKey);
            // Assert
            Assert.IsNotNull(Expecting);
        }

        [TestMethod]
        public void Login_Is_Success()
        {
            User u = new User
            {
                UserName = "Mrvinltkn",
                Password = "123456"
            };
            MembershipUser user = Membership.GetUser(u.UserName);
            bool result = Membership.ValidateUser(u.UserName, u.Password);
            var current_user_role = Roles.GetRolesForUser(u.UserName);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_Is_Failed()
        {
            User u = new User
            {
                UserName = "Deneme",
                Password = "123456"
            };
            MembershipUser user = Membership.GetUser(u.UserName);
            bool result = Membership.ValidateUser(u.UserName, u.Password);
            var current_user_role = Roles.GetRolesForUser(u.UserName);
            Assert.IsFalse(result);
        }
    }
}
