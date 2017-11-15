﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using xyj.Plugs;
using System.ComponentModel;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Lib;

namespace ServicePlatform.Models
{
    public class SchoolModels
    {
    }

    [Table("T_StuVideo")]
    public class T_StuVideo
    {
        [Key, Column(Order = 0)]
        [Display(Name = "用户ID")]
        public string UserID { set; get; }

        [Key, Column(Order = 1)]
        [Display(Name = "内联ID")]
        public int InnerID { set; get; }

        [Display(Name = "视频心情")]
        public string VideoMood { set; get; }

        [Display(Name = "视频名字")]
        public string VideoName { set; get; }

        [Display(Name = "视频链接")]
        public string VideoLink { set; get; }

        [Display(Name = "顺序")]
        public int Secquence { set; get; }
    }

    [Table("T_StuResFile")]
    public class T_StuResFile
    {
        [Key, Column(Order = 0)]
        [Display(Name = "用户ID")]
        public string UserID { set; get; }

        [Key, Column(Order = 1)]
        [Display(Name = "内联ID")]
        public int InnerID { set; get; }

        [Display(Name = "附件心情")]
        public string ResFileMood { set; get; }

        [Display(Name = "附件名字")]
        public string ResFileName { set; get; }

        [Display(Name = "附件链接")]
        public string ResFileLink { set; get; }

        [Display(Name = "顺序")]
        public int Secquence { set; get; }
    }

    [Table("T_StuPic")]
    public class T_StuPic
    {
        [Key, Column(Order = 0)]
        [Display(Name = "用户ID")]
        public string UserID { set; get; }

        [Key,Column(Order=1)]
        [Display(Name = "内联ID")]
        public int InnerID { set; get; }

        [Display(Name = "照片心情")]
        public string PicMood { set; get; }

        [Display(Name = "照片名字")]
        public string PicName { set; get; }

        [Display(Name = "照片链接")]
        public string PicLink { set; get; }

        [Display(Name = "顺序")]
        public int Secquence { set; get; }
    }

    [Table("T_PhoneMsg")]
    public class T_PhoneMsg
    {
        [Key]
        [Display(Name = "ID")]
        public string ID { set; get; }

        [Display(Name = "内容")]
        public string MsgContent { set; get; }

        [Display(Name = "联系方式")]
        public string Phone { set; get; }

        [Display(Name = "标志")]
        public string Flag { set; get; }
    }

    [Table("T_SysMsg")]
    public class T_SysMsg
    {
        [Key]
        [Display(Name = "ID")]
        public string MsgID { set; get; }

        [Display(Name = "发送人")]
        public string MsgOwner { set; get; }

        [Display(Name = "发送时间")]
        public int CreateTime { set; get; }

        public int SendTime
        {
            get
            {
                string temp = this.MsgID;
                SchoolContext sc = new SchoolContext();

                var q = (from f in sc.T_MsgSend where f.MsgID == temp select f.SendTime).FirstOrDefault();
                return q;
            }
        }

        [Display(Name = "消息内容")]
        public string MsgContent { set; get; }

        [Display(Name = "消息类型ID")]
        public int MsgTypeID { set; get; }

        [Display(Name = "父消息ID")]
        public string FatherMsgID { set; get; }

        [Display(Name = "是否锁定")]
        public int IsLocked { set; get; }

        public int SendTypeID
        {
            get
            {
                string temp = this.MsgID;
                SchoolContext sc=new SchoolContext();

                var q = (from f in sc.T_MsgSend where f.MsgID == temp select f.SendTypeID).FirstOrDefault();
                return q;
            }
        }
        public string Receiver
        {
            get
            {
                string temp = this.MsgID;
                SchoolContext sc = new SchoolContext();

                var q = (from f in sc.T_MsgSend where f.MsgID == temp select f.Receiver).FirstOrDefault();
                return q;
            }
        }
    }

    [Table("T_MsgSend")]
    public class T_MsgSend
    {
        [Key]
        [Display(Name = "ID")]
        public string MsgID { set; get; }

        [Display(Name = "发送人")]
        public string Sender { set; get; }

        [Display(Name = "发送时间")]
        public int SendTime { set; get; }

        [Display(Name = "发送类型ID")]
        public int SendTypeID { set; get; }

        [Display(Name = "接收人")]
        public string Receiver { get; set; }

    }

    [Table("T_MsgRec")]
    public class T_MsgRec
    {
        [Key,Column(Order=0)]
        [Display(Name = "ID")]
        public string MsgID { set; get; }

