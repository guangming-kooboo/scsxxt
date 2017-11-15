using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using ServicePlatform.Controllers.Base;
using C_Position = ServicePlatform.Models.C_Position;
using T_Enterprise = ServicePlatform.Models.T_Enterprise;
using T_PracticePosition = ServicePlatform.Models.T_PracticePosition;
using T_PracticeVolunteer = ServicePlatform.Models.T_PracticeVolunteer;
using T_StuBatchReg = ServicePlatform.Models.T_StuBatchReg;

namespace ServicePlatform.Areas.School.Controllers
{
    public class S_PraEnrollManagerController : BaseController
    {
        private MyContext Db = new MyContext();
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();
       // ServiceApi.Controllers.ServiceController ServiceController = new ServiceApi.Controllers.ServiceController();

        //根据企业类型选择企业
        /* 
                public ActionResult KonwAboutEnt(string EntType="0")
                {
                    var q_cate = (from f in ctc.C_EntCategory select f).ToList();
                    List<SelectListItem> Enttype = new List<SelectListItem>();
                    for (int i = 0; i < q_cate.Count; i++)
                    {
                        if (q_cate[i].EntCategoryID == EntType)
                        {
                            Enttype.Add(new SelectListItem() { Text = q_cate[i].EntCategoryName, Value = q_cate[i].EntCategoryID, Selected = true });
                        }
                        else
                        {
                            Enttype.Add(new SelectListItem() { Text = q_cate[i].EntCategoryName, Value = q_cate[i].EntCategoryID, Selected = false });
                        }
                    }
                    ViewData["Enttype"] = Enttype;
                    //ViewData["BatchName"] = new SelectList(q_cate, "EntCategoryID", "EntCategoryName");
                    var q = (from f in ec.T_Enterprise where f.EntCategoryID == EntType select f);
                    return View(q);
                }
        */
        //重载。根据实习批次和类别选择企业
        [BaseActionFilter]
        public ActionResult KonwAboutEnt(string PracBatchID,string EntType="1")
        {
            var q_cate = (from f in ctc.C_EntCategory select f).ToList();
            List<SelectListItem> Enttype = new List<SelectListItem>();
            for (int i = 0; i < q_cate.Count; i++)
            {
                if (q_cate[i].EntCategoryID == EntType)
                {
                    Enttype.Add(new SelectListItem() { Text = q_cate[i].EntCategoryName, Value = q_cate[i].EntCategoryID, Selected = true });
                }
                else
                {
                    Enttype.Add(new SelectListItem() { Text = q_cate[i].EntCategoryName, Value = q_cate[i].EntCategoryID, Selected = false });
                }
            }
            ViewData["Enttype"] = Enttype;
            ViewBag.PracBatchID = PracBatchID;
            //ViewData["BatchName"] = new SelectList(q_cate, "EntCategoryID", "EntCategoryName");
            //int IsApproved = 2;
            //获得已经注册到该高校当前批次下的企业，注意，企业的状态一定是要“已经批准”的。
            //var q = (from f in ec.T_Enterprise from InBatch in ec.T_EntBatchReg where f.EntCategoryID == EntType && InBatch.PracBatchID == PracBatchID && InBatch.Ent_No == f.Ent_No && InBatch.ApplyStatus == IsApproved select f).ToList();
            List<Qx.Scsxxt.Extentsion.Entity.T_Enterprise> EnterpriseList;
            //获取和本校相关的企业
            var _temp = Db.T_PracBatch.Where(a => a.SchoolID == DataContext.UserUnit&&a.IsCurrentBatch=="是").
                SelectMany(b => b.T_EntBatchReg.Select(c => c.T_Enterprise)).ToList().
                Distinct(Qx.Tools.CommonExtendMethods.CommonExtendMethods.
                Equality<Qx.Scsxxt.Extentsion.Entity.T_Enterprise>.
                CreateComparer(x=>x.Ent_No)).ToList();
            if (Request.Form["search"]!=null)
            {
                string search = Request.Form["search"];
                EnterpriseList = (from f in _temp where f.EntCategoryID == EntType && f.Ent_Name.Contains(search) select f).ToList();
            }
            else
            {
                EnterpriseList = (from f in _temp where f.EntCategoryID == EntType select f).ToList(); ;
            }
            //var q = (from f in ec.T_Enterprise where f.EntCategoryID == EntType select f).ToList();

            ViewBag.EntList = EnterpriseList;
            ViewBag.DataNum = EnterpriseList.Count;

            string html_id = "var id = [";
            for (int i = 0; i < EnterpriseList.Count; i++)
            {
                html_id += "'" + EnterpriseList[i].Ent_No + "'" + ",";
            }
            html_id += "];";

            string html_name = "var entname = [";
            for (int i = 0; i < EnterpriseList.Count; i++)
            {
                html_name += "'" + EnterpriseList[i].Ent_Name + "'" + ",";
            }
            html_name += "];";

            string html_pic = "var logo = [";
            for (int i = 0; i < EnterpriseList.Count; i++)
            {
                DataContext.SetFiled("UnitID", EnterpriseList[i].Ent_No);
                var logo = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的Logo").FirstOrDefault();
                var logo2= logo == null ? "#" : logo.DLFileUrl;
                html_pic += "'" + logo2 + "'" + ",";
            }
            html_pic += "];";

            ViewBag.Html_id = html_id;
            ViewBag.Html_name = html_name;
            ViewBag.Html_pic = html_pic;
                //var q = (from f in ec.T_Enterprise where f.EntCategoryID == EntType  select f);
            return View();
        }

