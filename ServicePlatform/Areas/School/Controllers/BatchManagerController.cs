using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using ServicePlatform.Config;
using Webdiyer.WebControls.Mvc;
using System.Text.RegularExpressions;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.School.Controllers
{
    public class BatchManagerController : BaseAccountController
    {
        //private BatchContext bc = new BatchContext();

        private SchoolContext sc = new SchoolContext();
        private EnterpriseContext ec = new EnterpriseContext();


        [BaseActionFilter]
        //创建批次
        public ActionResult CreatBatch()
        {
            ViewBag.SchoolID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            T_PracBatch dbSet_TP = new T_PracBatch();
            dbSet_TP.SchoolID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            return View(dbSet_TP);
            //return View();
        }

        [BaseActionFilter]
        public ActionResult menuLinkToBatchList()
        {
            string UnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            string hLink = "/School/BatchManager/BatchList?id=1&schoolid=" + UnitID;
            return Redirect(hLink);
        }


        //批次列表
        [BaseActionFilter]
        public ActionResult BatchList(int id, string schoolid)
        {
            //var q = (from f in bc.PracBatch where f.SchoolID == schoolid select f).ToList();
            //return View(q);

            using (var mc = new SchoolContext())
            {
                // string userid = (Session["vars"] as ServicePlatform.Config.VarsConfig).MyUserID;
                //var q_msgrec = (from f in mc.Mession where f.Releaser == userid select f).ToList();
                var q_msgrec = (from f in sc.PracBatch where f.SchoolID == schoolid select f).ToList();
                int count = q_msgrec.Count;
                if (count != 0)
                {
                    string[] isactive = new string[count]; //是否有效
                    for (int i = 0; i < count; i++)
                    {
                        if (q_msgrec[i].IsActive == 1)
                        {
                            isactive[i] = "是";
                        }
                        else
                        {
                            isactive[i] = "否";
                        }

                    }
                    ViewBag.IsActive = isactive; //是否有效

                    var model = q_msgrec.ToPagedList(id, 10);
                    ViewBag.PageIndex = id; //页面索引
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("BatchList_List", model);
                    }
                    return View(model);
                }
                else
                {
                    return Content("批次列表空空如也  -_-||");
                }
            }
        }


        //批次创建操作
        [BaseActionFilter]
        public ActionResult BatchCreat(T_PracBatch tp)
        {
            //T_PracBatch tp = new T_PracBatch();
            tp.BatchID = tp.SchoolID + DateTimeFormat.ConvertDateTimeInt(DateTime.Now).ToString();
            //tp.SchoolID = schoolid;
            tp.PracBatchID = tp.BatchID + "-" + tp.SchoolID;
            string iscurrent = Request.Form["iscurrent"];
            tp.IsCurrentBatch = iscurrent;
            if (iscurrent == "是")
            {
                var q = (from f in sc.PracBatch where f.SchoolID == tp.SchoolID select f).ToList();
                for (int i = 0; i < q.Count; i++)
                {
                    T_PracBatch TP = q[i];
                    TP.IsCurrentBatch = "否";
                    sc.SaveChanges();
                }
            }
            string isactive = Request.Form["isactive"];
            if (isactive == "是")
            {
                tp.IsActive = 1;
            }
            else
            {
                tp.IsActive = 0;
            }
            sc.PracBatch.Add(tp);
            if (sc.SaveChanges() > 0)
            {
                return Alert("创建批次成功", "/School/BatchManager/BatchList?id=" + 1 + "&schoolid=" + DataContext.UserUnit);
            }
            else
            {
                return Alert("创建失败",-1);
            }
         //   return Alert(sc.SaveChanges() > 0 ? "批次创建成功" : "批次创建失败，请重试！"); //返回上一页并刷新
        }

        //批次删除操作
        [BaseActionFilter]

        public ActionResult BatchDelete(string PracBatchID)
        {
            T_PracBatch tp = sc.PracBatch.Find(PracBatchID);
            if (tp != null)
            {
                sc.PracBatch.Remove(tp);
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>"); //返回上一页并刷新
        }

        public ActionResult EditBatch(string PracBatchID)
        {
            T_PracBatch batch = sc.PracBatch.FirstOrDefault(a => a.PracBatchID == PracBatchID);

            return View(PracBatch_M.ToViewModel(batch));
        }
        [HttpPost]
        public ActionResult EditBatch(PracBatch_M batch)
        {
           sc.PracBatch.AddOrUpdate(new T_PracBatch()
           {
               PracBatchID = batch.PracBatchID,
               BatchID = batch.BatchID,
               SchoolID = batch.SchoolID,
               BatchName = batch.BatchName,
               StartEnd = batch.StartEnd,
               IsCurrentBatch = batch.IsCurrentBatch,
               IsActive = batch.IsActive,
               SchoolReviewWeight = batch.SchoolReviewWeight
           });
            if (sc.SaveChanges() > 0)
            {
                return Alert("批次修改成功", -2);
            }
            else
            {
                return Alert("批次修改成失败", -1);
            }
        }
        //设置当前批次
        [BaseActionFilter]
        public ActionResult SetCurrentBatch(string batchid, string schoolid)
        {
            T_PracBatch tp = sc.PracBatch.Find(batchid);
            if (tp != null)
            {
                tp.IsCurrentBatch = "是";
                sc.SaveChanges();
                var q = (from f in sc.PracBatch where f.SchoolID == schoolid && f.PracBatchID != batchid select f).ToList();
                for (int i = 0; i < q.Count; i++)
                {
                      T_PracBatch temp = q[i];
                      temp.IsCurrentBatch = "否";
                      sc.SaveChanges();
                    
                }

            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //审核/查看企业准入
        [BaseActionFilter]
        public ActionResult RegEntInBatchList(string BatchID, string SchoolID, Boolean readOnly)
        {
            //搜索批次号为：BatchID，学校编号为：SchoolID，的实习企业，并按照审批的状态排序。
            string IsCurrentFlag = "是";
            if (readOnly)
            {
                IsCurrentFlag = "否";
            }
            var EntReg = (from EntRegBatch in ec.T_EntBatchReg from PracBatch in ec.PracBatch where EntRegBatch.PracBatchID == BatchID && EntRegBatch.PracBatchID == PracBatch.PracBatchID && PracBatch.SchoolID == SchoolID && PracBatch.IsCurrentBatch == IsCurrentFlag orderby EntRegBatch.ApplyStatus ascending select EntRegBatch).ToList();
            if (EntReg.Count > 0)
            {
                var model = EntReg.ToPagedList(1, 30);
                ViewBag.PageIndex = 1;//页面索引
                ViewBag.ReadOnly = readOnly;
                if (Request.IsAjaxRequest())
                {
                    return View("RegEntList_List", model);
                }
                else
                {
                    return View(model);//返回上一页并刷新
                }

            }
            else
            {
                return Content("没有实习单位申请记录  -_-||");
            }


        }


        //企业申请审批通过
        [BaseActionFilter]
        public ActionResult EntRegApproved(string EntPracBatchID)       
        {
            T_EntBatchReg EntReg = ec.T_EntBatchReg.Find(EntPracBatchID);
            int pass = 2;

            if (EntReg != null)
            {
                EntReg.ApplyStatus = pass;
                ec.SaveChanges();
            }
            else
            {
                return Content("没有实习单位申请记录  -_-||");
            }

            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新

        }


        //企业申请审批驳回
        [BaseActionFilter]
        public ActionResult EntRegDeny(string EntPracBatchID)
        
        {
            T_EntBatchReg EntReg = ec.T_EntBatchReg.Find(EntPracBatchID);
            int deny = 3;

            if (EntReg != null)
            {
                EntReg.ApplyStatus = deny;
                ec.SaveChanges();
            }
            else
            {
                return Content("没有实习单位申请记录  -_-||");
            }

            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新

        }


        //企业岗位查看
        [BaseActionFilter]
        public ActionResult EntPositionList(string entpracno)
        {
            //获取企业提供给该高校的职位
            CodeTableContext ctc = new CodeTableContext();
            var q2 = (from f in ec.T_PracticePosition where f.EntPracNo == entpracno select f).ToList();
            string[] posname = new string[q2.Count];
            string[] time = new string[q2.Count];
            string[] enrollnum = new string[q2.Count];//岗位已报人数
            for (int i = 0; i < q2.Count; i++)
            {
                string temp = q2[i].PositionID;
                time[i] = DateTimeFormat.ConvertIntDateTime(q2[i].PosExpireDate).ToShortDateString();
                C_Position cp = ctc.C_Position.Find(temp);
                posname[i] = cp.PositionName;

                var q_enrollnum = (from f in sc.PracticeVolunteer where f.EntPracNo == entpracno && f.PosID == temp && f.PreVolStatus == "1" select f).ToList();
                enrollnum[i] = q_enrollnum.Count.ToString();
            }
            ViewBag.PositionName = posname;
            ViewBag.Time = time;
            ViewBag.Num = enrollnum;
            return View(q2);
        }




    }//ending for class
}//ending for namespace
