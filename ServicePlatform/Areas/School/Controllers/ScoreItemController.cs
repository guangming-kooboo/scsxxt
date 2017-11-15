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
    public class ScoreItemController : BaseAccountController, IDMLController<T_SchoolReviewItem>
    {
        //
        // GET: /School/ScoreItem/

        public IDML<T_SchoolReviewItem> Services { get { return S<T_SchoolReviewItem>(); } }

        public ActionResult Index(string id)
        {   
            return View(Services.All(o=>o.PracBatchID==DataContext.PracBatchID));
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
        public ActionResult Create(T_SchoolReviewItem model)
        {
            try
            {
                Services.Add(DataContext, model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(string id)
        {
            return View(Services.Find(id));
        }


        [HttpPost]
        public ActionResult Edit(string id, T_SchoolReviewItem model)
        {
            try
            {
                Services.Update(DataContext, model);
                return RedirectToAction("Index");
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
                Services.Delete(DataContext, id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return Alert("请先删除与该评分项关联的分配记录！");
            }
           
        }


        [HttpPost]
        public ActionResult Delete(string id, T_SchoolReviewItem model)
        {
            throw new NotImplementedException();
        }
      

    }
}