        //public ActionResult ShowEnrollEnt(){
        //获取当前批次

        //return KonwAboutEnt("0" , PracBatchID);
        //}

        [BaseActionFilter]
        public string getStuPracBatchID(){

        string strStuPracBatchID="";
        string stuUserID = "";
        string IsCurrent = "是";
        stuUserID = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;//获取用户号
        //查询条件：学生的用户号，学生批次与企业注册批次相同，批次必须是当前批次（可以避免重修学生批次的混乱，只有当前批次的学生可以进行这个操作）
        List<T_StuBatchReg> StuBatchRegList = (from StuBatch in sc.StuBatchReg
                                               from PracBatch in sc.PracBatch
                                               where StuBatch.UserID == stuUserID && StuBatch.PracBatchID == PracBatch.PracBatchID && PracBatch.IsCurrentBatch == IsCurrent 
                                               select StuBatch).ToList();
            if (StuBatchRegList.Count > 0){
                strStuPracBatchID = StuBatchRegList[0].PracBatchID;
            }



        return strStuPracBatchID;
    }

        [BaseActionFilter]
        public string getStuPracNo(string UserID, string ProcBatchID)//获取学生的实习编号
        {

            string strStuPracNo = "";
            List<T_StuBatchReg> StuBatchRegList = (from StuBatch in sc.StuBatchReg
                                                   where StuBatch.UserID == UserID && StuBatch.PracBatchID == ProcBatchID 
                                                   select StuBatch).ToList();
            if (StuBatchRegList.Count > 0)
            {
                strStuPracNo = StuBatchRegList[0].PracticeNo;
            }

            return strStuPracNo;
        }

        [BaseActionFilter]
        public ActionResult menuLinkToEnrollEnt()
        {
            string UnitID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            string PracBatchID = getStuPracBatchID();
            string hLink = "/School/S_PraEnrollManager/KonwAboutEnt?EntType=1&PracBatchID=" + PracBatchID;
            return Redirect(hLink);
        }

