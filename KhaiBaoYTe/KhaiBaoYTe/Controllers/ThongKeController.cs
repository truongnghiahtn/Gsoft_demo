using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhaiBaoYTe.Models;
using KhaiBaoYTe.ViewModel;

namespace KhaiBaoYTe.Controllers
{
    public class ThongKeController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();
        // GET: ThongKe
        public ActionResult Index(TraLoiSearchModel searchModel)
        {
            ViewBag.IdChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe");
            ViewBag.IdTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate");
            var listCauHoi = db.CauHois.Select(x => new { x.TieuDe, x.IDCauHoi }).Distinct();
            ViewBag.IdCauHoi = new SelectList(listCauHoi, "IDCauHoi", "TieuDe");

            TempData["SoLgChuDe"] = db.ChuDes.Count();
            TempData["SoLgTemplate"] = db.Templates.Count();
            TempData["SoLgCauHoi"] = db.CauHois.Select(x => x.TieuDe).Distinct().Count();

            ThongKeVM thongKe = new ThongKeVM();
            thongKe.bangTraLoi = QueryListTraLoi(searchModel).ToList();

            var resultTraLoi = QueryTraLoiRadioCheckbox();
            var resultTextBox = QueryTraLoiTextBox();
            var user = db.CauTraLois.Select(x=>x);

            // loc de in ra man hinh
            if(searchModel != null)
            {
                if(searchModel.IdChuDe != null)
                {
                    int id = Int32.Parse(searchModel.IdChuDe);
                    resultTraLoi = resultTraLoi.Where(x => x.idChuDe == id);
                    resultTextBox = resultTextBox.Where(x => x.idChuDe == id);
                    user = user.Where(x => x.IDChuDe == id);
                }
                if(searchModel.IdTemplate != null)
                {
                    int id = Int32.Parse(searchModel.IdTemplate);
                    resultTraLoi = resultTraLoi.Where(x => x.idTemplate == id);
                    resultTextBox = resultTextBox.Where(x => x.idTemplate == id);
                    user = user.Where(x => x.IDTemplate == id);
                }
                if (searchModel.IDCauHoi != null)
                {
                    int id = Int32.Parse(searchModel.IDCauHoi);
                    resultTraLoi = resultTraLoi.Where(x => x.idCauHoi == id);
                    resultTextBox = resultTextBox.Where(x => x.idCauHoi == id);
                }
            }
            
            thongKe.listThongKeTraLoi = resultTraLoi.ToList();
            thongKe.listHoTen = user.Select(x => x.HoTen).ToList();
            thongKe.listMssv = user.Select(x => x.MSNV).ToList();
            thongKe.listEmail = user.Select(x => x.Email).ToList();
            thongKe.listThongKeTextBox = resultTextBox.ToList();

            return View(thongKe);
        }

