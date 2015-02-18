using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class KidsShirtsController : Controller
    {
        //
        // GET: /KidsShirts/

        public ActionResult Index()
        {
            return View(new ImageModel("KidsShirtsImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadKidsShirtsGirls()
        {
            return PartialView("_AllImgWomen", new ImageModel("KidsShirtsImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadKidsShirtsBoys()
        {
            return PartialView("_AllImgMen", new ImageModel("KidsShirtsImages", false));
        }

    }
}
