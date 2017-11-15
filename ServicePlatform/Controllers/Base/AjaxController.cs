using Core.Services;
using Core.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Controllers.Base
{
    public class AjaxController : BaseAccountController
    {
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetSchools()
        {
            DataContext.SetFiled("Ent_No", DataContext.UserUnit);
            return Json(
                S<T_EntBatchReg>().All(DataContext, "企业通过审核的批次[当前]").
                Select(a => new SelectListItem{ Value = a.EntPracNo, Text = a.T_PracBatch.T_School.SchoolName }).
                ToList()
                );
        }
        public ActionResult GetPositions(string EntPracNo)
        {
            DataContext.SetFiled("EntPracNo", EntPracNo);
            return Json(
                S<T_PracticePosition>().All(DataContext, "企业提供给某个学校的岗位").
                Select(a => new SelectListItem { Value = a.PositionID, Text = a.C_Position.PositionName }).
                ToList()
                );
        }
        

    }
}
