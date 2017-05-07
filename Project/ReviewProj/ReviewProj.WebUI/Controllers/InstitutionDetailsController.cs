using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Entities;

namespace ReviewProj.WebUI.Controllers
{
    public class InstitutionDetailsController : Controller
    {
        private IEnterpriseRepository repository;

        public InstitutionDetailsController(IEnterpriseRepository enterpriseRepository)
        {
            repository = enterpriseRepository;
        }

        // GET: InstitutionDetails
        public ActionResult Index(int id)
        {
            Enterprise ent = repository.Enterprises.FirstOrDefault(e => e.EntId == id);
            return View(ent);
        }
    }
}