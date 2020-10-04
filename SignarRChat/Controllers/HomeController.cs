using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignarRChat.Helpers;

namespace SignarRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            var data = CommonFunctions.GetContacts().FirstOrDefault(s => s.Name.ToLower().Equals(name.ToLower()));

            if (data == null) return View();

            Session["UserDetails"] = data;
            return RedirectToAction("Chat");

        } 

        public ActionResult Chat()
        {
            if (Session["UserDetails"] == null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public JsonResult GetContactWithMessages(int id)
        {
            var data  = CommonFunctions.GetContacts().FirstOrDefault(s => s.ContactId.Equals(id));

            return Json(new {data}, JsonRequestBehavior.AllowGet);
        }
    }
}