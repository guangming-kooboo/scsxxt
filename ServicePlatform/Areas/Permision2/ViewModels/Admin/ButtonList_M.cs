using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class ButtonList_M
    {
        public static ButtonList_M ToViewModel(string buttonid)
        {
            return new ButtonList_M() { buttonid = buttonid };
        }
        public string buttonid;
    }
}