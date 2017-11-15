
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Scsxxt.Extentsion.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class V3Controller : BaseV3Controller
    {
        private IRepository<C_EntRank> _entRank;
        private IRepository<C_EntCategory> _entCategory;
        private IRepository<V3_EnterpriseApply> _enterpriseApply;
        private IRepository<T_Enterprise> _enterprise;
        private IRepository<V3_StuEnterPriseApply> _stuEnterPriseApply;
        private IDisperseServices _idisperseServices;
        private IRepository<C_Position> _position;
        private IV3Services _service;
        public V3Controller(IV3Services service,IRepository<C_Position> position,IDisperseServices idisperseServices,IRepository<C_EntRank> entRank, IRepository<C_EntCategory> entCategory, 
            IRepository<V3_EnterpriseApply> enterpriseApply, IRepository<V3_StuEnterPriseApply> stuEnterPriseApply, IRepository<T_Enterprise> enterprise)
        {
            _service = service;
            _entCategory = entCategory;
            _entRank = entRank;
            _enterpriseApply = enterpriseApply;
            _stuEnterPriseApply = stuEnterPriseApply;
            _idisperseServices = idisperseServices;
            _position = position;
            _enterprise = enterprise;
        }
        //
        // GET: /WbsHqu/V3/
        public ActionResult Index()
        {
            InitMenu(new Dictionary<string, string>() {
                {"学生企业列表", "/WbsHqu/V3/EnterPriseApplyList"},
                {"学生申请企业列表", "/WbsHqu/V3/StuEnterPriseApplyList"},
                {"学生企业申请", "/WbsHqu/V3/EnterPriseApply"},
                {"申请学生企业", "/WbsHqu/V3/ChooseEnt"},
            });
            return View();
        }
        public ActionResult EnterPriseApplyList(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
          
            if (!reportId.HasValue())
            {
                return RedirectToAction("EnterPriseApplyList", new { reportId = "V3_EnterpriseApply.学生企业申请", Params = ";", pageIndex = 1, perCount = 10 });
            }         
            OverWriteParam(DataContext.UserUnit+Params);
            InitReport("审批企业入驻", "#", "",true , "Qx.Scsxxt.Extentsion");
            return View();
        }

        public ActionResult EnterPriseApply(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!Params.HasValue())
            {
                Params = ";";
            }
            if (!reportId.HasValue())
            {
                return RedirectToAction("EnterPriseApply", new { reportId = "V3_EnterpriseApply.申请企业记录", Params = Params, pageIndex = 1, perCount = 10 });
            }
            OverWriteParam(DataContext.UserID + Params);
            InitReport("申请记录", "/WbsHqu/V3/EnterPriseApplyAdd", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }

        public ActionResult EnterPriseList(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!Params.HasValue())
            {
                Params = ";";
            }
            if (!reportId.HasValue())
            {
                return RedirectToAction("EnterPriseList", new { reportId = "V3_EnterpriseApply.学生企业列表", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("学生企业列表", "#", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }
        public ActionResult GetRank(string EntCategoryID)
        {
            var category = _entCategory.Find(EntCategoryID);
            if (category == null)
            {
                return Json(new { data = "找不到该类型" }, JsonRequestBehavior.AllowGet);
            }
            var rank = _service.GetEntRank(EntCategoryID);
            return Json(new { data = rank }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EnterPriseApplyAdd()
        {
            InitForm("企业入驻申请");     
            return View(EnterPriseApply_M.ToViewModel(_entCategory.ToSelectItems()));
        }
        // POST: Area/Controller/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnterPriseApplyAdd(EnterPriseApply_M viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rank = viewModel.EntRankID.UnPackString(';');
                    viewModel.ApplyState = 0;
                    viewModel.EntRankID = rank[0];
                    //viewModel.EntCategoryID = rank[1];
                    viewModel.RegisterTime = DateTime.Now.ToInt();
                    viewModel.UpdateTime = DateTime.Now.ToInt();
                    viewModel.UserID = DataContext.UserID;
                    viewModel.SchoolID = DataContext.UserUnit;
                    _enterpriseApply.Add(viewModel.ToModel());
                    // TODO: 这里编写添加逻辑
                    return Alert("申请成功,请等待审核...",-2);
                }
                else
                {
                    InitForm("学生企业申请");
                    viewModel.EntRankItems = _entRank.ToSelectItems();
                    viewModel.EntRankItems = _entCategory.ToSelectItems();
                    return View(viewModel);
                }

             }
            catch (Exception ex)
            {
                throw ex;
              //  return Alert("请联系开发人员\n" + ex.Message);
            }       
        }
        public ActionResult StuEnterPriseApplyList(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("StuEnterPriseApplyList", new { reportId = "V3_StuEnterpriseApply.学生申请企业", Params = ";", pageIndex = 1, perCount = 10 });
            }
            InitReport("审批分散实习", "#", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }
        public ActionResult StuEnterPriseList(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("StuEnterPriseApplyList", new { reportId = "V3_StuEnterpriseApply.企业学生列表", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("企业学生列表", "#", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }

        // // GET: /WbsHqu/V3/ApplyEntHistory
        public ActionResult ApplyEntHistory(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!Params.HasValue())
            {
                Params = ";";
            }
            if (!reportId.HasValue())
            {
                return RedirectToAction("ApplyEntHistory", new { reportId = "V3_StuEnterpriseApply.分散实习申请记录", Params = ";", pageIndex = 1, perCount = 10 });
            }
            OverWriteParam(DataContext.UserID+Params);
            InitReport("分散实习申请记录", "/WbsHqu/V3/ChooseEnt", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }


        // // GET: /WbsHqu/V3/ChooseEnt
        public ActionResult ChooseEnt(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!Params.HasValue())
            {
                Params = ";";
            }
            if (!reportId.HasValue())
            {
                return RedirectToAction("ChooseEnt", new { reportId = "V3_StuEnterpriseApply.选择学生企业", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("选择分散实习企业", "#", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }
        // // GET: /WbsHqu/V3/StuEnterPriseApply?ent_no
        public ActionResult StuEnterPriseApply(string ent_no)
        {
            InitForm("申请分散实习");
            var ent = _enterprise.Find(ent_no);
            if (ent != null)
            {
                return View(StuEnterPriseApply_M.ToViewModel(_position.ToSelectItems(), ent_no, ent.Ent_Name));
            }
            else
            {
                return Alert("找不到该企业", -1);
            }
        }
        [HttpPost]
        public ActionResult StuEnterPriseApply(StuEnterPriseApply_M viewModel)
        {
            if (ModelState.IsValid)
             {
                var stuapply = new V3_StuEnterPriseApply()
                {
                    ApplyState = 0,
                    ApplyTime = DateTime.Now.ToInt(),
                    Ent_No = viewModel.Ent_No,
                    UserID = DataContext.UserID,
                    StuEnterPriseApplyID = DateTime.Now.Random2(),
                    posDesc = viewModel.posDesc,
                    practiceDept = viewModel.practiceDept,
                    practiceDivDept = viewModel.practiceDivDept,
                    practiceEnd = viewModel.practiceEnd.ToInt(),
                    practiceStart = viewModel.practiceStart.ToInt(),
                    positionId=viewModel.positionId
                };

                _stuEnterPriseApply.Add(stuapply);
                return Alert("申请成功,请等待审核...", "/WbsHqu/V3/ApplyEntHistory");
            }
            else
            {

                InitForm("申请分散实习");
                return Alert();
                return View(viewModel);
            }
        }
        //编辑申请记录
        public ActionResult EditStuEnterPriseApply(string applyid)
        {
            InitForm("编辑申请分散实习");
            var applyrecord = _stuEnterPriseApply.Find(applyid);
            if (applyrecord != null)
            {
                return View(EditStuEnterPriseApply_M.ToViewModel(_position.ToSelectItems(), applyrecord));
            }
            else
            {
                return Alert("找不到该企业", -1);
            }
        }
        [HttpPost]
        public ActionResult EditStuEnterPriseApply(EditStuEnterPriseApply_M viewModel)
        {
            if (ModelState.IsValid)
            {
                var stuapply = new V3_StuEnterPriseApply()
                {
                    ApplyState = 0,
                    ApplyTime = DateTime.Now.ToInt(),
                    Ent_No = viewModel.Ent_No,
                    UserID = DataContext.UserID,
                    StuEnterPriseApplyID = viewModel.StuEnterPriseApplyID,
                    posDesc = viewModel.posDesc,
                    practiceDept = viewModel.practiceDept,
                    practiceDivDept = viewModel.practiceDivDept,
                    practiceEnd = viewModel.practiceEnd.ToInt(),
                    practiceStart = viewModel.practiceStart.ToInt(),
                    positionId = viewModel.positionId
                };

                _stuEnterPriseApply.Update(stuapply);
                return Alert("申请成功,请等待审核...", "/WbsHqu/V3/ApplyEntHistory");
            }
            else
            {

                InitForm("申请分散实习");
                return Alert();
                return View(viewModel);
            }
        }

        public ActionResult StuApplyPassOrNo(string id,int flag)
        {
            var stuapply = _stuEnterPriseApply.Find(id);
            if (flag == 1)
            {
                try
                {
                    if (_idisperseServices.OneKeyToPractice(id))
                    {
                        return Alert("操作成功！", "/WbsHqu/V3/StuEnterPriseApplyList");
                    }
                    else
                    {
                        return Alert("该学生已在当前批次下申请过分散实习或已参加集中实习，不能在当前批次下再次申请分散实习！", "/WbsHqu/V3/StuEnterPriseApplyList");
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message);
                }              
            }
          else  if (flag == 5)
            {
                try
                {
                    if (_idisperseServices.OneKeyToPractice_RoleBack(id))
                    {
                        return Alert("恭喜：取消审核成功！", "/WbsHqu/V3/StuEnterPriseApplyList");
                    }
                    else
                    {
                        return Alert("抱歉：取消审核失败，请联系开发人员解决！", "/WbsHqu/V3/StuEnterPriseApplyList");
                    }
                }
                catch (Exception)
                {

                    return Alert("抱歉：取消审核失败（回滚只适用于分散实习误操作的情况，若想将当前批次的学生从集中实习更改为分散实习，请联系数据库管理员！）", "/WbsHqu/V3/StuEnterPriseApplyList");
                }

            }
            else
            {
               stuapply.ApplyState = -1;
                _stuEnterPriseApply.Update(stuapply);
                return Alert("操作成功", "/WbsHqu/V3/StuEnterPriseApplyList");
            }
        }
        public ActionResult EntApplyPassOrNo(string id, int flag)
        {
            var entapply = _enterpriseApply.Find(id);
            if (flag == 1)
            {
                entapply.ApplyState = 1;
                _idisperseServices.AgreeStuEnterprise(entapply.Ent_No, entapply.Ent_Name, 
                    entapply.EntCategoryID, entapply.EntRankID, entapply.EntAddress, 
                    entapply.UserID, entapply.RegisterTime ?? 0, entapply.UpdateTime ?? 0, 
                    entapply.Email, entapply.EntResume, entapply.Contectinfo);
            }
            else
            {
                entapply.ApplyState = -1;
            }
            _enterpriseApply.Update(entapply);
            return Alert("操作成功", "/WbsHqu/V3/EnterPriseApplyList");
        }
        //删除申请记录
        public ActionResult DeleteApplyRecord(string applyid)
        {
            var applyrecord = _stuEnterPriseApply.Find(applyid);
            if (applyrecord != null)
            {
                _stuEnterPriseApply.Delete(applyid);
                return Alert("删除成功", "/WbsHqu/V3/ApplyEntHistory");
            }
            else
            {
                return Alert("删除失败", "/WbsHqu/V3/ApplyEntHistory");
            }
        }

        //public ActionResult Test()
        //{
        //    _idisperseServices.OneKeyToPractice("10385-1414141017", "QZYH", "10", "单元测试哈哈哈", "保安部", "小卖部", "2017-03-06至2017-03-09");
        //    _idisperseServices.OneKeyToPractice_RoleBack("10385-1414141017", "QZYH", "10", "单元测试哈哈哈", "保安部", "小卖部", "2017-03-06至2017-03-09");
        //    return Alert("测试通过！");PracticeScoreController
        //}
    }
}