        [Key, Column(Order = 1)]
        [Display(Name = "接收人")]
        public string Receiver { get; set; }    
        
        [Display(Name = "阅读时间")]
        public int ReadTime { set; get; }


    }

    [Table("T_School")]
    public class T_School
    {
        [Key]
        [Display(Name = "学校ID")]
        public string SchoolID { set; get; }

        [Display(Name = "学校名")]
        public string SchoolName { set; get; }

        [Display(Name = "地址")]
        public string Address { set; get; }

        [Display(Name = "联系方式")]
        public string Contact { set; get; }

        [Display(Name = "邮箱")]
        public string Email { set; get; }

        [Display(Name = "用户名")]
        public string UserID { set; get; }

        [Display(Name = "学校状态")]
        public int Status { set; get; }

        [Display(Name = "信息锁定")]
        public int InfoLocked { set; get; }

        [Display(Name = "学校Logo")]
        public string SchoolLogo { set; get; }
    }

    [Table("T_PracBatch")]
    public class T_PracBatch
    {
        [Key]
        [Display(Name = "组合批次ID")]
        public string PracBatchID { set; get; }
        [Display(Name = "批次ID")]
        public string BatchID { set; get; }

        [Display(Name = "学校名")]
        public string SchoolID { set; get; }

        public string SchoolName
        {
            get
            {
                SchoolContext sc = new SchoolContext();
                var q = (from f in sc.School where f.SchoolID == this.SchoolID select f.SchoolName).FirstOrDefault();
                return q;
            }
        }

        [Display(Name = "批次名")]
        public string BatchName { set; get; }

        [Display(Name = "起止日期")]
        public string StartEnd { set; get; }

        [Display(Name = "是否当前批次")]
        public string IsCurrentBatch { set; get; }

        [Display(Name = "是否生效")]
        public int IsActive { set; get; }
         [Display(Name = "评分比重")]
        public int SchoolReviewWeight { set; get; }
    
    }

    [Table("T_StuBatchReg")]
    public class T_StuBatchReg
    {
        [Key]
        [Display(Name = "组合批次ID")]
        public string PracticeNo { set; get; }
        [Display(Name = "用户ID")]
        public string UserID { set; get; }

        [Display(Name = "批次ID")]
        public string PracBatchID { set; get; }

        [Display(Name = "周记分")]
        public double WeekRecordScore { set; get; }

        [Display(Name = "实习案例分")]
        public double PracticeCaseScore { set; get; }

        [Display(Name = "周记评论")]
        public string WeekRecordComment { set; get; }

        [Display(Name = "实习报告")]
        public string PracticeReport { set; get; }

        [Display(Name = "实习部门")]
        public string PracticeDept { set; get; }

        [Display(Name = "实习div部门")]
        public string PracticeDivDept { set; get; }

        [Display(Name = "实习开始时间")]
        public string PracticeStartEnd { set; get; }

        [Display(Name = "Review分")]
        public double ReviewScore { set; get; }
        [Display(Name = "企业评语")]
        public string PracUnitComment { set; get; }
        [Display(Name = "学校评语")]
        public string SchoolComment { set; get; }
        [Display(Name = "导师评语")]
        public string TutorComment { set; get; }

        [Display(Name = "实习报告附件")]
        public string PracticeReportFile { set; get; }

        [Display(Name = "实习内容")]
        public string PracticeContent { set; get; }

        [Display(Name = "生涯状态")]
        public int CareerStatusID { set; get; }

        [Display(Name = "学生评价企业")]
        public double StuEvaEntScore { set; get; }

        [Display(Name = "学生评价学校")]
        public double StuEvaSchoolScore { set; get; }

        public string StuClass
        {
            get
            {
                string temp = this.UserID;
                SchoolContext sc = new SchoolContext();
                var q = (from f in sc.Student where f.UserID == temp select f.StuClass).FirstOrDefault();
                return q;
            }
        }

        public string StuSpeNo
        {
            get
            {
                string temp = this.StuClass;
                SchoolContext sc = new SchoolContext();
                var q = (from f in sc.T_Class where f.ClassID == temp select f.SpeNo).FirstOrDefault();
                return q;
            }
        }

        public string StuEntryYear
        {
            get
            {
                string temp = this.StuClass;
                SchoolContext sc = new SchoolContext();
                var q = (from f in sc.T_Class where f.ClassID == temp select f.EntryYear).FirstOrDefault();
                return q+"";
            }
        }
         
    }

    [Table("T_Student")]
    public class T_Student
    {
        [Key]
        [Display(Name = "用户ID")]

        public string UserID { set; get; }

