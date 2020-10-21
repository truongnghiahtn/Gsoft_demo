using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class ThongKeTextBoxVM
    {
        public int idChuDe { get; set; }
        public int idTemplate { get; set; }
        public string tenTemplate { get; set; }
        public int idCauHoi { get; set; }
        public string tenCauHoi { get; set; }
        public List<string> cauTraLoi { get; set; }
    }
}