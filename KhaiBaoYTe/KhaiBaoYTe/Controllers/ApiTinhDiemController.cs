using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class ApiTinhDiemController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        //:GET api/TinhDiem/{idCauTraLoi}/{idTemplate}
        [HttpGet]
        public object GetTongSoDiem(int idCauTraLoi, int idTemplate)
        {
            //lấy ra các object có id câu trả lời, id câu hỏi và điểm của mỗi đáp án đúng (sub). 
            var DiemTungSub = from ct in db.CauTraLoi_ChiTiet
                              join d in db.CauTraLoiDungs on ct.IDCauHoi equals d.IDCauHoi
                              group ct by new { ct.IDCauHoi, ct.IDCauTraLoi, ct.CauTraLoi, d.CauTraLoi_Dung } into gr
                              select new
                              {
                                  IDCauTraLoi = gr.Key.IDCauTraLoi,
                                  IDCauHoi = gr.Key.IDCauHoi,
                                  Diem = gr.Key.CauTraLoi.Contains(gr.Key.CauTraLoi_Dung) ? 
                                  (float)db.CauHois.Where(v => v.IDCauHoi == gr.Key.IDCauHoi).Select(x => x.SoDiem)
                                    .FirstOrDefault() / db.CauTraLoiDungs.Where(x => x.IDCauHoi == gr.Key.IDCauHoi)
                                    .Select(x => x.CauTraLoi_Dung).Count() 
                                  : (float)0
                              };
            // lấy ra các object, mỗi object có id câu trả lời, id câu hỏi, tên câu hỏi, chuỗi câu trả lời của user
            var DiemMotCau = from ct in db.CauTraLoi_ChiTiet
                        group ct by new {ct.IDCauTraLoi, ct.IDCauHoi, ct.CauHoi.TieuDe, ct.CauTraLoi} into gr
                        select new
                        {
                            IDCauTraLoi = gr.Key.IDCauTraLoi,
                            IDCauHoi = gr.Key.IDCauHoi,
                            TenCauHoi = gr.Key.TieuDe,
                            CauTraLoi = gr.Key.CauTraLoi,
                            DiemMax = db.CauHois.Where(x=>x.IDCauHoi == gr.Key.IDCauHoi).Select(x=>x.SoDiem).FirstOrDefault(),
                            Diem = DiemTungSub.Where(x=>x.IDCauTraLoi == gr.Key.IDCauTraLoi && x.IDCauHoi == gr.Key.IDCauHoi).Select(x=>x.Diem).Sum()
                        };
            var DiemCaTemplate = from trl in db.CauTraLois
                         where trl.IDCauTraLoi == idCauTraLoi && trl.IDTemplate == idTemplate
                         select new
                         {
                             IDCauTraLoi = trl.IDCauTraLoi,
                             UserID = trl.UserID,
                             HoTen = trl.HoTen,
                             DapAn = DiemMotCau.Where(x => x.IDCauTraLoi == trl.IDCauTraLoi).ToList(),
                             TongDiem = DiemMotCau.Where(x => x.IDCauTraLoi == trl.IDCauTraLoi).Select(x => x.Diem).DefaultIfEmpty().Sum()
                         };
            return DiemCaTemplate.FirstOrDefault();
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
