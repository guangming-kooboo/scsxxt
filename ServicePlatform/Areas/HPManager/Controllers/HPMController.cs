using ServicePlatform.Areas.HPManager.FuncHelper;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.Areas.CodeManager.ToolHelpers;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.App_Start;

namespace ServicePlatform.Areas.HPManager.Controllers
{
    public class HPMController : Controller
    {

        /*=========================================================
        变量声明*/

        private string UnitID;

        /*==========================================================*/

        private ContentContext DataA = new ContentContext();

        [BaseActionFilter]
        public ActionResult HPMPage()
        {
            string UnitID;
            UnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//菜单调用的直接入口，故而直接读取Session变量

            //根据单位获取首页栏目
            var HPcolumn = from hp in DataA.T_HomePageColumn
                           where hp.UnitID == UnitID
                           select hp;
            IEnumerable<T_HomePageColumn> getHPContentColumn = HPcolumn;            
            ViewData["CContent"] = getHPContentColumn;

            return View();
        }

        /*====================================================================================================================================*/
        //首页栏目 上 半部分表格
        /*====================================================================================================================================*/
        [BaseActionFilter]
        public ActionResult GetHPMData() {
            
            //接收首页栏目下拉菜单的输入值
            //ComboBox传递的参数值
            
            //这个部分应该改为函数的接口参数
            UnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();

            // * 
            //
            int Colid = 0;
            if (Request["BaichaoTestData"] != null)
            {
                Colid = Convert.ToInt32(Request["BaichaoTestData"]);
            }          

            //接受DataGrid传来的参数
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new HPMGetPageInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            int TotalNum = 0;
            var service = new GetColData();
            var theCol = service.LoadColData(Temp, out TotalNum, UnitID);

            #region 时间显示处理
            //时间在表格中显示处理,我的目的是创建一个时间转换对照表，所以这个表不允许有重复的键值
            List<C_NewsTime> ts = new List<C_NewsTime>();
            var ContentCollection = DataA.T_Content.ToList();
            var ContentCollectionNum = DataA.T_Content.Count();
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

            #region 列表显示所需数据采集
            List<T_HomePageContent> GetHPContent = DataA.T_HomePageContent.ToList();
            List<T_Content> GetContent = DataA.T_Content.ToList();
            List<T_HomePageColumn> GetHPC = DataA.T_HomePageColumn.ToList();
            List<C_ContentType> GetType = DataA.C_ContentType.ToList();
            #endregion

            var HPCResult = from a in theCol from b in GetContent from c in ts where (a.HPCID == b.ContentID && b.PubDate == c.OriTime) select new { b.ContentID, b.ContentTypeID, b.ContentTitle, b.ContentPublisher, c.ManTime ,a.HPCSeq};
            //依据首页栏目下拉菜单选择值做进一步筛选
            if (Colid != 0)
            {
                HPCResult = from a in GetHPContent from b in GetContent from c in GetHPC from d in ts where (a.HPCID == b.ContentID && a.HPColID == c.HPColumnID && c.HPColumnID == Colid && b.PubDate == d.OriTime) select new { b.ContentID, b.ContentTypeID, b.ContentTitle, b.ContentPublisher, d.ManTime, a.HPCSeq };
                TotalNum = HPCResult.Count();
            }
            else
            {
                //HPCResult = from a in theCol from b in GetContent where (a.HPCID == b.ContentID) select new { b.ContentID, b.ContentTypeID, b.ContentTitle, b.ContentPublisher, b.PubDate };
                HPCResult = from a in theCol from b in GetContent from c in ts where (a.HPCID == b.ContentID && b.PubDate == c.OriTime ) select new { b.ContentID, b.ContentTypeID, b.ContentTitle, b.ContentPublisher, c.ManTime, a.HPCSeq };
            }

            var JsonResult = new { total = TotalNum, rows = HPCResult };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult RemoveHPContent() {
            //删除HomePageColumn数据
            string TargetNums = Request.Form[0];
            string[] SplitArray = TargetNums.Split(',');
            int ArrayCount = SplitArray.Count();
            string StringCount = Convert.ToString(ArrayCount);
            //*****
            ContentContext hpm = new ContentContext();
            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string TargetID = SplitArray[a];
                string ContentInsert = "delete from T_HomePageContent where HPCID='" + TargetID + "'";
                try                {

                    hpm.Database.ExecuteSqlCommand(ContentInsert);
                }
                catch (SqlException ex)
                {
                    //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                }                
            }
            //*****
            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult EditSeq() {
            string GetHPID = Request.Form[0];
            int GetHPSEQ = Convert.ToInt32(Request.Form[5]);

            //数据库更新
            ContentContext hpm = new ContentContext();
            string strSQL = "update T_HomePageContent set HPCSeq=" + GetHPSEQ + " where HPCID='" + GetHPID + "';";
            try
            {
                hpm.Database.ExecuteSqlCommand(strSQL);
            }
            catch (SqlException ex)
            {
                //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
            }

            return Content("1");
        }

        /*====================================================================================================================================*/
        //首页栏目下半部分表格
        /*====================================================================================================================================*/

        [BaseActionFilter]
        public ActionResult GetHPMBottom() {

            UnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();

            string aima = "";

            //联动下拉菜单返回值
            string GetColIDStr = Request["ColID"];
            string GetTypeIDStr = Request["TypeID"];
            //int GetColID = Convert.ToInt32(Request["ColID"]);
            //int GetTypeID = Convert.ToInt32(Request["TypeID"]);
            
            //接受DataGrid传来的参数
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            var Temp = new HPMGetPageInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int TotalNum = 0; 
            #region 时间显示处理
                //时间在表格中显示处理,我的目的是创建一个时间转换对照表，所以这个表不允许有重复的键值
                List<C_NewsTime> ts = new List<C_NewsTime>();
                var ContentCollection = DataA.T_Content.ToList();
                var ContentCollectionNum = DataA.T_Content.Count();
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

            #region 列表显示所需数据采集
            List<T_HomePageColumn> GetHPC = DataA.T_HomePageColumn.ToList();
            List<T_News> GetNews = DataA.T_News.ToList();
            List<C_ContentType> GetType = DataA.C_ContentType.ToList();
            var service = new GetContentData();
            #endregion


            var BaseContent = service.LoadBaseContentData(Temp, out TotalNum, UnitID);
            var HPCResult = from a in BaseContent from b in ts from c in GetType where (a.UnitID == UnitID && a.PubDate == b.OriTime && c.ConTypeID == a.ContentTypeID) select new { a.ContentID, c.ConTypeName, a.ContentTitle,a.ContentPublisher, b.ManTime, UnitID };
            if (GetTypeIDStr == "")
            {
                //所有内容
                HPCResult = HPCResult;
            }
            else {
                int GetColID = Convert.ToInt32(Request["ColID"]);
                int GetTypeID = Convert.ToInt32(Request["TypeID"]);
                var theContent = service.LoadContentData(Temp, out TotalNum, UnitID, GetTypeID, GetColID);
                HPCResult = from a in theContent from b in ts from c in GetType where (a.PubDate == b.OriTime && c.ConTypeID == GetTypeID) select new { a.ContentID, c.ConTypeName, a.ContentTitle, a.ContentPublisher, b.ManTime, UnitID };
                //HPCResult = from a in HPCResult from c in GetType where (a.ConTypeName == c.ConTypeName && c.ConTypeID == GetTypeID) select new { a.ContentID, c.ConTypeName, a.ContentTitle,a.ContentPublisher, a.ManTime, UnitID };
            }
  
            var JsonResult = new { total = TotalNum, rows = HPCResult };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult AddToColumn() {
            /*===================将选择的内容添加到指定的栏目中======================================*/

            string TransInfo = Request.Form[1];//获取需要添加到栏目的内容编号组成的字符串，且以逗号分割           
            string[] SplitArray = TransInfo.Split(',');//将分割后的内容编号放进数组里
            int ArrayCount = SplitArray.Count();
            string StringCount = Convert.ToString(ArrayCount);//用于Content（“”）方法中返回条数

            //获得该栏目的HomePageContent的条数是否多于指定行数
            int tarCol = Convert.ToInt32(Request["HPItem"]);

            var targetHPContent = DataA.T_HomePageContent.Where(p => p.HPColID == tarCol);
            int targetHPContentNum = targetHPContent.Count();

            var RowCountCon = from a in DataA.T_HomePageColumn where (a.HPColumnID == tarCol) select a;
            int RowCount = -1;
            foreach(var n in RowCountCon){
                RowCount = n.ColRowCount;
            }

            if (ArrayCount > 0 && targetHPContentNum < RowCount)
            {
                for (int i = 0; i < ArrayCount; i++)
                {
                    //这里需要做一个判断，以避免重复插入
                    //依次添加入栏目
                    string ContentInsert = "insert into T_HomePageContent(HPCID,HPColID,HPCSeq) values('" + SplitArray[i] + "'," + tarCol + "," + 1 + ")";
                    try
                    {
                        DataA.Database.ExecuteSqlCommand(ContentInsert);
                    }
                    catch (SqlException ex)
                    {
                        //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                    }
                }
            }
            else {
                StringCount = "0";//0条记录被修改
            }

            return Content(StringCount);
        }

        public JsonResult GetColFromType()
        {
            string strContentType = Request["S_type"].ToString();
            string SubSystem = (Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem();
            var theContentColumn = (from p in DataA.C_ContentColumn where p.SybSystem == SubSystem select p).ToList();

            if (strContentType == "")
            {
                theContentColumn = (from p in DataA.C_ContentColumn where p.SybSystem == SubSystem select p).ToList();
            }
            else
            {
                int ColTypeID = int.Parse(strContentType);

                theContentColumn = (from p in DataA.C_ContentColumn where p.ContTypeID == ColTypeID && p.SybSystem == SubSystem select p).ToList();
            }
           
            return Json(new { count = theContentColumn.Count, Pos = theContentColumn });
        }

    }
}
