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
    public class ChuDes_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/ChuDes_Api
        public IQueryable<Object> GetChuDes()
        {
            //return db.ChuDes;
            return db.ChuDes.Select(x=> new { TenChuDe = x.TenChuDe, MoTa = x.MoTa}); 
        }

        // GET: api/ChuDes_Api/5
        [ResponseType(typeof(ChuDe))]
        public IHttpActionResult GetChuDe(int id)
        {
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return Ok(chuDe);
        }

        // PUT: api/ChuDes_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChuDe(int id, ChuDe chuDe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chuDe.IDChuDe)
            {
                return BadRequest();
            }

            db.Entry(chuDe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChuDeExists(id))
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

        // POST: api/ChuDes_Api
        [ResponseType(typeof(ChuDe))]
        public IHttpActionResult PostChuDe(ChuDe chuDe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChuDes.Add(chuDe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chuDe.IDChuDe }, chuDe);
        }

        // DELETE: api/ChuDes_Api/5
        [ResponseType(typeof(ChuDe))]
        public IHttpActionResult DeleteChuDe(int id)
        {
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return NotFound();
            }

            db.ChuDes.Remove(chuDe);
            db.SaveChanges();

            return Ok(chuDe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChuDeExists(int id)
        {
            return db.ChuDes.Count(e => e.IDChuDe == id) > 0;
        }
    }
}