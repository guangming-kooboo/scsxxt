using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Controllers.Base;


namespace ServicePlatform.Areas.School.Controllers
{
    public class ScoreItemToTeacherController : BaseAccountController, IDMLController<T_SchoolReviewerTask>
    {
       

        public IDML<T_SchoolReviewerTask> Services { get { return S<T_SchoolReviewerTask>(); } }

        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Alert("请勿盗链！");
            
            ViewBag.ItemID = id;
            ViewBag.UserUnit = DataContext.UserUnit;
            DataContext.SetFiled("ItemID", id);
            ViewBag.NotArragedTeachers = S<T_Faculty>().All(DataContext, "该学校未被安排到该评分项的教师");  
            return View(Services.All(DataContext, "当前批次该评分项已分配情况"));
        }


        public ActionResult Details(string id)
        {
            return View(Services.Find(id));
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(T_SchoolReviewerTask model)
        {
            if (Services.Add(DataContext, model)!=null)
            {
                return RedirectToAction("Index", new { id = model.ItemID });
            }
            else
            {
                return Alert("请重试");
            }
           
        }


        public ActionResult Edit(string id)
        {
            return View(Services.Find(id));
        }


        [HttpPost]
        public ActionResult Edit(string id, T_SchoolReviewerTask model)
        {
            try
            {
                Services.Update(DataContext, model);
                return RedirectToAction("Index", new { id = model.ItemID });
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(string id)
        {
            try
            {
                return Delete(id, new T_SchoolReviewerTask() { ItemID = Request.QueryString["ItemID"] });
            }
            catch (Exception)
            {
                return Alert("请先删除与该教师关联的分配记录！");
              
            }
            
       
        }


        [HttpPost]
        public ActionResult Delete(string id, T_SchoolReviewerTask model)
        {
            Services.Delete(DataContext, id);
            return RedirectToAction("Index", new { id = model.ItemID });
        }
        public ActionResult SelectItem()
        {
            DataContext.SetFiled("TeacherID", DataContext.UserID);
            return View(Services.All(DataContext, "当前批次某教师已分配情况"));
        }
    }
}
