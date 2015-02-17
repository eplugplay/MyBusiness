using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class CapsController : Controller
    {
        //
        // GET: /Hats/

        public ActionResult Index()
        {
            return View(new ImageModel("CapsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadCapsWomen()
        {
            return PartialView("_AllImgWomen", new ImageModel("CapsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadCapsMen()
        {
            return PartialView("_AllImgMen", new ImageModel("CapsImages"));
        }

    }
}
