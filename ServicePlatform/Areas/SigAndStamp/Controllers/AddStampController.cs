using ServicePlatform.App_Start;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.Areas.SigAndStamp.Funcs;
using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.SigAndStamp.Controllers
{
    public class AddStampController : Controller
    {
/*=========================================================
        变量声明*/
        private int GETINNER = 0;
        private string UNITID;//单位
        private int CONTENTTYPEID = ContentType.Stamp;//印章资源种类为41
        private bool UploadSuccess = false;

        /*=========================================================*/
        private ContentContext StoreD = new ContentContext();

        //添加一条下载资源

        [BaseActionFilter]
        public ActionResult AddStamp()
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilter]
        public ActionResult AddStamp(T_Stamps DLF, FormCollection collection, HttpPostedFileBase StampUpload)
        {

            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
            string SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统
            //UNITID = "10385";
            //string SUBSYS = "School";

            #region 获得InnerId的值
            CheckMaxInnerID CIId = new CheckMaxInnerID();
            CIId.getMaxInnerId();
            GETINNER = CIId.MaxInnerId + 1;
            #endregion

            #region 存储数据构建
            string stampid = "Stamp" + Convert.ToString(GETINNER);
            string stamptitle = Convert.ToString(collection["StampTitle"]);
            string stamppublish = Convert.ToString(collection["StampPublish"]);
            int stamptypeid = Convert.ToInt32(DLF.StampsTypeID);//财务章等印章种类
            DateTime OriginalTime = Convert.ToDateTime(collection["StampPubdate"]);
            int FinalTime = ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;
            string stampurl = "";
            int innerid = GETINNER;
            int contenttypeid = CONTENTTYPEID;
            #endregion

            if (StampUpload != null)
            {
                string STAMPPath = ContentUpload.ConstructPath(SUBSYS, UNITID, ContentType.Stamp) + StampUpload.FileName;//用来存入数据库的相对路径
                UploadSuccess = ContentUpload.Upload(StampUpload, HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, ContentType.Stamp)), StampUpload.FileName);
                stampurl = STAMPPath;
            }

            if (UploadSuccess)
            { 
                #region 内容表添加
                string ContentInsert = "insert into T_Content(ContentID,ContentTypeID,ContentTitle,ContentPublisher,PubDate,UnitID) values('" + stampid + "'," + contenttypeid + ",'" + stamptitle + "','" + stamppublish + "'," + pubdate + ",'" + UNITID + "')";
                bool InsertSuccess0 = DataBaseHandler.Execute(StoreD, ContentInsert);
                #endregion
                #region 印章表添加
                string strSQL = "insert into T_Stamps (StampsID,StampsTypeID,StampsUrl,InnerID) values('" + stampid + "'," + stamptypeid + ",'" + stampurl + "'," + innerid + ")";
                bool InsertSuccess1 = DataBaseHandler.Execute(StoreD, strSQL);
                #endregion
            }


            return RedirectToAction("StampTable", "SSCenter");
        }
    }
}
