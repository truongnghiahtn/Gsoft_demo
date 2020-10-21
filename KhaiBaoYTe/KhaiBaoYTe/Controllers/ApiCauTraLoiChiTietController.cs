using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace KhaiBaoYTe.Controllers
{
    public class ApiCauTraLoiChiTietController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        //POST: api/ApiCauTraLoiChiTiet
        [ResponseType(typeof(CauTraLoi_ChiTiet))]
        [HttpPost]
        public IHttpActionResult PostCauTraLoiChiTiet(CauTraLoi_ChiTiet chiTiet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            chiTiet.IDCauTraLoiChiTiet = CreateIdChiTiet();
            db.CauTraLoi_ChiTiet.Add(chiTiet);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ChiTietExist(chiTiet.IDCauTraLoiChiTiet))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = chiTiet.IDCauTraLoiChiTiet }, chiTiet);
        }

        private int CreateIdChiTiet()
        {
            var lastRow = db.CauTraLoi_ChiTiet.OrderByDescending(x => x.IDCauTraLoiChiTiet).SingleOrDefault();
            if(lastRow == null)
            {
                return 1;
            }
            else
            {
                return lastRow.IDCauTraLoiChiTiet + 1;
            }
        }

        private bool ChiTietExist(int id)
        {
            return db.CauTraLoi_ChiTiet.Count(x => x.IDCauTraLoiChiTiet == id) > 0;
        }
    }
}
