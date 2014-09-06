using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class ShoesController : Controller
    {
        //
        // GET: /Shoes/

        public ActionResult Index()
        {
            return View(new ImageModel("ShoesImages"));
        }

    }
}
