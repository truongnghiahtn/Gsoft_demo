using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class EntitiesCountController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        [ChildActionOnly]
        public PartialViewResult GetEntitiesCount(string entity)
        {
            int count = 0;
            if(entity == "ChuDe")
            {
                count = db.ChuDes.Count();
            }
            if(entity == "Template")
            {
                count = db.Templates.Count();
            }
            if(entity == "CauHoi")
            {
                count = db.CauHois.Count();
            }
            ViewBag.TotalCount = count;
            return PartialView("~/Views/EntitiesCount/_PartialView.cshtml");
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