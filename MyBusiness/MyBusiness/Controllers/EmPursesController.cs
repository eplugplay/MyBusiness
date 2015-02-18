using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class EmPursesController : Controller
    {
        //
        // GET: /EmPurses/

        public ActionResult Index()
        {
            return View(new ImageModel("EmPursesImages", false));
        }

        [HttpPost]
        public PartialViewResult ReloadEmPurses()
        {
            return PartialView("_AllImgWomen", new ImageModel("EmPursesImages", false));
        }
    }
}
