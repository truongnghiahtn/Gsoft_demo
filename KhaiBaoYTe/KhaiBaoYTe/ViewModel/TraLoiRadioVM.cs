using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class TraLoiRadioVM
    {
        public int idCauHoi { get; set; }
        public string tenCauHoi { get; set; }
        public List<NoiDungCauHoiVM> noiDung { get; set; }
    }
}