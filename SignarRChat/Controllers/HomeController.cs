using SignarRChat.Helpers;
using System;
using System.Collections.Generic;
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

        public JsonResult GetConnections()
        {
            return Json(new { StaticValues.ConnectionDetails }, JsonRequestBehavior.AllowGet );
        }
    }
}