        //我要报名
        [BaseActionFilter]
        public ActionResult IWantPrac(string Entid)
        {
            if(Request.Form["batch"]!=null)
            {
                Entid = Request.Form["batch"];
            }
            ViewBag.NowOpEntID = Entid;
            //获取该高校的当前批次
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string practiceno = SchoolHelper.GetStuPracticeNo(userid);
            //判断学生是否存在于当前批次
            var Stu_Reged = (from StuBatchReg in sc.StuBatchReg where StuBatchReg.PracBatchID == prcbatchid && StuBatchReg.UserID == userid select StuBatchReg).ToList(); ;
            if (Stu_Reged.Count == 0)
            {
                return Content("您还未被注册到当前批次，无法报名，请联系学校管理员进行批次注册！");
            }
            ViewBag.NowBatch = prcbatchid;
            string SelectedEntpracticeNo="";
            string SelectedEntName = "";
            //获取参加该批次的企业
            var q1 = (from f in ec.T_EntBatchReg where f.PracBatchID == prcbatchid &&f.ApplyStatus==2 select f).ToList();
            List<SelectListItem> EntList = new List<SelectListItem>();
            for (int i = 0; i < q1.Count; i++)
            {
                string entino = q1[i].Ent_No;
                string entyname = q1[i].Ent_Name;
                if (q1[i].Ent_No == Entid)
                {
                    EntList.Add(new SelectListItem() { Text = entyname, Value = q1[i].Ent_No, Selected = true });
                    SelectedEntpracticeNo = q1[i].EntPracNo;
                    //同时获得选中企业的“企业实习号”
                    SelectedEntName = q1[i].Ent_Name;                  
                }
                else
                {
                    EntList.Add(new SelectListItem() { Text = entyname, Value = q1[i].Ent_No, Selected = false });
                }
            }
            ViewData["EntList"] = EntList;//参加该批次的企业列表

            if (q1.Count != 0 && SelectedEntpracticeNo == "")
            {
                SelectedEntpracticeNo = q1[0].EntPracNo;
                SelectedEntName = q1[0].Ent_Name;      
            }
            //获取企业提供给该高校的职位     
            ViewBag.EntProNo = SelectedEntpracticeNo;
            ViewBag.SelectedEntName = SelectedEntName;
            var q2 = (from f in ec.T_PracticePosition where f.EntPracNo == SelectedEntpracticeNo select f).ToList();
            string[] posname = new string[q2.Count];
            string[] time = new string[q2.Count];
            string[] enrollnum = new string[q2.Count];//岗位已报人数
            for (int i = 0; i < q2.Count; i++)
            {
                string temp = q2[i].PositionID;
                time[i] = DateTimeFormat.ConvertIntDateTime(q2[i].PosExpireDate).ToShortDateString();
                C_Position cp = ctc.C_Position.Find(temp);
                posname[i] = cp.PositionName;

                var q_enrollnum = (from f in sc.PracticeVolunteer where f.EntPracNo == SelectedEntpracticeNo && f.PosID == temp && f.PreVolStatus == "1" select f).ToList();
                enrollnum[i] = q_enrollnum.Count.ToString();
            }
            ViewBag.PositionName = posname;
            ViewBag.Time = time;
            ViewBag.Num = enrollnum;
            ViewBag.ThisEntPosList = q2;

            //处理预报名
            #region 处理预报名
            var q_PerEnroll = (from f in sc.PracticeVolunteer where f.PracticeNo == practiceno && f.PreVolStatus == "0" select f).ToList();

            string[] Pre_EnrollNum = new string[q_PerEnroll.Count];
            for (int i = 0; i < q_PerEnroll.Count; i++)
            {
                string temp = q_PerEnroll[i].PosID;
                string temp1 = q_PerEnroll[i].EntPracNo;
                var q_enrollnum = (from f in sc.PracticeVolunteer where f.EntPracNo == temp1 && f.PosID == temp && f.PreVolStatus == "1" select f).ToList();
                Pre_EnrollNum[i] = q_enrollnum.Count.ToString();
            }

            ViewBag.PerEnrollTable = q_PerEnroll;
            ViewBag.Pre_EnrollNum = Pre_EnrollNum;
            #endregion

            //处理第一第二志愿
            #region 处理第一第二志愿
            var q_RealEnroll_V1 = (from f in sc.PracticeVolunteer where f.PracticeNo == practiceno && f.VolunteerSequence == 1 && f.PreVolStatus == "1" select f).ToList();
            var q_RealEnroll_V2 = (from f in sc.PracticeVolunteer where f.PracticeNo == practiceno && f.VolunteerSequence == 2 && f.PreVolStatus == "1" select f).ToList();


            string[] EnrollNum_V1 = new string[q_RealEnroll_V1.Count];//已报人数
            string[] EnrollNum_V2 = new string[q_RealEnroll_V2.Count];//已报人数

            for (int i = 0; i < q_RealEnroll_V1.Count;i++ )
            {
                string temp = q_RealEnroll_V1[i].PosID;
                string temp1 = q_RealEnroll_V1[i].EntPracNo;
                var q_enrollnum_v1 = (from f in sc.PracticeVolunteer where f.EntPracNo == temp1 && f.PosID == temp && f.PreVolStatus == "1"  select f).ToList();
                EnrollNum_V1[i] = q_enrollnum_v1.Count.ToString();
            }

            for (int i = 0; i < q_RealEnroll_V2.Count; i++)
            {
                string temp = q_RealEnroll_V2[i].PosID;
                string temp1 = q_RealEnroll_V2[i].EntPracNo;
                var q_enrollnum_v2 = (from f in sc.PracticeVolunteer where f.EntPracNo == temp1 && f.PosID == temp && f.PreVolStatus == "1" select f).ToList();
                EnrollNum_V2[i] = q_enrollnum_v2.Count.ToString();
            }

            ViewBag.RealEnrollTable_V1 = q_RealEnroll_V1;
            ViewBag.RealEnrollTable_V2 = q_RealEnroll_V2;
            ViewBag.EnrollNum_V1 = EnrollNum_V1;
            ViewBag.EnrollNum_V2 = EnrollNum_V2;
            #endregion

            return View();
        }

