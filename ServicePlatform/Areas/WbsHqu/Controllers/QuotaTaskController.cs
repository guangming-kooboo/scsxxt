using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class QuotaTaskController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<QuotaTask> _repository;
        private IWbsService _wbsServices;
        private IWbsProvider _wbsProvider;
        public QuotaTaskController(IWbsProvider wbsProvider,IWbsService wbsServices, IRepository<QuotaTask> repository)
        {
            _repository = repository;
            _wbsServices = wbsServices;
            _wbsProvider = wbsProvider;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.定额任务父列表", Params = Params, pageIndex = 1, perCount = 10 });
            }

            InitReport("定额任务列表", "/WbsHqu/QuotaTask/Create?wbstaskId=" + Params, "", true, "Qx.Wbs.Hqu");
            return View();
        }
        public ActionResult SubTask(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            var WbsTaskId = Q("WbsTaskId");
            if (!reportId.HasValue())
            {
                return RedirectToAction("SubTask", new { reportId = "Qx.Wbs.Hqu.定额任务子列表", Params = Params, WbsTaskId = WbsTaskId, pageIndex = 1, perCount = 10 });
            }
            var fathers = _wbsServices.FindFather(Params);
            InitReport("定额任务列表", "/WbsHqu/QuotaTask/CreateSub?fatherid=" + Params + "&WbsTaskId=" + WbsTaskId, "&WbsTaskId=" + WbsTaskId, pageIndex, perCount, false);
            return View(QuotaTask_M.ToViewModel(fathers));
        }

        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            InitForm("添加定额任务");
            var FatherID = "0";
            var WbsTaskID = Q("wbstaskId");
            return View(QuotaTask_M.ToViewModel(FatherID, WbsTaskID));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(QuotaTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.RelativeWeight *= 0.01;
                    if (!_wbsServices.QuotaTaskWeight(viewModel.ToModel()))
                        return Alert("任务总比重超过 1 ,请重新设置");
                    viewModel.CreateTime = DateTime.Now;
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeight(viewModel.WbsTaskID, viewModel.RelativeWeight);
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index", new { Params = Q("wbstaskId") });
                }
                else
                {
                    InitForm("添加定额任务");
                    viewModel.IsLeafNodeItem = new List<SelectListItem>()
                    {
                    new SelectListItem()
                    {Value = "1",Text = "是"},
                    new SelectListItem()
                    {Value = "0",Text = "否"}
                    };
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }

        public ActionResult CreateSub()
        {
            InitForm("添加定额任务");
            var FatherID = Q("fatherid");
            var WbsTaskID = Q("WbsTaskId");
            return View("Create", QuotaTask_M.ToViewModel(FatherID, WbsTaskID));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult CreateSub(QuotaTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.RelativeWeight *= 0.01;
                    if (!_wbsServices.QuotaTaskWeight(viewModel.ToModel()))
                        return Alert("任务总比重超过 1 ,请重新设置");
                    viewModel.QuotaTaskID = DateTime.Now.Random().ToString();
                    viewModel.CreateTime = DateTime.Now;
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeightSub(viewModel.FatherID, viewModel.RelativeWeight);
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("SubTask", new { Params = Q("fatherid"), WbsTaskId = Q("WbsTaskId") });
                }
                else
                {
                    InitForm("添加定额任务");
                    viewModel.IsLeafNodeItem = new List<SelectListItem>()
                    {
                    new SelectListItem()
                    {Value = "1",Text = "是"},
                    new SelectListItem()
                    {Value = "0",Text = "否"}
                    };
                    return View("Create", viewModel);
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
            InitForm("编辑定额任务");
            var quotatask = _repository.Find(id);
            if (quotatask != null)
            {
                return View(QuotaTask_M.ToViewModel(quotatask));
            }
            else
            {
                return Alert("操作失败!");
            }
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(QuotaTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.RelativeWeight *= 0.01;
                    if (!_wbsServices.QuotaTaskWeight(viewModel.ToModel()))
                        return Alert("任务总比重超过 1 ,请重新设置");
                    if (viewModel.FatherID == "0")
                    {
                        viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeight(viewModel.WbsTaskID, viewModel.RelativeWeight);
                    }
                    else
                    {
                        viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeightSub(viewModel.FatherID, viewModel.RelativeWeight);
                    }
                    _repository.Update(viewModel.ToModel());
                    if (!_wbsServices.UpdateSubAbsoluteWeight(viewModel.QuotaTaskID))
                        Alert("子节点绝对占比更新失败!");
                    // TODO: 这里编写更新逻辑
                    if (viewModel.FatherID == "0")
                    {
                        return RedirectToAction("Index", new { Params = viewModel.WbsTaskID });
                    }
                    else
                    {
                        return RedirectToAction("SubTask", new { Params = viewModel.FatherID, WbsTaskId = viewModel.WbsTaskID });
                    }
                }
                else
                {
                    InitForm("添加定额任务");
                    viewModel.IsLeafNodeItem = new List<SelectListItem>()
                    {
                    new SelectListItem()
                    {Value = "1",Text = "是"},
                    new SelectListItem()
                    {Value = "0",Text = "否"}
                    };
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
            if (_wbsProvider.QuotaHasChild(id))
            {
                return Alert("下级有子任务，请先删除子任务，再删除本级任务");
            }
            else
            {
                var quota = _repository.Find(id);
                if (quota != null)
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
        }

        // GET: Area/Controller/Details/5
        public ActionResult Details(string id)
        {
            //InitForm("这里填写标题");
            // TODO: 这里编写详情逻辑
            string url = Request.Url.ToString();

            return Alert(url);
        }
        public ActionResult MoveUp(string id)
        {
            var quota = _repository.Find(id);
            if (quota != null)
            {
                if (quota.TaskSequence == 0)
                    return Alert("此顺序已为最先,请检查其他数据的顺序!");
                else
                {
                    var newsequence = --quota.TaskSequence;
                    var quotabrother = _wbsServices.quotaBrother(quota);
                    if (quotabrother != null)
                    {
                        var quotaitem = _repository.Find(quotabrother);
                        quotaitem.TaskSequence++;
                        _repository.Update(quotaitem);
                    }
                    _repository.Update(quota);
                }
                return Refresh();
            }
            else
            {
                return Alert("操作失败!");
            }
        }

        public ActionResult MoveDown(string id)
        {
            var quota = _repository.Find(id);
            if (quota != null)
            {

                var newsequence = ++quota.TaskSequence;
                var quotabrother = _wbsServices.quotaBrother(quota);
                if (quotabrother != null)
                {
                    var quotaitem = _repository.Find(quotabrother);
                    quotaitem.TaskSequence--;
                    _repository.Update(quotaitem);
                }
                _repository.Update(quota);
                return Refresh();
            }
            else
            {
                return Alert("操作失败!");
            }
        }
    }
}