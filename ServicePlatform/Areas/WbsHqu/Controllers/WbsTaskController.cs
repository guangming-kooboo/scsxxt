using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class WbsTaskController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<WbsTask> _repository;
        private IWbsService _wbsServices;
        private IWbsProvider _wbsProvider;
        public WbsTaskController(IWbsProvider wbsProvider,IRepository<WbsTask> repository, IWbsService wbsServices)
        {
            _repository = repository;
            _wbsServices = wbsServices;
            _wbsProvider = wbsProvider;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex=1 , int perCount=10 )
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.工作量列表", Params, pageIndex = 1, perCount = 10 });
            }

            OverWriteParam(DataContext.UserID+";"+Params);

            InitReport("工作量列表", "/WbsHqu/WbsTask/Create","", pageIndex, perCount);
            return View();
        }

        public ActionResult WbsTask(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("WbsTask", new { reportId = "Qx.Wbs.Hqu.工作量报表", Params, pageIndex = 1, perCount = 10 });
            }

            OverWriteParam(DataContext.UserID + ";" + Params);
            InitReport("工作量报表", "#", "", pageIndex, perCount);
            return View();
        }
        public ActionResult WbsTaskDis(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("WbsTaskDis", new { reportId = "Qx.Wbs.Hqu.工作量分配",  Params=Params, pageIndex = 1, perCount = 10 });
            }
            OverWriteParam(DataContext.UserID+";"+Params);
            InitReport("工作量分配", "#", "", pageIndex, perCount);
            return View();
        }
        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            InitForm("添加工作量");
            var ownerid = DataContext.UserID;
            return View(WbsTask_M.ToViewModel(ownerid));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(WbsTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.QuantifyTaskWeight *= 0.01;
                    viewModel.MotorTaskWeight *= 0.01;
                    viewModel.QuotaTaskWeight *= 0.01;
                    double weight = viewModel.QuotaTaskWeight + viewModel.MotorTaskWeight + viewModel.QuantifyTaskWeight;
                    if (weight != 1)
                    {
                        return Alert("任务总比重不为 1 ,请重新设置");
                    }
                    viewModel.WbsTaskID = DateTime.Now.Random().ToString();
                    viewModel.CreateTime=DateTime.Now;
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index");
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
            InitForm("编辑工作量");
            var wbstask = _repository.Find(id);
            if (wbstask!=null)
            {
                return View(WbsTask_M.ToViewModel(wbstask));
            }
            else
            {
                return Alert("操作失败!");
            } 
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(WbsTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.QuantifyTaskWeight *= 0.01;
                    viewModel.MotorTaskWeight *= 0.01;
                    viewModel.QuotaTaskWeight *= 0.01;
                    double weight = viewModel.QuotaTaskWeight + viewModel.MotorTaskWeight + viewModel.QuantifyTaskWeight;
                    if (weight!=1)
                    {
                        return Alert("任务总比重不为 1 ,请重新设置");
                    }
                    _repository.Update(viewModel.ToModel());
                    if (!_wbsServices.UpdateAbsoluteWeight(viewModel.WbsTaskID))
                        Alert("子节点绝对占比更新失败!");
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index");
                }
                else
                {
                    InitForm("编辑产品品牌");
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
            if (_wbsProvider.HasQuantifyChild(id) || _wbsProvider.HasQuotaChild(id))
            {
                return Alert("下级有子任务，请先删除子任务，再删除本级任务");
            }
            else
            {
                var wbstask = _repository.Find(id);
                if (wbstask != null)
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
            InitForm("这里填写标题");
            // TODO: 这里编写详情逻辑
            return View();
        }
        public ActionResult CopyWbs(string id)
        {
            if (id.HasValue())
            {
                if (_wbsProvider.CopyWbs(id))
                {
                    Alert("操作成功！");
                    return Refresh();
                }
                else
                {
                    return Alert("操作失败！");
                }
            }
            else
            {
                return Alert("操作失败！");
            }
        }

    }
}