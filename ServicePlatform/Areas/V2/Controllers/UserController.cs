using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Scsxxt.Interfaces;
using ServicePlatform.Areas.V2.Controllers;
using ServicePlatform.Areas.V2.ViewModels;
using ServicePlatform.Controllers.Base;


namespace ServicePlatform.Areas.V2.Controllers
{
    public class UserController : BaseV2Controller, ICrud<User_M>
    {
        // TODO: 把<T> 更换为数据库model类型如 <Car>
        private IRepository<T_User> _repository;
      

        public UserController(IRepository<T_User> repository)
        {
            _repository = repository;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("Index", new { reportId = "Qx.Contents.内容列", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("内容列", "/Contents/CCD/Create?CTD_ID="+Params, pageIndex, perCount);
            return View(User_M.ToViewModel() );
        }

        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            var CTD_ID = Q("CTD_ID");
            InitForm("添加内容列");
            CTD_ID = CTD_ID.UnPackString(';')[0];
            return View(User_M.ToViewModel());
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(User_M viewModel)
        {
            try
            {    
                if (ModelState.IsValid)
                {
                    _repository.Add(viewModel.ToModel());
                    return RedirectToAction("Index",new {Params="参数" });
                }
                else
                {
                    InitForm("添加内容列");
                    return View(viewModel);
                }
                
            }
            catch(Exception ex)
            {
                return Alert("请联系开发人员\n"+ex.Message);
            }
        }

        // GET: Area/Controller/Edit/5
        public ActionResult Edit(string id)
        {
            InitForm("编辑内容列");
            return View(User_M.ToViewModel());
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(User_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Update(viewModel.ToModel());
                    return RedirectToAction("Index", new { Params = "参数"});
                }
                else
                {
                    InitForm("编辑内容列");
                    return View(viewModel);
                }
             
            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }

        // GET: Area/Controller/Delete/5
        public ActionResult Delete(string id)
        {
            if (id.HasValue())
            {
                _repository.Delete(id);
                return Alert("删除成功!");
            }
            else
                return Alert("操作失败!");
        }


        // GET: Area/Controller/Details/5
        public ActionResult Details(string id)
        {
            InitForm("这里填写标题");
            // TODO: 这里编写详情逻辑
            return View();
        }

    }
}