using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhaiBaoYTe.Models;

namespace KhaiBaoYTe.ViewModel
{
    public class ThongKeUserVM
    {
        public int idCauTraLoi { get; set; }
        public int idChuDe { get; set; }
        public int idTemplate { get; set; }
        public string tenTemplate { get; set; }
    }
}