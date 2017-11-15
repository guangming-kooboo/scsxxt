
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
    public class EmployeeController : BaseAccountController
    {
        public IDML<T_Employee> Services
        {
            get
            {
                return  S<T_Employee>();
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(T_Employee model)
        {
            DataContext.SetFiled("Ent_No", DataContext.UserUnit);//企业号  
            DataContext.SetFiled("RoleID", Request.Form["UserType"]);//角色编号
            Services.Add(DataContext, model);
            return RedirectToAction("Index");
            // return string.IsNullOrEmpty(Services.Add(DataContext, model)) ? Alert("员工添加成功", "/Enterprise/Home/UserListShow") : Alert("员工注册失败,已存在该员工号", -1);
        }

        public ActionResult Delete(string id)
        {
            Services.Delete(DataContext, id);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            return View(Services.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(string id, T_Employee model)
        {
            Services.Update(DataContext, model, "编辑员工资料");
            return RedirectToAction("Index");
        }
        
        public ActionResult Index(string id)
        {
            DataContext.SetFiled("Ent_No", DataContext.UserUnit);//企业号  
            return View(Services.All(DataContext,"该企业所有员工"));
        }
    }
}
