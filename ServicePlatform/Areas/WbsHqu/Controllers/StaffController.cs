using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using ServicePlatform.Areas.WbsHqu.ViewModels;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class StaffController : BaseWbsHquController
    {
        //
        // GET: /WbsHqu/Staff/
        private IRepository<Staff> _staff;

        public StaffController(IRepository<Staff> staff)
        {
            _staff = staff;
        }
      
        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            //用户界面
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index",new {reportId = "Qx.Wbs.Hqu.人员表", Params = Params, pageIndex = 1, perCount = 10});
            }
            InitReport("人员表", "/WbsHqu/Staff/Create","");
            return View();
        }

        public ActionResult Create()
        {
            InitForm("添加人员");
            return View();
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(Staff_M viewModel)
        {
            //try
            //{
            if (ModelState.IsValid)
            {

                _staff.Add(viewModel.ToModel());
                // TODO: 这里编写添加逻辑
                return RedirectToAction("Index");
            }
            else
            {
                InitForm("添加人员");
                return View(viewModel);
            }
        }
        public ActionResult Edit(string id)
        {
            InitForm("编辑人员");
            var staff = _staff.Find(id);
            return View(Staff_M.ToViewModel(staff));
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(Staff_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _staff.Update(viewModel.ToModel());
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index");
                }
                else
                {
                    InitForm("编辑人员");
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }
        public ActionResult Delete(string id)
        {
            var staff = _staff.Find(id);
            if (staff != null)
            {
                _staff.Delete(id);
                return Alert("操作成功!");
            }
            // TODO: 这里编写删除逻辑
            else
            {
                return Alert("操作失败!");
            }
        }
    }
}
