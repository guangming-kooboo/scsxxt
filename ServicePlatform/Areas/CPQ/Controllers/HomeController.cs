using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;


namespace ServicePlatform.Areas.CPQ.Controllers
{
    public class HomeController : BaseAccountController//针对游客
    {
        //
        // GET: /CPQ/Home/


        #region 问卷的显示
        //在系统首页浮框上展示部分已经发布的问卷
        public ActionResult showQuestionnaire()
        {
            return View();
                
        }


        //点击浮框上的更多，展示全部已经发布的问卷
        //CPQ/Home/showQuestionnaire_More?ReportID=CPQ查看更多问卷&Params=;
        public ActionResult showQuestionnaire_More(string ReportID = "CPQ查看更多问卷", string Params = ";", int pageIndex = 1, int perCount = 10)
        {
                                       
            InitReport("查看更多问卷", "#", pageIndex, perCount);
            return View();
        }


        #endregion 已经创建问卷的操作管理


       
       

        //游客答完题目进入谢谢答题页面
        public ActionResult thanksForAnswer( )
        {
            InitAdminLayout("欢迎答题页面", "");
            return View();
        }


       
    }
}
