using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class RoleMenuList_M
    {
        public static RoleMenuList_M ToViewModel(Role role)
        {
            return new RoleMenuList_M() { role = role };
        }
        public Role role;
    }
}