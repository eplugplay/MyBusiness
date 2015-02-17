﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBusiness.Models;

namespace MyBusiness.Controllers
{
    public class KidsCapsController : Controller
    {
        //
        // GET: /KidsCaps/

        public ActionResult Index()
        {
            return View(new ImageModel("KidsCapsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadKidsCapsGirls()
        {
            return PartialView("_KidsCapsGirls", new ImageModel("KidsCapsImages"));
        }

        [HttpPost]
        public PartialViewResult ReloadKidsCapsBoys()
        {
            return PartialView("_KidsCapsBoys", new ImageModel("KidsCapsImages"));
        }

    }
}