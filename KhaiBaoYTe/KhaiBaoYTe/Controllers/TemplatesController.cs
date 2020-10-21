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
    public class TemplatesController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: Templates
        [HttpGet]
        public ActionResult Index(int? foreign_id, int? id)
        {
            var templates = db.Templates.Include(t => t.ChuDe);
            if(foreign_id != null)
            {
                templates = db.Templates.Where(x => x.IDChuDe == foreign_id).Select(b => b);
                TempData["TenChuDe"] = db.ChuDes.Where(x => x.IDChuDe == foreign_id).Single().TenChuDe;
                TempData["IDChuDe"] = foreign_id;

            }
            return View(templates.ToList());

        }

        // GET: Templates/Details/5
        public ActionResult Details(int? foreign_id, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // GET: Templates/Create
        public ActionResult Create(int? foreign_id)
        {
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe");
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTemplate,IDChuDe,TenTemplate,MoTa,NgayTao,NguoiTao,NgayUpdate,NguoiUpdate")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.Templates.Add(template);
                db.SaveChanges();
                return RedirectToAction("Index", new { foreign_id = TempData["IDChuDe"]});
            }

            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", template.IDChuDe);
            return View(template);
        }

        // GET: Templates/Edit/5
        public ActionResult Edit(int? foreign_id, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", template.IDChuDe);
            return View(template);
        }



        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTemplate,IDChuDe,TenTemplate,MoTa,NgayTao,NguoiTao,NgayUpdate,NguoiUpdate")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.Entry(template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { foreign_id = TempData["IDChuDe"]});
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", template.IDChuDe);
            return View(template);
        }

        // GET: Templates/Delete/5
        public ActionResult Delete(int? foreign_id, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int foreign_id)
        {
            Template template = db.Templates.Find(foreign_id);
            db.Templates.Remove(template);
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
