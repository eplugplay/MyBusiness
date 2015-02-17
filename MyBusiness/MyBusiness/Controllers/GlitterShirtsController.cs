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
            return View(new ImageModel("GlitterShirtsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadEmGlitterShirtsWomen()
        {
            return PartialView("_AllImgWomen", new ImageModel("GlitterShirtsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadGlitterShirtsMen()
        {
            return PartialView("_AllImgMen", new ImageModel("GlitterShirtsImages"));
        }

    }
}
