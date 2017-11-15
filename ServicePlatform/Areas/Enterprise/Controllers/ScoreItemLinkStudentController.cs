using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Newtonsoft.Json;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;


namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class ScoreItemLinkStudentController : BaseAccountController
    {
      

        public IDML<T_EntStudentReviewLink> Services { get { return S<T_EntStudentReviewLink>(); } }
        [HttpPost]
        public ActionResult GetStudents(string EntPracNo, string PositionID, string TaskID)
        {
            var repository = new Core.Services.Entity.Repository();
            DataContext.SetFiled("TaskID", TaskID);
            DataContext.SetFiled("EntPracNo", EntPracNo);
            DataContext.SetFiled("PosID", PositionID);
            //return Json(S<T_PracticeVolunteer>().All(DataContext, "参加该岗位的所有学生").Where(A=>A.VolunteerStatus == 5).
            //    Select(m=>m.T_StuBatchReg).
            //    Except(Services.All(DataContext, "该员工已分配情况").
            //    Select(n=>n.T_StuBatchReg),CommonExtendMethods.Equality<T_StuBatchReg>.CreateComparer(a=>a.UserID)).
            //    Select(o =>new { PracticeNo = o.PracticeNo, UserID = o.UserID, StuName = o.T_Student.StuName })
            //    );
            //return Json(S<T_PracticeVolunteer>().All(DataContext, "参加该岗位的所有学生").Where(A => A.VolunteerStatus == 5).
            //    Select(m => repository.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == repository.T_PracticeVolunteer.FirstOrDefault().PracticeNo)).
            //    Except(Services.All(DataContext, "该员工已分配情况").
            //    Select(n => n.T_StuBatchReg), CommonExtendMethods.Equality<T_StuBatchReg>.CreateComparer(a => a.UserID)).
            //    Select(o => new { PracticeNo = o.PracticeNo, UserID = o.UserID, StuName = o.T_Student.StuName })
            //    );
            return Json(string.Format(@"
select 
T_StuBatchReg.PracticeNo,
T_Student.UserID,
T_Student.StuName
from T_PracticeVolunteer,T_Student,T_StuBatchReg
where T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo and 
      T_StuBatchReg.UserID=T_Student.UserID and
	 T_PracticeVolunteer.EntPracNo='{0}' and
	T_PracticeVolunteer.PosID='{1}' and 
	  T_PracticeVolunteer.VolunteerStatus=5

	  except
	  (
	  select 
T_StuBatchReg.PracticeNo,
T_Student.UserID,
T_Student.StuName
from T_EntStudentReviewLink,T_Student,T_StuBatchReg
where T_EntStudentReviewLink.PracticeNo=T_StuBatchReg.PracticeNo and 
      T_StuBatchReg.UserID=T_Student.UserID and T_EntStudentReviewLink.TaskID='{2}'
	  )
", EntPracNo, PositionID, TaskID).ExecuteReader(ConfigurationManager.ConnectionStrings["Repository"].ConnectionString).
ToTableList());
            //return Json(repository.T_PracticeVolunteer.Where(
            //    a=>a.PracticeNo==repository.T_StuBatchReg.FirstOrDefault().PracticeNo&&
            //    repository.T_StuBatchReg.FirstOrDefault().UserID==repository.T_Student.FirstOrDefault().UserID&&
            //    a.EntPracNo==EntPracNo&&a.PosID==PositionID).Select(o=>new
            //    {
            //        PracticeNo = o.PracticeNo,
            //        UserID = o.UserID,
            //        StuName = o.T_Student.StuName
            //    }));
        }
        [HttpPost]
        public ActionResult GetArrangedList(string TaskID)
        {
            DataContext.SetFiled("TaskID", TaskID);
            return Content(JsonConvert.SerializeObject(
                Services.All(DataContext, "该员工已分配情况").
                Select(o => new
                {
                    UserID = o.T_StuBatchReg.UserID,
                    StuName = o.T_StuBatchReg.T_Student.StuName,
                    LinkID = o.LinkID
                })
                .ToList()));
        }

        [HttpPost]
        public ActionResult AjaxCreate(T_EntStudentReviewLink model)
        {
            var key = Services.Add(DataContext, model);
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            else
            {
                return Json(Services.Find(key));
            }
        }
        [HttpPost]
        public ActionResult AjaxDelete(string LinkID)
        {

            return Content(Services.Delete(DataContext, LinkID).ToString());

        }
        public ActionResult Index(string TaskID)
        {

            var temp = S<T_EntReviewerTask>().Find(TaskID);

            ViewBag.UserUnit = DataContext.UserUnit;
            ViewBag.TaskID = TaskID;
            ViewBag.EntPracNo = temp.T_EntReviewItem.EntPracNo;
            return View();
        }




        [HttpPost]
        public ActionResult GetMarkingStudents(string EntPracNo, string PositionID, string TaskID)
        {
            DataContext.SetFiled("TaskID", TaskID);
            DataContext.SetFiled("EntPracNo", EntPracNo);
            DataContext.SetFiled("PositionID", PositionID);
            return Json(Services.All(DataContext, "该员工针对某岗位的已分配情况").
                 Select(o => new
                 {
                     UserID = o.T_StuBatchReg.UserID,
                     StuName = o.T_StuBatchReg.T_Student.StuName,
                     ReviewScore = o.ReviewScore,
                     ReviewComment = o.ReviewComment,
                     LinkID = o.LinkID
                 }).
                 ToList());
        }
        [HttpPost]
        public ActionResult AjaxMarking(string LinkID, float ReviewScore, string ReviewComment)
        {
            var old = Services.Find(LinkID);
            old.ReviewScore = ReviewScore;
            old.ReviewComment = string.IsNullOrEmpty(ReviewComment) ? " " : ReviewComment;
            if (Services.Update(DataContext, old))
            {
                return null;
            }
            else
            {
                return Json(true);
            }
        }

        public ActionResult SelectStudent(string TaskID)
        {
            ViewBag.TaskID = TaskID;
            ViewBag.UserUnit = DataContext.UserUnit;
            return View();
        }
    }
}
