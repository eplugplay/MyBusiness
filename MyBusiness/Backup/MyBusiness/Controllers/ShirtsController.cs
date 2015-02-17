﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class ShirtsController : Controller
    {
        //
        // GET: /Shirts/

        public ActionResult Index()
        {
            return View(new ImageModel("ShirtsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadShirtsWomen()
        {
            return PartialView("_ShirtsWomen", new ImageModel("ShirtsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadShirtsMen()
        {
            return PartialView("_ShirtsMen", new ImageModel("ShirtsImages"));
        }
    }
}