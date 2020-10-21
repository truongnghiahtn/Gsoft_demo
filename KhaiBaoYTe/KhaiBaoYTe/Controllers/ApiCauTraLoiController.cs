using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhaiBaoYTe.Models;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using System.Data.Entity.Validation;

namespace KhaiBaoYTe.Controllers
{
    public class ApiCauTraLoiController : ApiController
    {
        private KhaiBaoYTeEntities db = new KhaiBaoYTeEntities();

        // POST: api/ApiCauTraLoi
        [ResponseType(typeof(CauTraLoi))]
        [HttpPost]
        public IHttpActionResult PostCauTraLoi(CauTraLoi traLoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            traLoi.IDCauTraLoi = CreateIdCauTraLoi();
            traLoi.UserID = CreateUserId(traLoi);

            // insert vào bảng chi tiết ở db
            int i = 0;
            foreach(var item in traLoi.CauTraLoi_ChiTiet)
            {
                item.IDCauTraLoiChiTiet = CreateIdChiTiet() + i;
                item.IDCauTraLoi = traLoi.IDCauTraLoi;
                i++;
            }

            db.CauTraLois.Add(traLoi);
            try
            {
                db.SaveChanges();
                
            }
            catch (DbUpdateException)
            {
                if (CauTraLoiExists(traLoi.IDCauTraLoi))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = traLoi.IDCauTraLoi }, traLoi);
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // trả về true nếu trong bảng Câu trả lời đã tồn tại ít nhất 1 IDCauTraLoi
        private bool CauTraLoiExists(int id)
        {
            return db.CauTraLois.Count(x => x.IDCauTraLoi == id) > 0;
        }

        private int CreateIdCauTraLoi()
        {
            var lastRow = db.CauTraLois.OrderByDescending(x => x.IDCauTraLoi).FirstOrDefault();
            if(lastRow == null)
            {
                return 1;
            }
            else
            {
                return lastRow.IDCauTraLoi + 1;
            }
        }

        //Khởi tạo 1 id cho bảng Câu trả lời hoặc tạo thêm nếu trong bảng câu trả lời đã tồn tại id
        private int CreateUserId(CauTraLoi traLoi)
        {
            var user = db.CauTraLois.Where(x => x.HoTen == traLoi.HoTen && x.MSNV == traLoi.MSNV && x.Email == traLoi.Email);
            if (user.Count() == 0)
            {
                var userLastRow = db.CauTraLois.OrderByDescending(x => x.UserID).FirstOrDefault();
                if (userLastRow == null)
                {
                    return 1;
                }
                else
                {
                    return userLastRow.UserID.Value + 1;
                }
            }
            else
            {
                return user.FirstOrDefault().UserID.Value;
            }
        }
        //Khởi tạo 1 id cho bảng Câu trả lời chi tiết hoặc tạo thêm nếu trong bảng câu trả lời đã tồn tại id
        private int CreateIdChiTiet()
        {
            var lastRow = db.CauTraLoi_ChiTiet.OrderByDescending(x => x.IDCauTraLoiChiTiet).FirstOrDefault();
            if (lastRow == null)
            {
                return 1;
            }
            else
            {
                return lastRow.IDCauTraLoiChiTiet + 1;
            }
        }
    }
}
