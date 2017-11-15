using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.ToolHelper
{
    public static class CurrentTimeToEasyuiValue
    {
        public static string getValue() {

            #region 转换日期
            //获取当前系统时间转换为Easyui插件DateTimeBox的value默认值
            string theDate = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));//获得系统当前日期，格式为2008-9-4
            //使用split函数按照-将其分割
            string[] SplitDate = theDate.Split('-');
            //组装字符串变成Easyui的10/11/2012格式
            string CurrentDate = SplitDate[2] + "/" + SplitDate[1] + "/" + SplitDate[0] + " ";
            #endregion

            #region 转换时间
            string theTime = DateTime.Now.ToString("hh:mm:ss");//当前系统时间格式为08:05:57
            #endregion
            string ResultTime = CurrentDate + theTime;
            return ResultTime;
        }
    }
}