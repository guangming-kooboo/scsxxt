using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using ServicePlatform.Models;
using xyj.Plugs;
namespace ServicePlatform.Models
{



    
    public  class T_EntBatchReg:IModelNote
    {
        [ModelNoteAttrabute("企业名称")]

        public string Ent_Name
        {
            get
            {
                EnterpriseContext db = new EnterpriseContext();
                string temp = this.Ent_No;
                var q = (from f in db.T_Enterprise where f.Ent_No == temp select f.Ent_Name).ToList();
                return q[0];
            }
        }
        [ModelNoteAttrabute("申请状态")]


        public string ApplyStatusName
        {
            get
            {
                CodeTableContext ctc = new CodeTableContext();
                int temp = this.ApplyStatus;
                var q = (from f in ctc.C_ApplyStatus where f.ApplyStatusID == temp select f.ApplyStatusName).ToList();
                return q[0];
            }
        }
         [Key]
        [ModelNoteAttrabute("企业实习号")]
        public string EntPracNo { get; set; }
        [ModelNoteAttrabute("批次号")]
         public string PracBatchID { get; set; }
        [Display(Name = "学校名称")]//针对哪个学校发布的岗位
        [ModelNoteAttrabute("学校名称")]
        public string SchoolName
        {
            get
            {
                EnterpriseContext ec = new EnterpriseContext();
                SchoolContext sc = new SchoolContext();
                return (from a in ec.T_EntBatchReg.ToList()
                        join
                            b in sc.PracBatch on a.PracBatchID equals b.PracBatchID
                        join
                            c in sc.School on b.SchoolID equals c.SchoolID
                        where a.EntPracNo == EntPracNo
                        select c.SchoolName).FirstOrDefault();
            }
        }

        [Display(Name = "学校ID")]//针对哪个学校发布的岗位
        [ModelNoteAttrabute("学校ID")]
        public string SchoolID
        {
            get
            {
                EnterpriseContext ec = new EnterpriseContext();
                SchoolContext sc = new SchoolContext();
                return (from a in ec.T_EntBatchReg.ToList()
                        join
                            b in sc.PracBatch on a.PracBatchID equals b.PracBatchID
                        join
                            c in sc.School on b.SchoolID equals c.SchoolID
                        where a.EntPracNo == EntPracNo
                        select c.SchoolID).FirstOrDefault();
            }
        }
        [ModelNoteAttrabute("企业号")]
        public string Ent_No { get; set; }
     
       [ModelNoteAttrabute("提交日期")]
        public int RegistTime { get; set; }
        [ModelNoteAttrabute("申请状态代码")]
        public int ApplyStatus { get; set; }
        [ModelNoteAttrabute("批次名称")]
        public string PracBatchName
        {
            get
            {
                SchoolContext db = new SchoolContext();
                return (from a in db.PracBatch
                        where a.PracBatchID == PracBatchID
                        select a.BatchName).FirstOrDefault();
            }
        }
      
       
      
    }
    public  class T_Employee
    {
        
        public string Ent_No { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
         [Key]
        public string UserID { get; set; }
    }
    public  class T_Enterprise
    {
         [Key]
        public string Ent_No { get; set; }
        public string Ent_Name { get; set; }
        public string CountyID { get; set; }
        public string EntCategoryID { get; set; }
        public string EntRank { get; set; }
        public string EntAddress { get; set; }
        public string EntResume { get; set; }
        public string EntLogo { get; set; }
        public string EntAdPics { get; set; }
        public string EntPhotos { get; set; }
        public string EntVideos { get; set; }
        public string EntPPTs { get; set; }
        public string EntFiles { get; set; }
        public int EntState { get; set; }
        public int RegisterTime { get; set; }
        public int UpdateTime { get; set; }
        public string Email { get; set; }
        public string Contectinfo { get; set; }
        public string UserID { get; set; }
        public int InfoLocked { get; set; }
    }

