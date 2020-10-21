using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class ThongKeTraLoiVM
    {
        //lay cac cau tra loi la checkbox va radiobutton
        public int idChuDe { get; set; }
        public int idTemplate { get; set; }
        public int idCauHoi { get; set; }
        public string tenTemplate { get; set; }
        public string tenCauHoi { get; set; }
        public int idLoaiCauHoi { get; set; }
        
        public List<NoiDungCauHoiVM> noiDung { get; set; }
    }
}