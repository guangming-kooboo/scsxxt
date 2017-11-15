using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using ServicePlatform.Areas.WbsHqu.ViewModels;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class QuantifyTaskCompletionController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<QuantifyTaskCompletion> _repository;
        private IRepository<ScoringRule> _scorepository;
        private IRepository<Staff> _staff;

        public QuantifyTaskCompletionController(IRepository<Staff> staff,IRepository<QuantifyTaskCompletion> repository, IRepository<ScoringRule> scorepository)
        {
            _staff = staff;
            _repository = repository;
            _scorepository = scorepository;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex=1 , int perCount=10 )
        {
            var flag = Q("flag");
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.完成情况", flag = flag, Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("完成情况", "/WbsHqu/QuantifyTaskCompletion/Create?quantifytaskId=" + Params, "&quantifytaskId=" + Params, pageIndex, perCount, false);
            return View(QuantifyTaskCompletion_M.ToViewModel(Params.UnPackString(';')[1], flag));
        }
        public ActionResult Completion(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            var quantifytaskId = Q("quantifytaskId");
            if (!reportId.HasValue())
            {

                return RedirectToAction("Completion", new { reportId = "Qx.Wbs.Hqu.完成详情", quantifytaskId= quantifytaskId,Params = Params+";"+quantifytaskId.UnPackString(';')[0], pageIndex = 1, perCount = 10 });
            }
            var staffId = Params.UnPackString(';')[0];
           
            InitReport("完成详情", "/WbsHqu/QuantifyTaskCompletion/Create2?staffId=" + staffId+"&quantifytaskId=" + quantifytaskId,"", pageIndex, perCount, false);
            return View(QuantifyTaskCompletion_M.ToViewModel(quantifytaskId));
        }
        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            var quantifytaskId = Q("quantifytaskId");//定量任务ID加工作量ID
            InitForm("添加完成情况");
            return View(QuantifyTaskCompletion_M.ToViewModel(quantifytaskId, _scorepository.ToSelectItems(quantifytaskId.UnPackString(';')[0])));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(QuantifyTaskCompletion_M viewModel)
        {
            //try
            //{
                if (ModelState.IsValid)
                {

                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index", new { Params=viewModel.BackParams});
                }
                else
                {
                    InitForm("添加完成情况");
                    viewModel.ScoringRuleItem = _scorepository.ToSelectItems(Q("Params"));
                    return View(viewModel);
                }

            //}
            //catch (Exception ex)
            //{
            //    return Alert("请联系开发人员\n" + ex.Message);
            //}
        }
        public ActionResult Create2()
        {
            var staffId=Q("staffId");
            var quantifytaskId = Q("quantifytaskId");
            var staffname = _staff.Find(staffId);      
           //name 要修改
            InitForm("添加完成情况");
            return View(QuantifyTaskCompletion_M.ToViewModel(quantifytaskId, staffId, staffname.StaffName, _scorepository.ToSelectItems(quantifytaskId.UnPackString(';')[0])));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create2(QuantifyTaskCompletion_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Completion", new { Params =viewModel.StaffID, quantifytaskId = viewModel.BackParams });
                }
                else
                {
                    InitForm("添加完成情况");
                    viewModel.ScoringRuleItem = _scorepository.ToSelectItems(Q("quantifytaskId"));
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
                return View();
            }
            else
            {
                return Alert("操作失败!");
            } 
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(QuantifyTaskCompletion_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Update(viewModel.ToModel());
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index", new { Params=viewModel.QuantifyTaskID});
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
            var QuantifyTaskCompletion = _repository.Find(id);
            if (QuantifyTaskCompletion!=null)
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