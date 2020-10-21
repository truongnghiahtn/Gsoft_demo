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
    public class SubCauHoiController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: SubCauHoi
        public ActionResult Index(string stringId)
        {
            var sub_CauHoi = db.Sub_CauHoi.Include(s => s.CauHoi);
            if(!String.IsNullOrEmpty(stringId))
            {
                int tableid = Int32.Parse(stringId);
                sub_CauHoi = db.Sub_CauHoi.Where(x => x.IDCauHoi == tableid).Select(b => b);
                TempData["TieuDeCH"] = db.CauHois.Where(i => i.IDCauHoi == tableid).Single().TieuDe;
                TempData["idTemplate"] = sub_CauHoi.Select(x => x.CauHoi.IDTemplate).FirstOrDefault();
            }
            return View(sub_CauHoi.ToList());
        }

        // GET: SubCauHoi/Details/5
        public ActionResult Details(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_CauHoi sub_CauHoi = db.Sub_CauHoi.Find(id);
            if (sub_CauHoi == null)
            {
                return HttpNotFound();
            }
            return View(sub_CauHoi);
        }

        // GET: SubCauHoi/Create
        public ActionResult Create(string stringId)
        {
            int? idCauHoi = null;
            if (!String.IsNullOrEmpty(stringId))
            {
                idCauHoi = Int32.Parse(stringId);
            }
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", idCauHoi);
            TempData["IdCauHoi"] = stringId;
            return View();
        }

        // POST: SubCauHoi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSubCauHoi,IDCauHoi,NoiDung")] Sub_CauHoi sub_CauHoi)
        {
            if (ModelState.IsValid)
            {
                sub_CauHoi.IDSubCauHoi = CreateIdSubCauHoi();
                db.Sub_CauHoi.Add(sub_CauHoi);
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdCauHoi"]});
            }

            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", sub_CauHoi.IDCauHoi);
            return View(sub_CauHoi);
        }

        // GET: SubCauHoi/Edit/5
        public ActionResult Edit(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_CauHoi sub_CauHoi = db.Sub_CauHoi.Find(id);
            if (sub_CauHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", sub_CauHoi.IDCauHoi);
            return View(sub_CauHoi);
        }

        // POST: SubCauHoi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSubCauHoi,IDCauHoi,NoiDung")] Sub_CauHoi sub_CauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_CauHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdCauHoi"]});
            }
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", sub_CauHoi.IDCauHoi);
            return View(sub_CauHoi);
        }

        // GET: SubCauHoi/Delete/5
        public ActionResult Delete(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_CauHoi sub_CauHoi = db.Sub_CauHoi.Find(id);
            if (sub_CauHoi == null)
            {
                return HttpNotFound();
            }
            return View(sub_CauHoi);
        }

        // POST: SubCauHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_CauHoi sub_CauHoi = db.Sub_CauHoi.Find(id);
            db.Sub_CauHoi.Remove(sub_CauHoi);
            db.SaveChanges();
            return RedirectToAction("Index", new { stringId = TempData["IdCauHoi"]});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private int CreateIdSubCauHoi()
        {
            var query = db.Sub_CauHoi.OrderByDescending(x => x.IDSubCauHoi).FirstOrDefault();
            if (query == null)
            {
                return 1;
            }
            else
            {
                return query.IDSubCauHoi + 1;
            }
        }
    }
}
