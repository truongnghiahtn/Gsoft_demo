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
    public class TemplateController : Controller
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: Template
        
        public ActionResult Index(TraLoiSearchModel search)
        {
            var tem = QueryListTraLoi(search.IdChuDe);
            ViewBag.IdChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe");
            ThongKeTemplate thongKeT = new ThongKeTemplate();
            thongKeT.bangTraLoi = tem.ToList();
            return View(thongKeT);
        }


        //loc cau tra loi theo chu de, template, ten cau hoi
        private IQueryable<TemplateVM> QueryListTraLoi(string search)
        {
            var templates = from t in db.Templates select t;
            if (search != null)
            {
                if (!String.IsNullOrEmpty(search))
                {
                    int id = Int32.Parse(search);

                    templates = templates.Where(x => x.IDChuDe == id);
                }
            }

            var TraLoiAll = from ch in db.CauHois
                            join t in db.Templates on ch.IDTemplate equals t.IDTemplate
                            join ct in db.CauTraLoi_ChiTiet on ch.IDCauHoi equals ct.IDCauHoi
                            group ch by t.IDTemplate into gr
                            select gr;

            var result = from t in templates
                             group t by new { t.IDTemplate, t.ChuDe.TenChuDe, t.TenTemplate, t.MoTa,
                                 t.NgayTao, t.NgayUpdate, t.NguoiTao, t.NguoiUpdate, t.HienThi, t.TinhDiem, t.Random, t.TemplateEnable}
                             into gr
                             select new TemplateVM()
                             {
                                 IDTemplate = gr.Key.IDTemplate,
                                 TenChuDe = gr.Key.TenChuDe,
                                 TenTemplate = gr.Key.TenTemplate,
                                 MoTa = gr.Key.MoTa,
                                 NgayTao = gr.Key.NgayTao.Value,
                                 NgayUpdate = gr.Key.NgayUpdate.Value,
                                 NguoiTao = gr.Key.NguoiTao,
                                 NguoiUpdate = gr.Key.NguoiUpdate,
                                 SoLgCauHoi = db.CauHois.Where(x => x.IDTemplate == gr.Key.IDTemplate).Count(),
                                 SoLgCauTraLoi = TraLoiAll.SelectMany(x => x).Where(x => x.IDTemplate == gr.Key.IDTemplate).Count(),
                                 HienThi = gr.Key.HienThi.Value,
                                 TinhDiem = gr.Key.TinhDiem,
                                 Random = gr.Key.Random,
                                 TemplateEnable = gr.Key.TemplateEnable
                             };

            return result;
        }

        // GET: Template/Details/5
        public ActionResult Details(int? id, string stringId)
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

        // GET: Template/Create
        public ActionResult Create(string stringId)
        {
            int? idChuDe = null;
            if(!String.IsNullOrEmpty(stringId))
            {
                idChuDe = Int32.Parse(stringId);
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", idChuDe);
            return View();
        }

        // POST: Template/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTemplate,NguoiTao,IDChuDe,TenTemplate,MoTa,HienThi,TinhDiem,Random,TemplateEnable")] Template template)
        {
            if (ModelState.IsValid)
            {
                template.IDTemplate = CreateIdTemplate();
                template.NgayTao = DateTime.Now;
                template.NgayUpdate = DateTime.Now;
                if(template.HienThi == null)
                {
                    template.HienThi = 5;
                }

                db.Templates.Add(template);
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdChuDe"] });
            }

            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", template.IDChuDe);
            return View(template);
        }

        // GET: Template/Edit/5
        public ActionResult Edit(int? id, string stringId)
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

        [HttpPost]
        public ActionResult EditEnable(int Id, bool val)
        {
            if (ModelState.IsValid)
            {

                var stt = Id;
                var result = db.Templates.Find(stt);
                result.TemplateEnable = val;
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        // POST: Template/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTemplate,IDChuDe,TenTemplate,MoTa,HienThi,NguoiUpdate,TinhDiem,Random")] Template template)
        {
            if (ModelState.IsValid)
            {

                template.NgayUpdate = DateTime.Now;
                if (template.HienThi == null)
                {
                    template.HienThi = 5;
                }

                db.Entry(template).State = EntityState.Modified;
                //db.Entry(template).Property(x => x.NguoiTao).IsModified = false;
                db.Entry(template).Property(x => x.NgayTao).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index", new { stringId = TempData["IdChuDe"] });
            }
            ViewBag.IDChuDe = new SelectList(db.ChuDes, "IDChuDe", "TenChuDe", template.IDChuDe);
            return View(template);
        }

        // GET: Template/Delete/5
        public ActionResult Delete(int? id, string stringId )
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

        // POST: Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = db.Templates.Find(id);
            db.Templates.Remove(template);
            db.SaveChanges();
            return RedirectToAction("Index", new { stringId = TempData["IdChuDe"]});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int CreateIdTemplate()
        {
            var query = db.Templates.OrderByDescending(x => x.IDTemplate).FirstOrDefault();
            if (query == null)
            {
                return 1;
            }
            else
            {
                return query.IDTemplate + 1;
            }

        }
    }
}
