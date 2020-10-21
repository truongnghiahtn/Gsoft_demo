using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using static KhaiBaoYTe.Models.MetaData;

namespace KhaiBaoYTe.Models
{
    [MetadataType(typeof(ChuDeMetaData))]
    public partial class ChuDe
    {
    }

    [MetadataType(typeof(TemplateMetaData))]
    public partial class Template
    {
    }

    [MetadataType(typeof(CauHoiMetaData))]
    public partial class CauHoi
    {
    }

    [MetadataType(typeof(CauTraLoiMetaData))]
    public partial class CauTraLoi
    {
    }

    [MetadataType(typeof(Sub_CauHoiMetaData))]
    public partial class Sub_CauHoi
    {
    }


}