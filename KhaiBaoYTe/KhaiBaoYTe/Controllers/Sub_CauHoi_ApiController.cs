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
    public class Sub_CauHoi_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/Sub_CauHoi_Api
        public IQueryable<Sub_CauHoi> GetSub_CauHoi()
        {
            return db.Sub_CauHoi;
        }

        // GET: api/Sub_CauHoi_Api/5
        [ResponseType(typeof(Sub_CauHoi))]
        public IHttpActionResult GetSub_CauHoi(int id)
        {
            Sub_CauHoi sub_CauHoi = db.Sub_CauHoi.Find(id);
            if (sub_CauHoi == null)
            {
                return NotFound();
            }

            return Ok(sub_CauHoi);
        }

        // PUT: api/Sub_CauHoi_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSub_CauHoi(int id, Sub_CauHoi sub_CauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sub_CauHoi.IDSubCauHoi)
            {
                return BadRequest();
            }

            db.Entry(sub_CauHoi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sub_CauHoiExists(id))
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

        // POST: api/Sub_CauHoi_Api
        [ResponseType(typeof(Sub_CauHoi))]
        public IHttpActionResult PostSub_CauHoi(Sub_CauHoi sub_CauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sub_CauHoi.Add(sub_CauHoi);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sub_CauHoi.IDSubCauHoi }, sub_CauHoi);
        }

        // DELETE: api/Sub_CauHoi_Api/5
        [ResponseType(typeof(Sub_CauHoi))]
        public IHttpActionResult DeleteSub_CauHoi(int id)
        {
            Sub_CauHoi sub_CauHoi = db.Sub_CauHoi.Find(id);
            if (sub_CauHoi == null)
            {
                return NotFound();
            }

            db.Sub_CauHoi.Remove(sub_CauHoi);
            db.SaveChanges();

            return Ok(sub_CauHoi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Sub_CauHoiExists(int id)
        {
            return db.Sub_CauHoi.Count(e => e.IDSubCauHoi == id) > 0;
        }
    }
}