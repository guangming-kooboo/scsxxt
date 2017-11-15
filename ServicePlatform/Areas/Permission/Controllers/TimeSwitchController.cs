using ServicePlatform.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyj.Plugs;
using xyj.Lib;
using ServicePlatform.Areas.Permission.Models;
using System.Data.Entity;
using System.Data;
namespace ServicePlatform.Areas.Permission.Controllers
{
    public class TimeSwitchController : BasePermission,ICutPage,IModelNote
    {
        //
        // GET: /Permission/TimeSwitch/

        public ActionResult Index(string ModuleID)
        {
            ViewBag.addLink = "Add?ModuleID=" + ModuleID;
            return View(db.ModuleBatchOpenSetting.Where(o => o.ModuleID == ModuleID).ToList());
        }
        //添加
        [BaseActionFilter]
        public ActionResult Add(string ModuleID)
        {
            ViewBag.Title = "添加定时";
            return View(new T_ModuleBatchOpenSetting() { ModuleID = ModuleID });
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult Add(T_ModuleBatchOpenSetting f)
        {
            db.Entry(f).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Content(PageHelper.Tip("保存成功", "/Permission/TimeSwitch/Index?ModuleID=" + f.ModuleID));
        }
        //编辑
        [BaseActionFilter]
        public ActionResult Edit(string ModuleID,string PracBatchID)
        {
            ViewBag.Title = "编辑定时";
            return View(db.ModuleBatchOpenSetting.Find(ModuleID, PracBatchID));
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult Edit(T_ModuleBatchOpenSetting f)
        {
            db.Entry(f).State =EntityState.Modified;
            db.SaveChanges();
            return Content(PageHelper.Tip("保存成功", "/Permission/TimeSwitch/Index?ModuleID=" + f.ModuleID));
        }
        public ActionResult Delete(string ModuleID, string PracBatchID)
        {
            db.Entry(db.ModuleBatchOpenSetting.Find(ModuleID, PracBatchID)).State = EntityState.Deleted;
            db.SaveChanges();
            return Content(PageHelper.Tip("删除成功", "/Permission/TimeSwitch/Index?ModuleID=" + ModuleID));
        }
    }
}
