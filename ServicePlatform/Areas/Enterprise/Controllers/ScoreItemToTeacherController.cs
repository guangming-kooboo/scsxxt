using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;


namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class ScoreItemToTeacherController : BaseAccountController
    {
       

        public IDML<T_EntReviewerTask> Services
        { get { return S<T_EntReviewerTask>(); } }

        public ActionResult Index(string ItemID)
        {
            if (string.IsNullOrEmpty(ItemID))
                return Alert("请勿盗链！");

            DataContext.SetFiled("Ent_No", DataContext.UserUnit);
            DataContext.SetFiled("ItemID", ItemID);
            var Arranged = S<T_EntReviewerTask>().All(DataContext, "该评分项已分配情况");
            ViewBag.ItemID = ItemID;
            ViewBag.NotArragedEmployees = S<T_Employee>().All(DataContext, "该企业所有员工").
                Except(Arranged.Select(a=>a.T_Employee),CommonExtendMethods.Equality<T_Employee>.CreateComparer(b=>b.UserID)).ToList();
            return View(Arranged);
        }



        [HttpPost]
        public ActionResult Create(T_EntReviewerTask model)
        {
            if (Services.Add(DataContext, model)!=null)
            {
                return RedirectToAction("Index", new { ItemID = model.ItemID });
            }
            else
            {
                return Alert("请重试");
            }
           
        }


        public ActionResult Delete(string id)
        {

            try
            {
                return Delete(id, new T_EntReviewerTask() { ItemID = Request.QueryString["ItemID"] });
            }
            catch (Exception)
            {
                return Alert("请先删除与该员工关联的分配记录！");
            }
            
       
        }


        [HttpPost]
        public ActionResult Delete(string id, T_EntReviewerTask model)
        {
            Services.Delete(DataContext, id);
            return RedirectToAction("Index", new { ItemID = model.ItemID });
        }



        public ActionResult GetEntItems(string EntPracNo)
        {
            DataContext.SetFiled("EntPracNo", EntPracNo);
            DataContext.SetFiled("EmployeeID", DataContext.UserID);
            return Json(
               Services.All(DataContext, "该员工负责某批次的评分项").
               Select(a => new { TaskID = a.TaskID, ItemID = a.ItemID, ItemName = a.T_EntReviewItem.ItemName, ItemWeight = a.T_EntReviewItem.ItemWeight, ItemSequence = a.T_EntReviewItem.ItemSequence }).
               ToList()
                );
        }


        public ActionResult SelectItem(string EntPracNo)
        {
            return View();
        }
    }
}
