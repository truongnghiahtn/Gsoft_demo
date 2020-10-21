using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class ThongKeVM
    {
        public List<BangFilterTraLoiVM> bangTraLoi { get; set; } //bang dap an cho bo loc
        public List<ThongKeTraLoiVM> listThongKeTraLoi { get; set; } //ds cac cau tra loi cua user
        public List<string> listHoTen { get; set; }
        public List<string> listMssv { get; set; }
        public List<string> listEmail { get; set; }
        public List<ThongKeTextBoxVM> listThongKeTextBox { get; set; }
        public ThongKeVM()
        {
            bangTraLoi = new List<BangFilterTraLoiVM>();
            listThongKeTraLoi = new List<ThongKeTraLoiVM>();
            listThongKeTextBox = new List<ThongKeTextBoxVM>();
            listHoTen = new List<string>();
            listMssv = new List<string>();
            listEmail = new List<string>();
        }
    }

   
}