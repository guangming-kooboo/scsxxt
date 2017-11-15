using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class BanButtonList_M
    {
        public static BanButtonList_M ToViewModel(Role role)
        {
            return new BanButtonList_M() { role = role };
        }
        public Role role;
    }
}