using ServicePlatform.App_Start;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.Areas.News.ToolHelper;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.News.Controllers
{
    public class NewsCenterController : Controller
    {

        /*=========================================================
        变量声明*/

        private int STATE_WAITCHECK = 1;//新闻FlowState属性 1=>待审核
        private int STATE_PASSCHECK = 2;//新闻FlowState属性 2=>通过
        private int STATE_BACKCHECK = 3;//新闻FlowState属性 3=>驳回

        private int ISSHOW_SHOW = 1;//新闻IsShow属性 1=>显示
        private int ISSHOW_HIDE = -1;//新闻IsShow属性 -1=>隐藏

        private int ISLOCK_LOCK = 1;//新闻IsLock属性 1=>锁定
        private int ISLOCK_NOTLOCK = -1;//新闻IsLock属性 -1=>解锁

        //Session信息获取
        private string UNITID;
        private string SUBSYS;
        private int USERTYPE;
        private string UNITNAME;    

        /*==========================================================*/

        //新闻列表页的显示
        private ContentContext storeDB = new ContentContext();
        private ContentContext storeDED = new ContentContext();
        private UserContext da = new UserContext();
        private UserContext storeDC = new UserContext();
        private EnterpriseContext storeDA = new EnterpriseContext();

        [BaseActionFilter]
        public ActionResult HomePageTable()
        {
            IEnumerable<T_News> GetNews = storeDB.T_News;
            return View();
        }

        [BaseActionFilter]
        public ActionResult GetTheData()
        {
            var user = (Session["Vars"] as ServicePlatform.Config.Vars);
            UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();//获取当前单位号
            SUBSYS = SessionHandler.SubSysHandle((Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem());//获取当前子系统
            string NickName = da.T_User.Find(user.UserID).NickName;
            //新闻列表分页和查询功能，接受DataGrid传来的参数
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            //获取后台查询值
            string TheAttribute = Request["NewsAttribute"];
            string TheValue = Request["NewsValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            int TotalNum = 0;
            var service = new GetNews();
            var theNews = service.LoadNewsData(Temp, UNITID, out TotalNum);

            #region Category Search
            #region 禁用与锁定内容构建
            NewsIsShow sh1 = new NewsIsShow();
            sh1.IsShowID = ConstantDefine.ISSHOW_SHOW;
            sh1.IsShowName = "不禁用";
            NewsIsShow sh2 = new NewsIsShow();
            sh2.IsShowID = ConstantDefine.ISSHOW_HIDE;
            sh2.IsShowName = "禁用";
            List<NewsIsShow> showList = new List<NewsIsShow>();
            showList.Add(sh1);
            showList.Add(sh2);

            NewsIsLocked so1 = new NewsIsLocked();
            so1.IsLockID = ConstantDefine.ISLOCK_LOCK;
            so1.IsLockName = "锁定";
            NewsIsLocked so2 = new NewsIsLocked();
            so2.IsLockID = ConstantDefine.ISLOCK_NOTLOCK;
            so2.IsLockName = "解锁";
            List<NewsIsLocked> lockList = new List<NewsIsLocked>();
            lockList.Add(so1);
            lockList.Add(so2);
            #endregion
            #region 时间显示处理
            //时间在表格中显示处理,我的目的是创建一个时间转换对照表，所以这个表不允许有重复的键值
            List<C_NewsTime> ts = new List<C_NewsTime>();
            var ContentCollection = storeDED.T_Content.ToList();
            var ContentNum = storeDED.T_Content.Count();
            int[] aa = new int[ContentNum];
            int ii = 0;
            foreach (var m in ContentCollection)
            {
                bool Weather=false;
                int aim= Convert.ToInt32(m.PubDate);
                for(int i=0;i<aa.Length;i++){
                    if(aim==aa[i]){
                        Weather=true;
                    }
                }
                if(Weather==false){
                    aa[ii] = Convert.ToInt32(m.PubDate);
                    ii++;
                }
            }
            for (int ie = 0; ie < ContentNum; ie++)
            {
                //List<C_NewsTime> ts中不能添加重复的时间
                C_NewsTime cc = new C_NewsTime();
                cc.OriTime = aa[ie];
                cc.SetManTime(cc.OriTime);
                ts.Add(cc);
            }
            #endregion

            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            List<C_NewsState> GetState = storeDB.C_NewsState.ToList();
            List<C_NewsType> Gettype = storeDB.C_NewsType.ToList();
            List<C_ContentColumn> Getcolumn = storeDB.C_ContentColumn.ToList();
            List<T_User> GetUserInfo = storeDC.T_User.ToList();

            var result = from a in theNews from b in ContentCollection from c in Gettype from d in GetState from e in showList from f in lockList from g in Getcolumn from h in ts where (a.NewsID == b.ContentID && a.NewsTypeID == c.TypeID && a.FlowState == d.NewsStateID && a.IsShow == e.IsShowID && a.IsLocked == f.IsLockID && a.NewsColumnID == g.ContColumnID && b.PubDate == h.OriTime) orderby a.InnerID select new { a.NewsID, b.ContentTitle, c.TypeName, a.NewsAuthor, b.ContentPublisher, h.ManTime, d.NewsStateName, e.IsShowName, f.IsLockName, g.ContColumnName, NickName };

            if (TheAttribute == "NewsID")
            {
                string TargetID = TheValue;
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.NewsID == TargetID) select a;
            }
            if (TheAttribute == "NewsTitle")
            {
                //result = from a in result where (String.IsNullOrEmpty(TheValue) || a.NewsTitle.Contains(TheValue)) select a;
            }
            if (TheAttribute == "NewsAuthor")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.NewsAuthor.Contains(TheValue)) select a;
            }
            if (TheAttribute == "FlowState")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.NewsStateName.Contains(TheValue)) select a;
            }
            if (TheAttribute == "IsLocked")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.IsLockName.Contains(TheValue)) select a;
            }

            #endregion

            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串


            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult DeleteNews()
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
                string ContentInsert1 = "delete from T_News where NewsID='" + TargetID + "'";
                conn.Open();
                try
                {
                    SqlCommand cmd1 = new SqlCommand(ContentInsert, conn);
                    SqlCommand cmd2 = new SqlCommand(ContentInsert1, conn);
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                }
                conn.Close();
            }
            #region 删除功能（摈弃）
            //for (int a = 0; a < ArrayCount; a++)
            //{
            //    //依次删除相应数据
            //    string TargetID = SplitArray[a];
            //    var TargetData = storeDB.T_News.Find(TargetID);
            //    storeDB.T_News.Remove(TargetData);
            //    storeDB.SaveChanges();
            //}
            #endregion

            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult StatePass()
        {
            //通过审核按钮点击触发方法
            #region 单选更改方式
            //int getSID = Convert.ToInt32(Request.Form[0]);
            //T_News tns = (from a in storeDB.T_News where a.NewsID == getSID select a).FirstOrDefault();
            //if(tns.FlowState==1){
            //    //判断该新闻是否处于待审核状态
            //    tns.FlowState = 2;//通过审核
            //}
            //if (ModelState.IsValid)
            //{
            //    storeDB.Entry(tns).State = EntityState.Modified;
            //    storeDB.SaveChanges();
            //}
            //string PassCount = "1";
            #endregion
            string getSID = Request.Form[0];//获取需要删除的新闻编号组成的字符串，且以逗号分割
            string[] SplitIDArray = getSID.Split(',');//取到要删除的新闻编号放进数组里
            int PassCount = SplitIDArray.Count();
            //string StringCount = Convert.ToString(PassCount);
            int returnCount = 0;
            for (int a = 0; a < PassCount; a++)
            {
                //依次删除相应数据
                string TargetID = SplitIDArray[a];
                T_News tns = (from z in storeDB.T_News where z.NewsID == TargetID select z).FirstOrDefault();
                if (tns.FlowState == STATE_WAITCHECK)
                {
                    //判断该新闻是否处于待审核状态
                    tns.FlowState = STATE_PASSCHECK;//通过审核
                    returnCount++;
                }
                if (ModelState.IsValid)
                {
                    storeDB.Entry(tns).State = EntityState.Modified;
                    storeDB.SaveChanges();
                }
            }
            string returnCountString = Convert.ToString(returnCount);//获取实际审核通过新闻数回传
            return Content(returnCountString);
        }

        [BaseActionFilter]
        public ActionResult StateReject()
        {
            string rejectID = Request.Form[0];
            string[] rejectArray = rejectID.Split(',');//取到要删除的新闻编号放进数组里
            int PassCount = rejectArray.Count();
            //string StringCount = Convert.ToString(PassCount);
            int returnCount = 0;
            for (int i = 0; i < PassCount; i++)
            {
                //依次删除相应数据
                string TargetID = rejectArray[i];
                T_News tns1 = (from z in storeDB.T_News where z.NewsID == TargetID select z).FirstOrDefault();
                if (tns1.FlowState == STATE_PASSCHECK)
                {
                    //判断该新闻是否处于待审核状态
                    tns1.FlowState = STATE_BACKCHECK;//驳回
                    returnCount++;
                }
                if (ModelState.IsValid)
                {
                    storeDB.Entry(tns1).State = EntityState.Modified;
                    storeDB.SaveChanges();
                }
            }
            string returnCountString = Convert.ToString(returnCount);//获取实际审核通过新闻数回传
            return Content(returnCountString);
        }

        [BaseActionFilter]
        public ActionResult NewsDetail(string id)
        {
            id = id.Replace("'", "");
            //显示新闻详情页
            var tns1 = (from z in storeDB.T_News where z.NewsID == id select z).ToList();//获取详情页新闻对象
            //使用ViewData传递一些可以使用的值去视图
            var targetItem = storeDB.T_News.Where(e => e.NewsID == id).ToList();
            var ContentItem = storeDED.T_Content.Where(e => e.ContentID == id).ToList();
            foreach (var m in targetItem)
            {
                int theType = m.NewsTypeID;
                ViewData["thetype"] = storeDB.C_NewsType.Find(theType).TypeName; //当前新闻种类

                int theState = m.FlowState;
                ViewData["thestate"] = storeDB.C_NewsState.Find(theState).NewsStateName; //当前新闻状态

                int theColumn = Convert.ToInt32(m.NewsColumnID);
                ViewData["thecolumn"] = storeDB.C_ContentColumn.Find(theColumn).ContColumnName; //当前新闻栏目
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
        public ActionResult UpdateNews(string id)
        {
            id = id.Replace("'", "");
            //显示新闻详情页
            T_News tts = storeDB.T_News.Find(id); //获取详情页新闻对象
            //在内容表中获取该条新闻的时间
            int tc = storeDED.T_Content.Find(id).PubDate;
            string tt = storeDED.T_Content.Find(id).ContentTitle;
            string tp = storeDED.T_Content.Find(id).ContentPublisher;
            ViewData["FromContentTime"] = tc;
            ViewData["FromContentTitle"] = tt;
            ViewData["FromContentPublish"] = tp;
            return View(tts);
        }
        [HttpPost]
        [ValidateInput(false)]
        [MultiButton("sub1")]
        [BaseActionFilter]
        public ActionResult UpdateNews(T_News getNew, FormCollection collection, HttpPostedFileBase NewsPicUpload1, HttpPostedFileBase NewsVideoUpload1)
        {
            string PICNewPath = "";
            string VNewPath = "";
            if (NewsPicUpload1 != null && NewsPicUpload1.ContentLength > 0)
            {
                //获取文件上传路径
                //string FilePath = Path.Combine(HttpContext.Server.MapPath("/Areas/School/SchoolUploadFile/PicNewsFile/"), Path.GetFileName(NewsPicUpload.FileName));
                string FilePath = "/Areas/News/UploadFile/PicNewsFile/" + NewsPicUpload1.FileName;
                //保存文件
                PICNewPath = FilePath;
                NewsPicUpload1.SaveAs(HttpContext.Server.MapPath(FilePath));
                Response.Write("<script>alert('文件上传成功');</script>");
            }

            if (NewsVideoUpload1 != null && NewsVideoUpload1.ContentLength > 0)
            {
                string FilePath1 = "/Areas/News/UploadFile/VideoNewsFile/" + NewsVideoUpload1.FileName;
                //保存文件
                VNewPath = FilePath1;
                NewsVideoUpload1.SaveAs(HttpContext.Server.MapPath(FilePath1));
                Response.Write("<script>alert('文件上传成功');</script>");
            }

            string id = getNew.NewsID;
            string title = collection["ContentTitle"];
            int typeid = Convert.ToInt32(getNew.NewsTypeID);
            string author = getNew.NewsAuthor;
            string publish = Convert.ToString(collection["ContentPublisher"]);

            //这里的flowstate需要查询数据库获取原来的值
            var tt = storeDB.T_News.Find(id);
            int flowstate = STATE_WAITCHECK; //每次提交应变为待审核状态         

            int isshow = ISSHOW_SHOW;
            int islock = ISLOCK_NOTLOCK;
            int columnid = Convert.ToInt32(getNew.NewsColumnID);
            DateTime OriginalTime = Convert.ToDateTime(collection["GnewPubdate"]);
            int FinalTime = HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;

            string picurl = tt.PicUrl;
            string videourl = tt.VideoUrl;
            if (PICNewPath != "")
            {
                picurl = PICNewPath;
            }
            if (VNewPath != "")
            {
                videourl = VNewPath;
            }

            string htmlurl = getNew.Html;
            string linkurl = getNew.LinkUrl;
            int innerid = getNew.InnerID;
            string download = getNew.Download;
            //数据库跟新
            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            string strSQL = "update T_News set NewsTypeID=" + typeid + ",NewsAuthor='" + author + "',FlowState=" + flowstate + ",IsShow=" + isshow + ",IsLocked=" + islock + ",NewsColumnID=" + columnid + ",PicUrl='" + picurl + "',VideoUrl='" + videourl + "',Html='" + htmlurl + "',LinkUrl='" + linkurl + "',InnerID=" + innerid + ",Download='" + download + "' where NewsID='" + id + "';";
            string strSQL1 = "update T_Content set ContentTitle='" + title + "',ContentPublisher='" + publish + "',PubDate=" + pubdate + " where ContentID='" + id + "';";
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                SqlCommand cmd1 = new SqlCommand(strSQL1, conn);
                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
            }
            conn.Close();

            return RedirectToAction("HomePageTable", "NewsCenter");
        }

        [HttpPost]
        [ValidateInput(false)]
        [MultiButton("sub2")]
        [BaseActionFilter]
        public ActionResult DraftNews(T_News getNew, FormCollection collection, HttpPostedFileBase NewsPicUpload1, HttpPostedFileBase NewsVideoUpload1)
        {
            #region 保存成草稿
            string PICNewPath = "";
            string VNewPath = "";
            if (NewsPicUpload1 != null && NewsPicUpload1.ContentLength > 0)
            {
                //获取文件上传路径
                //string FilePath = Path.Combine(HttpContext.Server.MapPath("/Areas/School/SchoolUploadFile/PicNewsFile/"), Path.GetFileName(NewsPicUpload.FileName));
                string FilePath = "/Areas/News/UploadFile/PicNewsFile/" + NewsPicUpload1.FileName;
                //保存文件
                PICNewPath = FilePath;
                NewsPicUpload1.SaveAs(HttpContext.Server.MapPath(FilePath));
                Response.Write("<script>alert('文件上传成功');</script>");
            }

            if (NewsVideoUpload1 != null && NewsVideoUpload1.ContentLength > 0)
            {
                string FilePath1 = "/Areas/News/UploadFile/VideoNewsFile/" + NewsVideoUpload1.FileName;
                //保存文件
                VNewPath = FilePath1;
                NewsVideoUpload1.SaveAs(HttpContext.Server.MapPath(FilePath1));
                Response.Write("<script>alert('文件上传成功');</script>");
            }

            string id = getNew.NewsID;
            string title = collection["ContentTitle"]; 
            int typeid = Convert.ToInt32(getNew.NewsTypeID);
            string author = getNew.NewsAuthor;
            string publish = Convert.ToString(collection["ContentPublisher"]);

            //这里的flowstate需要查询数据库获取原来的值
            var tt = storeDB.T_News.Find(id);
            int flowstate = STATE_BACKCHECK;//其他都不变，需要将状态改变成3，草稿箱状态

            int isshow = ISSHOW_SHOW;
            int islock = ISLOCK_NOTLOCK;
            int columnid = Convert.ToInt32(getNew.NewsColumnID);
            DateTime OriginalTime = Convert.ToDateTime(collection["GnewPubdate"]);
            int FinalTime = HandleTime.ConvertDateTimeInt(OriginalTime);
            int pubdate = FinalTime;

            string picurl = tt.PicUrl;
            string videourl = tt.VideoUrl;
            if (PICNewPath != "")
            {
                picurl = PICNewPath;
            }
            if (VNewPath != "")
            {
                videourl = VNewPath;
            }

            string htmlurl = getNew.Html;
            string linkurl = getNew.LinkUrl;
            int innerid = getNew.InnerID;
            string download = getNew.Download;
            //数据库跟新
            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            string strSQL = "update T_News set NewsTypeID=" + typeid + ",NewsAuthor='" + author + "',FlowState=" + flowstate + ",IsShow=" + isshow + ",IsLocked=" + islock + ",NewsColumnID=" + columnid + ",PicUrl='" + picurl + "',VideoUrl='" + videourl + "',Html='" + htmlurl + "',LinkUrl='" + linkurl + "',InnerID=" + innerid + ",Download='" + download + "' where NewsID='" + id + "';";
            string strSQL1 = "update T_Content set ContentTitle='" + title + "',ContentPublisher='" + publish + "',PubDate=" + pubdate + " where ContentID='" + id + "';";
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                SqlCommand cmd1 = new SqlCommand(strSQL1, conn);
                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
            }
            conn.Close();
            #endregion
            return RedirectToAction("HomePageTable", "NewsCenter");
        }

        public ActionResult LockNew()
        {
            string LockID = Request.Form[0];
            int islock = ISLOCK_LOCK;
            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            string strSQL = "update T_News set IsLocked=" + islock + " where NewsID='" + LockID + "';";
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            return Content("1");
        }
        [BaseActionFilter]
        public ActionResult UnLockNew()
        {
            string UnLockID = Request.Form[0];
            int islock = ISLOCK_NOTLOCK;
            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            string strSQL = "update T_News set IsLocked=" + islock + " where NewsID='" + UnLockID + "';";
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            return Content("1");
        }
        [BaseActionFilter]
        public ActionResult ShowNew()
        {
            string showID =Request.Form[0];
            int isshow = ISSHOW_HIDE;
            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            string strSQL = "update T_News set IsShow=" + isshow + " where NewsID='" + showID + "';";
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            return Content("1");
        }
        [BaseActionFilter]
        public ActionResult HideNew()
        {
            string hideID = Request.Form[0];
            int isshow = ISSHOW_SHOW;
            string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
            SqlConnection conn = new SqlConnection(Connectstr);
            string strSQL = "update T_News set IsShow=" + isshow + " where NewsID='" + hideID + "';";
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            return Content("1");
        }
    }
}
