using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult Index()
        {
            ViewBag.Page = new PageModel("AboutUs");
            return View();
        }

    }
}
