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
    public class ApiChuDeController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        private bool ChuDeExists(int id)
        {
            return db.ChuDes.Count(e => e.IDChuDe == id) > 0;
        }

        // GET: api/ApiChuDe
        public IQueryable<Object> GetChuDes()
        {
            var chuDeAll = db.ChuDes.Select(x => new { IDChuDe = x.IDChuDe, MoTa = x.MoTa, TenChuDe = x.TenChuDe });
            return chuDeAll;
        }

        // GET: api/ApiChuDe/5
        [ResponseType(typeof(Object))]
        public IHttpActionResult GetChuDe(int id)
        {
            var chuDe = db.ChuDes.Where(x => x.IDChuDe == id).Select(b => new { IDChuDe = b.IDChuDe, MoTa = b.MoTa,
                TenChuDe = b.TenChuDe }).SingleOrDefault();
            
            if (chuDe == null)
            {
                return NotFound();
            }

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

        
    }
}