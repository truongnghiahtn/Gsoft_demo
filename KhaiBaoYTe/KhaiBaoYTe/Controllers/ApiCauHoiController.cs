using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    
    public class ApiCauHoiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        //GET: api/CauHoi/1
        public IQueryable<Object> GetCauHois(int idTemplate)
        {
            //lấy ra các câu hỏi và số lượng đáp án đúng 
            var maxOption = from d in db.CauTraLoiDungs
                            join ch in db.CauHois on d.IDCauHoi equals ch.IDCauHoi
                            where ch.IDLoaiCauHoi != 1
                            select new
                            {
                                IDCauHoi = ch.IDCauHoi,
                                IDDangCauHoi = ch.IDLoaiCauHoi,
                                soLgOption = d.CauTraLoi_Dung // số lượng đáp án đúng là số lượng option cao nhất có thể chọn
                            };
            // lấy ra các thuộc tính của tất cả câu hỏi của 1 template
            var result =    from c in db.CauHois where c.IDTemplate == idTemplate
                            join s in db.Sub_CauHoi on c.IDCauHoi equals s.IDCauHoi into g
                            from nd in g.DefaultIfEmpty()       
                            group nd by new { c.IDCauHoi, c.LoaiCauHoi.DangCauHoi, c.TieuDe, c.CauHoiRequired, c.IDLoaiCauHoi} into gr
                            select new
                            {
                                IDCauHoi = gr.Key.IDCauHoi,
                                TieuDe = gr.Key.TieuDe,
                                DangCauHoi = gr.Key.DangCauHoi,
                                BatBuoc = gr.Key.CauHoiRequired,
                                MaxOption = maxOption.Where(x => x.IDCauHoi == gr.Key.IDCauHoi && x.IDDangCauHoi == gr.Key.IDLoaiCauHoi)
                                .Select(y => y.soLgOption).Count(),
                                NoiDung = gr.Where(x => x != null).Select(n => new
                                {
                                    label = n.NoiDung,
                                    value = n.NoiDung
                                }).ToList()
                            };

            // chọn ngẫu nhiên n câu hỏi đầu tiên của 1 template
            if (db.Templates.Where(x => x.IDTemplate == idTemplate).Select(x => x.Random).FirstOrDefault() == true)
            {
                Random rnd = new Random();
                int randomSoLgCauHoi = rnd.Next(1, result.Count());
                result = result.OrderBy(x => Guid.NewGuid()).Take(randomSoLgCauHoi);
            }
            return result.OrderBy(x=>x.IDCauHoi);
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
