using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class UserList_M
    {
        public static UserList_M ToViewModel(string userid)
        {
            return new UserList_M() { userid = userid };
        }
        public string userid;
    }
}