    public class T_PracticePosition:IModelNote
    {
        [Key,Column(Order=0)]
        [Display(Name = "公司编号")]
        [ModelNoteAttrabute("公司编号")]
        public string EntPracNo { get; set; }
        [Display(Name = "企业名称")]
        [ModelNoteAttrabute("企业名称")]
        public string EntName
        {
            get
            {
                string temp = this.EntPracNo;
                EnterpriseContext ec = new EnterpriseContext();
                var q = (from f in ec.T_EntBatchReg where f.EntPracNo == EntPracNo select f).ToList();
                return q[0].Ent_Name;
            }
        }
       [Key, Column(Order = 1)]
       [Display(Name = "岗位编号")]
       [ModelNoteAttrabute("岗位编号")]
        public string PositionID { get; set; }
        [Display(Name = "岗位名称")]
        [ModelNoteAttrabute("岗位名称")]
        public string PositionName
       {
           get
           {
               string temp = this.PositionID;
               CodeTableContext db=new CodeTableContext();
               var q = (from f in db.C_Position where f.PositionID == temp select f.PositionName).ToList();
               return q[0];
           }
       }
        [Display(Name = "发布时间Int")]
        [ModelNoteAttrabute("发布时间Int")]
       public int PubDate { get; set; }
        [Display(Name = "岗位描述")]
        [ModelNoteAttrabute("岗位描述")]
        public string PosDesc { get; set; }
        [Display(Name = "岗位数量")]
        [ModelNoteAttrabute("岗位数量")]
        public int PosQuantity { get; set; }
        [Display(Name = "截止日期Int")]
        [ModelNoteAttrabute("截止日期Int")]
        public int PosExpireDate { get; set; }
        [Display(Name = "发布时间")]
        [ModelNoteAttrabute("发布时间")]
        public string PubDateStr
        {
            get
            {
                DateTime time = Lib.DateTimeFormat.ConvertIntDateTime(PubDate);
                return time.ToString("yyyy-MM-dd") ;
            }
        }
        [Display(Name = "截止日期")]
        [ModelNoteAttrabute("截止日期")]
        public string PosExpireDateStr
        {
            get
            {
                DateTime time=Lib.DateTimeFormat.ConvertIntDateTime(PosExpireDate);
                return time.ToString("yyyy-MM-dd") ;
            }
        }
        [Display(Name = "学校名称")]//针对哪个学校发布的岗位
        [ModelNoteAttrabute("学校名称")]
        public string SchoolName
        {
            get
            {
                EnterpriseContext ec = new EnterpriseContext();
                SchoolContext sc = new SchoolContext();
                return (from a in ec.T_EntBatchReg.ToList() join 
                      b in sc.PracBatch on a.PracBatchID equals b.PracBatchID join 
                      c in sc.School on b.SchoolID equals c.SchoolID
                      where a.EntPracNo==EntPracNo
                      select c.SchoolName).FirstOrDefault();
            }
        }
    }

    public class T_EntStudentReviewLink : IEqualityComparer<T_EntStudentReviewLink>
    {
        [Key,Column(Order=0)]
        public string EntPracNo { get; set; }
        
        public string EntReviewerUserID { get; set; }
          [Key, Column(Order = 1)]
        public string PracticeNo { get; set; }
         [Key, Column(Order = 2)]
        public string ItemID { get; set; }

         public string ItemName {
             get
             {
                 string temp = this.ItemID;
                 CodeTableContext ctc = new CodeTableContext();
                 var q = (from f in ctc.C_EntReviewItem where f.ItemNo == temp select f.ItemName).ToList();
                 return q[0];
             }
         }

         public int ItemWeight
         {
             get
             {
                 string temp = this.ItemID;
                 CodeTableContext ctc = new CodeTableContext();
                 var q = (from f in ctc.C_EntReviewItem where f.ItemNo == temp select f.ItemWeight).ToList();
                 return q[0];
             }
         }
        public double ReviewScore { get; set; }
        public string ReviewComment { get; set; }

        public bool Equals(T_EntStudentReviewLink x, T_EntStudentReviewLink y)
        {
            return x.PracticeNo == y.PracticeNo;
        }

        public int GetHashCode(T_EntStudentReviewLink obj)
        {
            return obj.PracticeNo.GetHashCode();
        }
    }
    public class T_EntReviewerTask
    {
         [Key, Column(Order = 0)]
        public string EntPracNo { get; set; }
           [Key, Column(Order = 1)]
        public string EntReviewerUserID { get; set; }
           [Key, Column(Order = 2)]
        public string ReviewItemID { get; set; }
    }
    public class T_EntEvaluateStu
    {
         [Key, Column(Order = 0)]
        public string PracticeNo { get; set; }
          [Key, Column(Order = 1)]
        public string ItemNo { get; set; }
        public string ItemContent { get; set; }
        public string ItemGrade { get; set; }
    }
    public class T_PracticeIdentification
    {
        [Key, Column(Order = 0)]
        public string PracticeNo { get; set; }
        [Key, Column(Order = 1)]
        public string ItemNo { get; set; }
        public string ItemContent { get; set; }
    
    }

    #region 自定义类
    public class T_PracticeIdentification_View//实习鉴定表
    {
        [Key]
        public string UserID { set; get; }

        [Display(Name = "学号")]
        public string StuNo { set; get; }

        [Display(Name = "年级")]
        public int EntryYear { set; get; }

        [Display(Name = "姓名")]
        public string StuName { set; get; }

