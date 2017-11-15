using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using ServicePlatform.Areas.WbsHqu.ViewModels;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class StaffWorkSumaryController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<WbsTask> _repository;
        private IRepository<QuotaTaskStaffDistribution> _quotarepository;
        private IRepository<QuantifyTaskCompletion> _quantifyrepository;

        public StaffWorkSumaryController(IRepository<QuantifyTaskCompletion> quantifyrepository,IRepository<WbsTask> repository, IRepository<QuotaTaskStaffDistribution> quotarepository)
        {
            _repository = repository;
            _quotarepository = quotarepository;
            _quantifyrepository = quantifyrepository;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.人员报表", Params = Params, pageIndex = 1, perCount = 10 });
            }

            InitReport("人员报表", "#", "&WbsTaskID=" + Params, pageIndex, perCount);
            return View(StaffWorkSumary_M.ToViewModel(Params.UnPackString(';')[0]));
        }

        public ActionResult CompeleteDetail(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            Params = Params + ";" + Q("WbsTaskID");
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.人员报表详情", Params = Params, pageIndex = 1, perCount = 10 });
            }

            InitReport("完成详情", "#", "", pageIndex, perCount);
            return View();
        }
        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            InitForm("");
            return View();
        }
        // POST: Area/Controller/Create
        [HttpPost]
        public ActionResult Create(WbsTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
            InitForm("");
            return View();
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        public ActionResult Edit(WbsTask_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
            var wbstask = _repository.Find(id);
            if (wbstask != null)
            {
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

        public ActionResult ProofMaterial(string id)
        {
            var content = "未找到证明材料！";
            var note = "";
            InitForm("证明材料");
            //备注 和 图片
            var quota = _quotarepository.Find(id);
            if (quota == null)
            {
                //只有图片
                var quantify = _quantifyrepository.Find(id);
                if (quantify != null)
                {
                    content = quantify.Certificate;
                    note = quantify.CompleteNote;
                }
            }
            else
            {
                content = quota.Certificate;
            }
            return View(ProofMaterial_M.ToViewModel(content,note));
        }
    }
}