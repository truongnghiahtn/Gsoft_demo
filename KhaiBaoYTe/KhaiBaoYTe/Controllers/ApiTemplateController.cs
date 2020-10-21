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
    public class ApiTemplateController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // GET: api/Templates/2 --> lay theo idChuDe
        [HttpGet]
        public IQueryable<Object> GetTemplates(int idChuDe)
        {
            // lấy ra tất cả các template thuộc chủ đề có id là idChuDe
            // api/Templates/1
            var templateAll = db.Templates.Where(x => x.IDChuDe == idChuDe).Select(m => 
                new {
                    IDChuDe = m.IDChuDe,
                    IDTemplate = m.IDTemplate,
                    TenTemplate = m.TenTemplate,
                    Content = m.MoTa,
                    SoLgHienThi = m.HienThi,
                    TinhDiem = m.TinhDiem,
                    Random = m.Random
                });
            return templateAll;
        }

        // PUT: api/ApiTemplate/5
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

        // POST: api/ApiTemplate
        [ResponseType(typeof(Template))]
        public IHttpActionResult PostTemplate(Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Templates.Add(template);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TemplateExists(template.IDTemplate))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = template.IDTemplate }, template);
        }

        // DELETE: api/ApiTemplate/5
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