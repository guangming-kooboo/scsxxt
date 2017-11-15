using ServicePlatform.App_Start;
using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.School.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /School/Home/

        SchoolContext sc = new SchoolContext();
        #region 学校首页
        // GET: /School/Home/SchoolIndex?SchoolID=
        public ActionResult SchoolIndex(string SchoolID)
        {
            var old = sc.School.Find(SchoolID);
            if (old != null)
            {
                ViewBag.data = old;
                return Redirect("/School/SchoolHomePage/NewIndex?SchoolID=" + SchoolID);
                //return View();
            }
            else
            {
                return Redirect("/");
            }

        }
        #endregion
        [BaseActionFilter]
        public ActionResult Index(string type)
        {
            ViewBag.Type = type;
            return View();
        }

        public ActionResult _top()
        {
            return View();
        }

        public ActionResult _left()
        {
            return View();
        }

        public ActionResult _left_stu()
        {
            return View();
        }

        public ActionResult _right()
        {
            return View();
        }
        //生成 学校目录
        public int CreateSchoolDirectorys(string SchoolID)
        {
            if (SchoolID == null || SchoolID == "")
            {
                return -1;
            }
            else
            {
                string SchoolDirectorysHome = "/UserFiles/School/" + SchoolID;
                return FileOperate.CreateDirectorys(new List<string>()
            {
                SchoolDirectorysHome+"/BasicInfo",
                SchoolDirectorysHome+"/Contents",
                 SchoolDirectorysHome+"/Contents/Ads",
                 SchoolDirectorysHome+"/Contents/DownLoadFiles",
                  SchoolDirectorysHome+"/Contents/News",
                 SchoolDirectorysHome+"/Contents/Signatures",
                  SchoolDirectorysHome+"/Contents/Stamps",
                SchoolDirectorysHome+"/OtherFiles",
                SchoolDirectorysHome+"/OtherFiles/StuHeadPic",
                SchoolDirectorysHome+"/OtherFiles/StuReport",
                SchoolDirectorysHome+"/OtherFiles/StuResume",
                SchoolDirectorysHome+"/OtherFiles/StuShowPic",
                SchoolDirectorysHome+"/OtherFiles/StuShowVedios",

            });
            }

        }
        //删除 学校目录
        public int DeleteSchoolDirectorys(string SchoolID)
        {
            if (SchoolID == null || SchoolID == "")
            {
                return -1;
            }
            else
            {
                string EntDirectorysHome = "/UserFiles/School/" + SchoolID;
                return FileOperate.DeleteDirectorys(new List<string>()
            {
                EntDirectorysHome
            }
            );

            }
        }
    }
}
