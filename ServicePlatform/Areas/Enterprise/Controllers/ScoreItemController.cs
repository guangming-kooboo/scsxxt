using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Controllers.Base;


namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class ScoreItemController : BaseAccountController
    {
        //
        // GET: /Enterprise/ScoreItem/

       
        public ActionResult GetPositions(string EntPracNo)
        {
            DataContext.SetFiled("EntPracNo", EntPracNo);
            return Json(
                S<T_PracticePosition>().All(DataContext, "企业提供给某个学校的岗位").
                Select(a => new { EntPracNo = a.EntPracNo, PositionID = a.PositionID, PositionName = a.C_Position.PositionName}).
                ToList()
                );
        }


        public ActionResult GetEntItems(string EntPracNo)
        {
            DataContext.SetFiled("EntPracNo", EntPracNo);
            return Json(
               S<T_EntReviewItem>().All(DataContext, "企业实习号关联的评分项").
               Select(a => new { ItemID = a.ItemID, ItemName = a.ItemName, ItemWeight = a.ItemWeight, ItemSequence = a.ItemSequence }).
               ToList()
                );
        }

        public ActionResult Index(string EntPracNo = "-1")
        {
            DataContext.SetFiled("Ent_No", DataContext.UserUnit);
            DataContext.SetFiled("EntPracNo", EntPracNo); 
            return View(S<T_EntReviewItem>().All(DataContext, "企业实习号关联的评分项"));
        }



        public ActionResult Create(string EntPracNo)
        {
            if (string.IsNullOrEmpty(EntPracNo))
                return Alert("请勿盗链！");
      
            ViewBag.Title = "添加评分项";
            return View(new T_EntReviewItem());
        }


        [HttpPost]
        public ActionResult Create(T_EntReviewItem model)
        {
            try
            {
                S<T_EntReviewItem>().Add(DataContext, model);
                return RedirectToAction("Index", new { EntPracNo = model.EntPracNo });
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(string id)
        {
            ViewBag.Title = "编辑评分项";
            return View("Create",S<T_EntReviewItem>().Find(id));
        }


        [HttpPost]
        public ActionResult Edit(string id, T_EntReviewItem model)
        {
            S<T_EntReviewItem>().Update(DataContext, model, "编辑评分项");
            return RedirectToAction("Index", new { EntPracNo = model.EntPracNo });
        }


        public ActionResult AjaxDelete(string id)
        {
            return Json(new { result= S<T_EntReviewItem>().Delete(DataContext, id) });
        }


       
      

    }
}
