using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ServicePlatform.Areas.DownLoadFile.Func
{
    public class PageBar
    {
        /// <summary>
        /// 产生数字页码条
        /// </summary>
        /// <param name="pageIndex">当前也面值</param>
        /// <param name="pageCount">总的页数 </param>
        /// <returns></returns>
        public static string GetPageBar(int pageIndex, int pageCount)
        {
            //if (pageCount == 1)//如果总页数=1 
            //{
            //    return string.Empty;
            //}
            int start = pageIndex - 5;//起始位置，要求页面上显示10个数字页码
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;
            if (end > pageCount)
            {
                end = pageCount;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                //if (i == pageIndex) //正好循环的值等于当前页码值
                //{
                //    sb.Append(i);
                //}
                //else
                //{
                //    sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>", i));//{0}中显示的是i的值,也就是把pageIndex每次值都随着for循环+1
                //}
                sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>", i));
            }
            return sb.ToString();
        }
    }
}