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
    public class CauTraLois_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/CauTraLois_Api
        public IQueryable<CauTraLoi> GetCauTraLois()
        {
            return db.CauTraLois;
        }

        // GET: api/CauTraLois_Api/5
        [ResponseType(typeof(CauTraLoi))]
        public IHttpActionResult GetCauTraLoi(int id)
        {
            CauTraLoi cauTraLoi = db.CauTraLois.Find(id);
            if (cauTraLoi == null)
            {
                return NotFound();
            }

            return Ok(cauTraLoi);
        }

        // PUT: api/CauTraLois_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCauTraLoi(int id, CauTraLoi cauTraLoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cauTraLoi.IDCauTraLoi)
            {
                return BadRequest();
            }

            db.Entry(cauTraLoi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauTraLoiExists(id))
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

        private int CreateIDUser(CauTraLoi cauTraLoi)
        {
            var user = db.CauTraLois.Where(x => x.HoTen == cauTraLoi.HoTen && x.MSNV == cauTraLoi.MSNV && x.Email == cauTraLoi.Email);
            if (user.Count() == 0)
            {
                var newid = user.OrderByDescending(x => x.UserID).FirstOrDefault();
                if (newid == null)
                {
                    return 1;
                }
                else return newid.UserID.Value + 1;
            }
            else
            {
                return user.OrderByDescending(x => x.UserID).Select(x => x.UserID).FirstOrDefault().Value + 1;
            }
        }

        // POST: api/CauTraLois_Api
        [ResponseType(typeof(CauTraLoi))]
        public IHttpActionResult PostCauTraLoi(CauTraLoi cauTraLoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cauTraLoi.UserID = CreateIDUser(cauTraLoi);      
            foreach(var item in cauTraLoi.CauTraLoi_ChiTiet)
            {
                item.IDCauTraLoiChiTiet = cauTraLoi.IDCauTraLoi;
            }
            db.CauTraLois.Add(cauTraLoi);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cauTraLoi.IDCauTraLoi }, cauTraLoi);
        }

        // DELETE: api/CauTraLois_Api/5
        [ResponseType(typeof(CauTraLoi))]
        public IHttpActionResult DeleteCauTraLoi(int id)
        {
            CauTraLoi cauTraLoi = db.CauTraLois.Find(id);
            if (cauTraLoi == null)
            {
                return NotFound();
            }

            db.CauTraLois.Remove(cauTraLoi);
            db.SaveChanges();

            return Ok(cauTraLoi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CauTraLoiExists(int id)
        {
            return db.CauTraLois.Count(e => e.IDCauTraLoi == id) > 0;
        }
    }
}