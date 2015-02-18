using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class EmShirtsController : Controller
    {
        //
        // GET: /EmShirts/

        public ActionResult Index()
        {
            return View(new ImageModel("EmShirtsImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadEmShirtsWomen()
        {
            return PartialView("_AllImgWomen", new ImageModel("EmShirtsImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadEmShirtsMen()
        {
            return PartialView("_AllImgMen", new ImageModel("EmShirtsImages", false));
        }

    }
}
