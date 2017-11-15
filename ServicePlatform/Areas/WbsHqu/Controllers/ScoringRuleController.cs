using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using ServicePlatform.Areas.WbsHqu.ViewModels;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class ScoringRuleController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<ScoringRule> _repository;

        public ScoringRuleController(IRepository<ScoringRule> repository)
        {
            _repository = repository;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.计分标准", Params = Params, pageIndex = 1, perCount = 10 });
            }

            InitReport("计分标准", "/WbsHqu/ScoringRule/Create?quantifyId=" + Params, Params, pageIndex, perCount, false);
            return View(ScoringRule_M.ToViewModel(Params.UnPackString(';')[1], ""));
        }

        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            var quantifyId = Q("quantifyId");
            InitForm("添加工作量");
            return View(ScoringRule_M.ToViewModel(quantifyId));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(ScoringRule_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var param = viewModel.QuantifyTaskID;
                    viewModel.QuantifyTaskID = viewModel.QuantifyTaskID.UnPackString(';')[0];
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index", new { Params = param });
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
            var param = Q("Params");
            var scoringrule = _repository.Find(id);
            if (scoringrule != null)
            {
                return View(ScoringRule_M.ToViewModel(scoringrule,param));
            }
            else
            {
                return Alert("操作失败!");
            } 
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(ScoringRule_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Update(viewModel.ToModel());
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index", new  {Params=viewModel.param });
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
            var scoringRule = _repository.Find(id);
            if (scoringRule!=null)
            {
                _repository.Delete(id);
                return Alert("操作成功!");
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