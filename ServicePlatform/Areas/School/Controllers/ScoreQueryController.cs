using ServicePlatform.App_Start;
using ServicePlatform.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Config;
using xyj.Plugs;
using ServicePlatform.Controllers.Base;
using System.Data.Entity;
using Core.Services;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services.Utility;

namespace ServicePlatform.Areas.School.Controllers
{
    public class ScoreQueryController : BaseAccountController, ICutPage, IModelNote
    {
        //
        // GET: /School/ScoreQuery/
        private Repository Db = new Repository();

        private ServicePlatform.Models.SchoolContext _sc = new ServicePlatform.Models.SchoolContext();
        private ServicePlatform.Models.EnterpriseContext _ec = new ServicePlatform.Models.EnterpriseContext();
        private ScoreServices ScoreServices=new ScoreServices();


        #region 班级联动
        //根据学校取年级 HttpPost: /School/ScoreQuery/GetGrades
        [HttpPost]
        public JsonResult GetGrades(string schoolId)
        {
            return Json(_sc.C_Specialty.Where(m => m.SchoolID == schoolId).
                GroupBy(n=>n.EntryYear).
                Select(g => new { Value = g.Key, Text = g.FirstOrDefault().EntryYear }).ToList());
        }

        //根据年级取专业 HttpPost: /School/ScoreQuery/GetMajors
        [HttpPost]
        public JsonResult GetMajors(string schoolId,int gradeId)
        {
            
            return Json(_sc.C_Specialty.Where(m => m.EntryYear == gradeId&&m.SchoolID==schoolId).
                Select(item => new  { Value = item.SpeNo, Text = item.SpeName }).ToList());
        }

        //根据专业取班级 HttpPost: /School/ScoreQuery/GetClasss
        [HttpPost]
        public JsonResult GetClasss(string majorId)
        {
            return Json(_sc.T_Class.Where(m => m.SpeNo == majorId).
                Select(item => new  { Value = item.ClassID, Text = item.ClassName }).ToList());
        }
#endregion

        //根据班级取学生 HttpPost: /School/ScoreQuery/GetStudents
        [HttpPost]
        public ActionResult GetStudents(string classId)
        {
            DataContext.SetFiled("ClassId", classId);
            return Json(S<T_Student>().All(DataContext, "该学校所有学生").
                Select(a=>new { StuNo=a.StuNo, StuSex = a.StuSex, StuCellphone = a.StuCellphone, StuName = a.StuName }));
        }

        //根据班级取学生及其成绩的列表 HttpPost: /School/ScoreQuery/GetStudentsScore
        [HttpPost]
        public ActionResult GetStudentsScore(string classId)
        {
            DataContext.SetFiled("ClassId", classId);
            return Json((Db.T_StuBatchReg.Where(c=>c.T_Student.StuClass==classId).Select(s => 
            new { StuNo = s.T_Student.StuNo, StuName = s.T_Student.StuName, StuSex = s.T_Student.StuSex,
                StuCellphone = s.T_Student.StuCellphone, ReviewScore =s.ReviewScore ,StuUserid=s.UserID })).
                ToList());
        }

       [BaseActionFilter]
        public ActionResult Index(string classId, int perCount = 8, int pageIndex = 1)
       {
           ViewBag.schoolId = DataContext.UserUnit;
              return View();
        }

        //Post: /School/ScoreQuery/CaculateScore/
        [HttpPost]
         public ActionResult CaculateScore(string StuNo)
          {
              //var score =GetReviewScoreByStuNo(StuNo);

              //if (score <=0)
              double score;
              score = ScoreServices.CaculateReviewScoreByStuNo(StuNo);

              return Json(new
              {
                  StuNo = StuNo,
                  Score = score
              });

          }



    }
}
