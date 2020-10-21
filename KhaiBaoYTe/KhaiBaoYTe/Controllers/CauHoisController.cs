using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class CauHoisController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: CauHois
        public ActionResult Index(int? foreign_id, int? id)
        {
            var cauHois = db.CauHois.Include(c => c.LoaiCauHoi).Include(c => c.Template);
            if(foreign_id != null)
            {
                cauHois = db.CauHois.Where(x => x.IDTemplate == id).Select(b => b);
                TempData["TenTemplate"] = db.Templates.Where(x => x.IDTemplate == id).SingleOrDefault().TenTemplate;
                TempData["IDTemplate"] = id;
            }
            return View(cauHois.ToList());

        }

        // GET: CauHois/Details/5
        public ActionResult Details(int? foreign_id, int? id)
        {
            if (foreign_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(foreign_id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // GET: CauHois/Create
        public ActionResult Create()
        {
            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi");
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate");
            return View();
        }

        // POST: CauHois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCauHoi,IDTemplate,TieuDe,IDLoaiCauHoi,CauHoiRequired,NgayTao,NguoiTao,NgayUpdate,NguoiUpdate")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.CauHois.Add(cauHoi);
                db.SaveChanges();
                return RedirectToAction("Index", new { foreign_id = TempData["IDTemplate"] });
            }

            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi", cauHoi.IDLoaiCauHoi);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauHoi.IDTemplate);
            return View(cauHoi);
        }

        // GET: CauHois/Edit/5
        public ActionResult Edit(int? foreign_id, int? id)
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



        // POST: CauHois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCauHoi,IDTemplate,TieuDe,IDLoaiCauHoi,CauHoiRequired,NgayTao,NguoiTao,NgayUpdate,NguoiUpdate")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLoaiCauHoi = new SelectList(db.LoaiCauHois, "IDLoaiCauHoi", "DangCauHoi", cauHoi.IDLoaiCauHoi);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauHoi.IDTemplate);
            return View(cauHoi);
        }

        // GET: CauHois/Delete/5
        public ActionResult Delete(int? foreign_id, int? id)
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

        // POST: CauHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            db.CauHois.Remove(cauHoi);
            db.SaveChanges();
            return RedirectToAction("Index");
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
