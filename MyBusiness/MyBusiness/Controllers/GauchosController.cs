using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class GauchosController : Controller
    {
        //
        // GET: /Gauchos/

        public ActionResult Index()
        {
            return View(new ImageModel("GauchosImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadGauchosWomen()
        {
            return PartialView("_GauchosWomen", new ImageModel("GauchosImages"));
        }

    }
}
