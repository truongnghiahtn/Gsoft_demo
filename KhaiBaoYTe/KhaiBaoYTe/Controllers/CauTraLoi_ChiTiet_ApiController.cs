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
    public class CauTraLoi_ChiTiet_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/CauTraLoi_ChiTiet_Api
        public IQueryable<CauTraLoi_ChiTiet> GetCauTraLoi_ChiTiet()
        {
            return db.CauTraLoi_ChiTiet;
        }

        // GET: api/CauTraLoi_ChiTiet_Api/5
        [ResponseType(typeof(CauTraLoi_ChiTiet))]
        public IHttpActionResult GetCauTraLoi_ChiTiet(int id)
        {
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            if (cauTraLoi_ChiTiet == null)
            {
                return NotFound();
            }

            return Ok(cauTraLoi_ChiTiet);
        }

        // PUT: api/CauTraLoi_ChiTiet_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCauTraLoi_ChiTiet(int id, CauTraLoi_ChiTiet cauTraLoi_ChiTiet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cauTraLoi_ChiTiet.IDCauTraLoiChiTiet)
            {
                return BadRequest();
            }

            db.Entry(cauTraLoi_ChiTiet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauTraLoi_ChiTietExists(id))
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

        // POST: api/CauTraLoi_ChiTiet_Api
        [ResponseType(typeof(CauTraLoi_ChiTiet))]
        public IHttpActionResult PostCauTraLoi_ChiTiet(CauTraLoi_ChiTiet cauTraLoi_ChiTiet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CauTraLoi_ChiTiet.Add(cauTraLoi_ChiTiet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cauTraLoi_ChiTiet.IDCauTraLoiChiTiet }, cauTraLoi_ChiTiet);
        }

        // DELETE: api/CauTraLoi_ChiTiet_Api/5
        [ResponseType(typeof(CauTraLoi_ChiTiet))]
        public IHttpActionResult DeleteCauTraLoi_ChiTiet(int id)
        {
            CauTraLoi_ChiTiet cauTraLoi_ChiTiet = db.CauTraLoi_ChiTiet.Find(id);
            if (cauTraLoi_ChiTiet == null)
            {
                return NotFound();
            }

            db.CauTraLoi_ChiTiet.Remove(cauTraLoi_ChiTiet);
            db.SaveChanges();

            return Ok(cauTraLoi_ChiTiet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CauTraLoi_ChiTietExists(int id)
        {
            return db.CauTraLoi_ChiTiet.Count(e => e.IDCauTraLoiChiTiet == id) > 0;
        }
    }
}