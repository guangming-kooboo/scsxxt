using ServicePlatform.Areas.DownLoadFile.Func;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;
using ServicePlatform.App_Start;

namespace ServicePlatform.Areas.DownLoadFile.Controllers
{
    public class AddDLFController : BaseController
    {

        /*=========================================================
        变量声明*/
        private int GETINNER = 0;
        private string UNITID;//单位
        private int CONTENTTYPEID = ContentType.DownLoadFile;//本模块功能是上传文件，故内容类型直接设置为31
        private string SUBSYS;
        private bool UploadSuccess = false;//指示文件是否上传成功

        /*=========================================================*/

        #region 废弃的资源文件添加方法
        //添加一条下载资源
        //public ActionResult AddDLF()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult AddDLF(T_DownLoadFiles DLF, FormCollection collection, HttpPostedFileBase DLFUpload)
        //{
        //    ContentContext ne = new ContentContext();
        //    UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
        //    SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统

        //    string DLFPath = "";
        //    //构建文件上传存储路径
        //    if (DLFUpload != null && DLFUpload.ContentLength > 0)
        //    {
        //        //获取文件上传路径              
        //        string FilePath = ContentUpload.ConstructPath(SUBSYS, UNITID, CONTENTTYPEID) + DLFUpload.FileName;//用来存入数据库的相对路径
        //        string PhysicsPath = HttpContext.Server.MapPath(FilePath);//物理路径
        //        string Physic = HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, CONTENTTYPEID));
        //        //Physic = Physic.Remove(Physic.Length - 2);
        //        if (System.IO.Directory.Exists(Physic))
        //        {
        //            DLFUpload.SaveAs(PhysicsPath);
        //            UploadSuccess = true;
        //        }

        //        DLFPath = FilePath;//用于数据库中文件路径的存储              
        //    }

        //    #region 获得InnerId的值
        //    CheckMaxInnerID CIId = new CheckMaxInnerID();
        //    CIId.getMaxInnerId();
        //    GETINNER = CIId.MaxInnerId + 1;
        //    #endregion

        //    #region 存储数据构建
        //    string dlfid = "DLF" + Convert.ToString(GETINNER);
        //    string dlftitle = Convert.ToString(collection["DLFTitle"]);
        //    string dlfpublish = Convert.ToString(collection["DLFPulish"]);
        //    int dlfcolumnid = Convert.ToInt32(DLF.DLFileColumnID);
        //    DateTime OriginalTime = Convert.ToDateTime(collection["DLFPubdate"]);
        //    int FinalTime = ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertDateTimeInt(OriginalTime);
        //    int pubdate = FinalTime;
        //    string dlfurl = DLFPath;
        //    int innerid = GETINNER;
        //    int contenttypeid = CONTENTTYPEID;
        //    #endregion

        //    if (UploadSuccess)
        //    {
        //        #region 内容表添加
        //        string ContentInsert = "insert into T_Content(ContentID,ContentTypeID,ContentTitle,ContentPublisher,PubDate,UnitID) values('" + dlfid + "'," + contenttypeid + ",'" + dlftitle + "','" + dlfpublish + "'," + pubdate + ",'" + UNITID + "')";
        //        if (ne.Database.ExecuteSqlCommand(ContentInsert) <= 0)
        //        {
        //            return Redirect("/Home/ErrorPage?ErrorCode="+"InserFailer");
        //        };
        //        #endregion
        //        #region 资源表添加
        //        string strSQL = "insert into T_DownLoadFiles (DLFileID,DLFileColumnID,DLFileUrl,InnerID) values('" + dlfid + "'," + dlfcolumnid + ",'" + dlfurl + "'," + innerid + ")";

        //        if (ne.Database.ExecuteSqlCommand(strSQL) <= 0)
        //        {
        //            return Redirect("/Home/ErrorPage?ErrorCode=" + "InserFailer");
        //        };

        //    #endregion
        //    }

        //    return RedirectToAction("DLTable", "DLCenter");
        //}
        #endregion
        [BaseActionFilter]
        public ActionResult ADD()
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilter]
        public ActionResult ADD(T_DownLoadFiles DLF, FormCollection collection, HttpPostedFileBase DLFUpload,string returnUrl)
        {
            ContentContext ne = new ContentContext();
            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
            SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统

            string DLFPath = "";
            string Physic = "";
            //构建文件上传存储路径
            if (DLFUpload != null && DLFUpload.ContentLength > 0)
            {
                //获取文件上传路径              
                string FilePath = ContentUpload.ConstructPath(SUBSYS, UNITID, CONTENTTYPEID) + DLFUpload.FileName;//用来存入数据库的相对路径
                string PhysicsPath = HttpContext.Server.MapPath(FilePath);//物理路径
                Physic = HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, CONTENTTYPEID));
                //Physic = Physic.Remove(Physic.Length - 2);
                if (System.IO.Directory.Exists(Physic))
                {
                    DLFUpload.SaveAs(PhysicsPath);
                    UploadSuccess = true;
                }

                DLFPath = FilePath;//用于数据库中文件路径的存储              
            }

            #region 获得InnerId的值
            CheckMaxInnerID CIId = new CheckMaxInnerID();
            CIId.getMaxInnerId();
            GETINNER = CIId.MaxInnerId + 1;
            #endregion

            #region 存储数据构建
            string dlfid = "DLF" + Convert.ToString(GETINNER)+DateTime.Now.ToBinary();
            string dlftitle = Convert.ToString(collection["DLFTitle"]);
            string dlfpublish = Convert.ToString(collection["DLFPulish"]);
            int dlfcolumnid = Convert.ToInt32(DLF.DLFileColumnID);
            DateTime OriginalTime = Convert.ToDateTime(collection["DLdate"]);
            int FinalTime = ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;
            string dlfurl = DLFPath;
            int innerid = GETINNER;
            int contenttypeid = CONTENTTYPEID;
            #endregion

            if (UploadSuccess)
            {
                #region 内容表添加
                string ContentInsert = "insert into T_Content(ContentID,ContentTypeID,ContentTitle,ContentPublisher,PubDate,UnitID) values('" + dlfid + "'," + contenttypeid + ",'" + dlftitle + "','" + dlfpublish + "'," + pubdate + ",'" + UNITID + "')";
                if (ne.Database.ExecuteSqlCommand(ContentInsert) <= 0)
                {
                    return Redirect("/Home/ErrorPage?ErrorCode=" + "InserFailer");
                };
                #endregion
                #region 资源表添加
                string strSQL = "insert into T_DownLoadFiles (DLFileID,DLFileColumnID,DLFileUrl,InnerID) values('" + dlfid + "'," + dlfcolumnid + ",'" + dlfurl + "'," + innerid + ")";

                if (ne.Database.ExecuteSqlCommand(strSQL) <= 0)
                {
                    return Redirect("/Home/ErrorPage?ErrorCode=" + "InserFailer");
                };

                #endregion

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("DLTable", "DLCenter");
                }
                else
                {
                    return Alert("上传成功",returnUrl);
                }
                
            }
            else
            {
                return Alert("上传失败：企业文件目录不存在=>"+ Physic);
            }
            
        }
    }
}
