using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class FileBrowserController : Controller
    {
        //
        // GET: /FileBrowser/

        public ActionResult Index()
        {
            return View();
        }

        public string GetURL()
        {
            string url = "http://www.donnas-fashion.com/images/";
            return url;
        }

        [HttpPost]
        public ActionResult GetImages()
        {
            var directory = new System.IO.DirectoryInfo(Server.MapPath("/Images/"));
            string html = "<table>";
            int i = 0;
            foreach (var file in directory.GetFilesByExtensions(".png", ".gif", ".jpg"))
            {
                if (i % 20 == 0)
                {
                    html += "<tr><td><div class=\"thumbnail\"><img style=\"height:80px;width:80px;\" src=\"" + GetURL() + file.Name + "\"" + " alt=\"thumb\" title=\"" + GetURL() + file.Name + "\"/></div></td>";
                }
                else
                {
                    html += "<td><div class=\"thumbnail\"><img style=\"height:80px;width:80px;\" src=\"" + GetURL() + file.Name + "\"" + " alt=\"thumb\" title=\"" + GetURL() + file.Name + "\"/></div></td>";
                }

                if (i == file.Length - 1)
                {
                    html += "</table>";
                }
                i++;
            }

            return Content(html);
        }

    }
}
