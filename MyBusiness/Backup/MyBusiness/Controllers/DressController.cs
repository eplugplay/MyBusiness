﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class DressController : Controller
    {
        //
        // GET: /Dress/

        public ActionResult Index()
        {
            return View(new ImageModel("DressImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadDressWomen()
        {
            return PartialView("_DressWomen", new ImageModel("DressImages"));
        }

    }
}