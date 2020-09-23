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

        public ActionResult SingleChat()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SubmitDetails(HttpPostedFileBase profileImage)
        {
            if(profileImage != null)
            {
                var directoryPath = Server.MapPath("~/ProfileImage");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileGuid = Guid.NewGuid();
                var filename = string.Concat(fileGuid, Path.GetExtension(profileImage.FileName));
                var savePath = Path.Combine(directoryPath, filename);
                profileImage.SaveAs(savePath);
                return Json(new { success = true , profileImage = filename }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConnections()
        {
            return Json(new { StaticValues.ConnectionDetails }, JsonRequestBehavior.AllowGet );
        }
    }
}