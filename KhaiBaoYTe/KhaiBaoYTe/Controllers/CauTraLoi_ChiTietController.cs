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
    public class CauTraLoi_ChiTietController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: CauTraLoi_ChiTiet
        public ActionResult Index(int? foreign_id_CauHoi, int? foreign_id_CauTraLoi, int? id)
        {
            var cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Include(c => c.CauHoi).Include(c => c.CauTraLoi1);
            if(foreign_id_CauHoi != null && foreign_id_CauTraLoi != null)
            {
                cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Where(x => x.IDCauHoi == foreign_id_CauHoi && x.IDCauTraLoi == foreign_id_CauTraLoi).Select(x => x);
            }
            return View(cauTraLoi_ChiTiet.ToList());
        }

        // GET: CauTraLoi_ChiTiet/Details/5
        public ActionResult Details(int? foreign_id_CauHoi, int? foreign_id_CauTraLoi, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            if (cauTraLoi_ChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(cauTraLoi_ChiTiet);
        }

        // GET: CauTraLoi_ChiTiet/Create
        public ActionResult Create()
        {
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe");
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen");
            return View();
        }

        // POST: CauTraLoi_ChiTiet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCauTraLoiChiTiet,IDCauTraLoi,IDCauHoi,CauTraLoi")] CauTraLoi_ChiTiet cauTraLoi_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.CauTraLoi_ChiTiet.Add(cauTraLoi_ChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", cauTraLoi_ChiTiet.IDCauHoi);
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen", cauTraLoi_ChiTiet.IDCauTraLoi);
            return View(cauTraLoi_ChiTiet);
        }

        // GET: CauTraLoi_ChiTiet/Edit/5
        public ActionResult Edit(int? foreign_id_CauHoi, int? foreign_id_CauTraLoi, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            if (cauTraLoi_ChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", cauTraLoi_ChiTiet.IDCauHoi);
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen", cauTraLoi_ChiTiet.IDCauTraLoi);
            return View(cauTraLoi_ChiTiet);
        }

        // POST: CauTraLoi_ChiTiet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCauTraLoiChiTiet,IDCauTraLoi,IDCauHoi,CauTraLoi")] CauTraLoi_ChiTiet cauTraLoi_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauTraLoi_ChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", cauTraLoi_ChiTiet.IDCauHoi);
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen", cauTraLoi_ChiTiet.IDCauTraLoi);
            return View(cauTraLoi_ChiTiet);
        }

        // GET: CauTraLoi_ChiTiet/Delete/5
        public ActionResult Delete(int? foreign_id_CauHoi, int? foreign_id_CauTraLoi, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            if (cauTraLoi_ChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(cauTraLoi_ChiTiet);
        }

        // POST: CauTraLoi_ChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? foreign_id_CauHoi, int? foreign_id_CauTraLoi, int id)
        {
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            db.CauTraLoi_ChiTiet.Remove(cauTraLoi_ChiTiet);
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
