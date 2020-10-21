using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class TraLoiVM
    {
        public string TieuDe { get; set; }
        public int SoLg { get; set; }
    }
    public class TraLoiSearchModel
    {
        public string IdChuDe { get; set; }
        public string IdTemplate { get; set; }
        public string IDCauHoi { get; set; }
        public string TieuDe
        {
            get; set;
        }
        public int SoLg { get; set; }
    }
}