using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class ApparelController : Controller
    {
        //
        // GET: /Apparel/

        public ActionResult Index()
        {
            return View(new ImageModel("ApparelsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadApparelsWomen()
        {
            return PartialView("_ApparelsWomen", new ImageModel("ApparelsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadApparelsMen()
        {
            return PartialView("_ApparelsMen", new ImageModel("ApparelsImages"));
        }

    }
}
