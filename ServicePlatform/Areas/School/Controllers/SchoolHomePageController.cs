using ServicePlatform.Areas.HPManager.FuncHelper;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Lib;


namespace ServicePlatform.Areas.School.Controllers
{
    public class SchoolHomePageController : BaseAccountController
    {

        /*=========================================================
        变量声明*/

        private int STATE_COLUMN101 = 101;//新闻NewsColumnID属性 101=>高校端：学院公告
        private int STATE_COLUMN102 = 102;//新闻NewsColumnID属性 102=>高校端：班级公告
        private int STATE_COLUMN103 = 103;//新闻NewsColumnID属性 103=>高校端：企业公告
        private int STATE_COLUMN104 = 104;//新闻NewsColumnID属性 104=>高校端：视频新闻
        private int STATE_COLUMN105 = 105;//新闻NewsColumnID属性 105=>高校端：实习随拍
        private int STATE_COLUMN106 = 106;//新闻NewsColumnID属性 106=>高校端：招聘信息
        private int STATE_COLUMN107 = 107;//新闻NewsColumnID属性 107=>高校端：实习日记
        private int STATE_COLUMN108 = 108;//新闻NewsColumnID属性 108=>高校端：实习辅导
        private int STATE_COLUMN109 = 109;//新闻NewsColumnID属性 109=>高校端：资料下载
        private int STATE_COLUMN111 = 111;
        private int STATE_COLUMN112 = 112;
        private int STATE_COLUMN113 = 113;
        private int STATE_COLUMN114 = 114;
        private int STATE_COLUMN116 = 116;
        private int STATE_COLUMN117 = 117;
        private int STATE_COLUMN118 = 118;
        private int STATE_COLUMN119 = 119;

        private string CURRENT_Unit;

        private int STATE_TYPE1 = 1;//新闻NewsTypeID属性 1=>图文新闻
        private int STATE_TYPE2 = 2;//新闻NewsTypeID属性 2=>图片新闻

        static int TabCol =0;//这里需要初始化为数据
        static string GetSchoolID;
        /*==========================================================*/

        private ContentContext storeDA = new ContentContext();
        //private ContentContext storeDB = new ContentContext();

        public SchoolHomePageController() {
           
            //构造对象时，给当前单位赋值
        }

        //定义SelectNews方法获取当前单位下的新闻列表
        public List<T_News> SelectNews(string GetTheUnit) {
            //List<T_News> ConsoleNews = storeDA.T_News.Where(p => p.UnitID == GetTheUnit).ToList();
            List<T_News> ConsoleNews = (from a in storeDA.T_Content from b in storeDA.T_News where (a.ContentID == b.NewsID && a.UnitID == GetTheUnit) select b).ToList();
            return ConsoleNews;
        }
        //新主页----
        public ActionResult NewIndex(string SchoolID)
        {
         
            ViewData["SchoolID"] = SchoolID;
            HPShowData HPContData = new HPShowData();//获取单位完整内容集合
            switch (SchoolID)
            {
                case "10385":
                    ViewData["AcademyNotice"] = HPContData.GetShowData(STATE_COLUMN101);//获取学院新闻
                    ViewData["ClassNotice"] = HPContData.GetShowData(STATE_COLUMN102); //获取班级新闻
                    ViewData["EntNotice"] = HPContData.GetShowData(STATE_COLUMN103);//获取企业新闻
                    ViewData["VideoNotice"] = HPContData.GetShowData(STATE_COLUMN104);//获取视频新闻
                    ViewData["HireNotice"] = HPContData.GetShowData(STATE_COLUMN106); //获取招聘信息
                    ViewData["InternShipNotice"] = HPContData.GetShowData(STATE_COLUMN107);//获取实习日记
                    ViewData["InternShipGuide"] = HPContData.GetShowData(STATE_COLUMN108);//获取实习辅导
                    ViewData["DownLoad"] = HPContData.GetShowData(STATE_COLUMN109);//获取下载资源
                    break;
                default:
                    break;
            }

            return View(S<Core.Services.Entity.T_School>().Find(SchoolID));
        }

