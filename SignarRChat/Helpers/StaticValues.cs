using SignarRChat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SignarRChat.Helpers
{
    public static class StaticValues
    {
        public static List<ChatVm> ConnectionDetails = new List<ChatVm>();

        public static void TrashImage(ChatVm foundUser)
        {
            if (foundUser != null)
            {
                var directoryPath = System.Web.Hosting.HostingEnvironment.MapPath("~/ProfileImage/");
                var imagePath = Path.Combine(directoryPath, foundUser.ProfileImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
        }
    }
}