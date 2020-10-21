using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.ViewModel;
using KhaiBaoYTe.Models;
using System.Web.Http.Description;

namespace KhaiBaoYTe.Controllers
{
    public class ApiDefaultController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        //[HttpGet]
        // api/ApiDefault
        public IQueryable<Object> GetDefaultTemplate()
        {
            var checkTemplatesActive = db.Templates.Where(x => x.TemplateEnable == true);
            bool chose = true;
            IQueryable<Template> templates = null;
            if (!checkTemplatesActive.Any())
            {
                templates = db.Templates;
                chose = false;
            }
            else
            {
                templates = checkTemplatesActive;
            }
            var templateAll = templates.Select(m =>
                new {
                        IDChuDe = m.IDChuDe,
                        IDTemplate = m.IDTemplate,
                        TenTemplate = m.TenTemplate,
                        Content = m.MoTa,
                        SoLgHienThi = m.HienThi,
                        TinhDiem = m.TinhDiem,
                        Random = m.Random,
                        chose
                    });
            return templateAll;
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
