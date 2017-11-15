using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using System.Data;
using System.Data.Entity;
using Core.Services;
using ServicePlatform.Config;
using ServicePlatform.App_Start;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.School.Controllers
{
    public class ScoreSetupController : BaseAccountController
    {
        //
        // GET: /School/ScoreSetup/

        SchoolContext sc = new SchoolContext();
        CodeTableContext ct = new CodeTableContext();
        [BaseActionFilter]
        public ActionResult Index()
        {
            return View();
        }

        #region T_PracBatch（评分比例设置【针对某个批次】）

        //【ajax】获取比重
        public int Get_ScoreWeight()
        {
            var PracBatchID = GetCurentPracBatchID();
            var old = sc.PracBatch.Find(PracBatchID);
            return old != null ? old.SchoolReviewWeight : -1;
        }
        //【ajax】保存比重
        [BaseActionFilter]
        public ActionResult Save_ScoreWeight(int SchoolReviewWeight)
        {
            var PracBatchID = GetCurentPracBatchID();
            var old = sc.PracBatch.Find(PracBatchID);
            if (old != null)
            {
                old.SchoolReviewWeight = SchoolReviewWeight;
                sc.Entry(old).State = EntityState.Modified;
                sc.SaveChanges();
                return Content("设置成功");
            }
            else
            {
                return Content("设置失败");
            }
        }
        //界面
        [BaseActionFilter]
        public ActionResult ScoreWeightSet()
        {
            DataContext.SetFiled("SchoolID", DataContext.UserUnit);
            var dest = S<Core.Services.Entity.T_PracBatch>().All(DataContext, "该学校当前批次信息");
            if (dest.Count != 1)
            {
                return Alert("不存在当前批次或有多个当前批次！");
            }
            var model = dest.FirstOrDefault();
            DataContext.SetFiled("PracBatchID", model.PracBatchID);
            var itemCount = S<Core.Services.Entity.T_SchoolReviewItem>().All(DataContext, "该批次所有评分项").Count;
            ViewData["tip"] = itemCount > 0 ? "当下已存在评价项目，修改比重需要重新计算实习成绩！":"";


            return View(model);
        }

        #endregion
        //获取学校当前批次
        private string GetCurentPracBatchID()
        {
            var schoolId = (Session["Vars"] as Vars).getUserUnit();
            return
                sc.PracBatch.Where(a => a.IsCurrentBatch == "是" && a.SchoolID == schoolId)
                    .Select(b => b.PracBatchID)
                    .FirstOrDefault();
        }
        #region C_SchoolReviewItem（评分项目设置【针对某个批次】）

        //获取评分项
        private List<C_SchoolReviewItem> Get_ScoreItems()
        {
            var PracBatchID = GetCurentPracBatchID();
            return sc.C_SchoolReviewItem.Where(m => m.PracBatchID == PracBatchID).ToList();
        }
        //编辑评分项

        [BaseActionFilter]
        public ActionResult ScoreItem_Edict()
        {
            return View();
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult ScoreItem_Edict(C_SchoolReviewItem f)
        {
            string PracBatchID = Request.Form["PracBatchID"];
            sc.C_SchoolReviewItem.Add(f);
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItem_ListShow?PracBatchID" + PracBatchID);
        }
        //添加评分项
        [BaseActionFilter]
        public ActionResult ScoreItem_Add()
        {
            return View();
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult ScoreItem_Add(C_SchoolReviewItem f)
        {
           f.PracBatchID = GetCurentPracBatchID();
            sc.C_SchoolReviewItem.Add(f);
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItem_ListShow?PracBatchID" + f.PracBatchID);
        }
        //删除评分项

        [BaseActionFilter]
        public ActionResult ScoreItem_Delete(string ItemNo, string PracBatchID)
        {

            var old = sc.C_SchoolReviewItem.Find(ItemNo);
            sc.Entry(old).State = EntityState.Deleted;
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItem_ListShow?PracBatchID" + PracBatchID);
        }
        //评分项列表

        [BaseActionFilter]
        public ActionResult ScoreItem_ListShow(int perCount = 8, int pageIndex = 1)
        {

            //Vars vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = Get_ScoreItems();//筛选数据
            ViewBag.link = "/School/ScoreSetup/ScoreItem_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<C_SchoolReviewItem>(data, perCount, pageIndex);//分页操作
            //分页相关结束


            ViewBag.Title = "评分项列表";
            ViewBag.addLink = "/School/ScoreSetup/ScoreItem_Add";
            ViewBag.Limit = 4;
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new C_SchoolReviewItem_Note())[1];
            ViewBag.Action = new string[][]
             {
               
                new string[] {"/School/ScoreSetup/ScoreItemToTeacher_ListShow","分配教师"},
             new string[] {"/School/ScoreSetup/ScoreItem_Delete","删除"}
             };





            return View();
        }

        #endregion

        #region T_SchoolReviewerTask（评分项目分配到教师【针对某个批次】）
        //获取数据
        public List<T_SchoolReviewerTask> Get_ScoreItemToTeacherList(string ItemNo)
        {
            var PracBatchID = GetCurentPracBatchID();
            return sc.T_SchoolReviewerTask.Where(m => m.PracBatchID == PracBatchID).Where(n => n.ReviewItemID == ItemNo).ToList(); ;
        }
        private List<T_SchoolReviewerTask_Note> Get_ScoreItemToTeacherList(List<T_SchoolReviewerTask> dataList)
        {
            return (from a in dataList
                    join b in sc.C_SchoolReviewItem on a.ReviewItemID equals b.ItemNo
                    join c in sc.T_Faculty on a.SchoolReviewerUserID equals c.UserID
                    select new T_SchoolReviewerTask_Note()
                    {
                        PracBatchID = a.PracBatchID,
                        SchoolReviewerUserID = c.FacName,
                        ReviewItemID = b.ItemName
                    }).ToList();
        }
        private List<SelectListItem> Get_TeacherSelectList()
        {
            var schoolId = (Session["Vars"] as Vars).getUserUnit();
            var itemList = new List<SelectListItem>();
            var dataList = sc.T_Faculty.Where(a => a.SchoolID == schoolId).ToList();

            for (int i = 0; i < dataList.Count; i++)
            {
                itemList.Add(new SelectListItem() { Text = dataList[i].FacName, Value = dataList[i].UserID });
            }
            return itemList;
        }
        //添加

        [HttpPost]
        [BaseActionFilter]
        public ActionResult ScoreItemToTeacher_Add(T_SchoolReviewerTask f)
        {
            string PracBatchID = GetCurentPracBatchID();
            string ItemNo = f.ReviewItemID;
            f.PracBatchID = GetCurentPracBatchID();
            sc.T_SchoolReviewerTask.Add(f);
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItemToTeacher_ListShow?PracBatchID=" + PracBatchID + "&ItemNo=" + ItemNo);
        }
        //删除
        [BaseActionFilter]
        public ActionResult ScoreItemToTeacher_Delete(string ReviewItemID, string PracBatchID, string SchoolReviewerUserID)
        {
            string TeacherID = SchoolReviewerUserID;
            string ItemNo = ReviewItemID;
            var old = sc.T_SchoolReviewerTask.Find(PracBatchID, TeacherID, ItemNo);
            sc.Entry(old).State = EntityState.Deleted;
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItemToTeacher_ListShow?PracBatchID=" + PracBatchID + "&ItemNo=" + ItemNo);
        }
        //评分项目分配列表

        [BaseActionFilter]
        public ActionResult ScoreItemToTeacher_ListShow(string ItemNo, int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13
            var PracBatchID = GetCurentPracBatchID();
            Vars vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = Get_ScoreItemToTeacherList(ItemNo);//筛选数据
            var dataNote = Get_ScoreItemToTeacherList(data);//处理显示数据【add】
            ViewBag.link = "/School/ScoreSetup/ScoreItemToTeacher_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_SchoolReviewerTask>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_SchoolReviewerTask_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "给评分项目分配教师";
            ViewBag.addLink = "/School/ScoreSetup/ScoreItemToTeacher_Add";
            ViewBag.Limit = new int[] { 1 };//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_SchoolReviewerTask_Note())[1];
            ViewBag.Action = new string[][]
             {
               
                 new string[] {"/School/ScoreSetup/ScoreItemToStudent_ListShow","分配学生"},
                new string[] {"/School/ScoreSetup/ScoreItemToTeacher_Delete","删除"}
             };


            ViewBag.TeacherSelectList = Get_TeacherSelectList();
            ViewBag.PracBatchID = PracBatchID;
            ViewBag.ItemNo = ItemNo;
            return View();
        }


        #endregion

        #region T_SchoolStudentReviewLink（评分学生分配）
        //获取数据
        public List<T_SchoolStudentReviewLink> Get_StudentList(string ItemNo, string TeacherID)
        {
            var PracBatchID = GetCurentPracBatchID();
            return sc.T_SchoolStudentReviewLink.Where(m => m.PracBatchID == PracBatchID).Where(n => n.ItemID == ItemNo).Where(p => p.SchoolReviewerUserID == TeacherID).ToList();

        }
        //代码转化
        private List<T_SchoolStudentReviewLink_Note> Get_StudentList(List<T_SchoolStudentReviewLink> dataList)
        {
            return (from a in dataList
                    join b in sc.C_SchoolReviewItem on a.ItemID equals b.ItemNo
                    join c in sc.T_Faculty on a.SchoolReviewerUserID equals c.UserID
                    join d in sc.StuBatchReg on a.PracticeNo equals d.PracticeNo
                    join da in sc.Student on d.UserID equals da.UserID
                    select new T_SchoolStudentReviewLink_Note()
                    {
                        PracBatchID = a.PracBatchID,
                        SchoolReviewerUserID = c.FacName,
                        PracticeNo = da.StuName,
                        ItemID = b.ItemName,
                        ReviewScore = a.ReviewScore.ToString(),
                        ReviewComment = a.ReviewComment
                    }).ToList();
        }
        private List<SelectListItem> Get_StudentSelectList()
        {
            var schoolId = (Session["Vars"] as Vars).getUserUnit();
            return (from a in sc.StuBatchReg
                    join b in sc.Student on a.UserID equals b.UserID
                    join c in sc.PracBatch on a.PracBatchID equals c.PracBatchID
                    join d in sc.T_Class on b.StuClass equals  d.ClassID
                    where c.IsCurrentBatch == "是" &&
                    d.SchoolID==schoolId&&
                    a.CareerStatusID >= 13                       //只有在当前批次 且  招聘结束时  且 未被分配过 才能进行评分项分配
                    select new SelectListItem()
                    {
                        Text = b.StuName,
                        Value = a.PracticeNo
                    }).ToList();
        }
        //添加

        [HttpPost]
        [BaseActionFilter]
        public ActionResult ScoreItemToStudent_Add(T_SchoolStudentReviewLink f)
        {
            string PracBatchID = GetCurentPracBatchID();
            string ItemNo = f.ItemID;
            string TeacherID = f.SchoolReviewerUserID;
            f.PracBatchID = GetCurentPracBatchID();
            sc.T_SchoolStudentReviewLink.Add(f);
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItemToStudent_ListShow?PracBatchID=" + PracBatchID + "&ReviewItemID=" + ItemNo + "&SchoolReviewerUserID=" + TeacherID);
        }
        //删除
        [BaseActionFilter]
        public ActionResult ScoreItemToStudent_Delete(string SchoolReviewerUserID, string PracticeNo, string ItemID)
        {
            var pracBatchId = GetCurentPracBatchID();
            var old = sc.T_SchoolStudentReviewLink.Find(pracBatchId,SchoolReviewerUserID, PracticeNo, ItemID);
            sc.Entry(old).State = EntityState.Deleted;
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreItemToStudent_ListShow?" + "ReviewItemID=" + ItemID + "&SchoolReviewerUserID=" + SchoolReviewerUserID);
        }
        //评分项目分配列表

        [BaseActionFilter]
        public ActionResult ScoreItemToStudent_ListShow(string ReviewItemID, string SchoolReviewerUserID, int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13

            string TeacherID = SchoolReviewerUserID;
            string ItemNo = ReviewItemID;
            Vars vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = Get_StudentList(ItemNo, TeacherID);//筛选数据
            var dataNote = Get_StudentList(data);//处理显示数据【add】
            ViewBag.link = "/School/ScoreSetup/ScoreItemToStudent_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_SchoolStudentReviewLink>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_SchoolStudentReviewLink_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束

            var t1 = (from t in sc.T_Faculty where t.UserID == TeacherID select t).FirstOrDefault();
            string teacherName = t1 == null ? TeacherID : t1.FacName;
            ViewBag.Title = "给 [" + teacherName + "]负责的" + "的[" + ct.C_SchoolReviewItem.Find(ItemNo).ItemName + "]评分项分配学生";
            ViewBag.addLink = "/School/ScoreSetup/ScoreItemToStudent_Add";
            ViewBag.Limit = new int[] { 2 };//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_SchoolStudentReviewLink_Note())[1];
            ViewBag.Action = new string[][]
             {
               
               
                new string[] {"/School/ScoreSetup/ScoreItemToStudent_Delete","删除"}
             };


            ViewBag.TeacherSelectList = Get_StudentSelectList();

            ViewBag.ItemNo = ItemNo;
            ViewBag.TeacherID = TeacherID;

            return View();
        }

        //未分配学生列表
        [BaseActionFilter]

        public ActionResult Student_ListShow(string ReviewItemID, string SchoolReviewerUserID, int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13

            string TeacherID = SchoolReviewerUserID;
            string ItemNo = ReviewItemID;
            Vars vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = Get_StudentList(ItemNo, TeacherID);//筛选数据

            //求交集
            var dataNote = Get_StudentList(data);//处理显示数据【add】
            ViewBag.link = "/School/ScoreSetup/ScoreItemToStudent_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_SchoolStudentReviewLink>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_SchoolStudentReviewLink_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束

            var t1 = (from t in sc.T_Faculty where t.UserID == TeacherID select t).FirstOrDefault();
            string teacherName = t1 == null ? TeacherID : t1.FacName;
            ViewBag.Title = "给 [" + teacherName + "]负责的" + "的[" + ct.C_SchoolReviewItem.Find(ItemNo).ItemName + "]评分项分配学生";
            ViewBag.addLink = "/School/ScoreSetup/ScoreItemToStudent_Add";
            ViewBag.Limit = new int[] { 2 };//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_SchoolStudentReviewLink_Note())[1];
            ViewBag.Action = new string[][]
             {
               
                new string[] {"/School/ScoreSetup/ScoreItemToStudent_Delete","删除"}
             };


            ViewBag.TeacherSelectList = Get_StudentSelectList();

            ViewBag.ItemNo = ItemNo;
            ViewBag.TeacherID = TeacherID;

            return View();
        }

        #endregion

        #region T_SchoolStudentReviewLink（给学生评分）
        //获取数据
        private List<T_SchoolStudentReviewLink> Get_ScoreToStudentList(string TeacherID)
        {
            var pracBatchId = GetCurentPracBatchID();


            return sc.T_SchoolStudentReviewLink.Where(p => p.SchoolReviewerUserID == TeacherID && p.PracBatchID == pracBatchId).ToList();
        }
        //代码转换
        private List<T_SchoolStudentReviewLink_Note> Get_ScoreToStudentList(List<T_SchoolStudentReviewLink> dataList)
        {
            return (from a in dataList
                    join b in sc.C_SchoolReviewItem on a.ItemID equals b.ItemNo
                    join c in sc.T_Faculty on a.SchoolReviewerUserID equals c.UserID
                    join d in sc.StuBatchReg on a.PracticeNo equals d.PracticeNo
                    join da in sc.Student on d.UserID equals da.UserID
                    select new T_SchoolStudentReviewLink_Note()
                    {
                        PracBatchID = a.PracBatchID,
                        SchoolReviewerUserID = c.FacName,
                        PracticeNo = da.StuName,
                        ItemID = b.ItemName,
                        ReviewScore = a.ReviewScore.ToString(),
                        ReviewComment = a.ReviewComment
                    }).ToList();
        }

        //设置分数
        [BaseActionFilter]
        public ActionResult ScoreToStudent_Edict(string SchoolReviewerUserID, string PracticeNo, string ItemID)
        {
            
            
            var PracBatchID = GetCurentPracBatchID();
            
            ViewBag.PracBatchID = PracBatchID; ;
            ViewBag.SchoolReviewerUserID = SchoolReviewerUserID;
            ViewBag.PracticeNo = PracticeNo;
            ViewBag.ItemID = ItemID;
            return View(sc.T_SchoolStudentReviewLink.Find(PracBatchID, PracticeNo, ItemID));
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult ScoreToStudent_Edict(T_SchoolStudentReviewLink f)
        {
            string PracBatchID = Request.Form["PracBatchID"];
            string ItemNo = Request.Form["ItemNo"];
            string TeacherID = Request.Form["TeacherID"];

            sc.Entry(f).State = EntityState.Modified;
            sc.SaveChanges();

            return Redirect("/School/ScoreSetup/ScoreToStudent_ListShow?PracBatchID" + PracBatchID + "ItemNo=" + ItemNo + "TeacherID=" + TeacherID);
        }

        //评分学生列表
        [BaseActionFilter]
   
        public ActionResult ScoreToStudent_ListShow(string TeacherID, int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13

            Vars vars = (Session["Vars"] as Vars);
            TeacherID = vars.UserID;//获取当前登陆教师
            //分页相关开始
            var data = Get_ScoreToStudentList(TeacherID);//筛选数据
            var dataNote = Get_ScoreToStudentList(data);//处理显示数据【add】
            ViewBag.link = "/School/ScoreSetup/ScoreToStudent_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_SchoolStudentReviewLink>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_SchoolStudentReviewLink_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "给学生录入成绩";
            ViewBag.addLink = "/School/ScoreSetup/#";
            ViewBag.Limit = new int[] { 2, 3,4, 5 };//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_SchoolStudentReviewLink_Note())[1];
            ViewBag.Action = new string[][]
             {
               
               
                new string[] {"/School/ScoreSetup/ScoreToStudent_Edict","评分"}
             };



            ViewBag.TeacherID = TeacherID;

            return View();
        }


        #endregion
    }
}
