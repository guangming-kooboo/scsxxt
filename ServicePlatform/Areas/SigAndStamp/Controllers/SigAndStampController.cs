using ServicePlatform.Areas.News.ToolHelper;
using ServicePlatform.Areas.SigAndStamp.Funcs;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas
{
    public class SigAndStampController : Controller
    {

        /*=========================================================
        变量声明*/
        string UNITID = "10385";
        /*=========================================================*/

        ContentContext storeData = new ContentContext();
        ContentContext storeDB = new ContentContext();
        public ActionResult StampTable()
        {
            //印章管理表格
            return View();
        }

        public ActionResult StampData()
        {

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
            for (int ie = 0; ie < TotalNum; ie++)
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

            var Result = from a in theStamp from b in GetCon from c in GetType from d in ts where (a.StampsID == b.ContentID && a.StampsTypeID == c.TypeID && d.OriTime == b.PubDate) select new { a.StampsID, b.ContentTitle, b.ContentPublisher, d.ManTime, c.TypeName, b.UnitID };
            var JsonResult = new { total = TotalNum, rows = Result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串

            return Content(str);
        }

        public ActionResult DeleteStamp()
        {

            //删除新闻数据
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

    }
}


