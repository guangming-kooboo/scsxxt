using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.ToolHelper
{
    public class CheckMaxInnerID
    {
        private ContentContext Sd = new ContentContext();
        public int MaxInnerId = 0;
        public void getMaxInnerId() { 
            //获得记录中InnerId最大的InnerId数值
            int a = Sd.T_News.Count();
            if(a>0){
                //新闻表不为空的情况
                MaxInnerId = Sd.T_News.Max(p => p.InnerID);
               //做一步查询找到当前新闻数据中InnerID最大的值
            }
        }
    }
}