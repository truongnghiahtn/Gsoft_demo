using KhaiBaoYTe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KhaiBaoYTe.Controllers
{
    public class ApiTemplate_checkboxController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        //GET /api/ApiTemplate_checkbox/idTemplate
        public IQueryable<Object> GetTemplateCheckbox(int id)
        {
            // api/ApiTemplate_checkbox/1
            //lấy ra nội dung của sub câu hỏi và số lượng sub câu hỏi mà người dùng đã chọn
            var noiDung = from s in db.Sub_CauHoi
                          where s.CauHoi.IDLoaiCauHoi == 2 && s.CauHoi.IDTemplate == id
                          group s by new { s.NoiDung, s.IDCauHoi } into grj
                          select new
                          {
                              IDCauHoi = grj.Key.IDCauHoi,
                              Option = grj.Key.NoiDung,
                              SoLg = db.CauTraLoi_ChiTiet.Where(x => x.IDCauHoi == grj.Key.IDCauHoi && x.CauTraLoi.Contains(grj.Key.NoiDung))
                                .DefaultIfEmpty().Count(x => x != null)
                          };

            //lấy dữ liệu tương tự noiDung, mỗi object câu hỏi chứ 1 ds các sub câu hỏi và số lượng của sub đó đã đc người dùng chọn 
            var result = from ch in db.CauHois
                         join s in db.Sub_CauHoi on ch.IDCauHoi equals s.IDCauHoi
                         into grj
                         from g in grj.DefaultIfEmpty()
                         where ch.IDTemplate == id && ch.IDLoaiCauHoi == 2
                         group g by new { ch.IDCauHoi, ch.TieuDe } into gr
                         select new
                         {
                             IDCauHoi = gr.Key.IDCauHoi,
                             TenCauHoi = gr.Key.TieuDe,
                             NoiDung = noiDung.Where(x => x.IDCauHoi == gr.Key.IDCauHoi)
                         };
            return result;
        }
    }
}