        [Display(Name = "学号")]
        [ModelNoteAttrabute("学号")]
        public string StuNo { set; get; }

        [Display(Name = "姓名")]
        [ModelNoteAttrabute("姓名")]
        public string StuName { set; get; }

        [Display(Name = "性别")]
        public string StuSex { set; get; }

        [Display(Name = "身高")]
        public int StuHeight { set; get; }

        [Display(Name = "体重")]
        public int StuWeight { set; get; }

        [Display(Name = "电话")]
        public string StuCellphone { set; get; }

        [Display(Name = "邮箱")]
        public string StuEMail { set; get; }

        [Display(Name = "QQ")]
        public string StuQQ { set; get; }

        [Display(Name = "职位")]
        public string StuResume { set; get; }

        [Display(Name = "主要照片")]
        public string MainPhoto { set; get; }

        [Display(Name = "图片")]
        public string Pics { set; get; }

        [Display(Name = "录影")]
        public string Videos { set; get; }

        [Display(Name = "资源文件")]
        public string StuResourceFile { set; get; }

        [Display(Name = "班级ID")]
        public string StuClass { set; get; }

        [Display(Name = "学校ID")]
        public string SchoolID 
        {
            get
            {
                //string []temp = this.UserID.Split('-');
                SchoolContext sc = new SchoolContext();
                var temp = (from Stu in sc.Student
                            from Cla in sc.T_Class
                            where Stu.StuClass == Cla.ClassID && Stu.StuNo == this.StuNo
                            select Cla.SchoolID).ToList();
                if (temp == null)
                { return ""; }
                else
                { return temp[0]; }
            }
        }
        public string SchoolName
        {
            get
            {
                //string[] temp = this.UserID.Split('-');
                //string id = temp[0];
                SchoolContext sc = new SchoolContext();
                var q = (from f in sc.School where f.SchoolID == this.SchoolID  select f.SchoolName).FirstOrDefault();
                return q;
            }
        }

        public string PicMood { get;set;}

        public string VideoMood { get; set; }


        public string StuBirthday { get; set; }
    }

    [Table("T_Class")]
    public class T_Class
    {
        [Key]
        public string ClassID { get; set; }

        [Display(Name = "学校编号")]
        public string SchoolID { get; set; }

        [Required(ErrorMessage = "年级是必填项")]
        public int EntryYear { get; set; }
        [Required(ErrorMessage = "专业是必填项")]
        public string SpeNo { get; set; }
        [Required(ErrorMessage = "班级名称是必填项")]
        public string ClassName { get; set; }

        [NotMapped]
        [Display(Name = "专业名称")]
        public string SpeName
        {
            get
            {
                SchoolContext db = new SchoolContext();
                C_Specialty spe = new C_Specialty();
                //C_Specialty spe = db.C_Specialty.Find(this.EntryYear,this.SpeNo,this.SchoolID);
                var q = (from f in db.C_Specialty where f.EntryYear == this.EntryYear && f.SpeNo == this.SpeNo && f.SchoolID == this.SchoolID select f).ToList();
                if (q != null)
                {
                    spe = q[0];
                }
            
                if (spe != null)
                {
                    return spe.SpeName;
                }
                else
                {
                    return "";
                }
            }
        }
        //[Display(Name = "出生日期")]
        //public string StuBirthday{ get; set; }
    }


    [Table("T_PracticeVolunteer")]
    public class T_PracticeVolunteer : IModelNote
    {
        [Key, Column(Order = 0)]
        [Display(Name = "组合实习ID")]
        [ModelNoteAttrabute("组合实习ID")]
        public string PracticeNo { get; set; }
        [ModelNoteAttrabute("学号")]
        public string StuNo
        {
            get
            {
                SchoolContext sc = new SchoolContext();
                var stu = (from a in sc.StuBatchReg
                           join b in sc.Student on a.UserID equals b.UserID
                           where a.PracticeNo == PracticeNo
                           select b).FirstOrDefault();
                return stu == null ? "---" : stu.StuNo;
            }
        }

        [ModelNoteAttrabute("学生姓名")]
        public string StuName
        {
            get
            {
                SchoolContext sc = new SchoolContext();
                var stu = (from a in sc.StuBatchReg
                           join b in sc.Student on a.UserID equals b.UserID
                           where a.PracticeNo == PracticeNo
                           select b).FirstOrDefault();
                return stu == null ? "---" : stu.StuName+"("+ stu.StuSex+")";
            }
        }

