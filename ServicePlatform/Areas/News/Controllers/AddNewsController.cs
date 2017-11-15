using ServicePlatform.Areas.News.ToolHelper;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Lib;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.App_Start;

namespace ServicePlatform.Areas.News.Controllers
{
    public class AddNewsController : Controller
    {
        /*=========================================================
        变量声明*/

        private string UNITID ;
        private string SUBSYS;
        private int GETINNER = 0;
        private bool UploadSuccess=false;

        /*==========================================================*/

        private ContentContext StoreDD = new ContentContext();
        private T_Content StoreDE = new T_Content();

        //新闻添加

        [BaseActionFilter]
        public ActionResult AddNews()
        {

            return View();
        }
                
        [HttpPost]
        [ValidateInput(false)]
        [MultiButton("sub3")]
        [BaseActionFilter]
        public ActionResult AddNews(T_News theNew, FormCollection collection, HttpPostedFileBase NewsPicUpload, HttpPostedFileBase NewsVideoUpload)
        {
            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
            SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统

           #region 获得InnerId的值
            CheckMaxInnerID CIId=new CheckMaxInnerID();
            CIId.getMaxInnerId();
            GETINNER = CIId.MaxInnerId + 1;
            #endregion

           #region 存储数据构建
            string newsid = "News" + Convert.ToString(GETINNER);
            string newstitle = Convert.ToString(collection["ContentTitle"]);
            int newstypeid = Convert.ToInt32(theNew.NewsTypeID);
            string newsauthor = theNew.NewsAuthor;
            string newspublish = Convert.ToString(collection["ContentPublisher"]);
            int flowstate = ConstantDefine.STATE_WAITCHECK;
            int isshow = ConstantDefine.ISSHOW_SHOW;
            int islock = ConstantDefine.ISLOCK_NOTLOCK;
            int newscolumnid = Convert.ToInt32(theNew.NewsColumnID);
            DateTime OriginalTime = Convert.ToDateTime(collection["newPubdate"]);
            int FinalTime = ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;
            string picurl = "";
            string videourl = "";
            string htmlurl = "";
            string linkurl = "";         
            string download = ""; 
            int innerid = GETINNER;

           int contenttypeid = ConstantDefine.CONTENTTYPEID;
            #endregion

           #region 资源上传
           /*图片上传*/
           if (newstypeid == ConstantDefine.PIC_NEWTYPE && NewsPicUpload!=null)
           {
               string PICNewPath = ContentUpload.ConstructPath(SUBSYS, UNITID, ConstantDefine.CONTENTTYPEID) + ContentUpload.ConstructFileName(NewsPicUpload, newsid, UNITID);//用来存入数据库的相对路径
               UploadSuccess = ContentUpload.Upload(NewsPicUpload, HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, ContentType.News)), ContentUpload.ConstructFileName(NewsPicUpload, newsid, UNITID));
               picurl = PICNewPath;
           }
            /*视频上传*/
           else if (newstypeid == ConstantDefine.VIDEO_NEWTYPE && NewsVideoUpload != null)
           {
               string VIDEONewPath = ContentUpload.ConstructPath(SUBSYS, UNITID, ConstantDefine.CONTENTTYPEID) + ContentUpload.ConstructFileName(NewsVideoUpload, newsid, UNITID);//用来存入数据库的相对路径
               UploadSuccess = ContentUpload.Upload(NewsVideoUpload, HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, ContentType.News)), ContentUpload.ConstructFileName(NewsVideoUpload, newsid, UNITID));
               videourl = VIDEONewPath;
           }
           else if (newstypeid == ConstantDefine.MIX_NEWTYPE)
           {
               UploadSuccess = true;
               htmlurl = theNew.Html;
           }
           else if (newstypeid == ConstantDefine.LINK_NEWTYPE)
           {
               UploadSuccess = true;
               linkurl = theNew.LinkUrl;
           }
           else if (newstypeid == ConstantDefine.DOWNLOAD_NEWTYPE)
           {
               UploadSuccess = true;
               download = theNew.Download;
           }
           #endregion

           //内容表和新闻表要添加事务控制
           if (UploadSuccess)
           {
               #region 内容表添加
               string ContentInsert = "insert into T_Content(ContentID,ContentTypeID,ContentTitle,ContentPublisher,PubDate,UnitID) values('" + newsid + "'," + contenttypeid + ",'" + newstitle + "','" + newspublish + "'," + pubdate + ",'" + UNITID + "')";
               bool InsertSuccess0 = DataBaseHandler.Execute(StoreDD, ContentInsert);
               #endregion
               #region 新闻表添加
               string NewsInsert = "insert into T_News (NewsID,NewsTypeID,NewsAuthor,FlowState,IsShow,IsLocked,NewsColumnID,PicUrl,VideoUrl,Html,LinkUrl,Download,InnerID) values('" + newsid + "'," + newstypeid + ",'" + newsauthor + "'," + flowstate + "," + isshow + "," + islock + "," + newscolumnid + ",'" + picurl + "','" + videourl + "','" + htmlurl + "','" + linkurl + "','" + download + "'," + innerid + ")";
               bool InsertSuccess1 = DataBaseHandler.Execute(StoreDD, NewsInsert);
               #endregion
           }


           return RedirectToAction("HomePageTable", "NewsCenter");
        }

        [HttpPost]
        [ValidateInput(false)]
        [MultiButton("sub4")]
        [BaseActionFilter]
        public ActionResult AddToDraft(T_News theNew, FormCollection collection, HttpPostedFileBase NewsPicUpload, HttpPostedFileBase NewsVideoUpload)
        {
            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
            SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统

            #region 获得InnerId的值
            CheckMaxInnerID CIId = new CheckMaxInnerID();
            CIId.getMaxInnerId();
            GETINNER = CIId.MaxInnerId + 1;
            #endregion

            #region 存储数据构建
            string newsid = "News" + Convert.ToString(GETINNER);
            string newstitle = Convert.ToString(collection["ContentTitle"]);
            int newstypeid = Convert.ToInt32(theNew.NewsTypeID);
            string newsauthor = theNew.NewsAuthor;
            string newspublish = Convert.ToString(collection["ContentPublisher"]);
            int flowstate = ConstantDefine.STATE_BACKCHECK;
            int isshow = ConstantDefine.ISSHOW_SHOW;
            int islock = ConstantDefine.ISLOCK_NOTLOCK;
            int newscolumnid = Convert.ToInt32(theNew.NewsColumnID);
            DateTime OriginalTime = Convert.ToDateTime(collection["newPubdate"]);
            int FinalTime = ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;
            string picurl = "";
            string videourl = "";
            string htmlurl = theNew.Html;
            string linkurl = theNew.LinkUrl;
            int innerid = GETINNER;
            string download = theNew.Download;

            int contenttypeid = ConstantDefine.CONTENTTYPEID;
            #endregion

            /*图片上传*/
            if (newstypeid == ConstantDefine.PIC_NEWTYPE && NewsPicUpload != null)
            {
                string PICNewPath = ContentUpload.ConstructPath(SUBSYS, UNITID, ConstantDefine.CONTENTTYPEID) + ContentUpload.ConstructFileName(NewsPicUpload, newsid, UNITID);//用来存入数据库的相对路径
                UploadSuccess = ContentUpload.Upload(NewsPicUpload, HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, ContentType.News)), ContentUpload.ConstructFileName(NewsPicUpload, newsid, UNITID));
                picurl = PICNewPath;
            }

             /*视频上传*/
            else if (newstypeid == ConstantDefine.VIDEO_NEWTYPE && NewsVideoUpload != null)
            {
                string VIDEONewPath = ContentUpload.ConstructPath(SUBSYS, UNITID, ConstantDefine.CONTENTTYPEID) + ContentUpload.ConstructFileName(NewsVideoUpload, newsid, UNITID);//用来存入数据库的相对路径
                UploadSuccess = ContentUpload.Upload(NewsVideoUpload, HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, ContentType.News)), ContentUpload.ConstructFileName(NewsVideoUpload, newsid, UNITID));
                videourl = VIDEONewPath;
            }


            //内容表和新闻表要添加事务控制
            if (UploadSuccess)
            {
                #region 内容表添加
                string ContentInsert = "insert into T_Content(ContentID,ContentTypeID,ContentTitle,ContentPublisher,PubDate,UnitID) values('" + newsid + "'," + contenttypeid + ",'" + newstitle + "','" + newspublish + "'," + pubdate + ",'" + UNITID + "')";
                bool InsertSuccess0 = DataBaseHandler.Execute(StoreDD, ContentInsert);
                #endregion
                #region 新闻表添加
                string NewsInsert = "insert into T_News (NewsID,NewsTypeID,NewsAuthor,FlowState,IsShow,IsLocked,NewsColumnID,PicUrl,VideoUrl,Html,LinkUrl,Download,InnerID) values('" + newsid + "'," + newstypeid + ",'" + newsauthor + "'," + flowstate + "," + isshow + "," + islock + "," + newscolumnid + ",'" + picurl + "','" + videourl + "','" + htmlurl + "','" + linkurl + "','" + download + "'," + innerid + ")";
                bool InsertSuccess1 = DataBaseHandler.Execute(StoreDD, NewsInsert);
                #endregion
            }


            return RedirectToAction("HomePageTable", "NewsCenter");
            
        }

        [BaseActionFilter]
        public ActionResult NewsErrorPage1() {
            //当新增新闻不能为空的信息为空时跳转的错误页
            return View();
        }
        [BaseActionFilter]
        public ActionResult NewsErrorPage2()
        {
            //当新增新闻的ID号重复的时候跳转的错误页
            return View();
        }

    }
}
           