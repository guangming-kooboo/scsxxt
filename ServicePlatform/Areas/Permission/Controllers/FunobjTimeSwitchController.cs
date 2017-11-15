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
using EntityState = System.Data.Entity.EntityState;

namespace ServicePlatform.Areas.Permission.Controllers
{
    public class FunobjTimeSwitchController : BasePermission, ICutPage, IModelNote
    {
        //
        // GET: /Permission/FunobjTimeSwitch/

        public ActionResult Index(string FuncObjID)
        {
            ViewBag.addLink = "Add?FuncObjID=" + FuncObjID;
            return View(db.FuncBatchOpenSetting.Where(o => o.FuncObjID == FuncObjID).ToList());
        }
        //添加
        [BaseActionFilter]
        public ActionResult Add(string FuncObjID)
        {
            ViewBag.Title = "添加定时";
            return View(new T_FuncBatchOpenSetting() { FuncObjID = FuncObjID });
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult Add(T_FuncBatchOpenSetting f)
        {
            db.Entry(f).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Content(PageHelper.Tip("保存成功", "/Permission/FunobjTimeSwitch/Index?FuncObjID=" + f.FuncObjID));
        }
        //编辑
        [BaseActionFilter]
        public ActionResult Edit(string FuncObjID,string PracBatchID)
        {
            ViewBag.Title = "编辑定时";
            return View(db.FuncBatchOpenSetting.Find(FuncObjID, PracBatchID));
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult Edit(T_FuncBatchOpenSetting f)
        {
            db.Entry(f).State =EntityState.Modified;
            db.SaveChanges();
            return Content(PageHelper.Tip("保存成功", "/Permission/FunobjTimeSwitch/Index?FuncObjID=" + f.FuncObjID));
        }
        public ActionResult Delete(string FuncObjID, string PracBatchID)
        {
            db.Entry(db.FuncBatchOpenSetting.Find(FuncObjID, PracBatchID)).State = EntityState.Deleted;
            db.SaveChanges();
            return Content(PageHelper.Tip("删除成功", "/Permission/FunobjTimeSwitch/Index?FuncObjID=" + FuncObjID));
        }
    }
}
