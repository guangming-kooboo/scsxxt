using ServicePlatform.App_Start;
using ServicePlatform.Areas.News.ToolHelper;
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
    public class SSCenterController : Controller
    {
        /*=========================================================
        变量声明*/
        //Session信息获取
        private string UNITID;
        private int USERTYPE;
        private string UNITNAME;
        private string USERID;
        /*=========================================================*/

        ContentContext storeData = new ContentContext();
        ContentContext storeDB = new ContentContext();
        UserContext sa = new UserContext();
        private EnterpriseContext storeDA = new EnterpriseContext();
        private UserContext storeDC = new UserContext();

        [BaseActionFilter]
        public ActionResult StampTable()
        {
            //印章管理表格
            return View();
        }

        [BaseActionFilter]
        public ActionResult StampDataGet()
        {
            var user = (Session["Vars"] as ServicePlatform.Config.Vars);
            UNITID = user.getUserUnit();
            //USERTYPE = (Session["Vars"] as ServicePlatform.Config.Vars).UserType;
            //UNITID = "10385";
            USERTYPE = ContentType.Stamp;
            UNITNAME = sa.T_User.Find(user.UserID).NickName;
            //接受DataGrid传来的参数
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            int TotalNum = 0;
            var service = new GetStampData();
            var theStamp = service.LoadDLData(Temp, UNITID, out TotalNum);

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
            List<C_StampType> GetType = storeData.C_StampType.ToList();

            var Result = from a in theStamp from b in GetCon from c in GetType from d in ts where (a.StampsID == b.ContentID && a.StampsTypeID == c.TypeID && d.OriTime == b.PubDate) select new { a.StampsID, b.ContentTitle, b.ContentPublisher, d.ManTime, c.TypeName, UNITNAME };
            var JsonResult = new { total = TotalNum, rows = Result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串

            return Content(str);
        }
        [BaseActionFilter]
        public ActionResult DeleteStamp()
        {

            //删除印章数据
            string TargetNums = Request.Form[0];//获取需要删除的新闻编号组成的字符串，且以逗号分割
            string[] SplitArray = TargetNums.Split(',');//取到要删除的新闻编号放进数组里
            int ArrayCount = SplitArray.Count();
            string StringCount = Convert.ToString(ArrayCount);

            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string TargetID = SplitArray[a];
                string ContentInsert = "delete from T_Content where ContentID='" + TargetID + "'";
                conn.Open();
                try
                {
                    SqlCommand cmd1 = new SqlCommand(ContentInsert, conn);
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                }
                conn.Close();
            }

            return Content(StringCount);
        }
        [BaseActionFilter]
        public ActionResult StampDetail(string id)
        {
            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            UNITNAME = sa.T_User.Find(UNITID).NickName;
            id = id.Replace("'", "");
            //显示印章详情页
            var tns1 = (from z in storeData.T_Stamps where z.StampsID == id select z).ToList();

            var ContentItem = storeDB.T_Content.Where(e => e.ContentID == id).ToList();
            foreach (var m in tns1)
            {
                int theType = m.StampsTypeID;
                string TypeName = storeData.C_StampType.Find(theType).TypeName;
                ViewData["TypeName"] = TypeName;

                ViewData["UnitName"] = UNITNAME;
            }
            foreach (var mm in ContentItem)
            {
                string Ctime = Convert.ToString(ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertIntDateTime(Convert.ToInt32(mm.PubDate)));
                ViewData["thetime"] = Ctime;

                string CTitle = mm.ContentTitle;
                ViewData["thetitle"] = CTitle;

                string CPublish = mm.ContentPublisher;
                ViewData["thepublish"] = CPublish;
            }

            return View(tns1);
        }

        [BaseActionFilter]
        public ActionResult SigTable() {
            //签名管理表格
            return View();
        }

        [BaseActionFilter]
        public ActionResult SigDataGet()
        {
            USERID = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //接受DataGrid传来的参数
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            int TotalNum = 0;
            var service = new GetSigData();
            var theSig = service.LoadSigData(Temp, USERID, out TotalNum);

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

            var Result = from a in theSig from b in GetCon from d in ts where (a.SignatureID == b.ContentID && d.OriTime == b.PubDate) select new { a.SignatureID, b.ContentTitle, b.ContentPublisher, d.ManTime, USERID };
            var JsonResult = new { total = TotalNum, rows = Result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult DeleteSig()
        {
            string TargetNums = Request.Form[0];
            string[] SplitArray = TargetNums.Split(',');
            int ArrayCount = SplitArray.Count();
            string StringCount = Convert.ToString(ArrayCount);

            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string TargetID = SplitArray[a];
                string ContentInsert = "delete from T_Content where ContentID='" + TargetID + "'";
                conn.Open();
                try
                {
                    SqlCommand cmd1 = new SqlCommand(ContentInsert, conn);
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                }
                conn.Close();
            }

            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult SigDetail(string id)
        {
            USERID = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //USERID = "10385";

            id = id.Replace("'", "");
            //显示印章详情页
            var tns1 = (from z in storeData.T_Signature where z.SignatureID == id select z).ToList();
            var ContentItem = storeDB.T_Content.Where(e => e.ContentID == id).ToList();

            string UserNickName = storeDC.T_User.Find(USERID).NickName;
            ViewData["UserName"] = UserNickName;

            foreach (var mm in ContentItem)
            {
                string Ctime = Convert.ToString(ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertIntDateTime(Convert.ToInt32(mm.PubDate)));
                ViewData["thetime"] = Ctime;

                string CTitle = mm.ContentTitle;
                ViewData["thetitle"] = CTitle;

                string CPublish = mm.ContentPublisher;
                ViewData["thepublish"] = CPublish;
            }

            return View(tns1);
        }
    }
}
