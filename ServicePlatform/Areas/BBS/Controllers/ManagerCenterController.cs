using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Areas.BBS.ViewModels.ManagerCerter;
using Qx.Community.Interfaces;

namespace ServicePlatform.Areas.BBS.Controllers
{
    public class ManagerCenterController : BaseAccountController
    {
        //
        // GET: /BBS/ManagerCenter/

        private IForumService _forum;
        private IThemeService _theme;
        public ManagerCenterController(IForumService forumService,IThemeService themeService)
        {
            _forum = forumService;
            _theme = themeService;
        }
        //菜单导航
        public ActionResult Index()
        {
            InitMenu(new Dictionary<string, string>() { 
                {"版块管理","/BBS/ManagerCenter/ForumManager?ReportID=BBS_版块管理&Params=;"},
                {"查看版主","/BBS/ManagerCenter/SearchForumManager?ReportID=BBS_查看版主&Params=;"},
                {"子版块管理","/BBS/ManagerCenter/ColumnManager?ReportID=BBS_子版块管理&Params=;"},
                {"主题管理","/BBS/ManagerCenter/ThemeManager?ReportID=BBS_主题管理&Params=;"},
                {"帖子管理","/BBS/ManagerCenter/PostManager?ReportID=BBS_帖子管理&Params=;"},
                {"用户列表","/BBS/ManagerCenter/SearchAllUsers?ReportID=BBS_用户列表&Params=;"}
            });
            return View();
        }
       
        //BBS/ManagerCenter/ForumManager?ReportID=BBS_版块管理&Params=;
        public ActionResult ForumManager(string ReportID = "BBS_版块管理", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            InitReport("版块管理", "/BBS/CRUD/AddForum", pageIndex, perCount);
            return View(ForumManeger_M.ToViewModel(Params));
        }
        //BBS/ManagerCenter/SearchForumManager?ReportID=BBS_查看版主&Params=;
        public ActionResult SearchForumManager(string ReportID = "BBS_查看版主", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            var column = _forum.GetColumn(Params);
            InitReport("查看版主", "/BBS/CRUD/AddForumer?id=" + Params, pageIndex, perCount);
            return View(SearchForumManager_M.ToViewModel(_forum.GetForum(column.ForumID),column));
        }
        //BBS/ManagerCenter/ColumnManager?ReportID=BBS_子版块管理&Params=;
        public ActionResult ColumnManager(string ReportID = "BBS_子版块管理", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            InitReport("子版块管理", "/BBS/CRUD/AddColumn?id=" + Params, pageIndex, perCount);
            return View(ColumnManager_M.ToViewModel(_forum.GetForum(Params)));
        }
      
        //BBS/ManagerCenter/ThemeManager?ReportID=BBS_主题管理&Params=;
        public ActionResult ThemeManager(string ReportID = "BBS_主题管理", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            var column= _forum.GetColumn(Params);
            InitReport("主题管理", "/BBS/CRUD/AddTheme?id=" + Params, pageIndex, perCount);
            return View(ThemeManager_M.ToViewModel(_forum.GetForum(column.ForumID),column));
        }
        //BBS/ManagerCenter/PostManager?ReportID=BBS_帖子管理&Params=;
        public ActionResult PostManager(string ReportID = "BBS_帖子管理", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            var theme = _theme.GetThemeByThemeID(Params);
            var column = _forum.GetColumn(theme.ColumnID);
            InitReport("帖子管理", "#", pageIndex, perCount);
            return View(PostManager_M.ToViewModel(_forum.GetForum(column.ForumID),column,theme));
        }
        //BBS/ManagerCenter/SearchAllUsers?ReportID=BBS_用户列表&Params=;
        public ActionResult SearchAllUsers(string ReportID = "BBS_用户列表", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            InitReport("用户列表", "#", pageIndex, perCount);
            return View();
        }
    }
}
