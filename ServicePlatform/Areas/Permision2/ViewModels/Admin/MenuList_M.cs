using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class MenuList_M
    {
        public static MenuList_M ToViewModel(List<Menu> fathers)
        {
            return new MenuList_M() { fathers = fathers };
        }
        public List<Menu> fathers;
    }
}