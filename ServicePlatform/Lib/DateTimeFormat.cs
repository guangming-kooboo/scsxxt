using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Lib
{
    public class DateTimeFormat
    {
        public static int GetTime()
        {
            return ConvertDateTimeInt(System.DateTime.Now);
        }
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">int 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(int d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>int</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (int)(time - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// 将输入的年月日 转换为DateTime类型时间,如果输入不合法则返回 DateTime.MinValue 【年月日-->年—月-日】-->DateTime-->Int
        /// </summary>
        /// <param name="Y">年</param>
        /// <param name="M">月</param>
        /// <param name="D">日</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ChekInputDate(string Y, string M, string D)
         {

             int year=0;
             int month = 0;
             int day =0;
            //输入的为数字，并且满足要求的范围 
             if ( int.TryParse(Y,out year)&& int.TryParse(M, out month)&& int.TryParse(D, out day) &&year >= 1970 && year <= 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
             {
                 return TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(year, month, day));
             }
             else
             {
                 return DateTime.MinValue;
             }
         }

        public static System.DateTime ChekInputDate(string DateString)
        {
            DateString = DateString.Replace("-", "").Replace(":", "").Replace(" ", "");
            if (DateString.Length == 8)
            {
                DateString +="000000" ;
            }
            if (DateString.Length >14 )
            {
                DateString = DateString.Substring(0, 14);
            }
            if(DateString.Length==14)
            {
                string Y = DateString.Substring(0, 4);//年
                string M = DateString.Substring(4, 2);//月
                string D = DateString.Substring(6, 2);//日
                string h = DateString.Substring(8, 2);//时
                string m = DateString.Substring(10, 2);//分
                string s = DateString.Substring(12, 2);//秒
                int year = int.Parse(Y);
                int month =int.Parse(M);
                int day = int.Parse(D);
                int hour = int.Parse(h);
                int minute = int.Parse(m);
                int second = int.Parse(s);
                //输入的为数字，并且满足要求的范围 
                if ( 
                    year >= 1970 &&  year <= 2050 && 
                    month >= 1 &&  month <= 12 && 
                    day >= 1 && day <= 31&&
                    hour >= 0 && hour <= 23 &&
                    minute >= 0 && minute <= 59 &&
                    second >= 0 && second <= 59
                    )
                {
                    return new DateTime(year, month, day, hour, minute, second);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
           
            else
            {
                return DateTime.MinValue;
            }
        }
 
    }
}