using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class QuantifyTaskController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<QuantifyTask> _repository;
        private IWbsService _wbsServices;
        private IWbsProvider _wbsProvider;
        public QuantifyTaskController(IWbsProvider wbsProvider,IWbsService wbsServices,IRepository<QuantifyTask> repository)
        {
            _repository = repository;
            _wbsServices = wbsServices;
            _wbsProvider = wbsProvider;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex=1 , int perCount=10 )
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.定量任务列表", Params = Params, pageIndex = 1, perCount = 10 });
            }

            if (_wbsProvider.HasQuantifyChild(Params))
            {
                InitReport("定量任务列表", "#", "", true, "Qx.Wbs.Hqu");
            }
            else
            {
                InitReport("定量任务列表", "/WbsHqu/QuantifyTask/Create?wbstaskId=" + Params, "", true, "Qx.Wbs.Hqu");
            }
            return View();
        }

        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            InitForm("添加定量任务");
            var WbsTaskID = Q("wbstaskId");
            return View(QuantifyTask_M.ToViewModel(WbsTaskID));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(QuantifyTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_wbsServices.QuantifyTaskWeight(viewModel.ToModel()))
                        return Alert("任务总比重超过 1 ,请重新设置");
                    viewModel.CreateTime=DateTime.Now;
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeight(viewModel.WbsTaskID,viewModel.RelativeWeight);
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index",new {Params=viewModel.WbsTaskID});
                }
                else
                {
                    InitForm("这里填写标题");
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }

        // GET: Area/Controller/Edit/5
        public ActionResult Edit(string id)
        {
            InitForm("编辑定量任务");
            var quantifytask = _repository.Find(id);
            if (quantifytask != null)
            {
                return View(QuantifyTask_M.ToViewModel(quantifytask));
            }
            else
            {
                return Alert("操作失败!");
            } 
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(QuantifyTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_wbsServices.QuantifyTaskWeight(viewModel.ToModel()))
                        return Alert("任务总比重超过 1 ,请重新设置");
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeight(viewModel.WbsTaskID, viewModel.RelativeWeight);
                    _repository.Update(viewModel.ToModel());
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index", new { Params = viewModel.WbsTaskID });
                }
                else
                {
                    InitForm("编辑定量任务");
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
            var quantifyTask = _repository.Find(id);
            if (quantifyTask!=null)
            {
                _repository.Delete(id);
                Alert("操作成功!");
                return Refresh();
            }
            // TODO: 这里编写删除逻辑
            else
            {
                return Alert("操作失败!");
            }
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