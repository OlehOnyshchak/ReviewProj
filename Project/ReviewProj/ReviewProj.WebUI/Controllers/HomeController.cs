﻿using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Entities;

namespace ReviewProj.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IEnterpriseRepository repository;

        public HomeController(IEnterpriseRepository enterpriceRepository)
        {
            repository = enterpriceRepository;
        }
        public ActionResult Index()
        {
            IdentityConfig.RegisterAdmins();
            return View(repository.Enterprises);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}