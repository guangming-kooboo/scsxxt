﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.DownLoadFile.Func
{
    public class GetDataInfo
    {
        public int PageSize { get; set; }//获取第几页
        public int PageIndex { get; set; }//获取每一页的行数
    }
}