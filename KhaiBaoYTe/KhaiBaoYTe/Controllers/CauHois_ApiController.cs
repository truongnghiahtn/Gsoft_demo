using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.Controllers
{
    public class CauHois_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/CauHois_Api

        [HttpGet]
        public IQueryable GetCauHois(int? idTemp)
        {
            var result = from c in db.CauHois
                         join sub in db.Sub_CauHoi on c.IDCauHoi equals sub.IDCauHoi into tbl
                         from nd in tbl.DefaultIfEmpty()
                         where c.IDTemplate == idTemp
                         group nd by new { c.IDCauHoi, c.TieuDe, c.LoaiCauHoi.DangCauHoi } into gr
                         select new
                         {
                             Id = gr.Key.IDCauHoi,
                             TieuDe = gr.Key.TieuDe,
                             DangCauHoi = gr.Key.DangCauHoi,
                             NoiDung = gr.Select(n => n.NoiDung).Where(x => x != null)
                         };
            
            return result;
        }

        // GET: api/CauHois_Api/5
        [ResponseType(typeof(CauHoi))]
        public IHttpActionResult GetCauHoi(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return Ok(cauHoi);
        }

        // PUT: api/CauHois_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCauHoi(int id, CauHoi cauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cauHoi.IDCauHoi)
            {
                return BadRequest();
            }

            db.Entry(cauHoi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauHoiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CauHois_Api
        [ResponseType(typeof(CauHoi))]
        public IHttpActionResult PostCauHoi(CauHoi cauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CauHois.Add(cauHoi);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cauHoi.IDCauHoi }, cauHoi);
        }

        // DELETE: api/CauHois_Api/5
        [ResponseType(typeof(CauHoi))]
        public IHttpActionResult DeleteCauHoi(int id)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            db.CauHois.Remove(cauHoi);
            db.SaveChanges();

            return Ok(cauHoi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CauHoiExists(int id)
        {
            return db.CauHois.Count(e => e.IDCauHoi == id) > 0;
        }
    }
}