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
    public class QuotaTaskStaffDistributionController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<QuotaTaskStaffDistribution> _repository;
        private IRepository<Staff> _staffrepository;
        private IWbsService _wbsServices;
        public QuotaTaskStaffDistributionController(IRepository<Staff> staffrepository,IWbsService wbsServices,IRepository<QuotaTaskStaffDistribution> repository)
        {
            _staffrepository = staffrepository;
            _repository = repository;
            _wbsServices = wbsServices;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.人员分配", Params = Params, pageIndex = 1, perCount = 10 });
            }

            InitReport("人员分配", "/WbsHqu/QuotaTaskStaffDistribution/Create?quotataskId=" + Params.UnPackString(';')[0], "", pageIndex, perCount);
            string fatherId = _wbsServices.FindFatherQuotaTask(Params.UnPackString(';')[0]);
            return View(QuotaTaskStaffDistribution_M.ToViewModel(fatherId.UnPackString(';')[0], fatherId.UnPackString(';')[1], Params.UnPackString(';')[0]));
        }
        public ActionResult StaffDis(string id)
        {
            InitForm("添加人员分配");
            return View(QuotaTaskStaffDistribution_M.ToViewModel(id, _wbsServices.FindLeafNode(id), _staffrepository.ToSelectItems()));
        }
        [HttpPost]
        public ActionResult StaffDis(QuotaTaskStaffDistribution_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.RelativeWeight = viewModel.RelativeWeight * 0.01;
                    viewModel.QuotaTaskStaffDistributionID = DateTime.Now.Random().ToString();
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeightSub(viewModel.QuotaTaskID, viewModel.RelativeWeight);
                    viewModel.StaffName = _staffrepository.Find(viewModel.StaffID).StaffName;
                    if (!_wbsServices.StaffWeight(viewModel.ToModel()))
                        return Alert("人员总比重超过 1 ,请重新设置相对占比");
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("WbsTaskDis", "WbsTask", new { Params = ";" });
                }
                else
                {
                    InitForm("添加人员分配");
                    viewModel.StaffSelectItems = _staffrepository.ToSelectItems();
                    viewModel.IsCompleteItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = "1",Text = "是"
                    },
                    new SelectListItem()
                    {
                        Value = "0",Text = "否"
                    }
                };
                    viewModel.LeafNodeListItems = _wbsServices.FindLeafNode(viewModel.WbsTaskId);
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }
        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            InitForm("添加人员分配");
            var QuotaTaskID = Q("quotataskId");
            return View(QuotaTaskStaffDistribution_M.ToViewModel(QuotaTaskID.UnPackString(';')[0]));
        }

        // POST: Area/Controller/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(QuotaTaskStaffDistribution_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.RelativeWeight = viewModel.RelativeWeight * 0.01;
                    viewModel.QuotaTaskStaffDistributionID = DateTime.Now.Random().ToString();
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeightSub(viewModel.QuotaTaskID,viewModel.RelativeWeight);
                    if (!_wbsServices.StaffWeight(viewModel.ToModel()))
                        return Alert("人员总比重超过 1 ,请重新设置相对占比");
                    _repository.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index",new {Params=viewModel.QuotaTaskID + ";" });
                }
                else
                {
                    InitForm("这里填写标题");
                    viewModel.IsCompleteItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = "1",Text = "是"
                    },
                    new SelectListItem()
                    {
                        Value = "0",Text = "否"
                    }
                };
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
            //管理员
            InitForm("编辑分配人员");
            var distribution = _repository.Find(id);
            if (distribution != null)
            {
                return View(QuotaTaskStaffDistribution_M.ToViewModel(distribution));
            }
            else
            {
                return Alert("操作失败!");
            } 
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(QuotaTaskStaffDistribution_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.RelativeWeight = viewModel.RelativeWeight * 0.01;
                    viewModel.AbsoluteWeight = _wbsServices.AbsoluteWeightSub(viewModel.QuotaTaskID, viewModel.RelativeWeight);
                    if (!_wbsServices.StaffWeight(viewModel.ToModel()))
                        return Alert("人员总比重超过 1 ,请重新设置相对占比");
                    _repository.Update(viewModel.ToModel());
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index", new { Params = viewModel.QuotaTaskID+";" });
                }
                else
                {
                    InitForm("编辑分配人员");
                    viewModel.IsCompleteItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = "1",Text = "是"
                    },
                    new SelectListItem()
                    {
                        Value = "0",Text = "否"
                    }
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
            var quota = _repository.Find(id);
            if (quota!=null)
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
        public ActionResult MoveUp(string id)
        {
            var quodis = _repository.Find(id);
            if (quodis != null)
            {

                var newsequence = --quodis.StaffSequence;
                var quodisbrother = _wbsServices.DistributionBrother(quodis);
                if (quodisbrother != null)
                {
                    var quotaitem = _repository.Find(quodisbrother);
                    quotaitem.StaffSequence++;
                    _repository.Update(quotaitem);
                }
                _repository.Update(quodis);
                return Refresh();
            }
            else
            {
                return Alert("操作失败!");
            }
        }
        public ActionResult MoveDown(string id)
        {
            var quodis = _repository.Find(id);
            if (quodis != null)
            {

                var newsequence = ++quodis.StaffSequence;
                var quodisbrother = _wbsServices.DistributionBrother(quodis);
                if (quodisbrother != null)
                {
                    var quotaitem = _repository.Find(quodisbrother);
                    quotaitem.StaffSequence--;
                    _repository.Update(quotaitem);
                }
                _repository.Update(quodis);
                return Refresh();
            }
            else
            {
                return Alert("操作失败!");
            }
        }
        
    }
}