        //查看详情
        [BaseActionFilter]
        public ActionResult ViewDetail(string entpracno, string posid)
        {
            T_PracticePosition tp = ec.T_PracticePosition.Find(entpracno, posid);
            return Content(tp.PosDesc);//返回上一页并刷新

        }
        //从岗位列表跳转到企业详情
        public ActionResult TurnEnt(string EntPraNo)
        {
            var firstOrDefault = ec.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == EntPraNo);
            if (firstOrDefault != null)
            {
                var entno=firstOrDefault.Ent_No;
                return Redirect("/Enterprise/Home/T_Enterprise_Show?Ent_No="+ entno);
                // return View();
            }
            else
            {
                return Alert("出错了");
            }
        }

        //预报名
        [BaseActionFilter]
        public ContentResult PreEnroll(string pracno, string entpracno, string posid, string vol, string order,string nowopentid)
        {
            string returnurl = "/School/S_PraEnrollManager/IWantPrac?Entid=" + nowopentid;
            if (pracno != null)
            {
                //string userid = "10385132511XXX1";
                string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                T_PracticeVolunteer tp = new T_PracticeVolunteer();
                tp.PracticeNo = userid + pracno;
                tp.EntPracNo = entpracno;
                tp.PosID = posid;
                T_PracticeVolunteer tp_check = sc.PracticeVolunteer.Find(userid + pracno, entpracno, posid);
                if (tp_check != null)
                {
                    //return RedirectToAction("Results", "SubmitResults", new { results = "该志愿/顺序下的岗位已存在" });
                    return Content("<script>alert('该志愿/顺序下的岗位已存在');window.location = '" + returnurl + "';</script>");//返回上一页并刷新
                }
                //string vol = Request.Form["volunteer"];
                //string order = Request.Form["vorder"];
                if (vol == "firstv")
                {
                    tp.VolunteerSequence = 1;
                }
                else
                {
                    tp.VolunteerSequence = 2;
                }
                if (order == "1")
                {
                    tp.PosSequence = 1;
                }
                else
                {
                    tp.PosSequence = 2;
                }
                tp.VolunteerStatus = 0;
                tp.PreVolStatus = "0";
                sc.PracticeVolunteer.Add(tp);
                sc.SaveChanges();
                return Content("<script>alert('预报名成功');window.location = '" + returnurl + "';</script>");//返回上一页并刷新
            }
            else
            {
                return Content("<script>window.location = '" + returnurl + "';</script>");//返回上一页并刷新
            }
        }

