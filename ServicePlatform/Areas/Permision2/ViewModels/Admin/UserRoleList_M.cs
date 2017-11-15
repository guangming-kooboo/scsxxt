using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.Admin
{
    public class UserRoleList_M
    {
        public static UserRoleList_M ToViewModel(User user)
        {
            return new UserRoleList_M() {user=user };
        }
        public User user;
    }
}