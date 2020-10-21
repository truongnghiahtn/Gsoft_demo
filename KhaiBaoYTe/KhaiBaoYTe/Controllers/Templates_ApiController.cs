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
    public class Templates_ApiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/Templates_Api
        public IQueryable<Object> GetTemplates(int idChuDe)
        {
            var get = db.Templates.Where(x => x.IDChuDe == idChuDe).Select(m => new {  ID = m.IDTemplate, TenTemplate = m.TenTemplate, Content = "<div className='page-title-area'><h1>"+ m.TenTemplate+"</h1><p>"+m.MoTa+"</p><a href='https://play.google.com/store/apps/details?id=com.vnptit.innovation.ncovi'>https://play.google.com/store/apps/details?id=com.vnptit.innovation.ncovi</a><a href='https://apps.apple.com/vn/app/ncovi/id1501934178'>https://apps.apple.com/vn/app/ncovi/id1501934178</a><br /><span>*Bắt buộc</span></div>" });
            return get;
        }

        // GET: api/Templates_Api/5
        [ResponseType(typeof(Template))]
        public IHttpActionResult GetTemplate(int id)
        {
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        // PUT: api/Templates_Api/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTemplate(int id, Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != template.IDTemplate)
            {
                return BadRequest();
            }

            db.Entry(template).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
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

        // POST: api/Templates_Api
        [ResponseType(typeof(Template))]
        public IHttpActionResult PostTemplate(Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Templates.Add(template);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = template.IDTemplate }, template);
        }

        // DELETE: api/Templates_Api/5
        [ResponseType(typeof(Template))]
        public IHttpActionResult DeleteTemplate(int id)
        {
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return NotFound();
            }

            db.Templates.Remove(template);
            db.SaveChanges();

            return Ok(template);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemplateExists(int id)
        {
            return db.Templates.Count(e => e.IDTemplate == id) > 0;
        }
    }
}