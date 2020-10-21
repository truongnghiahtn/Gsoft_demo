using KhaiBaoYTe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KhaiBaoYTe.Controllers
{
    public class ApiTemplate_radioController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        //GET /api/ApiTemplate_radio/idTemplate
        public IQueryable<Object> GetTemplateRadio(int id)
        {
            //lấy ra nội dung của sub câu hỏi và số lượng sub câu hỏi mà người dùng đã chọn
        var noiDung =   from ch in db.CauHois
                        join s in db.Sub_CauHoi on ch.IDCauHoi equals s.IDCauHoi
                        where ch.IDLoaiCauHoi == 3 && ch.IDTemplate == id
                        group ch by new { ch.IDCauHoi, s.NoiDung } into gr
                        select new
                        {
                            idCH = gr.Key.IDCauHoi,
                            NoiDung = gr.Key.NoiDung,
                            SoLuong = db.CauTraLoi_ChiTiet.Where(x=>x.IDCauHoi == gr.Key.IDCauHoi && x.CauTraLoi.Equals(gr.Key.NoiDung))
                            .DefaultIfEmpty().Count(x=>x!=null)
                        };
            //lấy dữ liệu tương tự noiDung, mỗi object câu hỏi chứ 1 ds các sub câu hỏi và số lượng của sub đó đã đc người dùng chọn 
            var result = from ch in db.CauHois
                         where ch.IDLoaiCauHoi == 3 && ch.IDTemplate == id
                         group ch by new { ch.IDCauHoi, ch.TieuDe } into gr
                         select new
                         {
                             idCauHoi = gr.Key.IDCauHoi,
                             TenCauHoi = gr.Key.TieuDe,
                             NoiDung = noiDung.Where(c => c.idCH == gr.Key.IDCauHoi).Select(c => new { TenSub = c.NoiDung, SoLuong = c.SoLuong })
                         };
            return result;
        }
    }
}
