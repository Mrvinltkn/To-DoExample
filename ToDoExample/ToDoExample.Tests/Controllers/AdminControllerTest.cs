using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoExample;
using ToDoExample.App_Classes;
using ToDoExample.Controllers;
using ToDoExample.Models;


namespace ToDoExample.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void CreateToDo()
        {
            // Arrange
            AdminController controller = new AdminController();
            TO_DO td = new TO_DO
            {
                Name = "Example ToDo",
                Description = "Example Description",
                Deadline = new DateTime(2019, 06, 25),
                Status = 1,
                UserID = Guid.Parse("0797F007-813A-455C-BF42-5D8BE5DE34A7")
            };
            // Act
            ViewResult result = controller.CreateToDo(td) as ViewResult;
            ToDoEntities tden = new ToDoEntities();
            var expecting = tden.TO_DO.Where(x => x.Name == td.Name && x.Description == td.Description).FirstOrDefault();
            // Assert
            Assert.IsNotNull(expecting);
        }
        [TestMethod]
        public void SaveItem()
        {
            AdminController controller = new AdminController();
            string description = "Item 1";
            string ToDoId = "2";
            ViewResult result = controller.SaveItem(description,ToDoId) as ViewResult;
            ToDoEntities tden = new ToDoEntities();
            long ID = long.Parse(ToDoId);
            var expecting = tden.ITEMs.Where(x => x.Item_Description == description && x.To_Do_ID == ID).FirstOrDefault();
            Assert.IsNotNull(expecting);
        }
    }
}
