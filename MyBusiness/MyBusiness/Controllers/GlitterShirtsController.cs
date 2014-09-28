using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class GlitterShirtsController : Controller
    {
        //
        // GET: /Glitter/
        public ActionResult Index()
        {
            return View(new ImageModel("GlitterImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadEmGlitterShirtsWomen()
        {
            return PartialView("_GlitterShirtsWomen", new ImageModel("GlitterImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadGlitterShirtsMen()
        {
            return PartialView("_GlitterShirtsMen", new ImageModel("GlitterImages"));
        }

    }
}