        [Display(Name = "性别")]
          public string StuSex { set; get; }
         [Display(Name = "专业")]
          public string SpeName { set; get; }
          [Display(Name = "学校")]
          public string SchoolName { set; get; }
          [Display(Name = "起止时间 T_PracBatch")]
          public string StartEnd { set; get; }
          [Display(Name = "实习内容 T_StuBatchReg")]
          public string PracticeContent { set; get; }
          [Display(Name = "实习报告 T_StuBatchReg")]
          public string PracticeReport { set; get; }
          [Display(Name = "实习单位考核意见T_StuBatchReg")]
          public string PracUnitComment { set; get; }
          [Display(Name = "指导教师考核意见T_StuBatchReg")]
          public string TutorComment { set; get; }
          [Display(Name = "学院考核评定意见T_StuBatchReg	")]
          public string SchoolComment { set; get; }
         [Display(Name = "实习部门T_StuBatchReg	")]
          public string PracticeDept { set; get; }
         [Display(Name = "实习分部门T_StuBatchReg	")]
         public string PracticeDivDept { set; get; }
         [Display(Name = "实习单位名称	")]
         public string Ent_Name { set; get; }
    
       
    }

    #endregion


    #region 注释类
    public  class T_Enterprise_Note
    {
        public T_Enterprise_Note()
        {
            Ent_Name = "企业名称";
            EntState = "企业状态";
              Ent_No = "企业编号";
            
             CountyID = "县区ID";
             EntCategoryID = "企业类型代码";
             EntRank = "企业级别";
             EntAddress = "企业地址";
             EntResume = "企业简介";
             EntLogo = "企业Logo";
             EntAdPics = "轮播广告图";
             EntPhotos = "企业介绍图集";
             EntVideos = "企业视频集";
             EntPPTs = "企业PPT集";
             EntFiles = "企业资源文件";
             
             RegisterTime = "注册时间";
             UpdateTime = "更新时间";
             Email = "邮箱";
             Contectinfo = "联系方式";
             UserID = "登录名";
             InfoLocked = "是否锁定";
        }


        public string Ent_Name { get; set; }
        public string EntState { get; set; }
        public string Ent_No { get; set; }
       
        public string CountyID { get; set; }
        public string EntCategoryID { get; set; }
        public string EntRank { get; set; }
        public string EntAddress { get; set; }
        public string EntResume { get; set; }
        public string EntLogo { get; set; }
        public string EntAdPics { get; set; }
        public string EntPhotos { get; set; }
        public string EntVideos { get; set; }
        public string EntPPTs { get; set; }
        public string EntFiles { get; set; }
        
        public string RegisterTime { get; set; }
        public string UpdateTime { get; set; }
        public string Email { get; set; }
        public string Contectinfo { get; set; }
        public string UserID { get; set; }
        public string InfoLocked { get; set; }
    }

    public class T_EntBatchReg_Note
    {
        public T_EntBatchReg_Note()
        {
            EntPracNo = "申请编号";
            SchoolID = "批次注册编号";
            Ent_No = "企业编号";
            BatchID = "批次编号";
            RegistTime = "注册编号";
            ApplyStatus = "当前状态";
        }
        
        public string EntPracNo { get; set; }
        public string SchoolID { get; set; }
        public string Ent_No { get; set; }
        public string BatchID { get; set; }
        public string RegistTime { get; set; }
        public string ApplyStatus { get; set; }
    }


    public class T_PracticePosition_Note
    {
        public T_PracticePosition_Note()
        {
            EntPracNo = "企业实习编号";
            PositionID = "岗位编号";
            PubDate = "发布日期";
            PosDesc = "岗位描述";
            PosQuantity = "招生数量";
            PosExpireDate = "截止日期";
          
        }



        public string EntPracNo { get; set; }
        public string PositionID { get; set; }
        public string PubDate { get; set; }
        public string PosDesc { get; set; }
        public string PosQuantity { get; set; }
        public string PosExpireDate { get; set; }
    }

    public class T_EntStudentReviewLink_Note : IEqualityComparer<T_EntStudentReviewLink_Note>
    {

        public string EntPracNo { get; set; }

        public string EntReviewerUserID { get; set; }

        public string PracticeNo { get; set; }

        public string ItemID { get; set; }

        public string ReviewScore { get; set; }

        public string ReviewComment { get; set; }

        public T_EntStudentReviewLink_Note()
        {

            EntPracNo = "企业实习号";

            EntReviewerUserID = "员工姓名";

            PracticeNo = "学生姓名";

            ItemID = "评分项目名称";

            ReviewScore = "分数";

            ReviewComment = "评价内容";

        }

