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
    public class LoaiCauHoisController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: LoaiCauHois
        public ActionResult Index()
        {
            return View(db.LoaiCauHois.ToList());
        }

        // GET: LoaiCauHois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCauHoi loaiCauHoi = db.LoaiCauHois.Find(id);
            if (loaiCauHoi == null)
            {
                return HttpNotFound();
            }
            return View(loaiCauHoi);
        }

        // GET: LoaiCauHois/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiCauHois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoaiCauHoi,DangCauHoi")] LoaiCauHoi loaiCauHoi)
        {
            if (ModelState.IsValid)
            {
                db.LoaiCauHois.Add(loaiCauHoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiCauHoi);
        }

        // GET: LoaiCauHois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCauHoi loaiCauHoi = db.LoaiCauHois.Find(id);
            if (loaiCauHoi == null)
            {
                return HttpNotFound();
            }
            return View(loaiCauHoi);
        }

        // POST: LoaiCauHois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoaiCauHoi,DangCauHoi")] LoaiCauHoi loaiCauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiCauHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiCauHoi);
        }

        // GET: LoaiCauHois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCauHoi loaiCauHoi = db.LoaiCauHois.Find(id);
            if (loaiCauHoi == null)
            {
                return HttpNotFound();
            }
            return View(loaiCauHoi);
        }

        // POST: LoaiCauHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiCauHoi loaiCauHoi = db.LoaiCauHois.Find(id);
            db.LoaiCauHois.Remove(loaiCauHoi);
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
