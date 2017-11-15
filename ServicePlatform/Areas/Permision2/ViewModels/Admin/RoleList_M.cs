using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class RoleList_M
    {
        public static RoleList_M ToViewModel(string roleid)
        {
            return new RoleList_M() { roleid = roleid };
        }
        public string roleid;
    }
}