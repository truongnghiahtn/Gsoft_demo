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
    public class CauTraLoiChiTietController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: CauTraLoiChiTiet
        public ActionResult Index(string stringId)
        {
            var cauTraLoi_ChiTiet = from ct in db.CauTraLoi_ChiTiet select ct;
            if(!String.IsNullOrEmpty(stringId))
            {
                int tableid = Int32.Parse(stringId);
                cauTraLoi_ChiTiet = cauTraLoi_ChiTiet.Where(x => x.IDCauTraLoi == tableid).Select(x => x);
                TempData["IdTraLoi"] = tableid;
                TempData["IdTemplate"] = db.CauTraLois.Where(x => x.IDCauTraLoi == tableid).Select(x => x.IDTemplate).First();
            }
            var result = from ct in cauTraLoi_ChiTiet
                         select new TraLoiChiTietVM()
                         {
                             IDCauTraLoiChiTiet = ct.IDCauTraLoiChiTiet,
                             tenTemplate = ct.CauTraLoi1.Template.TenTemplate,
                             tenNgTraLoi = ct.CauTraLoi1.HoTen,
                             tenCauHoi = ct.CauHoi.TieuDe,
                             CauTraLoi = ct.CauTraLoi
                         };
            return View(result.ToList());
        }

        // GET: CauTraLoiChiTiet/Details/5
        public ActionResult Details(int? id, string stringId)
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

        // GET: CauTraLoiChiTiet/Create
        public ActionResult Create(string stringId)
        {
            int? idTraLoi = null;
            var query = from ct in db.CauTraLoi_ChiTiet select ct;
            if (!String.IsNullOrEmpty(stringId))
            {
                idTraLoi = Int32.Parse(stringId);
                query = query.Where(x => x.IDCauTraLoi == idTraLoi);
            }
            var idCauHoi = query.Select(x => x.IDCauHoi).FirstOrDefault();
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", idCauHoi);
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen", idTraLoi);
            return View();
        }

        // POST: CauTraLoiChiTiet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCauTraLoiChiTiet,IDCauTraLoi,IDCauHoi,CauTraLoi")] CauTraLoi_ChiTiet cauTraLoi_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                cauTraLoi_ChiTiet.IDCauTraLoiChiTiet = CreateChiTietId();
                db.CauTraLoi_ChiTiet.Add(cauTraLoi_ChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdTraLoi"] });
            }

            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", cauTraLoi_ChiTiet.IDCauHoi);
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen", cauTraLoi_ChiTiet.IDCauTraLoi);
            return View(cauTraLoi_ChiTiet);
        }

        // GET: CauTraLoiChiTiet/Edit/5
        public ActionResult Edit(int? id, string stringId)
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

        // POST: CauTraLoiChiTiet/Edit/5
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
                return RedirectToAction("Index", new { stringId = TempData["IdTraLoi"] });
            }
            ViewBag.IDCauHoi = new SelectList(db.CauHois, "IDCauHoi", "TieuDe", cauTraLoi_ChiTiet.IDCauHoi);
            ViewBag.IDCauTraLoi = new SelectList(db.CauTraLois, "IDCauTraLoi", "HoTen", cauTraLoi_ChiTiet.IDCauTraLoi);
            return View(cauTraLoi_ChiTiet);
        }

        // GET: CauTraLoiChiTiet/Delete/5
        public ActionResult Delete(int? id, string stringId)
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

        // POST: CauTraLoiChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            db.CauTraLoi_ChiTiet.Remove(cauTraLoi_ChiTiet);
            db.SaveChanges();
            return RedirectToAction("Index", new { stringId = TempData["IdTraLoi"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private int CreateChiTietId()
        {
            var lastRow = db.CauTraLoi_ChiTiet.OrderByDescending(x => x.IDCauTraLoiChiTiet).FirstOrDefault();
            if(lastRow == null)
            {
                return 1;
            }
            else
            {
                return lastRow.IDCauTraLoiChiTiet + 1;
            }
        }
    }
}
