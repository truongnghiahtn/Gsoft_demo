using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class CauHoiVM
    {
        //dap an cho bo loc, lay du lieu tu db
        public int IDCauHoi { get; set; }
        public string tenTemplate { get; set; }
        public string TieuDe { get; set; }
        public Nullable<int> IDLoaiCauHoi { get; set; }
        public string DangCauHoi { get; set; }
        public bool CauHoiRequired { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public Nullable<System.DateTime> NgayUpdate { get; set; }
        public string NguoiUpdate { get; set; }
        public int SoLgTraLoi { get; set; }
        public int SoLgNoiDung { get; set; }
        public int? SoDiem { get; set; }
        public bool? CauHoiEnable { get; set; }
    }
}