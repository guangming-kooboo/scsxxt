using System.Collections.Generic;
using Qx.Community.Models;
using System;

namespace ServicePlatform.Areas.BBS.ViewModels.ManagerCerter
{
    public class ForumManeger_M
    {
        public static ForumManeger_M ToViewModel(string id)
        {
            return new ForumManeger_M() { id = id };
        }
        public string id;
    }
}