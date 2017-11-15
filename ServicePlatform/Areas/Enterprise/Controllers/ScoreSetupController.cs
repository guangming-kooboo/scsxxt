using ServicePlatform.App_Start;
using ServicePlatform.Config;
using ServicePlatform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class ScoreSetupController : BaseAccountController
    {
        //
        // GET: /Enterprise/ScoreSetup/
        SchoolContext sc = new SchoolContext();
        CodeTableContext ct = new CodeTableContext();
        EnterpriseContext ec = new EnterpriseContext();
        public ActionResult Index()
        {
            return View();
        }
        //获取学校获得企业实习号
        private string GetEntPracNo(string SchoolID)
        {
            var Ent_No = (Session["Vars"] as Vars).getUserUnit();
            return (from a in ec.T_EntBatchReg
                    join b in ec.PracBatch on a.PracBatchID equals b.PracBatchID
                    where a.Ent_No == Ent_No && b.IsCurrentBatch == "是" && b.SchoolID == SchoolID
                    select a).FirstOrDefault().EntPracNo;
        }
        //根据企业实习号获取学校
        private string GetSchoolID(string EntPracNo)
        {
            return  (from a in ec.T_EntBatchReg
                        join b in ec.PracBatch on a.PracBatchID equals b.PracBatchID
                     where a.EntPracNo == EntPracNo && b.IsCurrentBatch == "是" 
                        select b.SchoolID).FirstOrDefault();
        }
     

        #region T_PracBatch（评分比例设置【针对某个学校】）

        //【ajax】获取比重
        public int Get_ScoreWeight(string SchoolID)
        {
            
            var Ent_No = (Session["Vars"] as Vars).getUserUnit();
            return (100-(from a in ec.T_EntBatchReg
                    join b in ec.PracBatch on a.PracBatchID equals b.PracBatchID
                         where a.Ent_No == Ent_No && b.IsCurrentBatch == "是" && b.SchoolID == SchoolID
                    select b).FirstOrDefault().SchoolReviewWeight);
        }
     
        //界面
        public ActionResult ScoreWeightSet()
        {
            return View();
        }

        #endregion

        #region C_EntReviewItem（评分项目设置【针对某个学校】）
        //获取评分项
        private List<C_EntReviewItem> Get_ScoreItems(string SchoolID)
        {
            var EntPracNo = GetEntPracNo(SchoolID);
            return ct.C_EntReviewItem.Where(m => m.EntPracNo == EntPracNo).ToList();
        }
        //编辑评分项
        public ActionResult ScoreItem_Edict()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ScoreItem_Edict(C_EntReviewItem f)
        {
            var SchoolID = Request.Form["SchoolID"];
            f.EntPracNo = GetEntPracNo(SchoolID);
            ct.Entry(f).State=EntityState.Modified;
            ct.SaveChanges();
            return Redirect("/Enterprise/ScoreSetup/ScoreItem_ListShow?SchoolID=" + SchoolID);
        }
        //添加评分项
        public ActionResult ScoreItem_Add(string SchoolID)
        {
            var EntPracNo = GetEntPracNo(SchoolID);
            ViewBag.EntPracNo = EntPracNo;
            return View();
        }
        [HttpPost]
        public ActionResult ScoreItem_Add(C_EntReviewItem f)
        {

         
            if (ct.C_EntReviewItem.Find(f.ItemNo) == null)
            {

                ct.C_EntReviewItem.Add(f);
                ct.SaveChanges();
                return Redirect("/Enterprise/ScoreSetup/ScoreItem_ListShow?SchoolID=" + GetSchoolID(f.EntPracNo));
            }
            else
                return Content(Lib.PageHelper.Tip("已存在该编号，请重试！", -1));

          
        }
        //删除评分项

        public ActionResult ScoreItem_Delete(string SchoolID,string ItemNo)
        {
            var EntPracNo = GetEntPracNo(SchoolID);
            var old = ct.C_EntReviewItem.Find(ItemNo);
            ec.Entry(old).State = EntityState.Deleted;
            ec.SaveChanges();

            return Redirect("/Enterprise/ScoreSetup/ScoreItem_ListShow?SchoolID=" + SchoolID);
        }
        //评分项列表
        [BaseActionFilter]
        public ActionResult ScoreItem_ListShow(string SchoolID="-1", int perCount = 8, int pageIndex = 1)
        {
            return Alert("企业端评分系统升级中...");
           // var EntPracNo =SchoolID!=null ? GetEntPracNo(SchoolID):"";
        
            //分页相关开始
            var data =SchoolID!="-1" ?Get_ScoreItems(SchoolID):new List<C_EntReviewItem>();//筛选数据
            ViewBag.link = "/Enterprise/ScoreSetup/ScoreItem_ListShow?SchoolID=" + SchoolID;
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<C_EntReviewItem>(data, perCount, pageIndex);//分页操作
            //分页相关结束


            ViewBag.Title = "评分项列表";
            ViewBag.addLink = "/Enterprise/ScoreSetup/ScoreItem_Add" ;
            ViewBag.Limit = 4;
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new C_EntReviewItem_Note())[1];
            ViewBag.Action = new string[][]
             {
               
                new string[] {"/Enterprise/ScoreSetup/ScoreItemToEmployee_ListShow","分配员工"},
             new string[] {"/Enterprise/ScoreSetup/ScoreItem_Delete","删除"}
             };




            ViewBag.SchoolID = SchoolID;
            if (SchoolID != "-1")
            {
                ViewBag.EntPracNo = GetEntPracNo(SchoolID);
            }
          
            return View();
        }

        #endregion

        #region T_EntReviewerTask（评分项目分配到员工【针对某个学校】）
        //获取数据
        public List<T_EntReviewerTask> Get_ScoreItemToEmployeeList(string EntPracNo, string ItemNo)
        {
            return ec.T_EntReviewerTask.Where(m => (m.EntPracNo == EntPracNo&&m.ReviewItemID == ItemNo)).ToList();
        }
        //代码转换
        private List<T_EntReviewerTask_Note> Get_ScoreItemToEmployeeList(List<T_EntReviewerTask> resourseList)
        {
            return (from a in resourseList
                    join b in ec.C_EntReviewItem on a.ReviewItemID equals b.ItemNo
                    join c in ec.T_Employee on a.EntReviewerUserID equals c.UserID
                    select new T_EntReviewerTask_Note()
                    {
                        EntPracNo = a.EntPracNo,
                        EntReviewerUserID = c.EmployeeName,
                        ReviewItemID = b.ItemName//此处转换代码
                    }).ToList();
        }
        private List<SelectListItem> Get_EmployeeSelectList(string Ent_No)
        {
             return (from a in ec.T_Employee
                     where a.Ent_No==Ent_No
                    select new SelectListItem()
                    {
                        Text = a.EmployeeName,
                        Value = a.UserID
                    }).ToList();
        }
        //添加

        [HttpPost]
        public ActionResult ScoreItemToEmployee_Add(T_EntReviewerTask f)
        {
            ec.T_EntReviewerTask.Add(f);
            ec.SaveChanges();

            return Redirect("/Enterprise/ScoreSetup/ScoreItemToEmployee_ListShow?EntPracNo=" + f.EntPracNo + "&ItemNo=" + f.ReviewItemID);
        }
        //删除
        public ActionResult ScoreItemToEmployee_Delete(string EntPracNo, string ItemNo, string EntReviewerUserID)
        {

            var old = ec.T_EntReviewerTask.Find(EntPracNo, EntReviewerUserID, ItemNo);
            ec.Entry(old).State = EntityState.Deleted;
            ec.SaveChanges();

            return Redirect("/Enterprise/ScoreSetup/ScoreItemToEmployee_ListShow?EntPracNo=" + EntPracNo + "&ItemNo" + ItemNo);
        }
        //评分项目分配列表
        [BaseActionFilter]

        public ActionResult ScoreItemToEmployee_ListShow(string EntPracNo, string ItemNo, int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13
          
            var Ent_No = (Session["Vars"] as Vars).getUserUnit();
            
            //分页相关开始

            var data = Get_ScoreItemToEmployeeList(EntPracNo, ItemNo);//筛选数据
            var dataNote = Get_ScoreItemToEmployeeList(data);//处理显示数据【add】
            ViewBag.link = "/Enterprise/ScoreSetup/ScoreItemToEmployee_ListShow?EntPracNo=" + EntPracNo + "&ItemNo" + ItemNo;
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_EntReviewerTask>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_EntReviewerTask_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "把评分项目分配给员工";
            ViewBag.addLink = "/Enterprise/ScoreSetup/ScoreItemToEmployee_AddEntPracNo=" + EntPracNo + "&ItemNo" + ItemNo;
            ViewBag.Limit = new int[] { 1 };//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_EntReviewerTask_Note())[1];
            ViewBag.Action = new string[][]
             {
               
                 new string[] {"/Enterprise/ScoreSetup/ScoreItemToStudent_ListShow","分配学生"},
                new string[] {"/Enterprise/ScoreSetup/ScoreItemToEmployee_Delete","删除"}
             };


            ViewBag.EmployeeSelectList = Get_EmployeeSelectList(Ent_No);
            ViewBag.EntPracNo = EntPracNo;
            ViewBag.ItemNo = ItemNo;
            return View();
        }


        #endregion

        #region T_EntStudentReviewLink（评分学生分配）
        //获取数据
        public List<T_EntStudentReviewLink> Get_StudentList(string EntPracNo, string ItemNo, string EmployeeID)
        {
            return ec.T_EntStudentReviewLink.Where(m =>( m.EntPracNo == EntPracNo&&m.ItemID == ItemNo&&m.EntReviewerUserID == EmployeeID)).ToList();       
        }
        private List<T_EntStudentReviewLink_Note> Get_StudentList(List<T_EntStudentReviewLink> dataList)
        {
            return (from a in dataList
                    join b in ec.T_Employee on a.EntReviewerUserID equals b.UserID
                    join c in ec.T_StuBatchReg on a.PracticeNo equals c.PracticeNo
                    join ca in ec.Student on c.UserID equals ca.UserID
                    join d in ec.C_EntReviewItem on a.ItemID equals d.ItemNo
                    //where c.CareerStatusID>=13
                    select new T_EntStudentReviewLink_Note()
                    {
                        EntPracNo = a.EntPracNo,
                        EntReviewerUserID = b.EmployeeName,
                        PracticeNo = ca.StuName,
                        ItemID = d.ItemName,
                        ReviewScore = a.ReviewScore.ToString(),
                        ReviewComment = a.ReviewComment


                    }).Distinct(new T_EntStudentReviewLink_Note()).ToList();
        }

        public class MyEqualityComparer : IEqualityComparer<SelectListItem>
        {
            public bool Equals(SelectListItem x, SelectListItem y)
            {
                return x.Value == y.Value;
            }

            public int GetHashCode(SelectListItem obj)
            {
                return obj.Value.GetHashCode();
            }
        }
        private List<SelectListItem> Get_StudentSelectList(string SchoolId)
        {
            var EntPracNo = GetEntPracNo(SchoolId);
            return (from a in ec.PracticeVolunteer
                join b in ec.T_StuBatchReg on a.PracticeNo equals b.PracticeNo
                join c in ec.Student on b.UserID equals c.UserID
                join d in ec.T_Class on c.StuClass equals d.ClassID
                where a.EntPracNo == EntPracNo &&
                      d.SchoolID == SchoolId && a.VolunteerStatus >= 5
                select new SelectListItem()
                {
                    Text = c.StuName,
                    Value = a.PracticeNo
                }).ToList();
        }
        //添加

        [HttpPost]
        public ActionResult ScoreItemToStudent_Add( T_EntStudentReviewLink f)
        {
            ec.T_EntStudentReviewLink.Add(f);
            ec.SaveChanges();

            return Redirect("/Enterprise/ScoreSetup/ScoreItemToStudent_ListShow?EntPracNo=" + f.EntPracNo + "&ReviewItemID=" + f.ItemID + "&EntReviewerUserID=" + f.EntReviewerUserID);
        }
        //删除
        public ActionResult ScoreItemToStudent_Delete(string EntPracNo, string EntReviewerUserID, string PracticeNo, string ItemID)
        {
          
            var old = ec.T_EntStudentReviewLink.Find(EntPracNo, EntReviewerUserID, PracticeNo, ItemID);
            ec.Entry(old).State = EntityState.Deleted;
            ec.SaveChanges();

            return Redirect("/Enterprise/ScoreSetup/ScoreItemToStudent_ListShow?EntPracNo=" + old.EntPracNo + "&ReviewItemID=" + old.ItemID + "&EntReviewerUserID=" + old.EntReviewerUserID);
        }
        //评分学生列表
        [BaseActionFilter]
        public ActionResult ScoreItemToStudent_ListShow(string EntPracNo, string ReviewItemID, string EntReviewerUserID, int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13
            
           return Alert("企业端评分系统正在升级...");
            var SchoolID = GetSchoolID(EntPracNo);
       
         
            var ItemNo = ReviewItemID;
            var vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = Get_StudentList(EntPracNo, ItemNo, EntReviewerUserID);//筛选数据
            var dataNote = Get_StudentList(data);//处理显示数据【add】
            ViewBag.link = "/Enterprise/ScoreSetup/ScoreItemToStudent_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_EntStudentReviewLink>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_EntStudentReviewLink_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "给 [" + ec.T_Employee.Find(EntReviewerUserID).EmployeeName + "]负责的" + "的[" + ct.C_EntReviewItem.Find(ItemNo).ItemName + "]评分项分配学生";
            ViewBag.addLink = "/Enterprise/ScoreSetup/ScoreItemToStudent_Add";
            ViewBag.Limit = new int[] { 2};//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_EntStudentReviewLink_Note())[1];
            ViewBag.Action = new string[][]
             {
               
               
                new string[] {"/Enterprise/ScoreSetup/ScoreItemToStudent_Delete","删除"}
             };


            ViewBag.StudentSelectList = Get_StudentSelectList(SchoolID);
            ViewBag.EntPracNo = EntPracNo;
            ViewBag.ItemNo = ItemNo;
            ViewBag.EntReviewerUserID = EntReviewerUserID;

            return View();
        }


        #endregion

        #region T_EntStudentReviewLink（给学生评分）
        //获取数据
        private List<T_EntStudentReviewLink> Get_ScoreToStudentList(string SchoolID,string EmployeeID)
        {
            var EntPracNo = GetEntPracNo(SchoolID);
            return ec.T_EntStudentReviewLink.Where(p => p.EntReviewerUserID == EmployeeID && p.EntPracNo == EntPracNo).ToList().Distinct(new T_EntStudentReviewLink()).ToList();
        }
        private List<T_EntStudentReviewLink_Note> Get_ScoreToStudentList(List<T_EntStudentReviewLink> dataList)
        {

            return (from a in dataList
                    join b in ec.T_Employee on a.EntReviewerUserID equals b.UserID
                    join c in ec.T_StuBatchReg on a.PracticeNo equals c.PracticeNo
                    join ca in ec.Student on c.UserID equals ca.UserID
                    join d in ec.C_EntReviewItem on a.ItemID equals d.ItemNo
                    select new T_EntStudentReviewLink_Note()
                    {
                        EntPracNo = a.EntPracNo,
                        EntReviewerUserID = b.EmployeeName,
                        PracticeNo = ca.StuName,
                        ItemID = d.ItemName,
                        ReviewScore = a.ReviewScore.ToString(),
                        ReviewComment = a.ReviewComment


                    }).ToList();
        }

        //设置分数
        public ActionResult ScoreToStudent_Edict(string SchoolID, string EntPracNo, string EntReviewerUserID, string PracticeNo)
        {
            //拿到所有评分项
           var  items=ec.T_EntStudentReviewLink.Where(
                a => a.EntPracNo == EntPracNo && a.EntReviewerUserID == EntReviewerUserID && a.PracticeNo == PracticeNo)
                .ToList();

           ViewBag.SchoolID = SchoolID;
            ViewBag.EntPracNo = EntPracNo;
            ViewBag.EntReviewerUserID = EntReviewerUserID;
            ViewBag.PracticeNo = PracticeNo;

          
            return View(items);
        }
        [HttpPost]
        public ActionResult ScoreToStudent_Edict_post(string SchoolID, string EntPracNo, string EntReviewerUserID, string PracticeNo)
        {
            //拿到所有评分项
            var items = ec.T_EntStudentReviewLink.Where(
                 a => a.EntPracNo == EntPracNo && a.EntReviewerUserID == EntReviewerUserID && a.PracticeNo == PracticeNo)
                 .Select(a => new { EntPracNo = a.EntPracNo, EntReviewerUserID = a.EntReviewerUserID, PracticeNo = a.PracticeNo, ItemID = a.ItemID }).ToList();
            foreach (var item in items)
            {
                ec.Entry(new T_EntStudentReviewLink()
                {
                    EntPracNo = EntPracNo,
                    EntReviewerUserID = EntReviewerUserID,
                    PracticeNo = PracticeNo,
                    ItemID = Request.Form[item.ItemID],
                    ReviewScore = int.Parse(Request.Form[item.ItemID + "ReviewScore"]),
                    ReviewComment = Request.Form[item.ItemID + "ReviewComment"]
                }).State = EntityState.Modified;
              
            }
            ec.SaveChanges();
           

            return Redirect("/Enterprise/ScoreSetup/ScoreToStudent_ListShow?SchoolID" + SchoolID );
        }

        //评分学生列表
          [BaseActionFilter]
        public ActionResult ScoreToStudent_ListShow(string SchoolID="-1",int perCount = 8, int pageIndex = 1)
        {//List版本日期2015/12/20 上一版本2015/12/13
              return Alert("企业端评分系统升级中...");
            var EntPracNo = ""; 
              if (SchoolID != "-1")
              {
                   EntPracNo = GetEntPracNo(SchoolID); 
                  
              }
           
            var EmployeeID = (Session["Vars"] as Vars).UserID;// //获取当前登陆员工
            //分页相关开始
            var data =SchoolID!="-1"? Get_ScoreToStudentList(SchoolID, EmployeeID):new List<T_EntStudentReviewLink>();//筛选数据
            var dataNote = Get_ScoreToStudentList(data);//处理显示数据【add】
            ViewBag.link = "/Enterprise/ScoreSetup/ScoreToStudent_ListShow?SchoolID" + SchoolID;
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_EntStudentReviewLink>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_EntStudentReviewLink_Note>(dataNote, perCount, pageIndex);//显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "给学生录入成绩";
            ViewBag.addLink = "/Enterprise/ScoreSetup/#";
            ViewBag.Limit = new int[] { 2};//配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_EntStudentReviewLink_Note())[1];
            ViewBag.Action = new string[][]
             {
               
               
                new string[] {"/Enterprise/ScoreSetup/ScoreToStudent_Edict?SchoolID"+SchoolID,"评分"}
             };


            ViewBag.EntPracNo = EntPracNo;
           
            ViewBag.EmployeeID = EmployeeID;

            ViewBag.SchoolID = SchoolID;
            return View();
        }


        #endregion
    }
}
