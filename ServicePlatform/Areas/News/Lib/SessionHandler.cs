using ServicePlatform.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.Lib
{
    public static class SessionHandler
    {
        //用于处理Session值与文件路径文件夹名称的对接
        public static string SubSysHandle(string Subsys) {
            //子系统处理缓冲区
            string Sub = Subsys;
            if (Sub == SubSystem.PLATFORM)
            {
                Sub = "Platform";
            }
            else if (Sub == SubSystem.SCHOOL)
            {
                Sub = "School";
            }
            else if (Sub == SubSystem.ENTERPRISE)
            {
                Sub = "Enterprise";
            }
            else {
                Sub = null;
            }
            return Sub;
        }
    }
}