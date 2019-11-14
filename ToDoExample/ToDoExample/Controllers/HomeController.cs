using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoExample.Models;
using ToDoExample.App_Classes;
using ToDoExample.Functions;
using System.Web.Security;

namespace ToDoExample.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ToDoEntities enToDo = new ToDoEntities();

        [Authorize(Roles = "User")]
        public ActionResult ToDoList()
        {
            var usrid = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            var todolist = enToDo.TO_DO.Where(x => x.UserID == usrid).AsQueryable();
            return View(todolist);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult ToDoList(DateFilterModel d)
        {
            if (d.Status == "null")
            {
                d.Status = null;
            }
            var model = FilterFunction.Filter(d);
            return View(model);
        }

        [Authorize(Roles = "User")]
        public ActionResult CreateToDo()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult CreateToDo(TO_DO toDo)
        {
            string usrname = User.Identity.Name;
            using (var context = new ToDoEntities())
            {
                var usrid = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                context.ADD_TO_DO(toDo.Name, toDo.Description, toDo.Deadline, toDo.Status,usrid);
                TempData["Message"] = "Success";
            }
            return RedirectToAction("ToDoList", "Home");
        }

        [Authorize(Roles = "User")]
        public ActionResult DeleteToDo()
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            using (var context=new ToDoEntities())
            {
                context.DELETE_ITEM_TO_DO(id);
                context.DELETE_TO_DO(id);
            }
            return RedirectToAction("ToDoList","Home");
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult UpdateToDo(string To_Do_ID, string Status)
        {
            using (var context = new ToDoEntities())
            {
                context.UPDATE_TO_DO(Convert.ToInt32(To_Do_ID), Convert.ToByte(Status));
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "User")]
        public ActionResult Item()
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            var todo = (from t in enToDo.TO_DO where t.To_Do_ID==id select t).ToList();
            ViewData["ToDoInf"] = todo;
            var items = (from i in enToDo.ITEMs where i.To_Do_ID == id select i).ToList();
            if (items.Count != 0)
            {
                return View(items);
            }
            else
                return View();
        }
        
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult SaveItem(string Item_Description,string To_Do_ID)
        {
                using (var context = new ToDoEntities())
                {
                    context.ADD_ITEM(Item_Description, 1, Convert.ToInt32(To_Do_ID));
                }
           
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult DeleteItem(string Item_ID, string To_Do_ID)
        {
            using (var context = new ToDoEntities())
            {
                context.DELETE_ITEM(Convert.ToInt32(Item_ID));
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "User")]
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