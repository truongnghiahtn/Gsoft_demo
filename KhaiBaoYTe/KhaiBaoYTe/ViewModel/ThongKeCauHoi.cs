using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class ThongKeCauHoi
    {
        public List<CauHoiVM> bangTraLoi { get; set; }

        public ThongKeCauHoi()
        {
            bangTraLoi = new List<CauHoiVM>();
        }
        
    }
}