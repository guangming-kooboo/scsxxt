using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Tools.Scsxxt;
using ServicePlatform.Areas.V2.ViewModels.stubatchregextentAdd;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.V2.Controllers
{
    public class StuBatchRegExtentController : BaseV2Controller
    {
        // GET: /V2/StuBatchRegExtent/
        private IRepository<T_StuBatchReg_Extent> _t_stubatchReg_extent;
        private IRepository<T_StuBatchReg> _t_stubatchreg;
        private IRepository<T_Student> _t_student;
        private IRepository<T_Enterprise> _t_enterprise;
        public StuBatchRegExtentController(
            IRepository<T_StuBatchReg_Extent> t_stubatchReg_extent,
            IRepository<T_StuBatchReg> t_stubatchreg,
            IRepository<T_Student> t_student,
            IRepository<T_Enterprise> t_enterprise
            )
        {

            _t_stubatchReg_extent = t_stubatchReg_extent;
            _t_stubatchreg = t_stubatchreg;
            _t_student = t_student;
            _t_enterprise = t_enterprise;
        }
        //V2/StuBatchRegExtent/stubatchregextent
        public ActionResult stubatchregextent(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("stubatchregextent", new { reportId = "SCSXXT.分散实习学生列表", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("分散实习学生列表", "/V2/StuBatchRegExtent/stubatchregextentadd");
            return View();
        }
        public ActionResult stubatchregextentadd(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("stubatchregextentadd", new { reportId = "SCSXXT.添加分散实习学生", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("添加分散实习学生", "#");
            return View();
        }
        //V2/StuBatchRegExtent/stubatchregextentAdd
        public ActionResult stubatchregextentAdd2(string userid)
        {
            InitForm("添加分散实习学生");
            var studentname = _t_student.Find(userid).StuName;
            return View(stubatchregextentAdd_M.ToViewModel(studentname, _t_enterprise.ToSelectItems()));
        }
        //[HttpPost]
        //public ActionResult stubatchregextentAdd()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _crewlimitetype.Add(model.ToModel());
        //        return RedirectToAction("Msg", "Group/GroupEdit");
        //    }
        //    else
        //    {
        //        InitForm("添加通讯组类型");
        //        return View(model);
        //    }
        //}
    }
}
