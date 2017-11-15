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
    public class AddSigController : Controller
    {
/*=========================================================
        变量声明*/
        private int GETINNER = 0;
        private string USERID;//用户名
        private string UnitID;//用户名
        private int CONTENTTYPEID = ContentType.Signature;//签名资源种类为51
        private UserContext store = new UserContext();
        private bool UploadSuccess = false;
        private ContentContext StoreD = new ContentContext();
        /*=========================================================*/


        //添加一条下载资源
        [BaseActionFilter]
        public ActionResult AddSig()
        {
            //添加签名
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilter]
        public ActionResult AddSig(T_Signature Sig, FormCollection collection, HttpPostedFileBase SigUpload)
        {

            USERID = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;//获取当前用户名
            //USERID = "10385";
            UnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();

            string SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem()); 

            #region 获得InnerId的值
            CheckSigInnerId CIId = new CheckSigInnerId();
            CIId.getMaxSigInnerId();
            GETINNER = CIId.MaxInnerId + 1;
            #endregion

            #region 存储数据构建
            string sigid = "Sign"+"-" +USERID+"-"+ Convert.ToString(GETINNER);
            string sigtitle = Convert.ToString(collection["SigTitle"]);
            //string sigpublish = store.T_User.Find(USERID).NickName;//当前用户的昵称作为发布者  
            string sigpublish = "厦大测试";
            DateTime OriginalTime = Convert.ToDateTime(collection["SigPubdate"]);
            int FinalTime = ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;
            string sigurl = "";
            int innerid = GETINNER;
            int contenttypeid = CONTENTTYPEID;
            string unitid = UnitID;
            string userid = USERID;
            int isdefault = -1;
            #endregion

            if (SigUpload != null)
            {
                string SIGPath = ContentUpload.ConstructPath(SUBSYS, UnitID, CONTENTTYPEID) + ContentUpload.ConstructFileName(SigUpload, sigid, UnitID);//用来存入数据库的相对路径
                UploadSuccess = ContentUpload.Upload(SigUpload, HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UnitID, ContentType.Signature)), ContentUpload.ConstructFileName(SigUpload, sigid, UnitID));
                sigurl = SIGPath;
            }
            if (UploadSuccess)
            {
                #region 内容表添加
                string ContentInsert = "insert into T_Content(ContentID,ContentTypeID,ContentTitle,ContentPublisher,PubDate,UnitID) values('" + sigid + "'," + contenttypeid + ",'" + sigtitle + "','" + sigpublish + "'," + pubdate + ",'" + unitid + "')";
                bool InsertSuccess0 = DataBaseHandler.Execute(StoreD, ContentInsert);
                #endregion
                #region 签名表添加
                string strSQL = "insert into T_Signature (SignatureID,SignatureUrl,InnerID,UserID,IsDefault) values('" + sigid + "','" + sigurl + "'," + innerid + ",'" + userid + "'," + isdefault + ")";
                bool InsertSuccess1 = DataBaseHandler.Execute(StoreD, strSQL);
                #endregion
            }
            return RedirectToAction("SigTable", "SSCenter");
        }    
    }
}
