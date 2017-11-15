using Qx.Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.BBS.ViewModels.ManagerCerter
{
    public class SearchForumManager_M
    {
        public static SearchForumManager_M ToViewModel(Forum forum, Column column)
        {
            return new SearchForumManager_M() { forum = forum, column = column };
        }
        public Forum forum;
        public Column column;
    }
}