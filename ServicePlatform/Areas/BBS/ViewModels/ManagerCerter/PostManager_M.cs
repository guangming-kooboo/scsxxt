using System;
using System.Collections.Generic;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.ManagerCerter
{
    public class PostManager_M
    {
        public static PostManager_M ToViewModel(Forum forum, Column column, Theme theme)
        {
            return new PostManager_M() { forum = forum, column = column, theme = theme };
        }
        public Forum forum;
        public Column column;
        public Theme theme;

    }
}