        [Key, Column(Order = 1)]
        [Display(Name = "企业实习号")]
        [ModelNoteAttrabute("企业实习号")]
        public string EntPracNo { get; set; }
        [ModelNoteAttrabute("企业名称")]
        public string EntName
        {
            get
            {
                EnterpriseContext db = new EnterpriseContext();
                string temp = this.EntPracNo;
                var q = (from f in db.T_EntBatchReg where f.EntPracNo == temp select f.Ent_No).ToList();
                string temp1 = q[0];
                var q1 = (from f in db.T_Enterprise where f.Ent_No == temp1 select f.Ent_Name).ToList();
                return q1[0];
            }
        }

        [Key, Column(Order = 2)]
        [Display(Name = "岗位编号")]
        [ModelNoteAttrabute("岗位编号")]
        public string PosID { get; set; }

        [ModelNoteAttrabute("职位名称")]
        public string PosName
        {
            get
            {
                CodeTableContext db = new CodeTableContext();
                string temp = this.PosID;
                var q = (from f in db.C_Position where f.PositionID == temp select f.PositionName).FirstOrDefault();
                return q;
            }
        }


        [Display(Name = "志愿顺序")]
        [ModelNoteAttrabute("志愿顺序")]
        public int VolunteerSequence { get; set; }


        [Display(Name = "岗位顺序")]
        [ModelNoteAttrabute("岗位顺序")]
        public int PosSequence { get; set; }

        [Display(Name = "志愿状态")]
        [ModelNoteAttrabute("志愿状态")]
        public int VolunteerStatus { get; set; }
        [ModelNoteAttrabute("志愿状态名称")]
        public string VolunteerStatusName
        {
            get
            {
                CodeTableContext db = new CodeTableContext();
                int temp = this.VolunteerStatus;
                var q = (from f in db.C_VolPosStatus where f.VolStatusID == temp select f.VolStatusName).FirstOrDefault();
                return q;
            }
        }

        [Display(Name = "面试时间")]
        [ModelNoteAttrabute("面试时间Int")]
        public int InterviewTime { get; set; }
        [ModelNoteAttrabute("面试时间")]
        public string InterviewTimeString
        {
            get
            {
                if (InterviewTime == 0)
                    return "未安排";

                DateTime time = Lib.DateTimeFormat.ConvertIntDateTime(InterviewTime);
                return time.ToString("yyyy-MM-dd") + " " + time.ToLongTimeString();
            }
        }

        [Display(Name = "面试记录")]
        [ModelNoteAttrabute("面试记录")]
        public string InterviewRecord { get; set; }

        [Display(Name = "面试分数")]
        [ModelNoteAttrabute("面试记录")]
        public double? InterviewScore { get; set; }

        [Display(Name = "面试官")]
        [ModelNoteAttrabute("面试官")]
        public string Interviewee { get; set; }

        [Display(Name = "预报名状态")]
        [ModelNoteAttrabute("预报名状态")]
        public string PreVolStatus { get; set; }


        [ModelNoteAttrabute("实习部门")]
        public string PracticeDept
        {
            get
            {

                return (from t in new SchoolContext().StuBatchReg
                        where t.PracticeNo == PracticeNo
                        select t).FirstOrDefault().PracticeDept;
            }
        }
        [ModelNoteAttrabute("实习分部门")]
        public string PracticeDivDept
        {
            get
            {

                return (from t in new SchoolContext().StuBatchReg
                        where t.PracticeNo == PracticeNo
                        select t).FirstOrDefault().PracticeDivDept;
            }
        }
        [ModelNoteAttrabute("周记数量")]
        public string WeekRecordCount
        {
            get
            {
                var db = new SchoolContext();
                return (from t in db.WeekRecord
                        where t.PracticeNo == PracticeNo
                        select t).ToList().Count.ToString();
            }
        }
        //[ModelNoteAttrabute("自我鉴定数量")]
        //public string SelfEvaluationCount
        //{
        //    get
        //    {

