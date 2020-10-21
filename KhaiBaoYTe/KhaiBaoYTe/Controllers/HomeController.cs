using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhaiBaoYTe.Models;
using KhaiBaoYTe.ViewModel;

namespace KhaiBaoYTe.Controllers
{
    public class HomeController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        public ActionResult Index()
        {
            return View();
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

        [ChildActionOnly]
        public PartialViewResult GetTemplateThongKeLink()
        {
            var model = db.Templates.Select(x => new TemplateLinksVM()
            {
                idTemplate = x.IDTemplate,
                tenTemplate = x.TenTemplate
            });
            return PartialView("~/Views/Shared/_PartialViewTemplateLinks.cshtml", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}