        //正式报名
        [BaseActionFilter]
        public ActionResult RealEnroll(string pracno, string entpracno, string posid,string nowopentid)
        {
            string returnurl = "/School/S_PraEnrollManager/IWantPrac?Entid=" + nowopentid;
            //检测同一个公司的岗位只能存在于同一志愿下
            //var q_check = (from f in sc.PracticeVolunteer where f.PracticeNo == pracno && f.EntPracNo == entpracno select f.PosID).ToList();
            //if(posid!=q_check)
            //T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            var temp = (from f in sc.PracticeVolunteer where f.PracticeNo == pracno && f.EntPracNo == entpracno && f.PosID == posid select f).ToList();
            int vol = temp[0].VolunteerSequence;//志愿
            int order = temp[0].PosSequence;//顺序
            //查找当前批次是否存在已经正式报名的某志愿某顺序
            var q = (from f in sc.PracticeVolunteer where f.PracticeNo == pracno && f.VolunteerSequence == vol && f.PosSequence == order && f.PreVolStatus == "1" && f.PosID != posid select f).ToList();
            if (q == null || q.Count == 0)
            {
                var check = (from f in sc.PracticeVolunteer where f.PracticeNo == pracno && f.EntPracNo == entpracno && f.VolunteerSequence != vol && f.PreVolStatus == "1"  select f).ToList();
                if (check.Count == 0)
                {
                    T_PracticeVolunteer tp = temp[0];
                    tp.PreVolStatus = "1";
                    sc.SaveChanges();
                    return Content("<script>alert('正式报名成功，请到修改报名中查看');window.location = '" + returnurl + "';</script>");//返回上一页并刷新
                }
                else
                {
                    return Content("<script>alert('您已在同一个企业下提交了志愿，请将此志愿修改为与已提交企业志愿相同的志愿顺序，并修改岗位顺序！');window.location = '" + returnurl + "';</script>");//返回上一页并刷新
                }
            }
            else
            {
                return Content("<script>alert('该志愿-顺序下存在已经正式报名的岗位！');window.location = '" + returnurl + "';</script>");//返回上一页并刷新
            }

        }

        //删除报名
        [BaseActionFilter]
        public ActionResult DeletePreEnroll(string pracno, string entpracno, string posid, string nowopentid)
        {
            string returnurl = "/School/S_PraEnrollManager/IWantPrac?Entid=" + nowopentid;
            T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            if (tp != null)
            {
                sc.PracticeVolunteer.Remove(tp);
                sc.SaveChanges();
            }
            return Content("<script>window.location = '" + returnurl + "';</script>");//返回上一页并刷新

        }

