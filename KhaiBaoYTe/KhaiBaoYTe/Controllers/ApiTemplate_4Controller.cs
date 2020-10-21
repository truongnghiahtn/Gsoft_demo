using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class ApiTemplate_4Controller : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        //GET /api/ApiTemplate_4/idTemplate
        public IQueryable<Object> GetTemplates4(int id)
        {
            var result = from ctl in db.CauTraLois
                         where ctl.IDTemplate == id
                         select new
                         {
                             ID = ctl.IDCauTraLoi,
                             Hoten = ctl.HoTen,
                             MSNV = ctl.MSNV,
                             Email = ctl.Email
                         };
            return result;
        }
    }
}
