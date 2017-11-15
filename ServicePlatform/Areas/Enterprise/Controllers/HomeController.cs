using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Lib;
using ServicePlatform.Config;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Core.Services;
using ServicePlatform.App_Start;
using ServicePlatform.Areas.Enterprise.Models.Home;
using ServicePlatform.Areas.Permission.Models;
using ServicePlatform.Controllers.Base;
using xyj.Plugs;
using Qx.Scsxxt.Core.Services;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class HomeController : BaseController, ICutPage, IModelNote
    {


        //
        // GET: /Enterprise/Home/
        EnterpriseContext db = new EnterpriseContext();
        CodeTableContext ct = new CodeTableContext();
        SchoolContext sc = new SchoolContext();
        UserContext uc = new UserContext();
        PermissionContext pc = new PermissionContext();
        private IAppService _app;
        public HomeController(IAppService app)
        {
            _app = new AppService();
        }
        #region 企业首页
        // GET: /Enterprise/Home/Index?Ent_No=
        public ActionResult Index(string Ent_No)
        {
            if (!Ent_No.HasValue())
            {
                Ent_No = DataContext.UserUnit;
            }

            DataContext.SetFiled("UnitID", Ent_No);
            DataContext.SetFiled("Ent_No", Ent_No);
            var model = new Index()
            {
                Enterprise = S<Core.Services.Entity.T_Enterprise>().Find(Ent_No),
                NewaList = S<Core.Services.Entity.T_HomePageContent>().All(DataContext, "某单位的新闻栏目"),
                DownloadList = S<Core.Services.Entity.T_HomePageContent>().All(DataContext, "某单位的下载栏目"),
                PracticePositionList = S<Core.Services.Entity.T_PracticePosition>().All(DataContext, "企业提供的所有岗位"),
                AdList =S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的轮播广告图"),
                PicList =S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的介绍图集"),
                LogoList =S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的Logo"),
                ResFileList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的资源文件"),
                VideoList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的视频集"),
                PPTList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的PPT集"),

            };
            return View(model);

        }
        #endregion

        #region   省市联动
        [HttpPost]
        public JsonResult GetCityList()
        {
            string ProvinceID = Request["provinveCode"].ToString();
            //string ProvinceID = "350000";
            var City = (from t in ct.C_City where t.ProvinceID == ProvinceID select t).ToList();
            return Json(new { count = City.Count, Pos = City });
        }


        public JsonResult GetTownList()
        {
            string CityID = Request["cityCode"].ToString();
            var Town = (from t in ct.C_County where t.CityID == CityID select t).ToList();
            return Json(new { count = Town.Count, Pos = Town });
        }

        #endregion

        #region   企业类型联动
        [HttpPost]
        public JsonResult GetEntRankList()
        {
            string EntCategoryID = Request["EntCategoryID"].ToString();
            //string ProvinceID = "350000";
            var Rank = (from t in ct.C_EntRank where t.EntCategoryID == EntCategoryID select t).ToList();
            return Json(new { count = Rank.Count, Pos = Rank });
        }


    

        #endregion

        #region   实习编号联动
        [HttpPost]
        public JsonResult getPracBatchIDList()
        {
            string SchoolID = Request["SchoolID"].ToString();

            var Rank = (from t in sc.PracBatch where t.SchoolID == SchoolID && t.IsCurrentBatch == "是" select t).ToList();
            return Json(new { count = Rank.Count, Pos = Rank });
        }


       
        #endregion

        #region 企业资料
        



        [BaseActionFilter]
        public ActionResult T_Enterprise_Edict(string Ent_No)
        {
            if (DataContext.UserUnit != Ent_No)
            {
                return Alert("没有权限更改",-1);
            }
            DataContext.SetFiled("UnitID", Ent_No);
            DataContext.SetFiled("Ent_No", Ent_No);
            var model = new T_Enterprise_Edict()
            {
                Enterprise = S<Core.Services.Entity.T_Enterprise>().Find(Ent_No),
                AdList = 
                    S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的轮播广告图"),
                PicList = 
                    S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的介绍图集"),
                VideoList = 
                    S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的视频集"),
                PPTList = 
                    S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的PPT集"),
                ResourceList =
                    S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的资源文件"),
                LogoList = 
                    S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的Logo"),

            };
            ViewBag.flag = "T_Enterprise_Edict";
            return View("T_Enterprise_Add", model);

        }
        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilter]public ActionResult T_Enterprise_Edict(T_Enterprise f,
            HttpPostedFileBase EntLogo,
            HttpPostedFileBase[] EntAdPics,
            HttpPostedFileBase[] EntPhotos,
           // HttpPostedFileBase[] EntVideos,
            HttpPostedFileBase[] EntPPTs,
            HttpPostedFileBase[] EntFiles)
        {



            var old = (from t in db.T_Enterprise
                       where t.Ent_No == f.Ent_No
                       select new List<string>()
                         {
                            t.EntLogo,
                            t.EntAdPics,
                            t.EntPhotos,
                            t.EntVideos,
                            t.EntPPTs,
                            t.EntFiles
                           }  
                           ).FirstOrDefault();
                //old.UserID = "-1";
                f.EntLogo = f.EntLogo != null ? FileOperate.UpdateOldFile(old[0], EntLogo, "/UserFiles/Enterprise/" + f.Ent_No + "/EntLogo/") : old[0];
                f.EntAdPics = f.EntAdPics != null ? FileOperate.UpdateOldFile(old[1], EntAdPics, "/UserFiles/Enterprise/" + f.Ent_No + "/EntAdPics/") : old[1];
                f.EntPhotos = f.EntPhotos != null ? FileOperate.UpdateOldFile(old[2], EntPhotos, "/UserFiles/Enterprise/" + f.Ent_No + "/EntPhotos/") : old[2];
                //f.EntVideos = f.EntVideos != null ? FileOperate.UpdateOldFile(old[3], EntVideos, "/UserFiles/Enterprise/" + f.Ent_No + "/EntVideos/") : old[3];
                f.EntPPTs = f.EntPPTs != null ? FileOperate.UpdateOldFile(old[4], EntPPTs, "/UserFiles/Enterprise/" + f.Ent_No + "/EntPPTs/") : old[4];
                f.EntFiles = f.EntFiles != null ? FileOperate.UpdateOldFile(old[5], EntFiles, "/UserFiles/Enterprise/" + f.Ent_No + "/EntFiles/") : old[5];

            
           
            //f.UserID = UserID;

            f.UpdateTime = DateTimeFormat.GetTime();
            f.EntState = 1;//默认不锁
            f.InfoLocked = -1;//默认不锁
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return Alert("保存成功", "/Enterprise/Home/T_Enterprise_Edict?Ent_No="+f.Ent_No);
        }

        [BaseActionFilter]
        public ActionResult T_Enterprise_ApplyCheck(string Ent_No)
        {
            var old = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();
            if (old != null)
            {
                old.EntState = 2;//审核中
                old.InfoLocked = 1;//锁定
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Enterprise/Home/T_Enterprise_Show?Ent_No=" + Ent_No );
            }
            else
            {
                return Redirect("/Home/ErrorPage?ErrorCode=" + "EntNoExist");
            }
           
        }
        [BaseActionFilter]public ActionResult T_Enterprise_ApplyUnlock(string Ent_No)
        {

            var old = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();
            if (old != null)
            {
               
                old.InfoLocked = 0;//申请解锁
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Enterprise/Home/T_Enterprise_Show?Ent_No=" + Ent_No);
            }
            else
            {
                return Redirect("/Home/ErrorPage?ErrorCode=" + "EntNoExist");
            }
         
        }
        [BaseActionFilter]public ActionResult T_Enterprise_Show(string Ent_No = null)
        {
            Ent_No= Ent_No ?? (Session["Vars"] as Vars).UserUnit;
            return View(S<Core.Services.Entity.T_Enterprise>().Find(Ent_No));

        }
        
        public ActionResult T_Enterprise_Show2(string Ent_No = null)
        {
            V("Ent_No", Ent_No);
            return View();

        }
        #endregion

        #region 企业申请批次
        /*
        ApplyStatusID	ApplyStatusName
            -1	未提交
            1	待审核
            2	审核通过
            3	未通过
            4	已删除

         */

        //申请高校实习
        [BaseActionFilter]
        public ActionResult T_EntBatchReg_Add()
        {
            return View();
        }
        
         [HttpPost]
        [BaseActionFilter]
        public ActionResult T_EntBatchReg_Add(T_EntBatchReg f)
        {
            string SchoolID = Request.Form["SchoolID"];
            Vars vars = (Session["Vars"] as Vars);
            f.Ent_No = vars.UserUnit.Replace("Ent_No:", "");
            f.EntPracNo = f.Ent_No+"-" + f.PracBatchID;
            if (db.T_EntBatchReg.Find(f.EntPracNo) != null)
            {
                return Content("<script type='text/javascript'>alert('已提交过申请,无需重复提交...') ;history.go(-1)</script >");
            }
            else
            {
                f.ApplyStatus = 1;//待审核
                f.RegistTime = DateTimeFormat.GetTime();
               // f.Ent_No = vars.UserUnit.Replace("Ent_No:", "");
               // f.EntPracNo = f.Ent_No + f.PracBatchID;
                db.T_EntBatchReg.Add(f);
                db.SaveChanges();
                return Content("<script type='text/javascript'>alert('提交成功,请耐心等待平台审核') ;history.go(-1)</script >");
            }
           
        }
         //查看申请列表
         [BaseActionFilter]
        public ActionResult T_EntBatchReg_ListShow(int perCount = 8, int pageIndex = 1)
         {
             Vars vars=(Session["Vars"]as Vars);
             var data = (from t in db.T_EntBatchReg where t.Ent_No == vars.UserUnit.Replace("Ent_No:", "") select t).ToList();//筛选数据
             ViewBag.addLink = "T_EntBatchReg_Add";
             this.CutPage<
                 /*数据源模型*/T_EntBatchReg
                           , T_EntBatchReg>(
                 /*视图数据源模型*/this.GetModelNote<T_EntBatchReg>()
                           , this, perCount, pageIndex
                           ,/*数据源*/data
                           ,/*视图数据源*/data
                           ,/*要显示的视图列索引*/new int[] {  1,4, 9}
                           ,/*页面标题*/"批次列表"
                           
                           );

             return View();
         }
        #endregion

        #region 企业添加实习岗位
     

         //添加岗位
         [BaseActionFilter]
         public ActionResult T_PracticePosition_Add()
         {
             return View();
         }

         [HttpPost]
         [BaseActionFilter]
         public ActionResult T_PracticePosition_Add(T_PracticePosition f)
         {
             f.PosDesc = f.PosDesc.Replace("\r\n","<br/>");
             var old=db.T_PracticePosition.Find(f.EntPracNo, f.PositionID);
             if (old == null)
             {
                 f.PubDate = DateTimeFormat.ConvertDateTimeInt(DateTimeFormat.ChekInputDate(Request.Form["PubDate"]));
                 f.PosExpireDate = DateTimeFormat.ConvertDateTimeInt(DateTimeFormat.ChekInputDate(Request.Form["PosExpireDate"]));
                 try
                 {
                     db.T_PracticePosition.Add(f);
                     db.SaveChanges();
                 }
                 catch (Exception ex)
                 {
                    return Content("<script type='text/javascript'>alert('提交失败，请重试（信息填写完整了吗？）') ;history.go(-1)</script >");

                }

                return RedirectToAction("T_PracticePosition_ListShow");
                 
             }
             else
             {
                 return Content("<script type='text/javascript'>alert('提交失败，已存在该岗位编号') ;history.go(-1)</script >");

             }
             

         }
         //编辑岗位
         [BaseActionFilter]
         public ActionResult T_PracticePositionEdit(string EntPracNo, string PositionID)
         {
             var old = db.T_PracticePosition.Find(EntPracNo, PositionID);
            
             ViewBag.data = old;
             return View();
         }
         [HttpPost]
         [BaseActionFilter]
         public ActionResult T_PracticePositionEdit(T_PracticePosition f)
         {
             f.PubDate = DateTimeFormat.ConvertDateTimeInt(DateTimeFormat.ChekInputDate(Request.Form["PubDate"]));
             f.PosExpireDate = DateTimeFormat.ConvertDateTimeInt(DateTimeFormat.ChekInputDate(Request.Form["PosExpireDate"]));
             db.Entry(f).State = EntityState.Modified;
             db.SaveChanges();
             return RedirectToAction("T_PracticePosition_ListShow");
         }
         //删除岗位
         [BaseActionFilter]
         public ActionResult T_PracticePositionDelete(string EntPracNo, string PositionID)
         {
             var old = db.T_PracticePosition.Find(EntPracNo, PositionID);
             if(old!=null)
             {
                 try
                 {
                    db.Entry(old).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                 catch (Exception)
                 {

                    return Content("<script type='text/javascript'>alert('删除失败，已有学生报名该岗位') ;history.go(-1)</script >");

                }


            }
            
             return RedirectToAction("T_PracticePosition_ListShow");
         }
         //查看申请列表
         [BaseActionFilter]
         public ActionResult T_PracticePosition_ListShow(string EntPracNo = "", int perCount = 8, int pageIndex = 1)
         {
            var data=new List<Core.Services.Entity.T_PracticePosition>();
             if (!string.IsNullOrEmpty(EntPracNo))
             {
                DataContext.SetFiled("EntPracNo", EntPracNo);
                data = S<Core.Services.Entity.T_PracticePosition>().All(DataContext, "企业提供给某个学校的岗位");
                ViewBag.tip = "岗位数：" + data.Count() + "  招聘人数：" + data.Sum(a => a.PosQuantity);
            }
             return View(data);
         }
         #endregion

        #region 密码修改
         [BaseActionFilter]
         public ActionResult ChgPsw()
         {
             return View();
         }
         [HttpPost]
         [BaseActionFilter]
         public ActionResult ChgPsw(T_Class dd)
         {
             Vars vars = (Session["Vars"] as Vars);
             string OldPsw = Request.Form["OldPsw"];
             string NewPsw = Request.Form["NewPsw"];
             string NewPsw2 = Request.Form["NewPsw"];
             if (OldPsw == "" || NewPsw == "" || NewPsw2 == "")
             {
                 Response.Write("<script type='text/javascript'>alert('密码不能为空') </script >");
         
             }else if(NewPsw2!=NewPsw)
             {
                 Response.Write("<script type='text/javascript'>alert('2次输入的新密码不一致') </script >");

             }
             else if (Permission.Controllers.HomeController.UserPswChg(vars.UserID, OldPsw, NewPsw) == false)
             {
                 Response.Write("<script type='text/javascript'>alert('旧密码错误') </script >");
             }
             else 
             {
                 Response.Write("<script type='text/javascript'>alert('修改成功，下次请用新密码登录') </script >");
             }
             
             return View();
         }
        #endregion
 
         //学校编号---学校实习批次号（当前有效的只有1个）
         private T_PracBatch GetCurrentPracBatch(string SchoolID)
         {

             var temp=(from t in sc.PracBatch where t.SchoolID == SchoolID  && t.IsCurrentBatch == "是" select t).FirstOrDefault();

             return temp;
         }
         //学校实习批次号、企业号--企业申报某学校 且通过的 批次号（1个）
         private T_EntBatchReg GetPassedEntBatchReg(string CurrentPracBatch, string Ent_No)
         {

             var temp = (from t in db.T_EntBatchReg
                         where t.PracBatchID == CurrentPracBatch
                             && t.Ent_No == Ent_No
                             &&t.ApplyStatus==2
                         select t).FirstOrDefault();

             return temp;
         }
         // 企业申报某学校的批次号（1个）--企业申报某学校 提供的岗位号（多个）
         private List<T_PracticePosition> GetPracticePosition(string PracBatchID)
         {
             return db.T_PracticePosition.Where(m => m.EntPracNo == PracBatchID).ToList();
         }
         //岗位ID --该岗位的填报情况
         private List<T_PracticeVolunteer> GetPracticeVolunteer(string PositionID)
         {

             List<T_PracticeVolunteer> PracticeVolunteerList = (from t in sc.PracticeVolunteer
                                                                where t.PosID == PositionID
                                                                select t).ToList();
             return PracticeVolunteerList;
         }
         //企业ID --该企业申请通过的学校
         private List<T_School> GetPassedPracticeSchool(string Ent_No)
         {
             
             List<T_EntBatchReg> PracticeVolunteerList = (from t in db.T_EntBatchReg
                                                                where t.Ent_No == Ent_No
                                                                select t).ToList();
                        
             List<T_PracBatch>PracBatchList=new List<T_PracBatch>();
             for(int i=0;i<PracticeVolunteerList.Count;i++)
             {
               string PracBatchID=  PracticeVolunteerList[i].PracBatchID;
               PracBatchList.AddRange((from t in sc.PracBatch
                                       where t.PracBatchID == PracBatchID
                                       select t
                                           ).ToList());                 
             }
             
           
             List<T_School> SchoolList = new List<T_School>();
             for (int i = 0; i < PracBatchList.Count; i++)
             {
                 string SchoolID=PracBatchList[i].SchoolID;
                 SchoolList.AddRange((from  t in sc.School 
                                          where t.SchoolID==SchoolID
                                          select t
                                          ).ToList()
                     );
             }
             //去除重复学校
             return SchoolList.Distinct<T_School>().ToList();
         }
         //根据企业ID(session中获取企业编号)获取申请过的学校
          [BaseActionFilter]
         public List<SelectListItem> GetPassedPracticeSchoolItem(string SchoolID)
         {
             Vars vars = (Session["Vars"] as Vars);
             List<T_School> SchoolList = GetPassedPracticeSchool(vars.UserUnit);

            

             List<SelectListItem> items = new List<SelectListItem>();
             for (int i = 0; i < SchoolList.Count; i++)
             {
                 if (SchoolList[i].SchoolID == SchoolID)
                 {
                     items.Add(new SelectListItem() { Value = SchoolList[i].SchoolID, Text = SchoolList[i].SchoolName, Selected = true });
                 }
                 else
                 {
                     items.Add(new SelectListItem() { Value = SchoolList[i].SchoolID, Text = SchoolList[i].SchoolName, Selected = true });
                 }
                 
             }
             items.Add(new SelectListItem() { Value = "-1", Text = "请选择学校", Selected = true });
             return items;
         }
         //根据学校ID(session中获取企业编号)获取提供的实习岗位
          [BaseActionFilter]
          public List<SelectListItem> GetPracticePositionItem(string SchoolID, string PositionID)
         {
             Vars vars = (Session["Vars"] as Vars);
             string CurrentPracBatch= GetCurrentPracBatch(SchoolID).PracBatchID;
             string PassedEntBatchReg= GetPassedEntBatchReg(CurrentPracBatch,vars.UserUnit)==null?"": GetPassedEntBatchReg(CurrentPracBatch,vars.UserUnit).EntPracNo;
             List<T_PracticePosition>PracticePositionList= GetPracticePosition(PassedEntBatchReg);

             List<SelectListItem> items = new List<SelectListItem>();
             items.Add(new SelectListItem() { Value = "-1", Text = "请选择岗位" });
             items.Add(new SelectListItem() { Value = "0", Text = "全部" });
    
             for (int i = 0; i < PracticePositionList.Count; i++)
             {
                 if (PositionID == PracticePositionList[i].PositionID)
                 {
                     items.Add(new SelectListItem() {Selected=true, Value = PracticePositionList[i].PositionID, Text = ct.C_Position.Find(PracticePositionList[i].PositionID).PositionName });
    
                 }
                 else
                 {
                     items.Add(new SelectListItem() { Value = PracticePositionList[i].PositionID, Text = ct.C_Position.Find(PracticePositionList[i].PositionID).PositionName });
    
                 }
                 
             }
             return items;
         }


         //查看所有志愿信息
        
//         public ActionResult T_PracticeVolunteer_ListShow(string actionFlag="面试管理",string SchoolID="-1",string PositionID="-1", int perCount = 8, int pageIndex = 1)
//          {//actionFlag=面试管理 实习结果管理 岗位指派管理

//             ViewBag.SchoolID = SchoolID;
//             ViewBag.PositionID = PositionID;


//             //数据源
//             List<T_PracticeVolunteer> dataList = new List<T_PracticeVolunteer>();
//             switch (actionFlag) 
//             {
//                 case "面试管理":
//                     {
//                         /*
//             select T_PracticeVolunteer.*
//from T_PracticeVolunteer inner join T_StuBatchReg on T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo
//where T_StuBatchReg.CareerStatusID='13'
//             */
//                         string SQL = " select T_PracticeVolunteer.* from T_PracticeVolunteer inner join T_StuBatchReg on T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo ";

//                         dataList=sc.Database.SqlQuery<T_PracticeVolunteer>(SQL).ToList();
//                        // dataList = (from t in sc.PracticeVolunteer where t.VolunteerStatus < 5 select t).ToList();
//                     }break;
//                 case "实习结果管理":
//                     {
//                         string SQL = " select T_PracticeVolunteer.* from T_PracticeVolunteer inner join T_StuBatchReg on T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo  ";

//                         dataList = sc.Database.SqlQuery<T_PracticeVolunteer>(SQL).ToList();
//                         //dataList = (from t in sc.PracticeVolunteer where t.VolunteerStatus == 5 || t.VolunteerStatus >6 select t).ToList();
//                     } break;
//                 case "岗位指派管理":
//                     {
//                         string SQL = " select T_PracticeVolunteer.* from T_PracticeVolunteer inner join T_StuBatchReg on T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo";

//                         dataList = sc.Database.SqlQuery<T_PracticeVolunteer>(SQL).ToList();
                   
//                         //dataList = (from t in sc.PracticeVolunteer where t.VolunteerStatus == 5  select t).ToList();
//                     } break;
//                 case "实习材料查看":
//                     {
//                         string SQL = " select T_PracticeVolunteer.* from T_PracticeVolunteer inner join T_StuBatchReg on T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo ";

//                         dataList = sc.Database.SqlQuery<T_PracticeVolunteer>(SQL).ToList();

//                         //dataList = (from t in sc.PracticeVolunteer where t.VolunteerStatus == 5  select t).ToList();
//                     } break;
//             }
            

//             Vars vars = (Session["Vars"] as Vars);
//             //传入学校
//             var PassedPracticeSchoolItem = GetPassedPracticeSchoolItem(SchoolID);
//             ViewBag.PassedPracticeSchoolItem =PassedPracticeSchoolItem;
//            //穿入操作方式
//             ViewBag.actionFlag = actionFlag;
             

//             if (SchoolID == "-1")
//             {
//                 //传入岗位
//                 ViewBag.PracticePositionItem = new List<SelectListItem>();
//                 //筛选数据 (-1为空) 
//                 dataList = new List<T_PracticeVolunteer>();
//             }
//             else
//             {
//                 //传入岗位
//                 var PracticePositionItem= GetPracticePositionItem(SchoolID, PositionID);
//                 ViewBag.PracticePositionItem = PracticePositionItem;
//                 //筛选数据 
//                 dataList = (from t in dataList where t.PracticeNo.Contains(SchoolID) select t).ToList();
//             }

//             if (PositionID == "0")
//             {
//                 //筛选数据(0不筛选) 
//                // data = (from t in data select t).ToList();
//             }
//             else if (PositionID == "-1")
//             {
//                 //筛选数据(-1不筛选) 
//                 // data = (from t in data select t).ToList();
//             }
//             else
//             {
//                 //筛选数据 
//                 dataList = (from t in dataList  where t.PosID==PositionID select t).ToList();
//             }


//             var data = dataList;
//             this.CutPage< T_PracticeVolunteer>(
//                 this.GetModelNote<T_PracticeVolunteer>()
//                 , this, perCount, pageIndex
//                 ,data
//                 ,new int[] {0, 1,2,3,4,5, 6, 7, 8,9, 10,11, 12, 13, 14, 15,16,17,18,19,20,21,22 }
//                 , actionFlag
//                 );

//             //自动选择第一个学校的全部职位
//             if (SchoolID=="-1"&&PassedPracticeSchoolItem.Count>0)
//             {
//                 SchoolID=PassedPracticeSchoolItem[0].Value;
//                   return Redirect("/Enterprise/Home/T_PracticeVolunteer_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + "0" + "");
     
//             }
           
//             return View("T_PracticeVolunteer_ListShow");


//         }

         //根据志愿安排面试

         /*
         VolStatusID	VolStatusName
             0	未安排面试时间
             1  已安排面试时间
             2	已面试
             3  未通过面试----
             4  通过面试
             5	学生同意,分配成功----
             6	学生拒绝,分配失败----
             7  已录入实习鉴定表
             8  已录入实习评价
          
          */

         #region 面试管理
         

         //安排面试时间
         [BaseActionFilter]
         [HttpPost]
         public ActionResult T_PracticeVolunteer_ArrangeTime()
         {
             string PracticeNo = Request.Form["PracticeNo"];
             string EntPracNo = Request.Form["EntPracNo"];
             string PosID = Request.Form["PosID"];
             string tempSchoolID = Request.Form["SchoolID"];
             string actionFlag = Request.Form["actionFlag"];

             var old = (from t in sc.PracticeVolunteer
                        where t.PracticeNo == PracticeNo &&
                        t.EntPracNo == EntPracNo &&
                        t.PosID == PosID
                        select t).FirstOrDefault();
             old.InterviewTime = DateTimeFormat.ConvertDateTimeInt(DateTimeFormat.ChekInputDate(Request.Form["InterviewTime"]));
             old.VolunteerStatus = 1;//1	已安排面试时间
            

             sc.Entry(old).State = EntityState.Modified;
             sc.SaveChanges();
             return Redirect("/Enterprise/Home/Interview_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + tempSchoolID + "&PositionID=" + PosID + "");
         }
       
         //录入面试情况
         [BaseActionFilter]
         [HttpPost]
         public ActionResult T_PracticeVolunteer_Interview()
         {
             string PracticeNo=Request.Form["PracticeNo"];
             string EntPracNo = Request.Form["EntPracNo"];
             string PosID = Request.Form["PosID"];
             string tempSchoolID = Request.Form["SchoolID"];
             string actionFlag = Request.Form["actionFlag"];

             var old = (from t in sc.PracticeVolunteer 
                        where t.PracticeNo == PracticeNo &&
                        t.EntPracNo == EntPracNo &&
                        t.PosID == PosID 
                        select t).FirstOrDefault();
             old.VolunteerStatus = 2;//2	已面试
             old.InterviewRecord = Request.Form["InterviewRecord"];
             old.InterviewScore = int.Parse((Request.Form["InterviewScore"]));
             old.Interviewee = Request.Form["Interviewee"];
             sc.Entry(old).State = EntityState.Modified;
             sc.SaveChanges();
             return Redirect("/Enterprise/Home/Interview_ListShow?actionFlag" + actionFlag + "&SchoolID=" + tempSchoolID + "&PositionID=" + PosID + "");
         }
         //录取
         [BaseActionFilter]
         public ActionResult T_PracticeVolunteer_Interview_OK(string actionFlag,string PracticeNo, string EntPracNo, string PosID, string SchoolID, int VolunteerStatus = 4)
         {
            

             var old = (from t in sc.PracticeVolunteer
                        where t.PracticeNo == PracticeNo &&
                        t.EntPracNo == EntPracNo &&
                        t.PosID == PosID
                        select t).FirstOrDefault();
            

             old.VolunteerStatus = VolunteerStatus;

             sc.Entry(old).State = EntityState.Modified;
             sc.SaveChanges();
             return Redirect("/Enterprise/Home/Interview_ListShow?actionFlag" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + PosID + "");
         }
         //不录取
         [BaseActionFilter]
         public ActionResult T_PracticeVolunteer_Interview_NotOK(string actionFlag,string PracticeNo, string EntPracNo, string PosID, string SchoolID, int VolunteerStatus = 3)
         {
             string tempSchoolID = Request.Form["SchoolID"];

             var old = (from t in sc.PracticeVolunteer
                        where t.PracticeNo == PracticeNo &&
                        t.EntPracNo == EntPracNo &&
                        t.PosID == PosID
                        select t).FirstOrDefault();
             old.VolunteerStatus = VolunteerStatus;

             sc.Entry(old).State = EntityState.Modified;
             sc.SaveChanges();
             return Redirect("/Enterprise/Home/Interview_ListShow?actionFlag" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + PosID + "");
         }
         //面试管理学生列表
         [BaseActionFilter]
        public ActionResult Interview_ListShow(string actionFlag = "面试管理", string SchoolID = "-1", string PositionID = "-1", int perCount = 50, int pageIndex = 1, 
             string keyword = "", int VolunteerSequence = 1, int PosSequence = 1, string StuSex = "男"
             )
        {//actionFlag=面试管理 实习结果管理 岗位指派管理
            ViewBag.actionURL = "Interview_ListShow";
            ViewBag.SchoolID = SchoolID;
            ViewBag.PositionID = PositionID;

            Vars vars = (Session["Vars"] as Vars);
            var Ent_No = vars.getUserUnit();
            //数据源
            var dataList = (from a in sc.PracticeVolunteer.ToList().ToList()
                            join b in db.T_StuBatchReg.ToList() on a.PracticeNo equals b.PracticeNo
                            join d in sc.PracBatch.ToList() on b.PracBatchID equals d.PracBatchID
                            join c in db.T_EntBatchReg.ToList() on a.EntPracNo equals c.EntPracNo
                            join e in db.Student on b.UserID equals e.UserID
                            join f in db.T_Class on e.StuClass equals f.ClassID
                            join g in db.C_Specialty on new { f.EntryYear, f.SchoolID, f.SpeNo } equals new { g.EntryYear, g.SchoolID, g.SpeNo }
                            where c.Ent_No == Ent_No &&
                            a.PreVolStatus == "1" &&
                            d.IsCurrentBatch == "是" &&
                            a.PosSequence == PosSequence &&
                            a.VolunteerSequence == VolunteerSequence &&
                            (e.StuNo.Contains(keyword) ||
                            e.StuName.Contains(keyword) ||
                            f.ClassName.Contains(keyword) ||
                            g.SpeName.Contains(keyword)||
                            e.StuSex.Contains(keyword)
                            )
                            select a
                ).ToList();
            //select new { a.PracticeNo, a.EntPracNo, a.PosID, a.VolunteerSequence,a.PosSequence,a.VolunteerStatus,a.InterviewTime,a.InterviewRecord,a.InterviewScore,a.Interviewee,a.PreVolStatus ,b.UserID}
            //中断！
            //if (dataList.Count == 0)
            //    return Content(PageHelper.Tip("批次未通过审核或无学生报名"));


            //传入学校
            var PassedPracticeSchoolItem = GetPassedPracticeSchoolItem(SchoolID);
            ViewBag.PassedPracticeSchoolItem = PassedPracticeSchoolItem;
            //传入操作方式
            ViewBag.actionFlag = actionFlag;
            if (SchoolID == "-1")
            {
                //传入岗位
                ViewBag.PracticePositionItem = new List<SelectListItem>();
                //筛选数据 (-1为空) 
                dataList = new List<T_PracticeVolunteer>();
            }
            else
            {
                //传入岗位
                var PracticePositionItem = GetPracticePositionItem(SchoolID, PositionID);
                ViewBag.PracticePositionItem = PracticePositionItem;
                //筛选数据 
                dataList = (from t in dataList where t.PracticeNo.Contains(SchoolID) select t).ToList();
            }

            if (PositionID == "0")
            {
                //筛选数据(0不筛选) 
                // data = (from t in data select t).ToList();
            }
            else if (PositionID == "-1")
            {
                //筛选数据(-1不筛选) 
                // data = (from t in data select t).ToList();
            }
            else
            {
                //筛选数据 
                dataList = (from t in dataList where t.PosID == PositionID select t).ToList();
            }


            var data = dataList;
            this.CutPage<T_PracticeVolunteer>(
                this.GetModelNote<T_PracticeVolunteer>()
                , this, perCount, pageIndex
                , data
                , new int[] { 1, 2, 6, 7, 8, 10, 12, 14, 15 }
                , actionFlag
                );

            ////自动选择第一个学校的全部职位
            //if (SchoolID == "-1" && PassedPracticeSchoolItem.Count > 0)
            //{
            //    SchoolID = PassedPracticeSchoolItem[0].Value;
            //    return Redirect("/Enterprise/Home/Interview_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + "0" + "" + "&perCount=" + perCount + "&pageIndex=" + pageIndex);

            //}

            return View();


        }


        #endregion

        #region 实习材料查看
        //实习材料查看学生列表
        [BaseActionFilter]
         public ActionResult PracticeMaterial_ListShow(string actionFlag = "实习材料查看", string SchoolID = "-1", string PositionID = "-1", int perCount = 8, int pageIndex = 1)
         {//actionFlag=面试管理 实习结果管理 岗位指派管理
             ViewBag.actionURL = "PracticeMaterial_ListShow";
             ViewBag.SchoolID = SchoolID;
             ViewBag.PositionID = PositionID;


             Vars vars = (Session["Vars"] as Vars);
             var Ent_No = vars.getUserUnit();
             //数据源
             var dataList = (from a in sc.PracticeVolunteer.ToList()
                             join c in db.T_EntBatchReg.ToList() on a.EntPracNo equals c.EntPracNo
                             join f in db.PracBatch on c.PracBatchID equals f.PracBatchID
                             where c.Ent_No == Ent_No && f.IsActive == 1 && f.IsCurrentBatch == "是"
                             && a.VolunteerStatus==5
                             select a
                 ).ToList();


             //传入学校
             var PassedPracticeSchoolItem = GetPassedPracticeSchoolItem(SchoolID);
             ViewBag.PassedPracticeSchoolItem = PassedPracticeSchoolItem;
             //穿入操作方式
             ViewBag.actionFlag = actionFlag;


             if (SchoolID == "-1")
             {
                 //传入岗位
                 ViewBag.PracticePositionItem = new List<SelectListItem>();
                 //筛选数据 (-1为空) 
                 dataList = new List<T_PracticeVolunteer>();
             }
             else
             {
                 //传入岗位
                 var PracticePositionItem = GetPracticePositionItem(SchoolID, PositionID);
                 ViewBag.PracticePositionItem = PracticePositionItem;
                 //筛选数据 
                 dataList = (from t in dataList where t.PracticeNo.Contains(SchoolID) select t).ToList();
             }

             if (PositionID == "0")
             {
                 //筛选数据(0不筛选) 
                 // data = (from t in data select t).ToList();
             }
             else if (PositionID == "-1")
             {
                 //筛选数据(-1不筛选) 
                 // data = (from t in data select t).ToList();
             }
             else
             {
                 //筛选数据 
                 dataList = (from t in dataList where t.PosID == PositionID select t).ToList();
             }


             var data = dataList;
             this.CutPage<T_PracticeVolunteer>(
                 this.GetModelNote<T_PracticeVolunteer>()
                 , this, perCount, pageIndex
                 , data
                 , new int[] { 1, 2, 6,17,18,19,20}
                 , actionFlag
                 );

             ////自动选择第一个学校的全部职位
             //if (SchoolID == "-1" && PassedPracticeSchoolItem.Count > 0)
             //{
             //    SchoolID = PassedPracticeSchoolItem[0].Value;
             //    return Redirect("/Enterprise/Home/PracticeMaterial_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + "0" + "&perCount=" + perCount + "&pageIndex=" + pageIndex);

             //}

             return View("T_PracticeVolunteer_ListShow");


         }

        #endregion

         #region 实习结果管理

         //录入评价
         public ActionResult T_EntBatchReg_PracticelGrade(string PracticeNo, string EntPracNo, string PosID, string SchoolID)
         {


             string SQL = @"select * 
                            from T_Student 
                            inner join T_Class on T_Student.StuClass=T_Class.ClassID
                            inner join C_Specialty on (T_Class.SpeNo=C_Specialty.SpeNo and T_Class.EntryYear=C_Specialty.EntryYear and T_Class.SchoolID=C_Specialty.SchoolID) 
                            inner join T_School on C_Specialty.SchoolID=T_School.SchoolID 
                            inner join T_StuBatchReg on T_Student.UserID=T_StuBatchReg.UserID 
                            inner join T_PracBatch on T_StuBatchReg.PracBatchID=T_PracBatch.PracBatchID 
                            where T_StuBatchReg.PracticeNo='" + PracticeNo + "' ";

             ViewBag.T_PracticeIdentification_View = db.Database.SqlQuery<T_PracticeIdentification_View>(SQL).FirstOrDefault();//学生基本信息

             string SQL2 = @"select *  from   C_EntEvaluateStuItem 
                            where PracBatchID=
                            (select top 1  PracBatchID 
                            from T_EntBatchReg 
                            where EntPracNo='" + EntPracNo + "' group by PracBatchID)";
             ViewBag.C_EntEvaluateStuItemList = db.Database.SqlQuery<C_EntEvaluateStuItem>(SQL2).ToList();
             ViewBag.C_EntEvaStuGradeLevelItemList = ct.C_EntEvaStuGradeLevelItem.ToList();
             ViewBag.PracticeNo = PracticeNo;
             ViewBag.EntPracNo = EntPracNo;
             ViewBag.PosID = PosID;
             ViewBag.SchoolID = SchoolID;
             return View();
         }
         [BaseActionFilter]
         [HttpPost]
         public ActionResult T_EntBatchReg_PracticelGrade(T_EntEvaluateStu f)
         {
            
        
             string PracticeNo = Request.Form["PracticeNo"];
             string EntPracNo = Request.Form["EntPracNo"];
             string PosID = Request.Form["PosID"];
             string SchoolID = Request.Form["SchoolID"];



             //string PracticeDept = Request.Form["PracticeDept"];//实习部门
             //string PracticeDivDept = Request.Form["PracticeDivDept"];//实习分部门
             //string PracticeStartEnd = Request.Form["PracticeStartEnd"];//起止时间
          


             //var old = (from t in sc.StuBatchReg
             //           where t.PracticeNo == PracticeNo
             //           select t).FirstOrDefault();
             //old.PracticeDept = PracticeDept;
             //old.PracticeDivDept = PracticeDivDept;
             //old.PracticeStartEnd = PracticeStartEnd;
             //db.Entry(old).State = EntityState.Modified;


             //保存评分项
             List<C_EntEvaluateStuItem> C_EntEvaluateStuItemList = db.Database.SqlQuery<C_EntEvaluateStuItem>("select *  from   C_EntEvaluateStuItem where PracBatchID=(select top 1  PracBatchID from T_EntBatchReg where EntPracNo='" + EntPracNo + "' group by PracBatchID)").ToList();

             //读取评价项目（规则和视图保持一致）
             for (int i = 0; i < C_EntEvaluateStuItemList.Count; i++)//评价项目
             {

                 string star = Request.Form[C_EntEvaluateStuItemList[i].ItemNo + "-ItemGrade"];//评价项目星级
                 string content = Request.Form[C_EntEvaluateStuItemList[i].ItemNo + "-ItemContent"];//评价项目星级


                 T_EntEvaluateStu item = new T_EntEvaluateStu();

                 item.PracticeNo = PracticeNo;
                 item.ItemNo = C_EntEvaluateStuItemList[i].ItemNo;
                 item.ItemContent = content;
                 //添加前删除原有的
                 db.T_EntEvaluateStu.Remove(db.T_EntEvaluateStu.Find(item.PracticeNo, item.ItemNo));

                 db.T_EntEvaluateStu.Add(item);
             }

//更改生涯状态为15[实习结束]已录入实习评价 
          
//             string SQL = @" select T_PracticeVolunteer.* 
//                                 from T_PracticeVolunteer inner join T_StuBatchReg on T_PracticeVolunteer.PracticeNo=T_StuBatchReg.PracticeNo 
//                                 where T_StuBatchReg.CareerStatusID IN('13','14','15')";

//             var oldStuBatchReg = sc.Database.SqlQuery<T_StuBatchReg>(SQL).FirstOrDefault();
//             oldStuBatchReg.CareerStatusID = 15;
//             sc.Entry(oldStuBatchReg).State = EntityState.Modified;
//             sc.SaveChanges();

                 
             db.SaveChanges();//最后再保存！！！          


             return Redirect("/Enterprise/Home/PracticeResult_ListShow?actionFlag=实习结果管理&SchoolID=" + SchoolID + "&PositionID=" + PosID + "");
         }


         //录入实习鉴定表
         public ActionResult T_EntBatchReg_PracticeIdentification(string actionFlag,string PracticeNo, string EntPracNo, string PosID, string SchoolID)
         {
            var repository = new Core.Services.Entity.Repository();
            var stuBatch = repository.T_StuBatchReg.FirstOrDefault(b => b.PracticeNo == PracticeNo);
            if (stuBatch == null)
                return Alert("学生" + PracticeNo + "未参加当前批次");
            ViewBag.PracticeNo = PracticeNo;
            var uid = stuBatch.UserID;
            var student = _app.StuAllInfo(uid);
            var comment = _app.AllComment(uid);
            var volunteer = _app.GetFormaVolunteer(uid);
            var data = repository.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5 &&
                     repository.T_StuBatchReg.FirstOrDefault(b => b.PracticeNo == PracticeNo).T_PracBatch.IsCurrentBatch == "是");
            ViewBag.stuBatch = stuBatch;
            ViewBag.T_PracticeIdentification_View = data;
            if (data == null)
                return Alert("只有被企业录取后才能填写实习鉴定表");
            ViewBag.PracticeNo = PracticeNo;
            return View(App.ViewModel.Home.IdentificationTable_M.ToViewModel(uid, volunteer, student, comment));
        }
         [BaseActionFilter]
         [HttpPost]
         public ActionResult T_EntBatchReg_PracticeIdentification()
         {

             string PracticeNo = Request.Form["PracticeNo"];
             string EntPracNo = Request.Form["EntPracNo"];
             string PosID = Request.Form["PosID"];
             string SchoolID = Request.Form["SchoolID"];

              var oldStuBatchReg = sc.StuBatchReg.Find(PracticeNo);
              //oldStuBatchReg.PracticeStartEnd = Request.Form["PracticeStartEnd"];//起止时间
              ////oldStuBatchReg.PracticelName = Request.Form["PracticelName"];//实习名称
              //oldStuBatchReg.PracticeDept = Request.Form["PracticeDept"];//实习单位

              oldStuBatchReg.PracUnitComment = Request.Form["PracUnitComment"];//实习单位意见
              oldStuBatchReg.CareerStatusID = 14;

              var old2= db.PracticeVolunteer.Find(PracticeNo, EntPracNo, PosID);
              //old2.VolunteerStatus = 7;
              db.SaveChanges();
              sc.Entry(oldStuBatchReg).State = EntityState.Modified;
              sc.SaveChanges();
              return Redirect("/Enterprise/Home/PracticeResult_ListShow?actionFlag=实习结果管理&SchoolID=" + SchoolID + "&PositionID=" + PosID + "");
         }

         //实习结果管理学生列表
         [BaseActionFilter]
         public ActionResult PracticeResult_ListShow(string actionFlag = "实习结果管理", string SchoolID = "-1", string PositionID = "-1", int perCount = 8, int pageIndex = 1)
         {//actionFlag=面试管理 实习结果管理 岗位指派管理
             ViewBag.actionURL = "PracticeResult_ListShow";
             ViewBag.SchoolID = SchoolID;
             ViewBag.PositionID = PositionID;

             //数据源
             Vars vars = (Session["Vars"] as Vars);
             var Ent_No = vars.getUserUnit();
             //数据源
             var dataList = (from a in sc.PracticeVolunteer.ToList()
                             join c in db.T_EntBatchReg.ToList() on a.EntPracNo equals c.EntPracNo
                             join f in db.PracBatch on c.PracBatchID equals  f.PracBatchID
                             where c.Ent_No == Ent_No && f.IsActive==1&&f.IsCurrentBatch=="是"
                             && a.VolunteerStatus == 5
                             select a
                 ).ToList();


             //传入学校
             var PassedPracticeSchoolItem = GetPassedPracticeSchoolItem(SchoolID);
             ViewBag.PassedPracticeSchoolItem = PassedPracticeSchoolItem;
             //穿入操作方式
             ViewBag.actionFlag = actionFlag;


             if (SchoolID == "-1")
             {
                 //传入岗位
                 ViewBag.PracticePositionItem = new List<SelectListItem>();
                 //筛选数据 (-1为空) 
                 dataList = new List<T_PracticeVolunteer>();
             }
             else
             {
                 //传入岗位
                 var PracticePositionItem = GetPracticePositionItem(SchoolID, PositionID);
                 ViewBag.PracticePositionItem = PracticePositionItem;
                 //筛选数据 
                 dataList = (from t in dataList where t.PracticeNo.Contains(SchoolID) select t).ToList();
             }

             if (PositionID == "0")
             {
                 //筛选数据(0不筛选) 
                 // data = (from t in data select t).ToList();
             }
             else if (PositionID == "-1")
             {
                 //筛选数据(-1不筛选) 
                 // data = (from t in data select t).ToList();
             }
             else
             {
                 //筛选数据 
                 dataList = (from t in dataList where t.PosID == PositionID select t).ToList();
             }


             var data = dataList;
             this.CutPage<T_PracticeVolunteer>(
                 this.GetModelNote<T_PracticeVolunteer>()
                 , this, perCount, pageIndex
                 , data
                 , new int[] { 1, 2, 6, 17, 18, 22 }
                 , actionFlag
                 );

             ////自动选择第一个学校的全部职位
             //if (SchoolID == "-1" && PassedPracticeSchoolItem.Count > 0)
             //{
             //    SchoolID = PassedPracticeSchoolItem[0].Value;
             //    return Redirect("/Enterprise/Home/PracticeResult_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + "0" + "&perCount=" + perCount + "&pageIndex=" + pageIndex);

             //}

             return View("T_PracticeVolunteer_ListShow");


         }

        #endregion

        #region 实习鉴定表[学生、教师]

        //学生
        public ActionResult T_EntBatchReg_PracticeIdentification_Student(string PracticeNo)
        {
            var repository = new Core.Services.Entity.Repository();
            var stuBatch = repository.T_StuBatchReg.FirstOrDefault(b => b.PracticeNo == PracticeNo);
            if (stuBatch == null)
                return Alert("学生"+ PracticeNo+"未参加当前批次");
            ViewBag.PracticeNo = PracticeNo;
            var uid = stuBatch.UserID;
            var student = _app.StuAllInfo(uid);
            var comment = _app.AllComment(uid);
            var volunteer = _app.GetFormaVolunteer(uid);
            var data = repository.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5 &&
                     repository.T_StuBatchReg.FirstOrDefault(b => b.PracticeNo == PracticeNo).T_PracBatch.IsCurrentBatch == "是");
            ViewBag.stuBatch = stuBatch;
            ViewBag.T_PracticeIdentification_View = data;
            if (data == null)
                return Alert("只有被企业录取后才能填写实习鉴定表");
            ViewBag.PracticeNo = PracticeNo;
            return View(App.ViewModel.Home.IdentificationTable_M.ToViewModel(uid, volunteer, student, comment));
        }
        [BaseActionFilter]
        [HttpPost]
        public ActionResult T_EntBatchReg_PracticeIdentification_Student()
        {
            var PracticeNo = Request.Form["PracticeNo"];
            var oldStuBatchReg = sc.StuBatchReg.Find(PracticeNo);
            oldStuBatchReg.PracticeContent = Request.Form["PracticeContent"];//实习内容
            oldStuBatchReg.PracticeReport = Request.Form["PracticeReport"];//实习报告
            sc.Entry(oldStuBatchReg).State = EntityState.Modified;
            sc.SaveChanges();
            return Alert("保存成功！",-1);
        }
        //教师
        public ActionResult T_EntBatchReg_PracticeIdentification_Teacher(string PracticeNo)
        {
            var repository = new Core.Services.Entity.Repository();
            var stuBatch = repository.T_StuBatchReg.FirstOrDefault(b => b.PracticeNo == PracticeNo);
            if (stuBatch == null)
                return Alert("学生" + PracticeNo + "未参加当前批次");
            ViewBag.PracticeNo = PracticeNo;
            var uid = stuBatch.UserID;
            var student = _app.StuAllInfo(uid);
            var comment = _app.AllComment(uid);
            var volunteer = _app.GetFormaVolunteer(uid);
            var data = repository.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5 &&
                     repository.T_StuBatchReg.FirstOrDefault(b => b.PracticeNo == PracticeNo).T_PracBatch.IsCurrentBatch == "是");
            ViewBag.stuBatch = stuBatch;
            ViewBag.T_PracticeIdentification_View = data;
            if (data == null)
                return Alert("只有被企业录取后才能填写实习鉴定表");
            ViewBag.PracticeNo = PracticeNo;
            return View(App.ViewModel.Home.IdentificationTable_M.ToViewModel(uid, volunteer, student, comment));
        }
        [BaseActionFilter]
        [HttpPost]
        public ActionResult T_EntBatchReg_PracticeIdentification_Teacher()
        {
            var PracticeNo = Request.Form["PracticeNo"];
            var oldStuBatchReg = sc.StuBatchReg.Find(PracticeNo);
            oldStuBatchReg.TutorComment = Request.Form["TutorComment"];//教师意见
            sc.Entry(oldStuBatchReg).State = EntityState.Modified;
            sc.SaveChanges();
            return Alert("保持成功！",-2);
        }
        #endregion

        #region 岗位指派管理

        //更换岗位
        [BaseActionFilter]
         [HttpPost]
         public ActionResult T_PracticeVolunteer_ChangePosition(string a)

         {
             string PracticeNo = Request.Form["PracticeNo"];
             string EntPracNo = Request.Form["EntPracNo"];
             string PosID = Request.Form["PosID"];//原岗位
             string SchoolID = Request.Form["SchoolID"];
            string newPosID = Request.Form["newPosID"];//目标岗位

            var old = sc.PracticeVolunteer.FirstOrDefault(f => f.PracticeNo == PracticeNo &&
                                                               f.EntPracNo == EntPracNo &&
                                                               f.PosID == PosID);
            if (old == null)
            {
                return Alert("更换失败");
            }

            var newpos = sc.PracticeVolunteer.FirstOrDefault(f => f.PracticeNo == PracticeNo &&
                                                                f.EntPracNo == EntPracNo &&
                                                                f.PosID == newPosID);
            if (newpos == null)
            {
                sc.Database.ExecuteSqlCommand("insert into T_PracticeVolunteer" +
                                              " (PracticeNo,EntPracNo,PosID,VolunteerSequence,PosSequence,VolunteerStatus,InterviewTime,PreVolStatus) values" +
                                              " ('"+ PracticeNo + "','"+ EntPracNo + "','"+ newPosID + "','"+ old.VolunteerSequence + "','"+ old.PosSequence + "','"+5+"','"+0+"','"+1+"')");
                //old.PosID = newPosID;
                //sc.PracticeVolunteer.Add(new T_PracticeVolunteer()
                //{
                //    PracticeNo = PracticeNo ,
                //    EntPracNo = EntPracNo ,
                //    PosID = newPosID,
                //    VolunteerSequence =old.VolunteerSequence ,
                //    PosSequence = old.PosSequence,
                //    VolunteerStatus = 4,//直接通过面试
                //    InterviewTime = 0,
                //    PreVolStatus = "1",//正式志愿
                //});
                //sc.SaveChanges();
            }

                //将原岗位状态设置为默认状态7 将目标岗位设置为 5

                sc.Database.ExecuteSqlCommand("update T_PracticeVolunteer set VolunteerStatus='7' where PracticeNo='" +
                                              PracticeNo + "' and EntPracNo='" + EntPracNo + "' and PosID='" + PosID +
                                              "'");

            sc.Database.ExecuteSqlCommand("update T_PracticeVolunteer set VolunteerStatus='5' where PracticeNo='" +
                                          PracticeNo + "' and EntPracNo='" + EntPracNo + "' and PosID='" + newPosID +
                                          "'");

            sc.Database.ExecuteSqlCommand("update T_StuBatchReg set CareerStatusID='13' where PracticeNo='" +
                                          PracticeNo + "'");
            return Redirect("/Enterprise/Home/PracticeInfoChange_ListShow?actionFlag=岗位指派管理&SchoolID=" + SchoolID + "&PositionID=0");

         }
         //岗位指派管理学生列表
         [BaseActionFilter]
         public ActionResult PracticeInfoChange_ListShow(string actionFlag = "岗位指派管理", string SchoolID = "-1", string PositionID = "-1", int perCount = 8, int pageIndex = 1)
        {//actionFlag=面试管理 实习结果管理 岗位指派管理 部门信息维护
            ViewBag.actionURL = "PracticeInfoChange_ListShow";
             ViewBag.SchoolID = SchoolID;
             ViewBag.PositionID = PositionID;


             //数据源
             Vars vars = (Session["Vars"] as Vars);
             var Ent_No = vars.getUserUnit();
             //数据源
             var dataList = (from a in sc.PracticeVolunteer.ToList()
                             join c in db.T_EntBatchReg.ToList() on a.EntPracNo equals c.EntPracNo
                             join f in db.PracBatch on c.PracBatchID equals f.PracBatchID
                             where c.Ent_No == Ent_No && f.IsActive == 1 && f.IsCurrentBatch == "是"
                             && a.VolunteerStatus == 5
                             select a
                 ).ToList();


             //传入学校
             var PassedPracticeSchoolItem = GetPassedPracticeSchoolItem(SchoolID);
             ViewBag.PassedPracticeSchoolItem = PassedPracticeSchoolItem;
             //穿入操作方式
             ViewBag.actionFlag = actionFlag;


             if (SchoolID == "-1")
             {
                 //传入岗位
                 ViewBag.PracticePositionItem = new List<SelectListItem>();
                 //筛选数据 (-1为空) 
                 dataList = new List<T_PracticeVolunteer>();
             }
             else
             {
                 //传入岗位
                 var PracticePositionItem = GetPracticePositionItem(SchoolID, PositionID);
                 ViewBag.PracticePositionItem = PracticePositionItem;
                 //筛选数据 
                 dataList = (from t in dataList where t.PracticeNo.Contains(SchoolID) select t).ToList();
             }

             if (PositionID == "0")
             {
                 //筛选数据(0不筛选) 
                 // data = (from t in data select t).ToList();
             }
             else if (PositionID == "-1")
             {
                 //筛选数据(-1不筛选) 
                 // data = (from t in data select t).ToList();
             }
             else
             {
                 //筛选数据 
                 dataList = (from t in dataList where t.PosID == PositionID select t).ToList();
             }


             var data = dataList;
             this.CutPage<T_PracticeVolunteer>(
                 this.GetModelNote<T_PracticeVolunteer>()
                 , this, perCount, pageIndex
                 , data
                 , new int[] { 1, 2, 6, 17, 18 }
                 , actionFlag
                 );

             ////自动选择第一个学校的全部职位
             //if (SchoolID == "-1" && PassedPracticeSchoolItem.Count > 0)
             //{
             //    SchoolID = PassedPracticeSchoolItem[0].Value;
             //    return Redirect("/Enterprise/Home/PracticeInfoChange_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + "0" + "" + "&perCount=" + perCount + "&pageIndex=" + pageIndex);

             //}

             return View("T_PracticeVolunteer_ListShow");


         }

        #endregion

        #region 部门信息维护
        //部门信息维护

        [BaseActionFilter]
        [HttpPost]
        public ActionResult T_StuBatchReg_DepartChange(T_EntEvaluateStu f)
        {
            string PracticeNo = Request.Form["PracticeNo"];
            string EntPracNo = Request.Form["EntPracNo"];
            string PosID = Request.Form["PosID"];
            string SchoolID = Request.Form["SchoolID"];



            string PracticeDept = Request.Form["PracticeDept"];//实习部门
            string PracticeDivDept = Request.Form["PracticeDivDept"];//实习分部门



            var old = (from t in sc.StuBatchReg
                       where t.PracticeNo == PracticeNo
                       select t).FirstOrDefault();
            old.PracticeDept = PracticeDept;
            old.PracticeDivDept = PracticeDivDept;


            db.Entry(old).State = EntityState.Modified;
            db.SaveChanges();


            return Redirect("/Enterprise/Home/PracticeDeptChange_ListShow?actionFlag=部门信息维护&SchoolID=" + SchoolID + "&PositionID=0");
        }
        //部门信息维护 学生列表
        [BaseActionFilter]
        public ActionResult PracticeDeptChange_ListShow(string actionFlag = "部门信息维护", string SchoolID = "-1", string PositionID = "-1", int perCount = 8, int pageIndex = 1)
        {//actionFlag=面试管理 实习结果管理 岗位指派管理 部门信息维护
            ViewBag.actionURL = "PracticeDeptChange_ListShow";
            ViewBag.SchoolID = SchoolID;
            ViewBag.PositionID = PositionID;


            //数据源
            Vars vars = (Session["Vars"] as Vars);
            var Ent_No = vars.getUserUnit();
            //数据源
            var dataList = (from a in sc.PracticeVolunteer.ToList()
                            join c in db.T_EntBatchReg.ToList() on a.EntPracNo equals c.EntPracNo
                            join f in db.PracBatch on c.PracBatchID equals f.PracBatchID
                            where c.Ent_No == Ent_No && f.IsActive == 1 && f.IsCurrentBatch == "是"
                            && a.VolunteerStatus == 5
                            select a
                ).ToList();


            //传入学校
            var PassedPracticeSchoolItem = GetPassedPracticeSchoolItem(SchoolID);
            ViewBag.PassedPracticeSchoolItem = PassedPracticeSchoolItem;
            //穿入操作方式
            ViewBag.actionFlag = actionFlag;


            if (SchoolID == "-1")
            {
                //传入岗位
                ViewBag.PracticePositionItem = new List<SelectListItem>();
                //筛选数据 (-1为空) 
                dataList = new List<T_PracticeVolunteer>();
            }
            else
            {
                //传入岗位
                var PracticePositionItem = GetPracticePositionItem(SchoolID, PositionID);
                ViewBag.PracticePositionItem = PracticePositionItem;
                //筛选数据 
                dataList = (from t in dataList where t.PracticeNo.Contains(SchoolID) select t).ToList();
            }

            if (PositionID == "0")
            {
                //筛选数据(0不筛选) 
                // data = (from t in data select t).ToList();
            }
            else if (PositionID == "-1")
            {
                //筛选数据(-1不筛选) 
                // data = (from t in data select t).ToList();
            }
            else
            {
                //筛选数据 
                dataList = (from t in dataList where t.PosID == PositionID select t).ToList();
            }


            var data = dataList;
            this.CutPage<T_PracticeVolunteer>(
                this.GetModelNote<T_PracticeVolunteer>()
                , this, perCount, pageIndex
                , data
                , new int[] { 1, 2, 6, 17, 18 }
                , actionFlag
                );

            ////自动选择第一个学校的全部职位
            //if (SchoolID == "-1" && PassedPracticeSchoolItem.Count > 0)
            //{
            //    SchoolID = PassedPracticeSchoolItem[0].Value;
            //    return Redirect("/Enterprise/Home/PracticeDeptChange_ListShow?actionFlag=" + actionFlag + "&SchoolID=" + SchoolID + "&PositionID=" + "0" + "" + "&perCount=" + perCount + "&pageIndex=" + pageIndex);

            //}

            return View("T_PracticeVolunteer_ListShow");


        }
        #endregion

        #region 员工管理
        [BaseActionFilter]
         public ActionResult UserAdd()
         {
             ViewBag.actionLink = "/Enterprise/Home/UserAdd";
             return View();
         }
         [HttpPost]
         [BaseActionFilter]
         public ActionResult UserAdd(T_User f)
         {
            var service= S<Core.Services.Entity.T_Employee>();
           
            var RoleID = Request.Form["UserType"];//角色类型
            var UserPwd = Request.Form["UserPwd"];//密码 
            var EmployeeID = Request.Form["EmployeeID"];
            var EmployeeName = Request.Form["EmployeeName"];
            var Ent_No = DataContext.UserUnit;
                   
            DataContext.SetFiled("Ent_No", Ent_No);//企业号
            DataContext.SetFiled("UserPwd", UserPwd);//用户密码【密文存储 】
            DataContext.SetFiled("Ent_No", Ent_No);//用户编号
            DataContext.SetFiled("RoleID", RoleID);//角色编号
            var Employee = new Core.Services.Entity.T_Employee()
            {
                EmployeeID= EmployeeID,
                EmployeeName= EmployeeName
            };
         
             return Content(string.IsNullOrEmpty(service.Add(DataContext, Employee)) ? PageHelper.Tip("员工注册成功", "/Enterprise/Home/UserListShow") : PageHelper.Tip("员工注册失败,已存在该员工号",-1));
         }
        [BaseActionFilter]
         public ActionResult UserListShow(int perCount = 8, int pageIndex = 1)
         {
             //关键词User
             Vars vars = (Session["Vars"] as Vars);
            string Ent_No=vars.getUserUnit();
             //分页相关开始
             var data = (from a  in db.T_Employee
                         where a.Ent_No == Ent_No
                        select a
                            ).ToList();//原始数据
             ViewBag.link = "/Enterprise/Home/UserListShow";
             ViewBag.total = data.Count;
             ViewBag.currentPage = pageIndex;
             ViewBag.perCount = perCount;

             ViewBag.dataList = LibHelper.PageHelper.GetPage<T_Employee>(data, perCount, pageIndex);//分页操作
             //分页相关结束


             ViewBag.Title = "所有员工";
             ViewBag.addLink = "/Enterprise/Home/UserAdd";
             ViewBag.Limit = 3;
             ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_Employee_Note())[1];
             ViewBag.Action = new string[][]
             {
              
                //new string[] {"/Enterprise/Home/User_Delete","删除"}
             };

             return View();
         }

        
         #endregion
         public ActionResult Tool(string objName = "T_PracticePosition", string nameSpace = "ServicePlatform.Models", string operate = "Add")
        {
            if (operate == "Add")
            {

                ViewData["Properties"] = ServicePlatform.Areas.Permission.Lib.PermissionHelper.GetInfo(System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(nameSpace + "." + objName + "_Note"));
                ViewBag.Title = "添加记录";
                ViewBag.objName = (nameSpace + "." + objName);
            }
            else if (operate == "Edict")
            {
                ViewData["Properties"] = ServicePlatform.Areas.Permission.Lib.PermissionHelper.GetProperties(objName, nameSpace);
                ViewBag.Title = "编辑记录";
                ViewBag.objName = (nameSpace + "." + objName);
            }
            else if (operate == "Show")
            {
                ViewData["Properties"] = ServicePlatform.Areas.Permission.Lib.PermissionHelper.GetProperties(objName, nameSpace);
                ViewBag.Title = "查看记录";
                ViewBag.objName = (nameSpace + "." + objName);
            }
           
            return View("Tool");
        }
    }
}