        //        return (from t in new SchoolContext().SelfEvaluation
        //                where t.PracticeNo == PracticeNo
        //                select t).ToList().Count.ToString();
        //    }
        //}
        [ModelNoteAttrabute("实习案例数量")]
        public string PracticeCaseCount
        {
            get
            {

                return (from t in new SchoolContext().PracticeCase
                        where t.PracticeNo == PracticeNo
                        select t).ToList().Count.ToString();
            }
        }
        [ModelNoteAttrabute("实习评价")]
        public string EntEvaluateStu
        {
            get
            {

                var temp = (from t in new EnterpriseContext().T_EntEvaluateStu
                            where t.PracticeNo == PracticeNo
                            select t).ToList().Count;

                return (temp <= 0) ? "待录入" : "已录入";
            }
        }
        [ModelNoteAttrabute("实习鉴定表")]
        public string PracticeIdentification
        {
            get
            {
                var temp = (from t in new SchoolContext().StuBatchReg
                            where t.PracticeNo == PracticeNo
                            select t.PracUnitComment).FirstOrDefault();

                return (temp == "" || temp == null) ? "待录入" : "已录入";

            }
        }
        [ModelNoteAttrabute("志愿顺序名")]
        public string VolunteerSequenceName
        {
            get
            {
                int temp = this.VolunteerSequence;
                if (temp == 1)
                {
                    return "第一志愿";
                }
                else
                {
                    return "第二志愿";
                }
            }
        }
        public int PosExpireDate
        {
            get
            {
                EnterpriseContext ec = new EnterpriseContext();
                string temp = this.EntPracNo;
                string temp1 = this.PosID;
                var q = (from f in ec.T_PracticePosition where f.EntPracNo == temp && f.PositionID == temp1 select f.PosExpireDate).FirstOrDefault();
                return q;
            }

        }


        public string PosDesc
        {
            get
            {
                EnterpriseContext ec = new EnterpriseContext();
                string temp = this.EntPracNo;
                string temp1 = this.PosID;
                var q = (from f in ec.T_PracticePosition where f.EntPracNo == temp && f.PositionID == temp1 select f.PosDesc).FirstOrDefault();
                return q;
            }
        }

        public string UserID
        {
            get
            {
                SchoolContext sc = new SchoolContext();
                var stu = (from a in sc.StuBatchReg
                           join b in sc.Student on a.UserID equals b.UserID
                           where a.PracticeNo == PracticeNo
                           select b).FirstOrDefault();
                return stu == null ? "---" : stu.UserID;
            }
        }
        [ModelNoteAttrabute("手机号")]
        public string Phone
        {
            get
            {
                var firstOrDefault = (from t in new SchoolContext().Student
                    where t.UserID == UserID
                    select t).FirstOrDefault();
                return firstOrDefault != null ? firstOrDefault.StuCellphone : "";
            }
        }

    }



    [Table("T_PracticeCase")]
    public class T_PracticeCase
    {
        [Key,Column(Order=0)]
        [DisplayNameAttribute("批次号")]
        [Display(Name = "批次号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [DisplayNameAttribute("案例编号")]
        [Display(Name = "案例编号")]
        public int CaseNo { get; set; }

        [DisplayNameAttribute("案例名称")]
        [Display(Name = "案例名称")]
        public string CaseName { get; set; }

        [Key, Column(Order = 2)]
        [DisplayNameAttribute("案例项目编号")]
        [Display(Name = "案例项目编号")]
        public string ItemNo { get; set; }


        [DisplayNameAttribute("案例项目名")]
        [Display(Name = "案例项目名")]
        public string ItemName
        {
            get
            {
                string temp = this.ItemNo;
                CodeTableContext db = new CodeTableContext();
                var q = (from f in db.C_PracticeCaseItem where f.ItemNo == temp select f.ItemName).FirstOrDefault(); ;
                return q;
            }
        }

        [Display(Name = "批次号")]
        [DisplayNameAttribute("批次号")]
        public string ItemContent { get; set; }

    }

    [Table("T_WeekRecord")]
    public class T_WeekRecord
    {
        [Key, Column(Order = 0)]
        [DisplayNameAttribute("批次号")]
        [Display(Name = "批次号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [DisplayNameAttribute("周记编号")]
        [Display(Name = "周记编号")]
        public int RecordNo { get; set; }

        [DisplayNameAttribute("发布日期")]
        [Display(Name = "发布日期")]
        public int RecordDate { get; set; }


        [DisplayNameAttribute("发布日期")]
        [Display(Name = "发布日期")]
        public string RecordDateString
        {
            get
            {
                int temp = this.RecordDate;
                return DateTimeFormat.ConvertIntDateTime(temp).ToString();
            }
        }

        [DisplayNameAttribute("周记标题")]
        [Display(Name = "周记标题")]
        public string RecordTitle { get; set; }

        [DisplayNameAttribute("周记内容")]
        [Display(Name = "周记内容")]
        public string RecordContent { get; set; }

        [DisplayNameAttribute("周记摘要")]
        [Display(Name = "周记摘要")]
        public string RecordComment { get; set; }

        [DisplayNameAttribute("周记得分")]
        [Display(Name = "周记得分")]
        public double RecordScore { get; set; }
    }


    //[Table("T_DownLoadFiles")]
    //public class T_DownLoadFiles
    //{
    //    [Key]
    //    [Display(Name = "文件序号")]
    //    public string DLFileID { get; set; }

    //    [Display(Name = "文件名")]
    //    public string DLFileTitle { get; set; }

    //    [Display(Name = "发布者")]
    //    public string DLFilePubUser { get; set; }

    //    [Display(Name = "发布日期")]
    //    public int PubTime { get; set; }

    //    [Display(Name = "文件类型ID")]
    //    public int DLFileColumnID { get; set; }

    //    [Display(Name = "文件路径")]
    //    public string DLFileUrl { get; set; }

    //    [Display(Name = "内部编号")]
    //    public int InnerID { get; set; }

    //    [Display(Name = "所属单位ID")]
    //    public string UnitID { get; set; }

    //}


    [Table("T_StuEvaluateEnt")]
    public class T_StuEvaluateEnt
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "项目编号")]
        public string ItemNo { get; set; }

        public string ItemNoName
        {
            get
            {
                string temp = this.ItemNo;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaluateEntItem where f.ItemNo == temp select f.ItemName).FirstOrDefault();
                return q;
            }
        }

