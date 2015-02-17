using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class EmCapsController : Controller
    {
        //
        // GET: /EmCaps/

        public ActionResult Index()
        {
            return View(new ImageModel("EmCapsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadEmCapsWomen()
        {
            return PartialView("_AllImgWomen", new ImageModel("EmCapsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadEmCapsMen()
        {
            return PartialView("_AllImgMen", new ImageModel("EmCapsImages"));
        }

    }
}
