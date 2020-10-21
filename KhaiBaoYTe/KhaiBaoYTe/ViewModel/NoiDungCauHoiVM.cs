using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class NoiDungCauHoiVM
    {
        //lay thong tin cua tung cau hoi va so luong
        public int idCauHoi { get; set; }
        public int idTemplate { get; set; }
        public int idLoaiCauHoi { get; set; }
        public string noiDung { get; set; }
        public int soLg { get; set; }
    }
}