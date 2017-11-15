using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class About_M
    {
        public static About_M ToViewModel(string uid)
        {
            return new About_M()
            {
                uid=uid
            };
        }

        public string uid { get; set; }
    }
}