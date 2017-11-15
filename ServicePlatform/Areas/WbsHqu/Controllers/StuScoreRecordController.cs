using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using ServicePlatform.Areas.WbsHqu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Interfaces;
using Qx.Tools;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class StuScoreRecordController : BaseWbsHquController
    {
        //
        // GET: /WbsHqu/StuScoreRecord/
        private IRepository<T_StuScoreApply> _stuscoreapply;
        private IRepository<T_StuScoreRecord> _stuscorerecord;
        private IV3Services _v3services;
        public StuScoreRecordController(IV3Services v3services, IRepository<T_StuScoreApply> stuscoreapply, IRepository<T_StuScoreRecord> stuscorerecord)
        {
            _v3services = v3services;
            _stuscoreapply = stuscoreapply;
            _stuscorerecord = stuscorerecord;
        }
        public ActionResult Index(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            //管理员查看的记录
            if (!reportId.HasValue())
            {

                return RedirectToAction("Index", new { reportId = "StuScoreRecord.所有实习成绩记录", Params = ";", pageIndex = 1, perCount = 10 });
            }
            InitReport("实习成绩记录", "", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }
        public ActionResult MyRecord(string reportId, string Params, int pageIndex = 1, int perCount = 10)
        {
            //用户查看的记录
            if (!reportId.HasValue())
            {

                return RedirectToAction("MyRecord", new { reportId = "StuScoreRecord.我的实习成绩记录", Params = Params, pageIndex = 1, perCount = 10 });
            }
            OverWriteParam(DataContext.UserID);
            InitReport("实习成绩记录", "", "", true, "Qx.Scsxxt.Extentsion");
            return View();
        }
        public ActionResult Details(string id)
        {
            InitForm("实习成绩详情",false);
            try
            {
                var sturecord = _stuscorerecord.Find(id);
                return View(StuScoreRecord_M.ToViewModel(sturecord));
            }
            catch (Exception ex)
            {
                return Alert("请联系开发人员\n" + ex.Message);
            }
        }

        //重新审核
        public ActionResult ReExamination(string id)
        {
            //先删除当前的记录，将原有申请状态改为待审核，页面跳到实习成绩审核
            try
            {
                var recorde = _stuscorerecord.Find(id);
                if (recorde != null)
                {
                    _stuscorerecord.Delete(id);
                    if (_v3services.BackState(recorde.PraticeNo))
                    {
                        return Alert("你已经取消的该学生的当前成绩，请去“实习成绩审核”重新审核该学生的成绩", "/WbsHqu/StuScoreRecord/Index");
                    }
                    else
                    {
                        return Alert("不存在该条申请记录");
                    }
                }
                else
                {
                    return Alert("不存在该条记录");
                }
            }
            catch (Exception ex) 
            {
               return Alert(ex.Message);
            }
        }
    }
}
