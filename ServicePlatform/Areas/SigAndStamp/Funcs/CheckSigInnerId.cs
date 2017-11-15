using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.SigAndStamp.Funcs
{
    public class CheckSigInnerId
    {
        private ContentContext Sd = new ContentContext();
        public int MaxInnerId = 0;
        public void getMaxSigInnerId()
        {
            //获得记录中InnerId最大的InnerId数值
            int a = Sd.T_Signature.Count();
            if (a > 0)
            {
                //新闻表不为空的情况
                MaxInnerId = Sd.T_Signature.Max(p => p.InnerID);                
            }
        }
    }
}