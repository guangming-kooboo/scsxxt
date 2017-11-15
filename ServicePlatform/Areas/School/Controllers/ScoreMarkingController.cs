using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Newtonsoft.Json;
using ServicePlatform.Controllers.Base;
namespace ServicePlatform.Areas.School.Controllers
{
    public class ScoreMarkingController : BaseAccountController, IDMLController<T_SchoolStudentReviewLink>
    {
       

        public IDML<T_SchoolStudentReviewLink> Services { get { return S<T_SchoolStudentReviewLink>(); } }
        [HttpPost]
        public ActionResult GetStudents(string ClassId, string TaskID)
        {
             DataContext.SetFiled("TaskID", TaskID);
             DataContext.SetFiled("ClassId", ClassId);
            return Json(S<T_Student>().All(DataContext, "该学校某班级未被分配到该评分项的所有学生").Select(o=>
                new { UserID = o.UserID, StuName=o.StuName }));
        }
         [HttpPost]
        public ActionResult GetArrangedList(string TaskID)
        {
             DataContext.SetFiled("TaskID", TaskID);
             return Content(JsonConvert.SerializeObject(
                 Services.All(DataContext, "当前批次该评分项已分配情况").
                 Select(o=>new
                 {
                     UserID=o.T_StuBatchReg.UserID,
                     StuName = o.T_StuBatchReg.T_Student.StuName,
                     LinkID = o.LinkID
                 })
                 .ToList()));
        }
        
        [HttpPost]
        public ActionResult AjaxCreate(T_SchoolStudentReviewLink model)
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
        public ActionResult Index(string id)
        {
            DataContext.SetFiled("TaskID", id);
            ViewBag.UserUnit = DataContext.UserUnit;
            ViewBag.TaskID = id;
            //ViewBag.NotArragedStudents = S<T_Student>().All(DataContext, "该学校未被分配到该评分项的所有学生");
            return View();
        }


        public ActionResult Details(string id)
        {
            return View(Services.Find(id));
        }


        public ActionResult Create()
        {
           
            throw  new NotImplementedException();
        }


        [HttpPost]
        public ActionResult Create(T_SchoolStudentReviewLink model)
        {
            Services.Add(DataContext, model);
            return RedirectToAction("Index", new { id = model.TaskID });
        }


        public ActionResult Edit(string id)
        {
            return View(Services.Find(id));
        }


        [HttpPost]
        public ActionResult Edit(string id, T_SchoolStudentReviewLink model)
        {
            throw new NotImplementedException();
        }


        public ActionResult Delete(string id)
        {
            return Delete(id, new T_SchoolStudentReviewLink() { TaskID = Request.QueryString["TaskID"] });
       
        }


        [HttpPost]
        public ActionResult Delete(string id, T_SchoolStudentReviewLink model)
        {
            Services.Delete(DataContext, id);
            return RedirectToAction("Index", new { id = model.TaskID });
        }
    }
}
