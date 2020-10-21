using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.Models
{
    public class CheckBox
    {
        public string IDCauHoi { get; set; }
        public string TenCauHoi { get; set; }
        public List<NoiDung> list = new List<NoiDung>();
    }
}