using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.Models
{
    public class TraLoiVM
    {
        public int SoLgChuDe { get; set; }
        public int SoLgTemplate { get; set; }
        public int SoLgCauHoi { get; set; }
        public int SoLgTraLoi { get; set; }
        public List<ChiTietVM> TraLoi { get; set; }
    }

    public class ChiTietVM
    {
        public string TieuDe { get; set; }
        public int SoLg { get; set; }
    }

    public class TraLoiSearchModel
    {
        public string IdChuDe { get; set; }
        public string IdTemplate { get; set; }
        public string IDCauHoi { get; set; }
        public string TieuDe { get; set; }
        public int SoLg { get; set; }
    }
}