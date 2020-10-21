using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class ChuDeVM
    {
        public int IDChuDe { get; set; }
        public string TenChuDe { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public Nullable<System.DateTime> NgayUpdate { get; set; }
        public string NguoiUpdate { get; set; }
        public int SoLgCauHoi { get; set; }
        public int SoLgTraLoi { get; set; }
    }
}