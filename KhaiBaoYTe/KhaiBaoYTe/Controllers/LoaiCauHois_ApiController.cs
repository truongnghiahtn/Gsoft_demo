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
    public class LoaiCauHois_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/LoaiCauHois_Api
        public IQueryable<Object> GetLoaiCauHois()
        {
            return db.LoaiCauHois.Select(x => new { DangCauHoi = x.DangCauHoi });
        }

        // GET: api/LoaiCauHois_Api/5
        [ResponseType(typeof(LoaiCauHoi))]
        public IHttpActionResult GetLoaiCauHoi(int id)
        {
            LoaiCauHoi loaiCauHoi = db.LoaiCauHois.Find(id);
            if (loaiCauHoi == null)
            {
                return NotFound();
            }

            return Ok(loaiCauHoi);
        }

        // PUT: api/LoaiCauHois_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoaiCauHoi(int id, LoaiCauHoi loaiCauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loaiCauHoi.IDLoaiCauHoi)
            {
                return BadRequest();
            }

            db.Entry(loaiCauHoi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiCauHoiExists(id))
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

        // POST: api/LoaiCauHois_Api
        [ResponseType(typeof(LoaiCauHoi))]
        public IHttpActionResult PostLoaiCauHoi(LoaiCauHoi loaiCauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoaiCauHois.Add(loaiCauHoi);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = loaiCauHoi.IDLoaiCauHoi }, loaiCauHoi);
        }

        // DELETE: api/LoaiCauHois_Api/5
        [ResponseType(typeof(LoaiCauHoi))]
        public IHttpActionResult DeleteLoaiCauHoi(int id)
        {
            LoaiCauHoi loaiCauHoi = db.LoaiCauHois.Find(id);
            if (loaiCauHoi == null)
            {
                return NotFound();
            }

            db.LoaiCauHois.Remove(loaiCauHoi);
            db.SaveChanges();

            return Ok(loaiCauHoi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoaiCauHoiExists(int id)
        {
            return db.LoaiCauHois.Count(e => e.IDLoaiCauHoi == id) > 0;
        }
    }
}