        //修改报名页面
        [BaseActionFilter]
        public ActionResult ModifyEnroll()
        {
            //获取该高校的当前批次
            //string schoolid = "10385";
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            var q = (from f in sc.PracBatch where f.SchoolID == schoolid && f.IsCurrentBatch == "是" select f).ToList();
            //string userid = "10385132511XXX1";
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            string PracticeNo = userid + q[0].PracBatchID;

            var q_PerEnroll = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.PreVolStatus == "1" select f).ToList();
            string[] PosName_1 = new string[q_PerEnroll.Count];//志愿1
            string[] PosName_2 = new string[q_PerEnroll.Count];//志愿二
            string[] Desc_1 = new string[q_PerEnroll.Count];//岗位描述
            string[] Desc_2 = new string[q_PerEnroll.Count];//岗位描述
            string[] Time_1 = new string[q_PerEnroll.Count];//截止时间
            string[] Time_2 = new string[q_PerEnroll.Count];//截止时间
            int index1 = 0;
            int index2 = 0;
            for (int i = 0; i < q_PerEnroll.Count; i++)
            {
                string posid = q_PerEnroll[i].PosID;
                string EntProNo = q_PerEnroll[i].EntPracNo;
                T_PracticePosition tpp = ec.T_PracticePosition.Find(EntProNo, posid);

                var q_temp = (from f in ctc.C_Position where f.PositionID == posid select f.PositionName).ToList();
                if (q_PerEnroll[i].VolunteerSequence == 1)
                {
                    PosName_1[index1] = q_temp[0];
                    Desc_1[index1] = tpp.PosDesc;
                    Time_1[index1] = DateTimeFormat.ConvertIntDateTime(tpp.PosExpireDate).ToString();
                    index1++;
                }
                else
                {
                    PosName_2[index2] = q_temp[0];
                    Desc_2[index2] = tpp.PosDesc;
                    Time_2[index2] = DateTimeFormat.ConvertIntDateTime(tpp.PosExpireDate).ToString();
                    index2++;
                }

            }
            ViewBag.PosName_1 = PosName_1;
            ViewBag.PosName_2 = PosName_2;
            ViewBag.Desc_1 = Desc_1;
            ViewBag.Time_1 = Time_1;
            ViewBag.Desc_2 = Desc_2;
            ViewBag.Time_2 = Time_2;
            return View(q_PerEnroll);
        }


        //招聘确认
        [BaseActionFilter]
        public ActionResult ConfirmVolunteer(string ReadOnly)
        {
            //获取该高校的当前批次
            //string schoolid = "10385";
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            var q = (from f in sc.PracBatch where f.SchoolID == schoolid && f.IsCurrentBatch == "是" select f).ToList();
            //string userid = "10385132511XXX1";
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            string PracticeNo = getStuPracNo(userid,q[0].PracBatchID);

            var q_PerEnroll = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.PreVolStatus == "1" select f).ToList();


            //是否禁止当前页面的所有操作
            var qqq = (from f in sc.StuBatchReg where f.PracticeNo == PracticeNo select f.CareerStatusID).ToList();
            if(qqq.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "你的实习资料有误，请咨询高校管理员。（错误原因：找不到职业生涯状态）" });
            }       
            if (qqq[0] >= 13)
            {
                ReadOnly = "true";
            }
            else
            {
                ReadOnly = "false";
            }
            ViewBag.ReadOnly = ReadOnly;
            ViewBag.Fenge = "|";
            return View(q_PerEnroll);
        }


        //确认已经面试通过的岗位
        [BaseActionFilter]
        public ActionResult ConfrimEnroll(string pracno, string entpracno, string posid)
        {
            T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            if (tp != null)
            {
                tp.VolunteerStatus = 5;
                sc.SaveChanges();
            }
            T_StuBatchReg ts = sc.StuBatchReg.Find(pracno);
            if (ts != null)
            {
                ts.CareerStatusID = 13;//职业生涯修改为招聘结束
                //将其他所有志愿改为“已被其他企业录取”
                var q = (from f in sc.PracticeVolunteer where f.PracticeNo == pracno && f.PosID != posid && f.PreVolStatus == "1" select f).ToList();
                for (int i = 0; i < q.Count;i++ )
                {
                    T_PracticeVolunteer temp = q[i];
                    temp.VolunteerStatus = 7;
                    sc.SaveChanges();
                }
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //拒绝已经面试通过的岗位
        [BaseActionFilter]
        public ActionResult DenyEnroll(string pracno, string entpracno, string posid)
        {
            T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            if (tp != null)
            {
                tp.VolunteerStatus = 6;
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }
    }
}
