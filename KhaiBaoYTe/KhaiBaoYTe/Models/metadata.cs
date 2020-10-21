using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace KhaiBaoYTe.Models
{
    public class MetaData
    {
        public class ChuDeMetaData
        {
            [Display(Name = "tên chủ đề")]
            public string TenChuDe;
            
            [Display(Name = "mô tả")]
            public string MoTa;

            [StringLength(50)]
            [Display(Name = "người tạo")]
            public string NguoiTao;

            [StringLength(50)]
            [Display(Name = "người update")]
            public string NguoiUpdate;
        }

        public class TemplateMetaData
        {
            [Display(Name = "tên template")]
            public string TenTemplate;
            
            [Display(Name = "mô tả")]
            public string MoTa;

            [StringLength(50)]
            [Display(Name = "người update")]
            public string NguoiUpdate;

            [StringLength(50)]
            [Display(Name = "người tạo")]
            public string NguoiTao;
        }

        public class CauHoiMetaData
        {
            [StringLength(50)]
            [Display(Name = "người tạo")]
            public string NguoiTao;

            [StringLength(50)]
            [Display(Name = "người update")]
            public string NguoiUpdate;
        }

        public class CauTraLoiMetaData
        {
            [StringLength(50)]
            [Display(Name = "họ tên")]
            public string HoTen;

            [StringLength(10)]
            [Display(Name = "mã số nhân viên")]
            public string MSNV;

            [StringLength(100)]
            [Display(Name = "email")]
            public string Email;
        }

        public class Sub_CauHoiMetaData
        {
            [StringLength(50)]
            [Display(Name = "nội dung")]
            public string NoiDung;
        }


    }
}