using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    public class C_NewsTime
    {
        public int OriTime { get; set; }
        public string ManTime { get; set; }
        public void SetManTime(int a) {
            ManTime = Convert.ToString(ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertIntDateTime(a));
        }
    }
}