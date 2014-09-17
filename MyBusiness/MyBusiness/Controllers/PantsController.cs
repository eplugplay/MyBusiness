using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class PantsController : Controller
    {
        //
        // GET: /Pants/

        public ActionResult Index()
        {
            return View(new ImageModel("PantsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadPantsWomen()
        {
            return PartialView("_PantsWomen", new ImageModel("PantsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadPantsMen()
        {
            return PartialView("_PantsMen", new ImageModel("PantsImages"));
        }
    }
}