        //PageHelper.GetModelNote<T_EntStudentReviewLink>(new T_EntStudentReviewLink(),new string[]{"企业实习号","员工姓名","学生姓名","评分项目名称","分数","评价内容"});




        public bool Equals(T_EntStudentReviewLink_Note x, T_EntStudentReviewLink_Note y)
        {
            return x.PracticeNo == y.PracticeNo;
        }

        public int GetHashCode(T_EntStudentReviewLink_Note obj)
        {
           return obj.ToString().GetHashCode();
        }
      
    }

  

    public class T_EntReviewerTask_Note
    {

        public string EntPracNo { get; set; }

        public string EntReviewerUserID { get; set; }

        public string ReviewItemID { get; set; }

        public T_EntReviewerTask_Note()
        {

            EntPracNo = "企业实习号";

            EntReviewerUserID = "员工姓名";

            ReviewItemID = "评分项";

        }

        //PageHelper.GetModelNote<T_EntReviewerTask>(new T_EntReviewerTask(),new string[]{"企业实习号","员工姓名","评分项"});

    }

    public class T_EntEvaluateStu_Note
    {

        public string PracticeNo { get; set; }

        public string ItemNo { get; set; }

        public string ItemContent { get; set; }

        public string ItemGrade { get; set; }

        public T_EntEvaluateStu_Note()
        {

            PracticeNo = "学生实习号";

            ItemNo = "项目编号";

            ItemContent = "项目内容";

            ItemGrade = "项目等级";

        }

        //PageHelper.GetModelNote<T_EntEvaluateStu>(new T_EntEvaluateStu(),new string[]{"学生实习号","项目编号","项目内容","项目等级"});

    }

    public class T_Employee_Note
    {

        public string Ent_No { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string UserID { get; set; }

        public T_Employee_Note()
        {

            Ent_No = "企业号";

            EmployeeID = "员工号";

            EmployeeName = "员工姓名";

            UserID = "登陆名";

        }

        //PageHelper.GetModelNote<T_Employee>(new T_Employee(),new string[]{"企业号","员工号","员工姓名","登陆名"});

    }


#region 外部引用


    public class T_PracticeVolunteer_Note
    {

        public T_PracticeVolunteer_Note()
        {

          
             PracticeNo = "组合实习ID";


            
             EntPracNo = "企业实习号";

        
            
             PosID = "岗位编号";

            
             VolunteerSequence = "志愿顺序";

            
             PosSequence = "岗位顺序";

            
             VolunteerStatus = "志愿顺序";

            
             InterviewTime = "面试时间";

            
             InterviewRecord = "面试记录";

            
            InterviewScore = "面试分数";

            
             Interviewee = "面试官";

            
             PreVolStatus = "预报名状态";
        }


        public string PracticeNo { get; set; }


        public string EntPracNo { get; set; }


        public string PosID { get; set; }




        public string VolunteerSequence { get; set; }

        public string PosSequence { get; set; }


        public string VolunteerStatus { get; set; }


        public string InterviewTime { get; set; }

        public string InterviewRecord { get; set; }


        public string InterviewScore { get; set; }

 
        public string Interviewee { get; set; }

        public string PreVolStatus { get; set; }
    }


#endregion


#endregion


    ///////////////12.19添加    CQS
    [Table("T_RecruitPosition")]
    public class T_RecruitPosition
    {
        [Key, Column(Order = 0)]
        [Display(Name = "公司编号")]
        public string Ent_No { get; set; }

        public string Ent_Name
        {
            get
            {
                string temp = this.Ent_No;
                EnterpriseContext ec = new EnterpriseContext();
                var q = (from f in ec.T_Enterprise where f.Ent_No == temp select f.Ent_Name).ToList();
                return q[0];
            }
        }

        [Key, Column(Order = 1)]
        [Display(Name = "岗位ID")]
        public string PositionID { get; set; }

        public string PositionName
        {
            get
            {
                CodeTableContext db = new CodeTableContext();
                string temp = this.PositionID;
                var q = (from f in db.C_Position where f.PositionID == temp select f.PositionName).ToList();
                return q[0];
            }
        }

        [Display(Name = "发布日期")]
        public int PubDate { get; set; }

        [Display(Name = "岗位描述")]
        public string PosDesc { get; set; }

        [Display(Name = "招聘数量")]
        public int PosQuantity { get; set; }

        [Display(Name = "过期日")]
        public int PosExpireDate { get; set; }

        [Display(Name = "岗位要求")]
        public string PosRequirement { get; set; }

        [Display(Name = "薪资")]
        public string PosPay { get; set; }

        [Display(Name = "发布人")]
        public string Publisher { get; set; }

        [Display(Name = "接收申请")]
        public int IsActive { get; set; }

    }

}
  