        //[Display(Name = "评分星级")]
        //public int ItemStars { get; set; }

        [Display(Name = "评分内容")]
        public string ItemContent { get; set; }

        [Display(Name = "项目分数")]
        public string ItemGrade { get; set; }

        public string ItemGradeName
        {
            get
            {
                string temp = this.ItemGrade;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaEntGradeLevelItem where f.ItemNo == temp select f.ItemName).FirstOrDefault();
                return q;
            }
        }
        //权重
        public int ItemGradeWeight 
        {
            get 
            {
                //项id
                string temp = this.ItemGrade;
                CodeTableContext ctc = new CodeTableContext();

                var q = (from f in ctc.C_StuEvaEntGradeLevelItem where f.ItemNo == temp select f.Weight).ToList();

                if (q.Count > 1)
                {
                    throw new Exception("数据出错");
                }
                return q[0];
            }
        }
        //分数
        public int ItemPower
        {
            get
            {
                //等级id
                string temp = this.ItemNo;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaluateEntItem where f.ItemNo == temp select f.ItemPower).ToList();
                if (q.Count > 1)
                {
                    throw new Exception("数据出错");
                }
                return q[0];
            }
        }

    }


    [Table("T_PlatformPubToUnit")]
    public class T_PlatformPubToUnit
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracBatchID { get; set; }

        public string BatchName
        {
            get
            {
                SchoolContext sc = new SchoolContext();
                var q = (from f in sc.PracBatch where f.PracBatchID == this.PracBatchID select f.BatchName).FirstOrDefault();
                return q;
            }
        }

        [Key, Column(Order = 1)]
        [Display(Name = "单位编号")]
        public string UnitID { get; set; }

        public string UnitName
        {
            get
            {
                if (this.UnitID == "平台")
                {
                    return "平台";
                }
                else
                {
                    EnterpriseContext db = new EnterpriseContext();
                    var q = (from f in db.T_Enterprise where f.Ent_No == this.UnitID select f.Ent_Name).FirstOrDefault();
                    return q;
                }
            }
        }

        [Display(Name = "起始时间")]
        public int StartTime { get; set; }

        [Display(Name = "结束时间")]
        public int EndTime { get; set; }

        [Display(Name = "申请状态ID")]
        public int ApplyStatusID { get; set; }

        public string ApplyStatusName
        {
            get
            {
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_ApplyStatus where f.ApplyStatusID == this.ApplyStatusID select f.ApplyStatusName).FirstOrDefault();
                return q;
            }
        }
    }


    [Table("T_SchoolPubToUnit")]
    public class T_SchoolPubToUnit
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracBatchID { get; set; }

        public string BatchName
        {
            get
            {
                string temp = this.PracBatchID;
                SchoolContext db = new SchoolContext();
                var q = (from f in db.PracBatch where f.PracBatchID == temp select f.BatchName).FirstOrDefault();
                return q;
            }
        }

        [Key, Column(Order = 1)]
        [Display(Name = "企业号")]
        public string UnitID { get; set; }

        public string UnitName
        {
            get
            {
                EnterpriseContext db = new EnterpriseContext();
                string temp = this.UnitID;
                if(temp=="平台")
                {
                    return temp;
                }
                else
                {
                    var q = (from f in db.T_Enterprise where f.Ent_No == temp select f.Ent_Name).FirstOrDefault();
                    return q;
                }

            }
        }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public int ApplyStatusID { get; set; }

        public string ApplyStatusName 
        {
            get 
            {
                CodeTableContext ctc = new CodeTableContext();
                int temp = this.ApplyStatusID;
                var q = (from f in ctc.C_ApplyStatus where f.ApplyStatusID == temp select f.ApplyStatusName).FirstOrDefault();
                return q;
            }
        }
    }

    [Table("T_StuEvaluateSchool")]
    public class T_StuEvaluateSchool
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "项目编号")]
        public string ItemNo { get; set; }

        public string ItemNoName
        {
            get
            {
                string temp = this.ItemNo;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaluateSchoolItem where f.ItemNo == temp select f.ItemName).FirstOrDefault();
                return q;
            }
        }

        //[Display(Name = "评分星级")]
        //public int ItemStars { get; set; }

        [Display(Name = "评分内容")]
        public string ItemContent { get; set; }

        [Display(Name = "项目分数")]
        public string ItemGrade { get; set; }

        public string ItemGradeName
        {
            get
            {
                string temp = this.ItemGrade;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaSchoolGradeLevelItem where f.ItemNo == temp select f.ItemName).FirstOrDefault();
                return q;
            }
        }
        //权重
        public int ItemGradeWeight
        {
            get
            {
                //项id
                string temp = this.ItemGrade;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaluateSchoolItem where f.ItemNo == temp select f.ItemPower).FirstOrDefault();
                return q;
            }
        }
        //分数
        public int ItemPower
        {
            get
            {
                //等级id
                string temp = this.ItemNo;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuEvaSchoolGradeLevelItem where f.ItemNo == temp select f.Weight).FirstOrDefault();
                return q;
            }
        }

    }

    [Table("T_StuInfoPub")]
    public class T_StuInfoPub
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "信息类型ID")]
        public int InfoTypeID { get; set; }

        [Display(Name = "信息类型名字")]
        public string InfoTypeName
        {
            get
            {
                int temp = this.InfoTypeID;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_StuInfoType where f.InfoTypeID == temp select f.InfoTypeName).FirstOrDefault();
                return q;
            }
        }

        [Display(Name = "发布级别")]
        public string PubLevel { get; set; }


    }

    [Table("T_Resume")]
    public class T_Resume
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "简历名称")]
        public string ResumeName { get; set; }

        [Display(Name = "简历路由")]
        public string ResumeURL { get; set; }

        [Display(Name = "发布时间")]
        public int PubTime { get; set; }

        [Display(Name = "是否默认0")]
        public int IsDefault { get; set; }

        public string IsDefaultStr
        {
            get
            {
                if(this.IsDefault==1)
                {
                    return "默认";
                }
                return "";
            }
        }


    }

    [Table("T_JobWanted")]
    public class T_JobWanted
    {
        [Key, Column(Order = 0)]
        [Display(Name = "学生实习号")]
        public string PracticeNo { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "公司ID")]
        public string Ent_No { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "职位ID")]
        public string PositionID { get; set; }

        public string PositionName
        {
            get
            {
                CodeTableContext db = new CodeTableContext();
                string temp = this.PositionID;
                var q = (from f in db.C_Position where f.PositionID == temp select f.PositionName).FirstOrDefault();
                return q;
            }
        }


        [Display(Name = "简历路由")]
        public string ResumeURL { get; set; }

        public string ResumeName
        {
            get
            {
                SchoolContext sc = new SchoolContext();
                string temp = this.ResumeURL;
                var q = (from f in sc.Resume where f.ResumeURL == ResumeURL select f.ResumeName).FirstOrDefault();
                return q;
            }
        }

        [Display(Name = "面试时间")]
        public int ReviewTime { get; set; }

        [Display(Name = "面试记录")]
        public string ReviewRecord { get; set; }

        [Display(Name = "面试分数")]
        public int ReviewScore { get; set; }

        [Display(Name = "工作状态")]
        public int JobStatus { get; set; }

        public string JobStatusName
        {
            get
            {
                int temp = this.JobStatus;
                CodeTableContext db = new CodeTableContext();
                var q = (from f in db.C_VolPosStatus where f.VolStatusID == JobStatus select f.VolStatusName).FirstOrDefault();
                return q;
            }
        }

        [Display(Name = "自投标志")]
        public string Flag { get; set; }


    }

    [Table("T_StuFinalEntRecord")]
    public class T_StuFinalEntRecord
    {
        [Key,Column(Order=0)]
        [Display(Name = "实习号")]
        public string PracticeNo { set; get; }

        [Key, Column(Order = 1)]
        [Display(Name = "公司名称")]
        public string Ent_Name { set; get; }

        [Display(Name = "公司类型")]
        public string EntCategory { set; get; }

        [Display(Name = "公司类型")]
        public string EntRank { set; get; }

        [Display(Name = "公司地址")]
        public string EntAddress { set; get; }

        [Display(Name = "公司简介")]
        public string EntResume { set; get; }

        [Display(Name = "公司邮箱")]
        public string Email { set; get; }

        [Display(Name = "公司联系方式")]
        public string Contectinfo { set; get; }

    }
    public  class T_SchoolStudentReviewLink
    {
           [Key, Column(Order = 0)]
        public string PracBatchID { get; set; }
          
        public string SchoolReviewerUserID { get; set; }
        [Key, Column(Order = 1)]
        public string PracticeNo { get; set; }
           [Key, Column(Order = 2)]
        public string ItemID { get; set; }
        public double ReviewScore { get; set; }
        public string ReviewComment { get; set; }

        public string ItemName
        {
            get
            {
                string temp = this.ItemID;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_SchoolReviewItem where f.ItemNo == temp select f.ItemName).FirstOrDefault();
                return q;
            }
        }

        public int ItemWeight
        {
            get
            {
                string temp = this.ItemID;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_SchoolReviewItem where f.ItemNo == temp select f.ItemWeight).FirstOrDefault();
                return q;
            }
        }
    }
    public  class T_SchoolReviewerTask
    {
        [Key, Column(Order = 0)]
        public string PracBatchID { get; set; }
          [Key, Column(Order = 1)]
        public string SchoolReviewerUserID { get; set; }
          [Key, Column(Order = 2)]
        public string ReviewItemID { get; set; }
    }
    //public  class C_SchoolReviewItem
    //{
    //    [Key]
    //    public string ItemNo { get; set; }
    //    public string PracBatchID { get; set; }
    //    public string ItemName { get; set; }
    //    public int ItemWeight { get; set; }
    //    public int ItemSequence { get; set; }
      
    //}

    public  class T_Faculty
    {
        [Key]
        public string FacNo { get; set; }
        public string FacName { get; set; }
        public string FacSex { get; set; }

        public string Sexstring
        {
            get
            {
                if(this.FacSex.HasValue())
                {
                    return this.FacSex.Contains("m")?"男":"女";
                }
                else
                {
                    return "未填写";
                }
            }
        }
        public int FacProPos { get; set; }

        public string FacProPosName { 
            get 
            {
                int temp = this.FacProPos;
                CodeTableContext ctc = new CodeTableContext();
                var q = (from f in ctc.C_ProPosition where f.ProPosID == temp select f.ProPosName).FirstOrDefault();
                return q;
            } 
        }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string FacPhoto { get; set; }
        public string FacAbstract { get; set; }
        public int FacStatus { get; set; }

        public string StatusString
        {
            get
            {
                if(this.FacStatus==0)
                {
                    return "离职";
                }
                else
                {
                    return "在岗";
                }
            }
        }
        public string SchoolID { get; set; }
        public string UserID { get; set; }
    }
        #region 注释类
    public class T_SchoolStudentReviewLink_Note
    {

        public string PracBatchID { get; set; }

        public string SchoolReviewerUserID { get; set; }

        public string PracticeNo { get; set; }

        public string ItemID { get; set; }

        public string ReviewScore { get; set; }

        public string ReviewComment { get; set; }

        public T_SchoolStudentReviewLink_Note()
        {

            PracBatchID = "批次编号";

            SchoolReviewerUserID = "教师姓名";

            PracticeNo = "学生姓名";

            ItemID = "评分项";

            ReviewScore = "分数";

            ReviewComment = "评价内容";

        }

        //PageHelper.GetModelNote<T_SchoolStudentReviewLink>(new T_SchoolStudentReviewLink(),new string[]{"批次编号","教师姓名","学生姓名","评分项","分数","评价内容"});

    }

    public class T_SchoolReviewerTask_Note
    {

        public string PracBatchID { get; set; }

        public string SchoolReviewerUserID { get; set; }

        public string ReviewItemID { get; set; }

        public T_SchoolReviewerTask_Note()
        {

            PracBatchID = "批次编号";

            SchoolReviewerUserID = "教师姓名";

            ReviewItemID = "评分项";

        }

        //PageHelper.GetModelNote<T_SchoolReviewerTask>(new T_SchoolReviewerTask(),new string[]{"批次编号","教师姓名","评分项"});

    }
#endregion

}