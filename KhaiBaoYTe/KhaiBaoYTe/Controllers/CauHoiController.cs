using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhaiBaoYTe.Models;
using KhaiBaoYTe.ViewModel;

namespace KhaiBaoYTe.Controllers
{
    public class CauHoiController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: CauHoi
        public ActionResult Index(TraLoiSearchModel search)
        {
            var cauHoi = QueryListTraLoi(search);
            ViewBag.IdTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate");
            ViewBag.IdChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe");
            ThongKeCauHoi thongKeC = new ThongKeCauHoi();
            thongKeC.bangTraLoi = cauHoi.ToList();
            return View(thongKeC);
        }
        // QueryListTraLoi
        public IQueryable<CauHoiVM> QueryListTraLoi(TraLoiSearchModel search)
        {
            var cauHois = from ch in db.CauHois select ch;

            if (search != null)
            {
                if (!String.IsNullOrEmpty(search.IdChuDe))
                {
                    int tableid = Int32.Parse(search.IdChuDe);
                    cauHois = cauHois.Where(x => x.Template.IDChuDe == tableid);
                }
                if (!String.IsNullOrEmpty(search.IdTemplate))
                {
                    int tableid = Int32.Parse(search.IdTemplate);
                    cauHois = cauHois.Where(x => x.IDTemplate == tableid);
                }
            }
            
            TempData["idChuDe"] = cauHois.Select(x => x.Template.IDChuDe).FirstOrDefault();
            
            var TraLoiAll = from ch in cauHois
                            join ct in db.CauTraLoi_ChiTiet on ch.IDCauHoi equals ct.IDCauHoi
                            group ch by ch.IDCauHoi into gj
                            from gr in gj.DefaultIfEmpty()
                            select gr;

            var result = from ch in cauHois
                           select new CauHoiVM()
                           {
                               IDCauHoi = ch.IDCauHoi,
                               tenTemplate = ch.Template.TenTemplate,
                               TieuDe = ch.TieuDe,
                               IDLoaiCauHoi = ch.IDLoaiCauHoi,
                               DangCauHoi = ch.LoaiCauHoi.DangCauHoi,
                               CauHoiRequired = ch.CauHoiRequired,
                               NgayTao = ch.NgayTao,
                               NguoiTao = ch.NguoiTao,
                               NgayUpdate = ch.NgayUpdate,
                               NguoiUpdate = ch.NguoiUpdate,
                               SoDiem = ch.SoDiem.Value,
                               //CauHoiEnable = ch.CauHoiEnable.Value,
                               SoLgTraLoi = TraLoiAll.Where(x => x.IDCauHoi == ch.IDCauHoi).
                                    Select(x => x.CauTraLoi_ChiTiet.FirstOrDefault().IDCauTraLoiChiTiet).Count(),
                               SoLgNoiDung = ch.Sub_CauHoi.Where(x => x.IDCauHoi == ch.IDCauHoi).Count()
                           };
            return result;
        }

        // GET: CauHoi/Details/5
        public ActionResult Details(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // GET: CauHoi/Create
        public ActionResult Create(string stringId)
        {
            int? idTemplate = null;
            if (!String.IsNullOrEmpty(stringId))
            {
                idTemplate = Int32.Parse(stringId);
            }
            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi");
            ViewBag.ChuDe = db.ChuDes.ToList();
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", idTemplate);
            return View();
        }

        public JsonResult GetTemplateInChuDe(int? idChuDe)
        {
            var templateList = db.Templates.Select(x => x);
            IEnumerable<SelectListItem> result = null;
            if (idChuDe != null)
            {
                templateList = templateList.Where(x => x.IDChuDe == idChuDe);
                result = templateList.Select(x => new SelectListItem()
                {
                    Text = x.TenTemplate,//lấy tên template từ db
                    Value = x.IDTemplate.ToString() //lấy id template từ db
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: CauHoi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCauHoi,IDTemplate,TieuDe,IDLoaiCauHoi,CauHoiRequired,NguoiTao,SoDiem")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                

                cauHoi.IDCauHoi = CreateIdCauHoi();
                cauHoi.NgayTao = DateTime.Now;
                cauHoi.NgayUpdate = DateTime.Now;

                db.CauHois.Add(cauHoi);
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdTemplate"]});
            }

            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi", cauHoi.IDLoaiCauHoi);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauHoi.IDTemplate);
            return View(cauHoi);
        }

        // GET: CauHoi/Edit/5
        public ActionResult Edit(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi", cauHoi.IDLoaiCauHoi);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauHoi.IDTemplate);
            
            return View(cauHoi);
        }

        // POST: CauHoi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCauHoi,IDTemplate,TieuDe,IDLoaiCauHoi,CauHoiRequired,NguoiUpdate,SoDiem")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                cauHoi.NgayUpdate = DateTime.Now;

                db.Entry(cauHoi).State = EntityState.Modified;
                db.Entry(cauHoi).Property(x => x.NguoiTao).IsModified = false;
                db.Entry(cauHoi).Property(x => x.NgayTao).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdTemplate"] });
            }
            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi", cauHoi.IDLoaiCauHoi);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauHoi.IDTemplate);
            return View(cauHoi);
        }

        // GET: CauHoi/Delete/5
        public ActionResult Delete(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // POST: CauHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            db.CauHois.Remove(cauHoi);
            db.SaveChanges();
            return RedirectToAction("Index", new { stringId = TempData["IdTemplate"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int CreateIdCauHoi()
        {
            var query = db.CauHois.OrderByDescending(x => x.IDCauHoi).FirstOrDefault();
            if (query == null)
            {
                return 1;
            }
            else
            {
                return query.IDCauHoi + 1;
            }

        }
    }
}
