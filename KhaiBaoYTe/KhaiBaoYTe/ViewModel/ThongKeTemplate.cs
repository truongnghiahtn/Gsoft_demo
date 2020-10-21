using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhaiBaoYTe.ViewModel
{
    public class ThongKeTemplate
    {
        public List<TemplateVM> bangTraLoi { get; set; }

        public ThongKeTemplate()
        {
            bangTraLoi = new List<TemplateVM>();            
        }
    }
}