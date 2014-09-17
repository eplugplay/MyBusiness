using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class HatsController : Controller
    {
        //
        // GET: /Hats/

        public ActionResult Index()
        {
            return View(new ImageModel("HatsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadHatsWomen()
        {
            return PartialView("_HatsWomen", new ImageModel("HatsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadHatsMen()
        {
            return PartialView("_HatsMen", new ImageModel("HatsImages"));
        }

    }
}
