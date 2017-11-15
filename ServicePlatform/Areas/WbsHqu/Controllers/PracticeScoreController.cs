using System;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;
using Qx.Scsxxt.Extentsion.Repository;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Scsxxt.Extentsion.Interfaces;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class PracticeScoreController : BaseWbsHquController
    {
        // TODO: 把<string> 更换为数据库model类型如 <Car>
        private IRepository<T_StuScoreApply> _stuscoreapply;
        private IRepository<T_StuScoreRecord> _stuscorerecord;
        private IV3Services _v3services;
        public PracticeScoreController(IV3Services v3services,IRepository<T_StuScoreApply> stuscoreapply, IRepository<T_StuScoreRecord> stuscorerecord)
        {
            _v3services = v3services;
            _stuscoreapply = stuscoreapply;
            _stuscorerecord = stuscorerecord;
        }

        public ActionResult Index(string reportId, string Params=";", int pageIndex=1 , int perCount=10 )
        {
            //实习成绩审核
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "StuScoreApply.实习成绩审核", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("实习成绩审核", "", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }
        public ActionResult StudentApply(string reportId, string Params = ";", int pageIndex = 1, int perCount = 10)
        {
            //实习成绩审核
            if (!reportId.HasValue())
            {

                return RedirectToAction("StudentApply", new { reportId = "StuScoreApply.实习成绩申请", Params = Params, pageIndex = 1, perCount = 10 });
            }
            OverWriteParam(DataContext.UserID);
            InitReport("实习成绩申请", "/WbsHqu/PracticeScore/Create", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }

        public ActionResult PassOrNot(string id,string type)
        {
            InitForm("实习成绩审核");
            var stuapply = _stuscoreapply.Find(id);
            try
            {
                if(type=="0")//不通过
                {
                    stuapply.StateID = "已驳回";
                    stuapply.ReviewScore = null;
                    return View("NotPass", StuScoreApply_M.ToViewModel(stuapply));
                }
                else
                {
                    stuapply.StateID = "已通过";
                    stuapply.AuditOpinion = "";
                    return View("Pass", StuScoreApply_M.ToViewModel(stuapply));
                }
            }
            catch(Exception ex)
            {
                return Alert("实习成绩申请不存在\n" + ex.Message);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PassOrNot(StuScoreApply_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.type == "0") //审核不通过
                    {
                        if (!string.IsNullOrEmpty(viewModel.AuditOpinion)) //审核意见和最终成绩为必填项
                        {
                            if (!_v3services.PassOrNot(viewModel.ToModel()))
                            {
                                return Alert("操作失败!", "/WbsHqu/PracticeScore/Index");
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            return Alert("请将信息填写完整", -1);
                        }
                    }
                    else//审核通过
                    {
                        if (!string.IsNullOrEmpty(viewModel.AuditOpinion) &&
                            !string.IsNullOrEmpty(viewModel.ReviewScore.ToString())) //审核意见和最终成绩为必填项
                        {
                            if (!_v3services.PassOrNot(viewModel.ToModel()))
                            {
                                return Alert("操作失败!", "/WbsHqu/PracticeScore/Index");
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            return Alert("请将信息填写完整", -1);
                        }
                    }
                }
                else
                {
                    InitForm("实习成绩申请");
                    return Alert();
                 //   return View(viewModel);
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
            //实习成绩申请
            InitForm("实习成绩申请");
            var prano = _v3services.get_PracticeNo(DataContext.UserID);
            if (prano == null)
            {
                return Alert("实习号不存在!", "/WbsHqu/PracticeScore/StudentApply");
            }
            else if(_v3services.get_stuscoreapply(prano))
            {
                return Alert("已申请过实习成绩!", "/WbsHqu/PracticeScore/StudentApply");
            }
            else
            {
                return View(StuScoreApply_M.ToViewModel(prano, DataContext.UserID));
            }
        }

        // POST: Area/Controller/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(StuScoreApply_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: 这里编写添加逻辑
                    viewModel.StateID = "待提交";
                    _stuscoreapply.Add(viewModel.ToModel());
                    return RedirectToAction("StudentApply");
                }
                else
                {
                    InitForm("实习成绩申请");
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
            InitForm("编辑实习成绩申请");
            var stuapply = _stuscoreapply.Find(id);
            if(stuapply.StateID=="待审核")
            {
                return Alert("未审核状态不可编辑!", "/WbsHqu/PracticeScore/StudentApply");
            }
            return View(StuScoreApply_M.ToViewModel(stuapply));
        }

        // POST: Area/Controller/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(StuScoreApply_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: 这里编写更新逻辑
                    viewModel.StateID = "待提交";
                    _stuscoreapply.Update(viewModel.ToModel());
                    return RedirectToAction("StudentApply");
                }
                else
                {
                    InitForm("编辑实习成绩申请");
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }
        public ActionResult Commit(string id)
        {
            try
            {
                var stuapply = _stuscoreapply.Find(id);
                stuapply.StateID = "待审核";
                _stuscoreapply.Update(stuapply);
                return Alert("提交成功,请耐心等待审核!", "/WbsHqu/PracticeScore/StudentApply");
            }
            catch(Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message, "/WbsHqu/PracticeScore/StudentApply");
            }
        }
        public ActionResult ApplyDetails(string id)
        {
            InitForm("实习成绩申请",false);
            try
            {
                var stuapply = _stuscoreapply.Find(id);
                return View(StuScoreApply_M.ToViewModel(stuapply));
            }
            catch(Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message, "/WbsHqu/PracticeScore/StudentApply");
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
            InitForm("申请详情", false);
            // TODO: 这里编写详情逻辑
            var stuapply = _stuscoreapply.Find(id);
            return View(StuScoreApply_M.ToViewModel(stuapply));
        }

    }
}