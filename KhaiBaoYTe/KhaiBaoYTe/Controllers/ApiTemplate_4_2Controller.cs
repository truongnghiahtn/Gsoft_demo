using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class ApiTemplate_4_2Controller : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        //GET /api/Template_4/22
        public IQueryable<Object> GetTemplate42(int id)
        {
            var result = from ch in db.CauHois
                         join sub in db.Sub_CauHoi on ch.IDCauHoi equals sub.IDCauHoi into g
                         from nd in g.DefaultIfEmpty()
                         where ch.IDTemplate == id
                         group nd by new {ch.IDCauHoi, ch.TieuDe, ch.IDLoaiCauHoi } into f
                         select new
                         {
                             ID = f.Key.IDCauHoi,
                             TenCauHoi = f.Key.TieuDe,
                             LoaiCuaCauHoi = f.Key.IDLoaiCauHoi,
                             NoiDungTraLoi = f.Select(x=>x.NoiDung).Where(x=>x!=null)
                         };
            return result;
        }

    }
}
