using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class TemplateVM
    {
        //dap an cho bo loc, lay du lieu tu db
        public int IDTemplate { get; set; }
        public string TenChuDe { get; set; }
        public string TenTemplate { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayUpdate { get; set; }
        public string NguoiUpdate { get; set; }
        public string NguoiTao { get; set; }
        public int HienThi { get; set; }
        public int SoLgCauHoi { get; internal set; }
        public int SoLgCauTraLoi { get; internal set; }
        public bool? TinhDiem { get; set; }
        public bool? Random { get; set; }
        public bool TemplateEnable { get; set; }
    }
}