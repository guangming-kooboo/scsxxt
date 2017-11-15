using ServicePlatform.App_Start;
using ServicePlatform.Areas.DownLoadFile.Func;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.Areas.News.ToolHelper;
using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.DownLoadFile.Controllers
{
    public class DLCenterController : Controller
    {
        /*=========================================================
        变量声明*/
        //Session信息获取
        private string UNITID;
        private int USERTYPE;
        private string UNITNAME;
        private int CONTENTTYPEID = ContentType.DownLoadFile;
        private bool UploadSuccess = false;//指示文件是否上传成功
        static int GETINNER = 0;
        static string GetFileUrl = "";
        static string TargetEditId = "";
        /*=========================================================*/

        ContentContext storeData = new ContentContext();
        ContentContext storeDB = new ContentContext();
        private EnterpriseContext storeDA = new EnterpriseContext();

        [BaseActionFilter]
        public ActionResult DLTable(string id)
        {
            ViewData["id"] = id;
            //资源下载管理表格
            ViewData["DLTip"] = "公开栏目对所有人可见，其他栏目对本单位所有用户可见";
            return View();
        }
        [BaseActionFilter]
        public ActionResult DLData()
        {

            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            //USERTYPE = (Session["Vars"] as ServicePlatform.Config.Vars).UserType;
            string subSystem = (Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem();//获取用户登陆的子系统

            switch (subSystem)
            {
                case "平台端":
                    UNITNAME = "Platform";
                    break;
                case "学校端":
                    SchoolContext sc = new SchoolContext();
                    var SC = (from t in sc.School where t.SchoolID == UNITID select t).FirstOrDefault();
                    UNITNAME = SC.SchoolName;
                    break;
                case "企业端":
                    EnterpriseContext ec = new EnterpriseContext();
                    var EC = (from t in ec.T_Enterprise where t.Ent_No == UNITID select t).FirstOrDefault();
                    UNITNAME = EC.Ent_Name ;
                    break;
                default:
                    UNITNAME = "Unknown";
                    break;
            }

            //接受DataGrid传来的参数
            int pageIndex = int.Parse(Request["page"]);
            int pageSize =50;

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            int TotalNum = 0;
            var service = new GetDLData();
            var theDL = service.LoadDLData(Temp, UNITID, out TotalNum);

            #region 时间显示处理
            //时间在表格中显示处理,我的目的是创建一个时间转换对照表，所以这个表不允许有重复的键值
            List<C_NewsTime> ts = new List<C_NewsTime>();
            var ContentCollection = storeDB.T_Content.ToList();
            var ContentCollectionNum = storeDB.T_Content.Count();
            int[] aa = new int[ContentCollectionNum];
            int ii = 0;
            foreach (var m in ContentCollection)
            {
                bool Weather = false;
                int aim = Convert.ToInt32(m.PubDate);
                for (int i = 0; i < aa.Length; i++)
                {
                    if (aim == aa[i])
                    {
                        Weather = true;
                    }
                }
                if (Weather == false)
                {
                    aa[ii] = Convert.ToInt32(m.PubDate);
                    ii++;
                }
            }
            for (int ie = 0; ie < ContentCollectionNum; ie++)
            {
                //List<C_NewsTime> ts中不能添加重复的时间
                C_NewsTime cc = new C_NewsTime();
                cc.OriTime = aa[ie];
                cc.SetManTime(cc.OriTime);
                ts.Add(cc);
            }
            #endregion

            List<T_Content> GetCon = storeDB.T_Content.ToList();
            List<C_ContentColumn> GetCol = storeData.C_ContentColumn.ToList();

            var Result = from a in theDL from b in GetCon from c in GetCol from d in ts where (a.DLFileID == b.ContentID && a.DLFileColumnID == c.ContColumnID && d.OriTime == b.PubDate) select new { a.DLFileID, b.ContentTitle, b.ContentPublisher, d.ManTime, c.ContColumnName, UNITNAME };
            var JsonResult = new { total = TotalNum, rows = Result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult DeleteDL()
        {

            //删除文件数据
            string TargetNums = Request.Form[0];//获取需要删除的文件编号组成的字符串，且以逗号分割
            string[] SplitArray = TargetNums.Split(',');//取到要删除的文件编号放进数组里
            int ArrayCount = SplitArray.Count();
            string StringCount = Convert.ToString(ArrayCount);
            ContentContext ne = new ContentContext();

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string TargetID = SplitArray[a];
                
                var DelFileEntity = (from t in ne.T_DownLoadFiles where t.DLFileID == TargetID select t).FirstOrDefault();
                string filePath = HttpContext.Server.MapPath(DelFileEntity.DLFileUrl);
                //if (System.IO.File.Exists(filePath))//判断文件是否存在
                //{
                //    System.IO.File.Delete(filePath);//执行IO文件删除,需引入命名空间System.IO;    
                //} //删除物理文件
                //string ContentInsert = "delete from T_Content where ContentID='" + TargetID + "'";
                //ne.Database.ExecuteSqlCommand(ContentInsert);//删除数据库中数据
                DeleteDL abc = new DeleteDL();
                abc.Delete(TargetID, ne, filePath);
            }

            return Content(StringCount);
        }
        [BaseActionFilter]
        public ActionResult EditDLF(string id) 
        {
            id = id.Replace("'", "");
            //编辑上传文件信息
            T_DownLoadFiles dl = storeData.T_DownLoadFiles.Find(id);
            //int time = storeData.T_Content.Find(id).PubDate;
            GetFileUrl = dl.DLFileUrl;
            string title = storeData.T_Content.Find(id).ContentTitle;
            string publish = storeData.T_Content.Find(id).ContentPublisher;
            ViewData["Title"] = title;
            ViewData["Publish"] = publish;
            TargetEditId = id;
            GETINNER = dl.InnerID;
            return View(dl);
        }
        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilter]
        public ActionResult EditDLF(T_DownLoadFiles DLF, FormCollection collection, HttpPostedFileBase DLFUpload)
        {
            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
            string SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统

            string DLFPath = GetFileUrl;
            //构建文件上传存储路径
            if (DLFUpload != null && DLFUpload.ContentLength > 0)
            {
                //获取文件上传路径              
                string FilePath = ContentUpload.ConstructPath(SUBSYS, UNITID, CONTENTTYPEID) + DLFUpload.FileName;//用来存入数据库的相对路径
                string PhysicsPath = HttpContext.Server.MapPath(FilePath);//物理路径
                string Physic = HttpContext.Server.MapPath(ContentUpload.ConstructPath(SUBSYS, UNITID, CONTENTTYPEID));
                //Physic = Physic.Remove(Physic.Length - 2);
                System.IO.File.Delete(HttpContext.Server.MapPath(GetFileUrl));//删除原有文件
                if (System.IO.Directory.Exists(Physic))
                {
                    DLFUpload.SaveAs(PhysicsPath);                   
                }

                DLFPath = FilePath;//用于数据库中文件路径的存储              
            }

            #region 获得InnerId的值
            //ServicePlatform.Areas.DownLoadFile.Func.CheckMaxInnerID CIId = new ServicePlatform.Areas.DownLoadFile.Func.CheckMaxInnerID();
            //CIId.getMaxInnerId();
            //GETINNER = CIId.MaxInnerId + 1;
            #endregion

            #region 存储数据构建
            string dlfid = TargetEditId;
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

            #region 内容表编辑      
            string ContentInsert = "update T_Content set ContentTypeID=" + contenttypeid + ",ContentTitle='" + dlftitle + "',ContentPublisher='" + dlfpublish + "',PubDate=" + pubdate + ",UnitID='" + UNITID + "'where ContentID='" + dlfid + "';";
            //string ContentInsert = "update T_Content set ContentTypeID=31,ContentTitle='文件1',ContentPublisher='屋里百超',PubDate=1464842859,UnitID='A01'where ContentID='" + dlfid + "';";
            bool InsertSuccess0 = DataBaseHandler.Execute(storeData, ContentInsert);
            #endregion
            #region 资源表编辑                
            string strSQL = "update T_DownLoadFiles set DLFileColumnID=" + dlfcolumnid + ",DLFileUrl='" + dlfurl + "',InnerID=" + innerid + "where DLFileID='" + dlfid + "';";
            bool InsertSuccess1 = DataBaseHandler.Execute(storeData, strSQL);
            #endregion

            return RedirectToAction("DLTable", "DLCenter");
        }

    }
}
