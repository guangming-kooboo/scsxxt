using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class EasyuiToDateTime
    {
        //因为Easyui的时间格式返回的是：'3/4/2010 2:3:10'，需要转换成Datetime格式
        public string ToDateTime(string TargetTime)
        {
            string ConsoleTime = TargetTime.Replace("/", "-");
            return ConsoleTime;
        }
    }
}