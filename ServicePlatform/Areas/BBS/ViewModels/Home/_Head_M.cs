using Qx.Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class _Head_M
    {
        public static _Head_M ToViewModel(UserInformation user)
        {
            return new _Head_M() { User=user };
        }
        public UserInformation User;
    }
}