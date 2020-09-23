using SignarRChat.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignarRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int userid, string username)
        {
            Session["UserName"] = username;
            Session["UserId"] = userid;
            return RedirectToAction("Chat");
        }
        
        public ActionResult Chat()
        {
            if (Session["UserName"] != null && Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetConnections()
        {
            return Json(new { StaticValues.ConnectionDetails }, JsonRequestBehavior.AllowGet );
        }
    }
}