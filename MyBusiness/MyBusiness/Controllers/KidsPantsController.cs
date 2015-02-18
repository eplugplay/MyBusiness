using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class KidsPantsController : Controller
    {
        //
        // GET: /KidsPants/

        public ActionResult Index()
        {
            return View(new ImageModel("KidsPantsImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadKidsPantsGirls()
        {
            return PartialView("_AllImgWomen", new ImageModel("KidsPantsImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadKidsPantsBoys()
        {
            return PartialView("_AllImgMen", new ImageModel("KidsPantsImages", false));
        }

    }
}
