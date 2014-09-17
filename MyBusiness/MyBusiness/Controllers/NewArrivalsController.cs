using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class NewArrivalsController : Controller
    {
        //
        // GET: /NewArrivals/

        public ActionResult Index()
        {
            return View(new ImageModel("NewArrivalsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadNewArrivalsWomen()
        {
            return PartialView("_NewArrivalsWomen", new ImageModel("NewArrivalsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadNewArrivalsMen()
        {
            return PartialView("_NewArrivalsMen", new ImageModel("NewArrivalsImages"));
        }

    }
}
