using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.ToolHelper
{
    public class EasyuiToDateTime
    {
        //因为Easyui的时间格式返回的是：'3/4/2010 星期五 上午 2:3:10'，需要转换成Datetime格式
        public static string ToDateTime(string TargetTime)
        {
            //string ConsoleTime = TargetTime.Replace("/","-");
            string TrimTime = TargetTime.Trim();
            string[] SplitA = TrimTime.Split(' ');
            string[] SplitB = SplitA[0].Split('/');
            string NewTime = SplitB[0] + "-" + SplitB[1] + "-" + SplitB[2] + " " + SplitA[3];
            return NewTime;
        }
    }
}