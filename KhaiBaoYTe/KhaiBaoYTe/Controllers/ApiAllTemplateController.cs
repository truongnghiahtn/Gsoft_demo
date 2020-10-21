using KhaiBaoYTe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KhaiBaoYTe.Controllers
{
    public class ApiAllTemplateController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        [HttpGet]
        public IQueryable GetAllTemplate()
        {
            var template = db.Templates.Select(x => new {
                x.IDTemplate,
                x.IDChuDe,
                x.TenTemplate,
                Content = x.MoTa,
                SoLgHienThi = x.HienThi,
                x.TinhDiem,
                x.Random
            });
            return template;
        }
    }
}