        //放置学校主页
        public ActionResult Index(string SchoolID)
        {
            //学校主页
            //List<T_News> Tar1 = SelectNews(CurrentSession);            
            HPShowData HPContData = new HPShowData();//获取单位完整内容集合
            ViewData["SchoolID"] = SchoolID;//传递学校ID
            GetSchoolID = SchoolID;

            switch(SchoolID){
                case "10385":
                    ViewData["AcademyNotice"] = HPContData.GetShowData(STATE_COLUMN101);//获取学院新闻
                    ViewData["ClassNotice"] = HPContData.GetShowData(STATE_COLUMN102); //获取班级新闻
                    ViewData["EntNotice"] = HPContData.GetShowData(STATE_COLUMN103);//获取企业新闻
                    ViewData["VideoNotice"] = HPContData.GetShowData(STATE_COLUMN104);//获取学院新闻
                    ViewData["HireNotice"] = HPContData.GetShowData(STATE_COLUMN106); //获取招聘信息
                    ViewData["InternShipNotice"] = HPContData.GetShowData(STATE_COLUMN107);//获取实习日记
                    ViewData["InternShipGuide"] = HPContData.GetShowData(STATE_COLUMN108);//获取实习辅导
                    ViewData["DownLoad"] = HPContData.GetShowData(STATE_COLUMN109);//获取实习辅导
                break;
                case "10384":
                    ViewData["AcademyNotice"] = HPContData.GetShowData(STATE_COLUMN111);
                    ViewData["ClassNotice"] = HPContData.GetShowData(STATE_COLUMN112);
                    ViewData["EntNotice"] = HPContData.GetShowData(STATE_COLUMN113);
                    ViewData["VideoNotice"] = HPContData.GetShowData(STATE_COLUMN114);
                    ViewData["HireNotice"] = HPContData.GetShowData(STATE_COLUMN116);
                    ViewData["InternShipNotice"] = HPContData.GetShowData(STATE_COLUMN117);
                    ViewData["InternShipGuide"] = HPContData.GetShowData(STATE_COLUMN118);
                    ViewData["DownLoad"] = HPContData.GetShowData(STATE_COLUMN119);
                break;
                default:
                break;
            }

            List<T_News> PicNotice = (from s in storeDA.T_News from e in storeDA.T_Content where (s.NewsID == e.ContentID && e.UnitID == GetSchoolID && s.NewsTypeID == STATE_TYPE2) orderby s.NewsID select s).ToList();
            ViewData["PicNotice"] = PicNotice;//实习随拍轮播图片

            return View();
        }
        public ActionResult GetTabIndex()
        {
            //用于获取Tab标签页的选择:学校主页的Tab标签页通过onclick点击事件向后台传递所点击的栏目ID
            int GetColID = Convert.ToInt32(Request["IndexId"]);
            TabCol = GetColID;
            return View();
        }

        public ActionResult TabToNewsList()
        {
            //string GetUnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            string GetUnitID = GetSchoolID;
            int GetTabCol = 0;
            if (TabCol==0)
            {
                //当传回的Tab页点击为0时表示该单位的学院公告
                var Col = from a in storeDA.T_HomePageColumn where a.HPColumnName == "学院公告" && a.UnitID == GetUnitID select a;
                foreach(var a in Col){
                    GetTabCol = a.HPColumnID;
                }
            }
            else if (TabCol == 1)
            {
                var Col = from a in storeDA.T_HomePageColumn where a.HPColumnName == "班级公告" && a.UnitID == GetUnitID select a;
                foreach (var a in Col)
                {
                    GetTabCol = a.HPColumnID;
                }
            }
            else if (TabCol == 2)
            {
                var Col = from a in storeDA.T_HomePageColumn where a.HPColumnName == "企业公告" && a.UnitID == GetUnitID select a;
                foreach (var a in Col)
                {
                    GetTabCol = a.HPColumnID;
                }
            }
            List<T_Content> Result = CollectContentList.Collect(GetTabCol, 11);//该函数依据HomePageColumn表判断ContentFrom值是否存在，如果存在则直接获取Content表中该栏目的所有内容
            ViewData["TabGetMore"] = Result;
            return View();
        }

        public ActionResult HomeIndexNewsList(string ColumnName)
        {
            //点击More获取的新闻列表页

            string TargetColName = ColumnName;

            List<T_Content> Tar = CollectContentList.Collect2(TargetColName, 11, GetSchoolID);
            return View(Tar);
        }
        public ActionResult HomeNewsDetail(string id)
        {
            //前台新闻详情页
            T_Content TargetNewsDetail = storeDA.T_Content.Find(id);
            //依据T_Content表寻找T_News表内容
            var TargetNewsContent = from a in storeDA.T_News where (a.NewsID == id) select a;
            ViewData["TargetNewsPic"] = "";
            ViewData["TargetNewsHtml"] = "";
            foreach (var m in TargetNewsContent)
            {
                ViewData["TargetNewsPic"] = m.PicUrl;
                ViewData["TargetNewsHtml"] = m.Html;
            }
            return View(TargetNewsDetail);
        }

        public ActionResult VideoNewsList(string ColumnName)
        {
            string TargetColName = ColumnName;
            List<T_Content> Tar = CollectContentList.Collect2(TargetColName, 11, GetSchoolID);
            return View(Tar);
        }

        public ActionResult HomeVideoDetail(string id)
        {
            //视频新闻详情页
            T_Content TVDetail = storeDA.T_Content.Find(id);
            //string VideoLink = "";
            var Content = from a in storeDA.T_Content from b in storeDA.T_News where a.ContentID == b.NewsID && a.ContentID==id select b;

            ViewData["VideoLink"] = Content;
            return View(TVDetail);
        }
        public ActionResult DownLoad(string ColumnName)
        {
            //资源下载
            string TargetColName = ColumnName;
            List<T_Content> Tar = CollectContentList.Collect2(TargetColName, 31, GetSchoolID);
            return View(Tar);
        }
        public ActionResult PicNewsList()
        {
            //图片新闻列表页
            List<T_News> PicN = (from s in storeDA.T_News from e in storeDA.T_Content where (s.NewsID == e.ContentID && e.UnitID == GetSchoolID && s.NewsTypeID == STATE_TYPE2) orderby s.NewsID select s).ToList();
            ViewData["PicN"] = PicN;

            return View();
        }

        public ActionResult InternShipDiaryList()
        {
            //实习日记列表页

            return View();
        }

    }
}
