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
    public class CauTraLoisController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: CauTraLois
        public ActionResult Index(string stringId)
        {
            var cauTraLois = db.CauTraLois.Include(c => c.ChuDe).Include(c => c.Template);
            if (!String.IsNullOrEmpty(stringId))
            {
                int tableid = Int32.Parse(stringId);
                cauTraLois = cauTraLois.Where(x => x.IDTemplate == tableid).Select(x => x);
                TempData["IdTemplate"] = tableid;
            }
            TempData["CountUser"] = cauTraLois.Select(x => x.UserID).Distinct().Count();
            return View(cauTraLois.ToList());
        }

        // GET: CauTraLois/Details/5
        public ActionResult Details(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauTraLoi cauTraLoi = db.CauTraLois.Find(id);
            if (cauTraLoi == null)
            {
                return HttpNotFound();
            }
            return View(cauTraLoi);
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
                    Text = x.TenTemplate,
                    Value = x.IDTemplate.ToString()
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: CauTraLois/Create
        public ActionResult Create(string stringId)
        {
            var query = from t in db.Templates select t;
            int? idTemplate = null;
            if (!String.IsNullOrEmpty(stringId))
            {
                idTemplate = Int32.Parse(stringId);
                query = query.Where(x => x.IDTemplate == idTemplate);
            }
            var idChuDe = query.Select(x => x.IDChuDe).FirstOrDefault();
            ViewBag.IDChuDe = db.ChuDes.ToList();
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", idTemplate);
            return View();
        }

        // POST: CauTraLois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCauTraLoi,UserID,HoTen,MSNV,Email,IDChuDe,IDTemplate")] CauTraLoi cauTraLoi)
        {
            if (ModelState.IsValid)
            {
                cauTraLoi.IDCauTraLoi = CreateCauTraLoiId();
                cauTraLoi.UserID = CreateUserId(cauTraLoi);
                db.CauTraLois.Add(cauTraLoi);
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdTemplate"] });
            }

            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", cauTraLoi.IDChuDe);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauTraLoi.IDTemplate);
            return View(cauTraLoi);
        }

        // GET: CauTraLois/Edit/5
        public ActionResult Edit(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauTraLoi cauTraLoi = db.CauTraLois.Find(id);
            if (cauTraLoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", cauTraLoi.IDChuDe);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauTraLoi.IDTemplate);
            return View(cauTraLoi);
        }

        // POST: CauTraLois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCauTraLoi,UserID,HoTen,MSNV,Email,IDChuDe,IDTemplate")] CauTraLoi cauTraLoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauTraLoi).State = EntityState.Modified;
                db.Entry(cauTraLoi).Property(x => x.UserID).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdTemplate"] });
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", cauTraLoi.IDChuDe);
            ViewBag.IDTemplate = new SelectList(db.Templates, "IDTemplate", "TenTemplate", cauTraLoi.IDTemplate);
            return View(cauTraLoi);
        }

        // GET: CauTraLois/Delete/5
        public ActionResult Delete(int? id, string stringId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauTraLoi cauTraLoi = db.CauTraLois.Find(id);
            if (cauTraLoi == null)
            {
                return HttpNotFound();
            }
            return View(cauTraLoi);
        }

        // POST: CauTraLois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauTraLoi cauTraLoi = db.CauTraLois.Find(id);
            db.CauTraLois.Remove(cauTraLoi);
            
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

        private int CreateCauTraLoiId()
        {
            var lastRow = db.CauTraLois.OrderByDescending(x => x.IDCauTraLoi).FirstOrDefault();
            if(lastRow == null)
            {
                return 1;
            }
            else
            {
                return lastRow.IDCauTraLoi + 1;
            }
        }
        private int CreateUserId(CauTraLoi traLoi)
        {
            var user = db.CauTraLois.Where(x => x.HoTen == traLoi.HoTen && x.MSNV == traLoi.MSNV && x.Email == traLoi.Email);
            if (user.Count() == 0)
            {
                var userLastRow = db.CauTraLois.OrderByDescending(x => x.UserID).FirstOrDefault();
                if (userLastRow == null)
                {
                    return 1;
                }
                else
                {
                    return userLastRow.UserID.Value + 1;
                }
            }
            else
            {
                return user.FirstOrDefault().UserID.Value;
            }
        }
    }
}
