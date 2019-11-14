using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoExample.Functions;
using ToDoExample.Models;

namespace ToDoExample.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ToDoEntities td = new ToDoEntities();

        [Authorize(Roles = "Admin")]
        public ActionResult ToDoList()
        {
            ViewData["User"] = td.aspnet_Users.ToList();
            return View(td.TO_DO.AsQueryable());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ToDoList(DateFilterModel d)
        {
            if (d.Status=="null")
            {
                d.Status = null;
            }
            ViewData["User"] = td.aspnet_Users.ToList();
            var model = FilterFunction.FilterForAdmin(d);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateToDo()
        {
            ViewData["User"]=td.aspnet_Users.ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateToDo(TO_DO toDo)
        {
            using (var context = new ToDoEntities())
            {
                context.ADD_TO_DO(toDo.Name, toDo.Description, toDo.Deadline, toDo.Status, toDo.UserID);
                TempData["Message"] = "Success";
            }
            return RedirectToAction("ToDoList", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteToDo()
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            using (var context = new ToDoEntities())
            {
                context.DELETE_ITEM_TO_DO(id);
                context.DELETE_TO_DO(id);
            }
            return RedirectToAction("ToDoList", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateToDo(string To_Do_ID, string Status)
        {
            using (var context = new ToDoEntities())
            {
                context.UPDATE_TO_DO(Convert.ToInt32(To_Do_ID), Convert.ToByte(Status));
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Item()
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            var todo = (from t in td.TO_DO where t.To_Do_ID == id select t).ToList();
            ViewData["ToDoInf"] = todo;
            var items = (from i in td.ITEMs where i.To_Do_ID == id select i).ToList();
            if (items.Count != 0)
            {
                return View(items);
            }
            else
                return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult SaveItem(string Item_Description, string To_Do_ID)
        {
            using (var context = new ToDoEntities())
            {
                context.ADD_ITEM(Item_Description, 1, Convert.ToInt32(To_Do_ID));
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteItem(string Item_ID)
        {
            using (var context = new ToDoEntities())
            {
                context.DELETE_ITEM(Convert.ToInt32(Item_ID));
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateItem(string Item_ID, string Status)
        {
            using (var context = new ToDoEntities())
            {
                context.UPDATE_STATUS_ITEM(Convert.ToByte(Status), Convert.ToInt32(Item_ID));
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}