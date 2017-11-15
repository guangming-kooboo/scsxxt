using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;
using System.Collections.Generic;
using Qx.Wbs.Hqu.Models;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class CheckRecordController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<QuotaTaskStaffDistribution> _quotataskdis;
        private IRepository<QuotaTask> _quotatask;
        private IWbsService _wbsServices;
        private IRepository<ScoringRule> _scoringrule;
        private IRepository<QuantifyTaskCompletion> _quantifytaskcom;
        private IRepository<QuantifyTask> _quantifytask;
        private IRepository<CheckRecord> _checkrecord;
        public CheckRecordController(IRepository<CheckRecord> checkrecord,IRepository<QuantifyTask> quantifytask, IRepository<QuantifyTaskCompletion> quantifytaskcom, IRepository<ScoringRule> scoringrule, IRepository<QuotaTask> quotatask, IRepository<QuotaTaskStaffDistribution> quotataskdis, IWbsService wbsServices)
        {
            _checkrecord = checkrecord;
            _quantifytask = quantifytask;
            _quantifytaskcom = quantifytaskcom;
            _scoringrule = scoringrule;
            _quotataskdis = quotataskdis;
            _quotatask = quotatask;
            _wbsServices = wbsServices;
        }

        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            //用户界面
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "Qx.Wbs.Hqu.审核记录", Params = Params, pageIndex = 1, perCount = 10 });
            }
            var datasource = _wbsServices.CheckRecord(DataContext.UserID, true);
            InitReport(datasource, "审核记录表", "/WbsHqu/CheckRecord/Task", "", true);
            return View();
        }
        public ActionResult CheckRecord(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            //管理员界面
            if (!reportId.HasValue())
            {

                return RedirectToAction("CheckRecord", new { reportId = "Qx.Wbs.Hqu.管理员审核记录", Params = Params, pageIndex = 1, perCount = 10 });
            }
            var datasource = _wbsServices.CheckRecord(DataContext.UserID, false);
            InitReport(datasource, "审核记录表", "#", "", true);
            return View();
        }

        // GET: Area/Controller/Create
        public ActionResult Create()
        {
            InitForm("申请审核");
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
                    // TODO: 这里编写添加逻辑
                    return RedirectToAction("Index", new { Params = DataContext.UserID });
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

        public ActionResult Check(string taskid,string tasktype,string mytaskid="")
        {
            if(tasktype=="0")
            {
                return RedirectToAction("QuotaCheck",new { quotataskId=mytaskid });
            }
            else
            {
                var quantify = _wbsServices.GetQuantifyTask(taskid);
                return RedirectToAction("QuantifyCheck", new { quantifyId = quantify.QuantifyTaskID });
            }

        }

        public ActionResult QuotaCheck(string quotataskId)
        {
            //用户提交定额任务审核
            InitForm("我的定额任务");
            var quotadis = _wbsServices.GetQuotadis(quotataskId);
            return View(QuotaTaskStaffDistribution_M.ToViewModel(quotadis));

        }
        [HttpPost]
        public ActionResult QuotaCheck(QuotaTaskStaffDistribution_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var quotatask = _quotatask.Find(viewModel.QuotaTaskID);
                    viewModel.IsComplete = 2;
                    _quotataskdis.Update(viewModel.ToModel());
                    _wbsServices.CreateCheckRecord(viewModel.QuotaTaskStaffDistributionID, quotatask.TaskName, quotatask.WbsTaskID, 0, DataContext.UserID);
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index");
                }
                else
                {
                    InitForm("");
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }
        public ActionResult QuantifyCheck(string quantifyId)
        {
            InitForm("我的定量任务");
            return View(QuantifyTaskCompletion_M.ToViewModel(quantifyId, DataContext.UserID, DataContext.UserID.UnPackString('-')[1], _scoringrule.ToSelectItems(quantifyId)));
        }
        [HttpPost]
        public ActionResult QuantifyCheck(QuantifyTaskCompletion_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var quantifytask = _quantifytask.Find(viewModel.QuantifyTaskID);
                    var taskname = _scoringrule.Find(viewModel.ScoringRuleID).FormName;
                    _quantifytaskcom.Add(viewModel.ToModel());
                    _wbsServices.CreateCheckRecord(viewModel.QuantifyTaskCompletionID, taskname, quantifytask.WbsTaskID, 1, DataContext.UserID);
                    // TODO: 这里编写更新逻辑
                    return RedirectToAction("Index");
                }
                else
                {
                    InitForm("");
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
                    return RedirectToAction("Index", new { Params = DataContext.UserID });
                }
                else
                {
                    InitForm("");
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
            return Alert("操作失败!");
        }

        // GET: Area/Controller/Details/5
        public ActionResult Details(string id)
        {
            InitForm("这里填写标题");
            // TODO: 这里编写详情逻辑
            return View();
        }
        public ActionResult GetTasks()
        {
           var tasks=_wbsServices.GetTasks(DataContext.UserUnit);
            List<Task> model = new List<Task>();
            foreach (var task in tasks)
            {
                model.Add(new Task()
                {
                    Id = task.WbsTaskID,
                    Name = task.TaskName
                });
            }
            return Json(new
            {    
                model
            },JsonRequestBehavior.AllowGet
            );
        }
        public ActionResult GetMyTask(string type,string wbsid)
        {
            
           var model= _wbsServices.GetMyTask(type, wbsid, DataContext.UserID);
            return Json(new
            {
                model
            }, JsonRequestBehavior.AllowGet
);
        }
        public ActionResult GetScoring(string wbsId)
        {
            //WbsHqu/CheckRecord/GetScoring?wbsId=
            //获得积分标准
           
            var quantifytask = _wbsServices.GetQuantifyTask(wbsId);
            if (quantifytask == null)
            {
                return null;
            }
            else
            {
                string quantifytaskId = quantifytask.QuantifyTaskID;
                var model = _scoringrule.ToSelectItems(quantifytaskId);
                return Json(new
                {
                    model
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Task()
        {
            return View();
        }

        public ActionResult Distribution()
        {
            return View();
        }

        public ActionResult Distribute(string wbsId, string tasktype)
        {
            if (tasktype == "0")
            {
                return RedirectToAction("StaffDis", "QuotaTaskStaffDistribution",new { id =wbsId});
            }
            else if (tasktype == "1")
            {
                var quantifyId = _wbsServices.GetQuantifyTask(wbsId).QuantifyTaskID;
                if (quantifyId.HasValue())
                {
                    return RedirectToAction("Index", "QuantifyTaskCompletion", new {Params = quantifyId + ";" + wbsId});
                }
                else
                {
                    Alert("该工作量未设定量工作！");
                    return RedirectToAction("Distribution");
                }
            }
            else
            {
                Alert("操作失败！");
                return RedirectToAction("Distribution");
            }
        }

        public ActionResult CheckAccess(string checkrecordId)
        {
            InitForm("审核通过");
            //审核通过
            var record = _checkrecord.Find(checkrecordId);
            if (record != null)
            {
                return View(CheckRecord_M.ToViewModel(record));
            }
            else
                return Alert("操作失败");
        }
        [HttpPost]
        public ActionResult CheckAccess(CheckRecord_M viewModel)
        {
            try
            {
                if (ModelState.IsValid & viewModel.TaskType==0)
                {
                    viewModel.Auditor = DataContext.UserID;
                    viewModel.State = 1;
                    _checkrecord.Update(viewModel.ToModel());
                    var quotatask = _quotataskdis.Find(viewModel.FinishID);
                    quotatask.IsComplete = 1;
                    _quotataskdis.Update(quotatask);
                    return RedirectToAction("CheckRecord");
                }
                else if(ModelState.IsValid & viewModel.TaskType==1)
                {
                    viewModel.Auditor = DataContext.UserID;
                    viewModel.State = 1;
                    _checkrecord.Update(viewModel.ToModel());
                    return RedirectToAction("CheckRecord");
                }
                else
                {
                    InitForm("");
                    return View(viewModel);
                }
            }
            catch(Exception ex)
            {
                return Alert(ex.Message);
            }
        }
        public ActionResult CheckNoPass(string checkrecordId)
        {
            InitForm("审核不通过");
            var record = _checkrecord.Find(checkrecordId);
            if (record != null)
            {
                return View(CheckRecord_M.ToViewModel(record));
            }
            else
                return Alert("操作失败");
        }
        [HttpPost]
        public ActionResult CheckNoPass(CheckRecord_M viewModel)
        {
            try
            {
                if (ModelState.IsValid & viewModel.TaskType == 0)
                {
                    viewModel.Auditor = DataContext.UserID;
                    viewModel.State = 2;
                    _checkrecord.Update(viewModel.ToModel());
                    var quotatask = _quotataskdis.Find(viewModel.FinishID);
                    quotatask.IsComplete = 0;
                    _quotataskdis.Update(quotatask);
                    return RedirectToAction("CheckRecord");
                }
                else if (ModelState.IsValid & viewModel.TaskType == 1)
                {
                    viewModel.Auditor = DataContext.UserID;
                    viewModel.State = 2;
                    _checkrecord.Update(viewModel.ToModel());
                    _quantifytaskcom.Delete(viewModel.FinishID);
                    return RedirectToAction("CheckRecord");
                }
                else
                {
                    InitForm("");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                return Alert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AppCheck(string wbstaskid, string tasktype, string mytaskid ,string certificate,string note)
        {
            if (tasktype == "0")//定额
            {
                var mytask = _wbsServices.GetQuotadis(mytaskid);
                mytask.Certificate = certificate;
                mytask.IsComplete = 2;
                _quotataskdis.Update(mytask);
                _wbsServices.CreateCheckRecord(mytask.QuotaTaskStaffDistributionID, mytask.QuotaTask.TaskName, mytask.QuotaTask.WbsTaskID, 0, DataContext.UserID);
                return Redirect("/AppTeacher2/Thome/WorkLoad");
            }
            else
            {
                QuantifyTaskCompletion quantifytaskcom=new QuantifyTaskCompletion();
                quantifytaskcom.QuantifyTaskCompletionID = DateTime.Now.Random2();
                quantifytaskcom.QuantifyTaskID = _wbsServices.GetQuantifyTask(wbstaskid).QuantifyTaskID;
                quantifytaskcom.ScoringRuleID = mytaskid;
                quantifytaskcom.StaffID = DataContext.UserID;
                quantifytaskcom.StaffName = DataContext.UserID;
                quantifytaskcom.FinishTime = DateTime.Now;
                quantifytaskcom.CompleteNote = note;
                quantifytaskcom.Certificate = certificate;
                _quantifytaskcom.Add(quantifytaskcom);
                var formname = _scoringrule.Find(mytaskid).FormName;
                var wbsId = _quantifytask.Find(quantifytaskcom.QuantifyTaskID).WbsTaskID;
                _wbsServices.CreateCheckRecord(quantifytaskcom.QuantifyTaskCompletionID, formname, wbsId, 1, DataContext.UserID);
                return Redirect("/AppTeacher2/Thome/WorkLoad");
            }

        }
    }
}