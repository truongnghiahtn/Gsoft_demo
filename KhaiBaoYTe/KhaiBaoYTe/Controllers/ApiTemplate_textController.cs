using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class ApiTemplate_textController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        //GET /api/ApiTemplate_text/idTemplate
        public IQueryable<Object> GetTemplateText(int id)
        {
            // lấy ra tất cả các câu trả lời của câu hỏi dạng text box
            var result = from ch in db.CauHois
                         join l in db.LoaiCauHois on ch.IDLoaiCauHoi equals l.IDLoaiCauHoi
                         join ct in db.CauTraLoi_ChiTiet on ch.IDCauHoi equals ct.IDCauHoi into n
                         from ctl in n.DefaultIfEmpty()//left join cauhoi + loaicauhoi + ctrlchitiet 
                         where l.IDLoaiCauHoi == 1 && ch.IDTemplate == id
                         group ctl by new { ch.IDCauHoi, ch.TieuDe, ch.IDLoaiCauHoi } into g
                         select new
                         {
                             ID = g.Key.IDCauHoi,
                             TenCauHoi = g.Key.TieuDe,
                             LoaiCauHoi = g.Key.IDLoaiCauHoi,
                             NoiDungCauTraLoi = g.Select(y => y.CauTraLoi),
                             SoLuong = g.Select(y => y.CauTraLoi).Count(x=>x != null)
                         };

            return result;
        }

    }
}
