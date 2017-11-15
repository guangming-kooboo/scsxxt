using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.HPManager.FuncHelper
{
    public class HPMGetPageInfo
    {
        public int PageSize { get; set; }//获取第几页
        public int PageIndex { get; set; }//获取每一页的行数
    }
}