        public JsonResult GetTraLoiData(int? idChuDe, int? idTemplate, int? idCauHoi)
        {
            var result = QueryTraLoiRadioCheckbox();
            if (idChuDe != null)
            {
                result = result.Where(x => x.idChuDe == idChuDe);
            }
            if (idTemplate != null)
            {
                result = result.Where(x => x.idTemplate == idTemplate);
            }
            if (idCauHoi != null)
            {
                result = result.Where(x => x.idCauHoi == idCauHoi);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //loc cau tra loi theo chu de, template, ten cau hoi
        private IQueryable<BangFilterTraLoiVM> QueryListTraLoi(TraLoiSearchModel searchModel)
        {
            var listTraLoi = from ch in db.CauHois select ch;

            if (searchModel != null)
            {
                if (!String.IsNullOrEmpty(searchModel.IdChuDe))
                {
                    int id = Int32.Parse(searchModel.IdChuDe);
                    listTraLoi = listTraLoi.Where(x => x.Template.IDChuDe == id);
                }
                if (!String.IsNullOrEmpty(searchModel.IdTemplate))
                {
                    int id = Int32.Parse(searchModel.IdTemplate);
                    listTraLoi = listTraLoi.Where(x => x.IDTemplate == id);
                }
                if (!String.IsNullOrEmpty(searchModel.IDCauHoi))
                {
                    int id = Int32.Parse(searchModel.IDCauHoi);
                    listTraLoi = listTraLoi.Where(x => x.IDCauHoi == id);
                }
            }
            var result = from ls in listTraLoi
                         join ct in db.CauTraLoi_ChiTiet on ls.IDCauHoi equals ct.IDCauHoi into gj
                         from g in gj.DefaultIfEmpty()
                         group g by ls.TieuDe into gr
                         select new BangFilterTraLoiVM()
                         {
                             TieuDe = gr.Key,
                             SoLg = gr.Where(x => x.CauHoi.TieuDe == gr.Key).Select(x => x.IDCauTraLoiChiTiet).Count()
                         };

            return result;
        }

        // select cac cau tra loi text box
        private IQueryable<ThongKeTextBoxVM> QueryTraLoiTextBox()
        {
            var result = from ch in db.CauHois
                         where ch.IDLoaiCauHoi == 1
                         group ch by new { ch.IDCauHoi, ch.TieuDe, ch.IDTemplate, ch.Template.TenTemplate, ch.Template.IDChuDe }
                         into gr
                         select new ThongKeTextBoxVM()
                         {
                             idChuDe = gr.Key.IDChuDe.Value,
                             idCauHoi = gr.Key.IDCauHoi,
                             tenCauHoi = gr.Key.TieuDe,
                             idTemplate = gr.Key.IDTemplate.Value,
                             tenTemplate = gr.Key.TenTemplate,
                             cauTraLoi = db.CauTraLoi_ChiTiet.Where(x => x.IDCauHoi == gr.Key.IDCauHoi)
                                .Select(x => x.CauTraLoi).ToList()
                         };
            return result;
        }

        private IQueryable<ThongKeTraLoiVM> QueryTraLoiRadioCheckbox()
        {
            var nd = from ct in db.CauTraLoi_ChiTiet
                     join s in db.Sub_CauHoi on ct.IDCauHoi equals s.IDCauHoi
                     where ct.CauHoi.IDLoaiCauHoi != 1
                     group ct by new { ct.IDCauHoi, s.NoiDung, ct.CauHoi.IDTemplate, ct.CauHoi.IDLoaiCauHoi } into gr
                     select new NoiDungCauHoiVM()
                     {
                         idCauHoi = gr.Key.IDCauHoi.Value,
                         idTemplate = gr.Key.IDTemplate.Value,
                         idLoaiCauHoi = gr.Key.IDLoaiCauHoi.Value,
                         noiDung = gr.Key.NoiDung,
                         soLg = gr.Key.IDLoaiCauHoi == 2 ? gr.Where(x=>x.CauTraLoi.Contains(gr.Key.NoiDung)).Count() : gr.Where(x=>x.CauTraLoi.Equals(gr.Key.NoiDung, StringComparison.OrdinalIgnoreCase)).Count()
                     };
            var result = from ch in db.CauHois
                         join ct in db.CauTraLoi_ChiTiet on ch.IDCauHoi equals ct.IDCauHoi
                         where ch.IDLoaiCauHoi != 1
                         group ch by new
                         {
                             ch.IDCauHoi,
                             ch.TieuDe,
                             ch.IDTemplate,
                             ch.IDLoaiCauHoi,
                             ch.Template.TenTemplate,
                             ch.Template.IDChuDe
                         } into gr
                         select new ThongKeTraLoiVM()
                         {
                             idCauHoi = gr.Key.IDCauHoi,
                             idLoaiCauHoi = gr.Key.IDLoaiCauHoi.Value,
                             idTemplate = gr.Key.IDTemplate.Value,
                             tenCauHoi = gr.Key.TieuDe,
                             tenTemplate = gr.Key.TenTemplate,
                             idChuDe = gr.Key.IDChuDe.Value,
                             noiDung = nd.Where(x => x.idCauHoi == gr.Key.IDCauHoi && x.idTemplate == gr.Key.IDTemplate
                                && x.idLoaiCauHoi == gr.Key.IDLoaiCauHoi).ToList()
                         };
            return result;
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