using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services.School;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;
using System.Data.Entity.Migrations;

namespace Qx.Scsxxt.Core.Services
{
    #region models
    public class AnswerQuestion
    {
        public string AnsQueID;
        public string Answer;
        public string Question;
        public DateTime QuestionTime;
        public DateTime AnswerTime;
        public string Ent_No;
    }
    public class JobWanted
    {
        public string PracticeNo;
        public string Ent_No;
        public string PositionID;
        public string ResumeURL;
        public int ReviewTime;
        public string ReviewRecord;
        public int ReviewScore;
        public int JobStatus;
        public string Flag;
        public int Count;
    }
    public class Resume
    {
        public string PracticeNo;
        public string ResumeName;
        public string ResumeURL;
        public int PubTime;
        public int IsDefault;
    }
    public class DownLoadFiles
    {
        public string DLFileID;
        public string DLFileUrl;
        public string UnitID;
        public int DLFileColumnID;
    }
    public class Report
    {
        public string PracticeNo;
        public string UserID;
        public string PracticeReport;
    }

    //企业评价学生
    public class EntEvaluateStu
    {
        public string PracticeNo;
        public string ItemNo;
        public string ItemContent;
        public string ItemGrade;
        public string ItemName;

    }
    public class EntEvaluateStuItem
    {
        public string PraBatchID;
        public string ItemNo;
        public string ItemName;
        public int ItemSequence;
    }
    public class EntEvaluateStuGradeLevelItem
    {
        public string PraBatchID;
        public string ItemNo;
        public string ItemName;
        public string Note;
        public string Weight;
        public int ItemSequence;
    }

    //学生评价企业
    public class StuEvaluateEntItem
    {
        public string PraBatchID;
        public string ItemNo;
        public string ItemName;
        public int ItemSequence;
        public int ItemPower;
    }
    public class StuEvaluateEnt
    {
        public string PracticeNo;
        public string ItemNo;
        public string ItemContent;
        public string ItemGrade;
        public string ItemName;
        public string ItemGradeNo;
    }
    public class StuEvaEntGradeLevelItem
    {
        public string PraBatchID;
        public string ItemNo;
        public string ItemName;
        public string Note;
        public string Weight;
        public int ItemSequence;
    }

    //学生评价学校
    public class StuEvaluateSchoolItem
    {
        public string PraBatchID;
        public string ItemNo;
        public string ItemName;
        public int ItemSequence;
        public int ItemPower;
    }
    public class StuEvaluateSchool
    {
        public string PracticeNo;
        public string ItemNo;
        public string ItemContent;
        public string ItemGrade;
        public string ItemName;
        public string ItemGradeNo;
    }
    public class StuEvaSchoolGradeLevelItem
    {
        public string PraBatchID;
        public string ItemNo;
        public string ItemName;
        public string Note;
        public string Weight;
        public int ItemSequence;
    }


    public class Volunteer
    {
        public string Ent_No;
        public string PracticeNo;//学生实习号
        public string EntPracNo;//企业实习号
        public string EntName;//企业名字
        public string PosID;
        public string PositionName;
        public int VolunteerSequence;
        public int PosSequence;
        public int VolunteerStatus;
        public string PreVolStatus;
        public string VolStatusName;
        public string PositionDescription;
        public string PracticeDept;
        public string PracticeDivDept;
        public DateTime InterviewTime;
        public double InterviewScore;
    }
    public class Job
    {
        public string Ent_No;
        public string EntPracNo;
        public string PositionID;
        public string PositionName;
        public int PosQuantity;
        public int RemainRecruitingCount;
        //public string Sex;
        //  public string IsRotation;
        // public string Requirement;//岗位要求
        //public string Wage;
        public string Discription;
        public DateTime DeadLine;
        public DateTime PosExpireDate;
        public int RecruitingCount;
    }
    public class Grogshop
    {
        public string EntNo;
        public string EntPracNo;
        public string Name;
        public string Photos;
        public string Address;
        public string Phone;
        public string Email;
        public string Video;
        public string Description;
        public int Level;
        public int RecruitingCount;
        public string EntRank;
        public string EntCategoryID;
        public int PositionCount;
    }

    public class AllEnterprise
    {
        public string Ent_No;
        public string Ent_Name;
        public string Photos;
        public string EntAddress;
        public string Phone;
        public string Video;
        public string Email;
        public string EntResume;
        public int Level;
        public int RemainRecruitingCount;
        public int YetAddVolunteerCount;
        public int PositionCount;
        public string EntPracNo;
        public DateTime LastTime;
        public string EntRank;
        public string EntCategoryID;
        
    }
    public class Diary
    {
        public string PracticeNo;
        public int RecordNo;
        public int RecordDate;
        public int RecordTime;
        public string RecordTitle;
        public string RecordContent;
        public string RecordComment;
        public string Pic;
        public string uid;
        //  public double RecordScore;
    }
    public class Msg
    {
        public string Id;
        public DateTime Time;
        public string Title;
        public string Sender;
        public string Content;
    }
    public class PracticeCase
    {
        public string PracticeNo;
        public int CaseNo;
        public string ItemNo;
        [Required]
        public string CaseName;
        public string ItemContent;
        public string ItemName;
        public DateTime CaseTime;
        public string Pic;
    }

    public class UserInfo
    {
        public string Uid;
        public string Name;
        public string Photo;
        public string Psw;
        public string UnitId;
        public string UnitName;
        public string Sex;
        public string Phone;
        public string Mail;
        public string QQ;
        public string BirthDay;
        public string StuResume;
        public string SpeName;//专业名字
        public string ClassName;
        public int EntryYear;//年级
        public string StuNO;
    }

    public class AppBag
    {
        public string Uid;
        public UserInfo UserInfo;
        // public List<Mession> Messions;
        public List<Msg> Msgs;
        public List<Diary> Diarys;
        public List<Job> Jobs;
        public List<Volunteer> Volunteers;
        public List<Grogshop> Grogshops;
        public List<Report> Reports;
    }
    public class FAQ
    {
        public string FAQID;
        public string Title;
        public string Contents;
        public DateTime CreatTime;
        public string CreatTimeString;
        public string UserID;
        public string FAQTypeId;
        public string StateID;
        public string Video;
        public string CreatorName;
        public string TypeName;
        public string StateName;
        public string Abstract;
        public string Pic;
    }
    public class FAQType
    {
        public string TypeID;
        public string TypeName;
        public string FatherID;
    }
    #endregion

    public class AppService : BaseServices, IAppService
    {
        //判断该学生是否已经被录取了
        public T_PracticeVolunteer IsEnroll(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            return  Db.T_PracticeVolunteer.FirstOrDefault(o => o.PracticeNo == PracticeNo && o.VolunteerStatus == 5);
        
        }

        #region 管理中心

        public bool SavePic(string faqid,string picpath,string uid,string faqtypeid)
        {
            var pic = Db.T_FAQ.Where(a => a.FAQID == faqid);
            if (!pic.Any())
            {
                Db.T_FAQ.AddOrUpdate(new T_FAQ()
                {
                    FAQID = faqid,
                    Title = "Null",
                    Contents = "Null",
                    CreatTime = DateTime.Now,
                    UserID = uid,
                    FAQTypeId = faqtypeid,
                    StateID = "001",
                    Pic = picpath
                });
                return Db.Saved();
            }
            else
            {
                throw new Exception("已经存在");
            }
        }
        public List<SelectListItem> FAQState()
        {
            return Db.C_FAQState.Select(b => new SelectListItem()
            {
                Value = b.StateID,
                Text = b.StateName
            }).ToList();
        }

        public string FindFatherID(string childID)
        {
            var father = Db.C_FAQType.FirstOrDefault(a => a.FAQTypeId == childID);
            if (father!=null)//有父级类型
            {
                return father.FatherID;//父级类型ID
            }
            else
            {
                return null;
            }
        }

        public List<SelectListItem> FAQFatherType(string fatherid)
        {
            return Db.C_FAQType.Where(a => a.FatherID == fatherid).Select(b => new SelectListItem()
            {
                Value = Db.C_FAQType.FirstOrDefault(c => c.FatherID == b.FAQTypeId).FatherID,
                Text = b.TypeName,
            }).ToList();
        }
        public List<SelectListItem> FAQChildType(string fatherid)
        {
            var list= Db.C_FAQType.Where(a => a.FatherID == fatherid).Select(b => new SelectListItem()
            {
                Value =Db.T_FAQ.FirstOrDefault(c=>c.FAQTypeId== b.FAQTypeId).FAQTypeId,
                Text = b.TypeName,
            }).ToList();
            return list;
        }
        public List<SelectListItem> ChildType(string fatherid)
        {
            var list = Db.C_FAQType.Where(a => a.FatherID == fatherid).Select(b => new SelectListItem()
            {
                Value = b.FAQTypeId,
                Text = b.TypeName,
            }).ToList();
            return list;
        }
        public List<SelectListItem> ChildTypeNotNULL(string typeid)
        {
            var childtypelist = FAQChildType(typeid);
            List<SelectListItem> lists = new List<SelectListItem>();
            foreach (var item in childtypelist)
            {
                var faq = Db.T_FAQ.FirstOrDefault(a => a.FAQTypeId == item.Value);
                if (faq != null)
                {
                    SelectListItem list = new SelectListItem();
                    list.Value = item.Value;
                    list.Text = item.Text;
                    lists.Add(list);
                }            
            }
            return lists;
        }
        //FAQ类型
        public List<SelectListItem> FAQType()
        {
            return Db.C_FAQType.Where(a => a.FatherID == "0").Select(b => new SelectListItem()
            {
                Value = b.FAQTypeId,
                Text = b.TypeName,
            }).ToList();
        }

        public C_FAQType FindFAQType(string id)
        {
            return Db.C_FAQType.FirstOrDefault(a => a.FAQTypeId == id);
        }
        //FAQ列表
        public List<FAQ> FAQList(string faqtypeid)
        {
            if (!string.IsNullOrEmpty(faqtypeid))
            {
                return Db.T_FAQ.Where(a => a.FAQTypeId == faqtypeid).Select(b => new FAQ()
                {
                    FAQID = b.FAQID,
                    Title = b.Title,
                    Contents = b.Contents,                   
                    FAQTypeId = b.C_FAQType.FAQTypeId,
                    StateID = b.C_FAQState.StateID,
                    TypeName = b.C_FAQType.TypeName,
                    CreatTimeString=b.CreatTime.ToString(),
                    StateName = b.C_FAQState.StateName,
                    Video = b.Video,
                    Abstract = b.Abstract,
                    CreatTime = b.CreatTime,
                    Pic = b.Pic
                }).ToList();
            }
            else
            {
                return Db.T_FAQ.Select(b => new FAQ()
                {
                    FAQID = b.FAQID,
                    Title = b.Title,
                    Contents = b.Contents,
                    FAQTypeId = b.C_FAQType.FAQTypeId,
                    StateID = b.C_FAQState.StateID,
                    TypeName = b.C_FAQType.TypeName,
                    StateName = b.C_FAQState.StateName,
                    Video = b.Video,
                    CreatTimeString = b.CreatTime.ToString(),
                    Abstract = b.Abstract,
                    CreatTime = b.CreatTime,
                    Pic = b.Pic
                }).ToList();
            }
        }
        //FAQ详情
        public FAQ DetailFAQ(string faqid)
        {
            var faq = Db.T_FAQ.FirstOrDefault(a => a.FAQID == faqid);
            return new FAQ()
            {
                FAQID = faq.FAQID,
                Title = faq.Title,
                Contents = faq.Contents,
                CreatTime = faq.CreatTime,
                UserID = faq.UserID,
                Video = faq.Video,
                StateID = faq.StateID,
                FAQTypeId = faq.FAQTypeId,
                TypeName = faq.C_FAQType.TypeName,
                StateName = faq.C_FAQState.StateName,
                Abstract = faq.Abstract,
                Pic = faq.Pic
            };
        }

        //添加或编辑草稿FAQ
        public bool AddorEditDraftFAQ(FAQ faq)
        {
            Db.T_FAQ.AddOrUpdate(new T_FAQ()
            {
                FAQID = faq.FAQID,
                Title = faq.Title,
                Contents = faq.Contents,
                CreatTime = faq.CreatTime,
                UserID = faq.UserID,
                FAQTypeId = faq.FAQTypeId,
                StateID = "001",//001 为草稿  002为已发布
                Video = faq.Video,
                Abstract = faq.Abstract,
                Pic = faq.Pic
            });
            return Db.Saved();
        }
        //添加或编辑发布FAQ
        public bool AddorEditFormFAQ(FAQ faq)
        {
            Db.T_FAQ.AddOrUpdate(new T_FAQ()
            {
                FAQID = faq.FAQID,
                Title = faq.Title,
                Contents = faq.Contents,
                CreatTime = faq.CreatTime,
                UserID = faq.UserID,
                FAQTypeId = faq.FAQTypeId,
                StateID = faq.StateID,//001 为草稿  002为已发布
                Video = faq.Video,
                Abstract = faq.Abstract,
                Pic = faq.Pic
            });
            return Db.Saved();
        }
        //转为正式
        public bool TurnForm(string faqid)
        {
            var old = Db.T_FAQ.FirstOrDefault(a => a.FAQID == faqid);
            if (old != null)
            {
                old.StateID = "002";
                Db.Entry(old).State = EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
            else
            {
                throw new Exception("没有该草稿");
            }
        }
        //删除FAQ
        public bool DeleteFAQ(string faqid)
        {
            return Db.SaveDelete(Db.T_FAQ.FirstOrDefault(a => a.FAQID == faqid));
        }
        //添加或编辑类型
        public bool AddorEditFAQType(C_FAQType type)
        {
            Db.C_FAQType.AddOrUpdate(new C_FAQType()
            {
                FAQTypeId = type.FAQTypeId,
                TypeName = type.TypeName,
                FatherID = type.FatherID
            });
            return Db.Saved();
        }
        //删除类型
        public bool DeleteFAQType(string faqtypeid)
        {
            return Db.SaveDelete(Db.C_FAQType.FirstOrDefault(a => a.FAQTypeId == faqtypeid));
        }

        #endregion
        public T_User UserInfo(string uid)
        {
            return Db.T_User.FirstOrDefault(a => a.UserID == uid);
        }

        public string GetSchoolName(string schoolid)
        {
            var school = Db.T_School.FirstOrDefault(a => a.SchoolID == schoolid);
            if (school != null)
            {
                return school.SchoolName;
            }
            else
            {
                throw new Exception("不存在该学校");
            }
        }
        //public List<AnswerQuestion> GetAnswerQuestions(string Ent_No)
        //{
        //    return
        //        Db.T_AnswerQuestion.Where(a => a.Ent_No == Ent_No).ToList().Select(b => new AnswerQuestion()
        //        {
        //            AnsQueID = b.AnsQueID,
        //            Ent_No = b.Ent_No,
        //            Answer = b.Answer,
        //            Question = b.Question,
        //            AnswerTime = Convert.ToDateTime(b.AnswerTime),
        //            QuestionTime = Convert.ToDateTime(b.QuestionTime)
        //        }).ToList();
        //}
        //招聘阶段，企业找我，我找企业   Flag=自投，Flag=企业邀请
        public List<JobWanted> GetStuFindEnt(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var job =
                Db.T_JobWanted.Where(a => a.PracticeNo == PracticeNo && a.Flag == "自投")
                    .GroupBy(s => s.Ent_No)
                    .Select(b => new JobWanted()
                    {
                        Ent_No = b.Key,
                        Count = b.Count()
                    }).ToList();
            return job;
        }
        public List<JobWanted> GetEntFindStu(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var job =
                Db.T_JobWanted.Where(a => a.PracticeNo == PracticeNo && a.Flag == "企业邀请")
                    .GroupBy(s => s.Ent_No)
                    .Select(b => new JobWanted()
                    {
                        Ent_No = b.Key,
                        Count = b.Count()
                    }).ToList();
            return job;
        }

        public List<SelectListItem> YetSubmit(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var count = Db.T_PracticeVolunteer.Where(a => a.PracticeNo == PracticeNo)
                .GroupBy(s => s.EntPracNo)
                .Select(b => new SelectListItem()
                {
                    Text = b.Key,
                    Value = b.Count().ToString()
                }).ToList();
            return count;
        }
        //检查已经录取成功的志愿数量
        public int CheckAgreeCount(string PracticeNo)
        {
            return Db.T_PracticeVolunteer.Where(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5).Count();
        }
        //文件下载
        public List<DownLoadFiles> GetDownLoadFiles(string UnitID, int DLFileColumnID)
        {
            //var file = Convert.ToInt32(DLFileColumnID);
            var DownLoadFile = Db.T_DownLoadFiles.Where(o => o.T_Content.UnitID == UnitID && o.DLFileColumnID == DLFileColumnID)
                .ToList().Select(b => new DownLoadFiles()
                {
                    DLFileID = b.DLFileID,
                    DLFileColumnID = b.DLFileColumnID,
                    DLFileUrl = b.DLFileUrl
                }).ToList();
            return DownLoadFile;
        }
        //简历
        public List<Resume> GetResumes(string PracticeNo)
        {
            var Resume = Db.T_Resume.Where(a => a.PracticeNo == PracticeNo).ToList().Select(b => new Resume()
            {
                PracticeNo = b.PracticeNo,
                ResumeURL = b.ResumeURL,
                ResumeName = b.ResumeName,
                PubTime = Convert.ToInt32(b.PubTime),
                IsDefault = b.IsDefault
            }).ToList();
            return Resume;
        }
        //企业类型
        public List<SelectListItem> EntCategory()
        {
            return Db.C_EntCategory.ToList().Select(b => new SelectListItem()
            {
                Value = b.EntCategoryID,
                Text = b.EntCategoryName
            }).ToList().Format("");
        }

        //该企业是否当前批次
        public bool CheckEntBatch(string Ent_No, DataContext dataContext)
        {
            var first = Db.T_PracBatch.FirstOrDefault(a => a.SchoolID == dataContext.UserUnit && a.IsCurrentBatch == "是");
            var secound = Db.T_EntBatchReg.FirstOrDefault(b => b.PracBatchID == first.PracBatchID && b.Ent_No == Ent_No);
            if (secound != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddReport(Report report)
        {
            var PracticeNo = GetPracticeNoByUserID(report.UserID);
            var PracBatchID = GetPracBatchIDByUserID(report.UserID);
            var old = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == PracticeNo);
            if (old == null)
            {
                throw new Exception("不存在该学生");
            }
            else
            {
                old.PracBatchID = PracBatchID;
                old.PracticeReport = report.PracticeReport;
                Db.Entry(old).State = EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        public string ReportContent(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var Report = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == PracticeNo);
            return Report.PracticeReport;
        }

        //获取已经报了该岗位的人数
        public int GetCountByPosID(string EntPracNo, string PosID)
        {
            return Db.T_PracticeVolunteer.Where(a => a.EntPracNo == EntPracNo && a.PosID == PosID).Count();
        }
        //获取已经报了该岗位的人数,并且已经被录取了
        public int GetSucceedEnter(string EntPracNo, string PosID)
        {
            return Db.T_PracticeVolunteer.Where(a => a.EntPracNo == EntPracNo && a.PosID == PosID && a.PreVolStatus == "5").Count();
        }
        //获取该企业已经报名了的人数
        public int GetYetAddVolunteerCount(string EntPracNo)
        {
            return Db.T_PracticeVolunteer.Where(a => a.EntPracNo == EntPracNo).Count();
        }
        //获取该企业已经报名了的人数,并且被录取了
        public int GetYetAddVolunteerSucceedEnter(string EntPracNo)
        {
            return Db.T_PracticeVolunteer.Where(a => a.EntPracNo == EntPracNo && a.PreVolStatus == "5").Count();
        }

        //获取每个企业的招聘岗位数量
        public List<AllEnterprise> GetPositionCount()
        {
            var tmp = from a in Db.T_PracticePosition
                          //where a.EntPracNo == EntPracNo
                      group a by a.EntPracNo into g
                      select g;
            var count = tmp.ToList().Select(g => new AllEnterprise()
            {
                EntPracNo = g.FirstOrDefault().EntPracNo,
                PositionCount = g.Sum(a => a.PosQuantity),
                YetAddVolunteerCount = GetYetAddVolunteerCount(g.FirstOrDefault().EntPracNo),
                RemainRecruitingCount = g.Sum(a => a.PosQuantity) - GetYetAddVolunteerSucceedEnter(g.FirstOrDefault().EntPracNo),
                LastTime = g.Max(a => a.PosExpireDate).ToDateTime()
            }).ToList();
            return count;
        }
        //获取学生名字
        public string GetStuName(string uid)
        {
            var stu = Db.T_Student.Find(uid);
            return stu.StuName;
        }
        //志愿详情
        public Volunteer GetVolunteerDetail(string PracticeNo, string EntPracNo, string PosID)
        {
            var Volunteer =
                Db.T_PracticeVolunteer.FirstOrDefault(
                    a => a.PracticeNo == PracticeNo && a.EntPracNo == EntPracNo && a.PosID == PosID);
            return new Volunteer()
            {

                EntName = Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == EntPracNo).T_Enterprise.Ent_Name,
                PositionName = Db.C_Position.FirstOrDefault(a => a.PositionID == PosID).PositionName,
                PositionDescription = Db.T_PracticePosition.FirstOrDefault(a => a.EntPracNo == EntPracNo && a.PositionID == PosID).PosDesc
            };
        }
        public UserInfo StuAllInfo(string uid)
        {
            var stu = Db.T_Student.FirstOrDefault(a => a.UserID == uid);
            return new UserInfo()
            {
                EntryYear = stu.T_Class.EntryYear,
                SpeName = stu.T_Class.C_Specialty.SpeName,
                ClassName = stu.T_Class.ClassName,
                Name = stu.StuName,
                Sex = stu.StuSex,
                StuNO = stu.StuNo,
                Photo = stu.MainPhoto,
                StuResume = stu.StuResume
            };
        }
        //获取实习鉴定表里面的评论项
        public T_StuBatchReg AllComment(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var comment = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == PracticeNo);
            return comment;
        }
        //学生报名结束，改变学生的生涯状态，报名已结束
        public bool ChangeCareerStatus(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var stu = Db.T_StuBatchReg.Find(PracticeNo);
            if (stu == null)
            {
                throw new Exception("不存在该学生");
            }
            stu.CareerStatusID = 11;
            return Db.SaveModified(stu);
        }
        //招聘结束
        public bool OverBaoming(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var stu = Db.T_StuBatchReg.Find(PracticeNo);
            if (stu == null)
            {
                throw new Exception("不存在该学生");
            }
            stu.CareerStatusID = 13;
            return Db.SaveModified(stu);
        }
        //同意录取
        public bool AgreePosition(string PracticeNo, string EntPracNo, string PosID)
        {
            var volunteer =
                Db.T_PracticeVolunteer.FirstOrDefault(
                    a => a.PracticeNo == PracticeNo && a.EntPracNo == EntPracNo && a.PosID == PosID);
            if (volunteer == null)
            {
                throw new Exception("不存在该志愿");
            }
            else
            {
                volunteer.VolunteerStatus = 5;
                return Db.SaveModified(volunteer);
            }
        }
        //拒绝录取
        public bool DisagreePosition(string PracticeNo, string EntPracNo, string PosID)
        {
            var volunteer =
                Db.T_PracticeVolunteer.FirstOrDefault(
                    a => a.PracticeNo == PracticeNo && a.EntPracNo == EntPracNo && a.PosID == PosID);
            if (volunteer == null)
            {
                throw new Exception("不存在该志愿");
            }
            else
            {
                volunteer.VolunteerStatus = 6;
                return Db.SaveModified(volunteer);
            }
        }
        public int GetEvaluateSchoolCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            return Db.T_StuEvaluateSchool.Count(o => o.PracticeNo == PracticeNo);
        }

        public int GetEvaluateEntCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            return Db.T_StuEvaluateEnt.Count(o => o.PracticeNo == PracticeNo);
        }
        //获取企业评价学生详情
        public List<EntEvaluateStu> GetEntEvaluateStu(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var EvaluateStu = Db.T_EntEvaluateStu.Where(a => a.PracticeNo == PracticeNo).Select(b => new EntEvaluateStu()
            {
                ItemContent = b.ItemContent,
                ItemName = b.C_EntEvaluateStuItem.ItemName,
                ItemGrade = b.C_EntEvaStuGradeLevelItem.ItemName
            }).ToList();
            return EvaluateStu;
        }
        //获取企业评价项
        public List<StuEvaluateEntItem> GetEvaluateEntItem(string PraBatchID)
        {
            var item = Db.C_StuEvaluateEntItem.Where(a => a.PracBatchID == PraBatchID).Select(b => new StuEvaluateEntItem()
            {
                ItemName = b.ItemName,
                ItemNo = b.ItemNo,
                PraBatchID = b.PracBatchID,
                ItemSequence = b.ItemSequence,
                ItemPower = b.ItemPower
            }).ToList();
                return item;
        }
        //获取企业评价等级项
        public List<SelectListItem> GetStuEvaEntGradeLevelItem(string PraBatchID)
        {
            var item = Db.C_StuEvaEntGradeLevelItem.Where(a => a.PracBatchID == PraBatchID).Select(b => new SelectListItem()
            {
                Text = b.ItemName,
                Value = b.ItemNo,
            }).ToList();
                return item;
        }
        //获取学生实习企业的名字
        public string GetEntName(string PracticeNo)
        {
            var item = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5);

            if (item == null)
            {
                throw new Exception("该学生还没有实习的企业");
            }
            else
            {
                return Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == item.EntPracNo).T_Enterprise.Ent_Name;
            }
        }
        //学生评价企业
        public bool AddStuEvaluateEnt(List<StuEvaluateEnt> EvaluateEnt)
        {
            foreach (var item in EvaluateEnt)
            {
                Db.T_StuEvaluateEnt.AddOrUpdate(new T_StuEvaluateEnt()
                {
                    PracticeNo = item.PracticeNo,
                    ItemNo = item.ItemNo,
                    ItemContent = item.ItemContent,
                    ItemGrade = item.ItemGrade
                });
            }
            return Db.Saved();
        }
        //获取学生评价企业详情
        public List<StuEvaluateEnt> GetStuEvaluateEnt(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var evaluate = Db.T_StuEvaluateEnt.Where(a => a.PracticeNo == PracticeNo).Select(b => new StuEvaluateEnt()
            {
                ItemContent = b.ItemContent,
                ItemName = b.C_StuEvaluateEntItem.ItemName,
                ItemGrade = b.C_StuEvaEntGradeLevelItem.ItemName,
                ItemNo = b.ItemNo,
                ItemGradeNo = b.ItemGrade
            }).ToList();
            return evaluate;
        }

        //获取学校评价项
        public List<StuEvaluateSchoolItem> GetEvaluateSchoolItem(string PraBatchID)
        {
            var item = Db.C_StuEvaluateSchoolItem.Where(a => a.PracBatchID == PraBatchID).Select(b => new StuEvaluateSchoolItem()
            {
                ItemName = b.ItemName,
                ItemNo = b.ItemNo,
                PraBatchID = b.PracBatchID,
                ItemSequence = b.ItemSequence,
                ItemPower = b.ItemPower
            }).ToList();
                return item;
        }
        //获取学校评价等级项
        public List<SelectListItem> GetStuEvaSchoolGradeLevelItem(string PraBatchID)
        {
            var item = Db.C_StuEvaSchoolGradeLevelItem.Where(a => a.PracBatchID == PraBatchID).Select(b => new SelectListItem()
            {
                Text = b.ItemName,
                Value = b.ItemNo,
            }).ToList();
            return item;
        }
        //获取学生学校的名字
        //public string GetSchoolName(string PracticeNo)
        //{
        //    var item = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == a.PracticeNo && a.VolunteerStatus == 5);
        //    if (item == null)
        //    {
        //        throw new Exception("该学生还没有实习的企业");
        //    }
        //    else
        //    {
        //        return item.T_PracticePosition.T_EntBatchReg.T_Enterprise.Ent_Name;
        //    }
        //}
        //学生评价学校
        public bool AddStuEvaluateSchool(List<StuEvaluateSchool> EvaluateSchool)
        {
            foreach (var item in EvaluateSchool)
            {
                Db.T_StuEvaluateSchool.AddOrUpdate(new T_StuEvaluateSchool()
                {
                    PracticeNo = item.PracticeNo,
                    ItemNo = item.ItemNo,
                    ItemContent = item.ItemContent,
                    ItemGrade = item.ItemGrade
                });
            }
            return Db.Saved();
        }
        //获取学生评价学校详情
        public List<StuEvaluateSchool> GetStuEvaluateSchool(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var evaluate = Db.T_StuEvaluateSchool.Where(a => a.PracticeNo == PracticeNo).Select(b => new StuEvaluateSchool()
            {
                ItemContent = b.ItemContent,
                ItemName = b.C_StuEvaluateSchoolItem.ItemName,
                ItemGrade = b.C_StuEvaSchoolGradeLevelItem.ItemName,
                ItemNo = b.ItemNo,
                ItemGradeNo = b.ItemGrade
            }).ToList();
            return evaluate;
        }

        ////通过志愿表获取实习企业号
        //public string GetEntPracNo(string uid)
        //{
        //    var PracticeNo = GetPracticeNoByUserID(uid);
        //    var enterprise =Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5);
        //    if (enterprise != null)
        //        return enterprise.EntPracNo;
        //    else
        //        throw new Exception("该学生还没有实习的企业");
        //}
        //通过学生ID找到学生实习号
        public string GetPracticeNoByUserID(string uid)
        {
            var stuBatch = Db.T_StuBatchReg.FirstOrDefault(a => a.UserID == uid && a.T_PracBatch.IsCurrentBatch == "是");
            if (stuBatch != null)
            {
                return stuBatch.PracticeNo;
            }
            else
            {
                throw new Exception("学生未注册当前批次！");
            }
        }
        //通过企业实习号查找企业号
        public string GetEntNoByEntPracNo(string EntPracNo)
        {
            var Ent_No = Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == EntPracNo);
            return Ent_No.Ent_No;
        }
        //通过企业号查找企业实习号
        public string GetEntPracNoByEntNo(string Ent_No)
        {
            return Db.T_EntBatchReg
                .FirstOrDefault(a => a.ApplyStatus == 2 && a.T_PracBatch.IsCurrentBatch == "是").EntPracNo;
        }
        //通过企业号查找企业实习号，不过这里是所有批次的，不一定是针对当前的批次
        public string GetEntPracNoByEntNo_AllPrac(string Ent_No)
        {
            return Db.T_EntBatchReg
                .FirstOrDefault(a => a.ApplyStatus == 2).EntPracNo;
        }
        //通过用户id查找实习批次号
        public string GetPracBatchIDByUserID(string uid)
        {
            var practiceno = GetPracticeNoByUserID(uid);
            var PracBatchID = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == practiceno);
            return PracBatchID.PracBatchID;
        }
        //通过企业实习号查找岗位ID
        public string GetPositionID(string EntPracNo)
        {
            var PositionID = Db.T_PracticePosition.FirstOrDefault(a => a.EntPracNo == EntPracNo);
            return PositionID.PositionID;
        }
        public List<SelectListItem> GetSchoolIItems()
        {
            return S<T_School>().ToSelectItems();
        }
        public bool Login(DataContext dataContext, string uid, string psw)
        {
            psw = NoneEncrypt(psw);
            return Db.T_User.Any(a => a.UserID == uid && a.UserPwd == psw);
        }
        //通过uid查找详细信息
        public UserInfo GetPersonalInfo(string uid)
        {
            var user = Db.T_Student.Find(uid);
            return new UserInfo()
            {
                Sex = user.StuSex,
                Phone = user.StuCellphone,
                Mail = user.StuEMail,
                QQ = user.StuQQ,
                BirthDay = user.StuBirthday,
                Photo = user.MainPhoto,
                StuResume = user.StuResume
            };
        }

        public bool ForgetPsw(string uid, string psw)
        {
            var user = Db.T_User.Find(uid);
            user.UserPwd = NoneEncrypt(psw);
            Db.Entry(user).State = EntityState.Modified;
            return Db.Saved();
        }
        public bool UpdateInfo(DataContext dataContext, UserInfo userInfo)
        {
            var user = Db.T_User.Find(dataContext.UserID);
            //user.UserPwd = NoneEncrypt(userInfo.Psw);
            var stuInfo = Db.T_Student.Find(dataContext.UserID);
            stuInfo.StuCellphone = userInfo.Phone;
            stuInfo.StuEMail = userInfo.Mail;
            stuInfo.StuQQ = userInfo.QQ;
            stuInfo.StuBirthday = userInfo.BirthDay;
            stuInfo.StuSex = userInfo.Sex;
            stuInfo.StuResume = userInfo.StuResume;
            Db.Entry(user).State = EntityState.Modified;
            Db.Entry(stuInfo).State = EntityState.Modified;
            return Db.Saved();
        }
        //判断密码是否匹配
        public bool CheckPsw(string OldPwd, string uid)
        {
            var oldpsw = Db.T_User.Find(uid);
            var newpsw = NoneEncrypt(OldPwd);
            if (oldpsw.UserPwd == newpsw)
            {
                return true;
            }
            else
            {
                throw new Exception("密码不匹配");
            }
        }
        //修改密码
        public bool RevisePsw(DataContext dataContext, UserInfo userInfo)
        {
            var user = Db.T_User.Find(userInfo.Uid);
            user.UserPwd = NoneEncrypt(userInfo.Psw);
            Db.Entry(user).State = EntityState.Modified;
            return Db.Saved();
        }
        //find，通过uid查找学生表的详情
        public T_Student FindStudent(string uid)
        {
            if (uid.HasValue())
            {
                return Db.T_Student.Find(uid);
            }
            else
            {
                return null;
            }
        }
        //find，通过uid查找用户表的详情
        public T_User FindStu(string uid)
        {
            if (uid.HasValue())
            {
                return Db.T_User.Find(uid);
            }
            else
            {
                return null;
            }
        }
        //查志愿
        public T_PracticeVolunteer FindVolunteer(string uid)
        {
            if (uid.HasValue())
            {
                var PracticeNo = GetPracticeNoByUserID(uid);
                return Db.T_PracticeVolunteer.Find(PracticeNo);
            }
            else
                return null;
        }
        public bool UpdatePhoto(DataContext dataContext, UserInfo userInfo)
        {
            var stuInfo = Db.T_Student.Find(dataContext.UserID);
            stuInfo.MainPhoto = userInfo.Phone;
            return Db.Saved();
        }
        //通过uid查看学生生涯状态
        public int GetCareerStatus(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var CareerStatus = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == PracticeNo);
            return Convert.ToInt32(CareerStatus.CareerStatusID);
        }
        //修改学生生涯状态
        public int ReviseCareerStatus(string uid)
        {
            var CareerStatus = Db.T_StuBatchReg.Find(uid);
            if (CareerStatus == null)
            {
                throw new Exception("不存在该学生");
            }
            CareerStatus.CareerStatusID = 11;
            return Db.SaveChanges();
        }
        //报志愿
        public bool AddVolunteer(string PracticeNo, string EntPracNo, string PosID, int VolunteerSequence, int PosSequence)
        {
            Db.T_PracticeVolunteer.AddOrUpdate(new T_PracticeVolunteer()
            {
                PracticeNo = PracticeNo,
                EntPracNo = EntPracNo,
                PosID = PosID,
                VolunteerSequence = VolunteerSequence,
                PosSequence = PosSequence,//岗位顺序
                InterviewTime = 0,
                VolunteerStatus = 0,    //志愿状态
                PreVolStatus = "0"//预填志愿
            });
            return Db.Saved();
        }
        //修改编辑志愿
        public bool EditVolunteer(string PracticeNo, string EntPracNo, string PosID, int VolunteerSequence, int PosSequence)
        {
            var volunteer = Db.T_PracticeVolunteer.Find(PracticeNo, EntPracNo, PosID);
            if (volunteer == null)
            {
                throw new Exception("不存在这个志愿");
            }
            volunteer.VolunteerSequence = VolunteerSequence;
            volunteer.PosSequence = PosSequence;
            return Db.SaveModified(volunteer);
        }
        //确定志愿
        public bool ConfirVolunteer(string uid, string EntPracNo, string PosID)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var volunteer = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.EntPracNo == EntPracNo && a.PosID == PosID);
            if (volunteer == null)
            {
                throw new Exception("不存在该志愿");
            }
            else
            {
                volunteer.PreVolStatus = "1";//预填志愿改成1
                return Db.SaveModified(volunteer);
            }

        }
        //删除志愿
        public bool DeleteVolunteer(string PracticeNo, string EntPracNo, string PosID)
        {
            var volunteer = Db.T_PracticeVolunteer.Find(PracticeNo, EntPracNo, PosID);
            if (volunteer == null)
            {
                throw new Exception("不存在这个志愿");
            }
            return Db.SaveDelete(volunteer);
        }
        //检查是否已经添加志愿
        public bool GetPrepareVolunteer(string PracticeNo, string EntPracNo, string PosID)
        {
            var volunteer = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.EntPracNo == EntPracNo && a.PosID == PosID);
            if (volunteer == null)
            {
                return true;
            }
            else
                throw new Exception("志愿已经添加");
        }
        //获取志愿列表
        public List<Volunteer> GetVolunteers(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var volunteers = Db.T_PracticeVolunteer.Where
                (a => a.PracticeNo == PracticeNo).ToList().Select(b => new Volunteer()
                {
                    PracticeNo = b.PracticeNo,
                    EntPracNo = b.EntPracNo,
                    EntName = Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == b.EntPracNo).T_Enterprise.Ent_Name,
                    PosID = b.PosID,
                    PositionName = Db.T_PracticePosition.FirstOrDefault(a => a.EntPracNo == b.EntPracNo && a.PositionID == b.PosID).C_Position.PositionName,
                    VolunteerSequence = b.VolunteerSequence,
                    PosSequence = b.PosSequence,
                    VolunteerStatus = b.VolunteerStatus,//志愿状态
                    PreVolStatus = b.PreVolStatus,//预填志愿状态，1  正式志愿，0  预填志愿
                    VolStatusName = Db.C_VolPosStatus.FirstOrDefault(a => a.VolStatusID == b.VolunteerStatus).VolStatusName,
                    InterviewTime = Convert.ToInt32(b.InterviewTime).ToDateTime(),
                    InterviewScore=Convert.ToDouble(b.InterviewScore)
                }).ToList();
            return volunteers;
        }
        //查找具体的一个志愿
        public T_PracticeVolunteer GetVolunteer(string PracticeNo, string EntPracNo, string PosID)
        {
            if (PracticeNo.HasValue() && EntPracNo.HasValue() && PosID.HasValue())
            {
                var volunteer = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.EntPracNo == EntPracNo && a.PosID == PosID);
                return volunteer;
            }
            else
                return null;
        }
        //获取最终的实习志愿
        public Volunteer GetFormaVolunteer(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var volunteer = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5);
            if (volunteer == null)
            {
                return null;
                //throw new Exception("不存在该志愿");
            }                       
            else
            {
                return new Volunteer()
                {
                    EntName = Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == volunteer.EntPracNo).T_Enterprise.Ent_Name,
                    Ent_No = Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == volunteer.EntPracNo).Ent_No,
                    PositionName = Db.T_PracticePosition.FirstOrDefault(a => a.EntPracNo == volunteer.EntPracNo && a.PositionID == volunteer.PosID).C_Position.PositionName,
                    PracticeDept = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == PracticeNo).PracticeDept,
                    PracticeDivDept = Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == PracticeNo).PracticeDivDept
                };
            }
        }
        //获取自己的实习志愿数量
        public int GetFinalVolunteerCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            return Db.T_PracticeVolunteer.Count(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5);
        }

        //获取自己的正式志愿数量
        public int GetFormalVolunteerCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var conut = Db.T_PracticeVolunteer.Count(a => a.PracticeNo == PracticeNo && a.PreVolStatus == "1");
            return conut;
        }
        //获取志愿数量
        public int GetVolunteerCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var Count = Db.T_PracticeVolunteer.Where(a => a.PracticeNo == PracticeNo).Count();
            if (Count == 0)
            {
                throw new Exception("暂不能进行该项操作");
            }
            else
                return Count;
        }
        //获取预报名志愿数量
        public int GetPerparVolunteerCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var Count = Db.T_PracticeVolunteer.Where(a => a.PracticeNo == PracticeNo && a.PreVolStatus == "0").Count();
            //if (Count == 0)
            //{
            //    throw new Exception("该学生还不能写记录");
            //}
            //else
            return Count;
        }
        private string GetEntPic(string Ent_No)
        {
            var context = new DataContext();
            // 获取企业的轮播图
            context.SetFiled("UnitID", Ent_No);
            context.SetFiled("Ent_No", Ent_No);
            var adList = S<T_DownLoadFiles>().All(context, "某单位的介绍图集");
            return adList.Select(a => a.DLFileUrl).ToList().PackString(';');

        }
        //private List<string> GetEntPicture(string Ent_No)
        //{
        //    var PicList =new List<T_DownLoadFiles>();
        //    var Grogshop =GetGrogshops();
        //    var context = new DataContext();
        //    Grogshop.Where(a => true).Select(b => Ent_No).ToList().
        //       ForEach(c =>
        //      {
        //          context.SetFiled("UnitID", c);
        //           PicList.Add(S<T_DownLoadFiles>().All(context, "某单位的介绍图集").FirstOrDefault());
        //      });
        //    return PicList;
        //}
        //获取所有的企业列表
        public List<AllEnterprise> GetEnterprises(string EntCategoryID)
        {
            var enterprise = Db.T_Enterprise.Where(a => a.EntState == 3).ToList()
                    .Select(b => new AllEnterprise()
                    {
                        Ent_No = b.Ent_No,
                        Ent_Name = b.Ent_Name,
                        EntAddress = b.EntAddress,
                        EntResume = b.EntResume,
                        Email = b.Email,
                        Photos = GetEntPic(b.Ent_No),
                        // Photos = b.EntPhotos,
                        Video = b.EntVideos,
                        EntRank = b.EntRank,
                        EntPracNo = GetEntPracNoByEntNo_AllPrac(b.Ent_No),
                        EntCategoryID=b.EntCategoryID
                    }).ToList();
            if (EntCategoryID.HasValue())
            {
                enterprise = enterprise.Where(a => a.EntCategoryID== EntCategoryID).ToList();
               
            }
            return enterprise;
        }
        //条件查询
        public List<AllEnterprise> Enterprises(string EntCategoryID)
        {
            var enterprise = Db.T_Enterprise.Where(a => a.EntCategoryID == EntCategoryID).ToList()
                .Select(b => new AllEnterprise()
                {
                    Ent_No = b.Ent_No,
                    Ent_Name = b.Ent_Name,
                    EntAddress = b.EntAddress,
                    EntResume = b.EntResume,
                    Email = b.Email,
                    // Photos = GetEntPic(b.Ent_No)
                    Photos = b.EntPhotos,
                    Video = b.EntVideos,
                    EntRank = b.EntRank,
                    EntPracNo = GetEntPracNoByEntNo(b.Ent_No)
                }).ToList();
            return enterprise;
        }
        //获取企业数量
        public int GetEnterpriseCount()
        {
            return Db.T_Enterprise.Count();
        }
        //单个的企业详细信息
        public T_Enterprise SingleEnterprise(string Ent_No)
        {
            var Enterprise = Db.T_Enterprise.FirstOrDefault(a => a.Ent_No == Ent_No);
            return Enterprise;
        }
        //获取招聘企业列表
        public List<Grogshop> GetGrogshops(string EntCategoryID,string schoolid)
        {
            var entprise = Db.T_PracBatch.Where(a => a.SchoolID == schoolid && a.IsCurrentBatch == "是").
                SelectMany(x => x.T_EntBatchReg.
                Where(m => m.ApplyStatus == 2).Select(c => c.T_Enterprise).
                Where(g => g.EntState == 3)).ToList().Select(b => new Grogshop()
                {
                    EntNo = b.Ent_No,
                    Name = b.Ent_Name,
                    Address = b.EntAddress,
                    Description = b.EntResume,
                    Email = b.Email,
                    Photos = GetEntPic(b.Ent_No),
                    //Photos = b.EntPhotos,
                    Video = b.EntVideos,
                    EntRank = b.EntRank,
                    EntPracNo = b.T_EntBatchReg.FirstOrDefault(j=>j.T_PracBatch.IsCurrentBatch== "是").EntPracNo,
                    PositionCount = b.T_EntBatchReg.FirstOrDefault(k=>k.T_PracBatch.IsCurrentBatch=="是").T_PracticePosition.Count
                }).ToList();
            if (EntCategoryID.HasValue())
            {
                entprise = entprise.Where(
                   a => a.EntCategoryID == EntCategoryID).ToList();
            }
            var v3entprise = Db.V3_EnterpriseApply.ToList();
            if (v3entprise.Any()) //没有分散实习的企业,直接读取企业
            {
                v3entprise.ForEach(item =>
                {
                    var t = entprise.FirstOrDefault(d => d.EntNo == item.Ent_No);
                    if (t!=null)
                    {
                        entprise.Remove(t);
                    }
                });
            }
            return entprise;
        }
        //招聘企业的数量
        public int GrogshopsCount(string EntCategoryID,string schoolid)
        {
            return GetGrogshops(EntCategoryID,schoolid).Count;
        }
        //计算实习岗位的数量
        public int PractPositionCount(string EntPracNo)
        {
            return Db.T_PracticePosition.Count(a => a.EntPracNo == EntPracNo);
        }

        //单个招聘企业的详细信息
        public T_Enterprise Enterprise(string EntPracNo)
        {
            var Enterprise = Db.T_EntBatchReg.Where(a => a.EntPracNo == EntPracNo).Select
                    (b => b.T_Enterprise).FirstOrDefault();
            return Enterprise;
        }
        //获取岗位详情
        public List<Job> GetJobs(string EntPracNo)//企业实习号
        {
            var Job = Db.T_PracticePosition.Where
                  (a => a.EntPracNo == EntPracNo).ToList().
                  Select(b => new Job()
                  {
                      //  Ent_No=b.T_EntBatchReg.Ent_No,
                      EntPracNo = EntPracNo,
                      PositionID = b.PositionID,
                      PositionName = b.C_Position.PositionName,
                      PosQuantity = b.PosQuantity,//企业招聘人数
                      Discription = b.PosDesc,
                      PosExpireDate = b.PosExpireDate.ToDateTime(),
                      RemainRecruitingCount = b.PosQuantity - GetSucceedEnter(EntPracNo, b.PositionID)
                  }).ToList();
            return Job;
        }
        //找到表里面记录最大的值（周记ID int ）
        public int GetMaxRecordNo()
        {
            var RecordNo = Db.T_WeekRecord.Max(a => a.RecordNo);
            if (RecordNo == 0)
            {
                return RecordNo = 0;
            }
            return RecordNo;
        }
        //添加周记
        public bool AddDiary(Diary diary)
        {
            Db.T_WeekRecord.AddOrUpdate(new T_WeekRecord()
            {
                PracticeNo = diary.PracticeNo,
                RecordNo = diary.RecordNo,
                RecordTitle = diary.RecordTitle,
                RecordContent = diary.RecordContent,
                RecordComment = diary.RecordComment,
                RecordDate = Convert.ToInt32(diary.RecordDate)
            });
            Db.T_WeekRecordExtemsion.AddOrUpdate(new T_WeekRecordExtemsion()
            {
                PracticeNo = diary.PracticeNo,
                RecordNo = diary.RecordNo,
                pic = diary.Pic,
            });
            return Db.Saved();
        }
        //编辑周记
        public bool EditDiary(Diary diary)
        {
            // var Diary = Db.T_WeekRecord.Find(diary.PracticeNo, diary.RecordNo);
            if (diary == null)
            {
                throw new Exception("不存在这个周记");
            }
            Db.T_WeekRecord.AddOrUpdate(new T_WeekRecord()
            {
                RecordNo = diary.RecordNo,
                PracticeNo = diary.PracticeNo,
                RecordTitle = diary.RecordTitle,
                RecordContent = diary.RecordContent,
                RecordDate = diary.RecordDate
            });
            return Db.Saved();
        }
        //删除周记
        public bool DeleteDiary(string PracticeNo, int RecordNo)
        {
            var diary = Db.T_WeekRecord.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.RecordNo == RecordNo);
            var diaryExtemsion = Db.T_WeekRecordExtemsion.Find(PracticeNo, RecordNo);
            if (diary == null && diaryExtemsion == null)
            {
                throw new Exception("不存在该周记");
            }
            Db.Entry(diary).State = System.Data.Entity.EntityState.Deleted;
            Db.Entry(diaryExtemsion).State = System.Data.Entity.EntityState.Deleted;
            return Db.SaveChanges() > 0;
        }
        //判断给用户是否已经有写周记了,获得他的周记数量
        public int GetDiaryCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var count = Db.T_WeekRecord.Where(a => a.PracticeNo == PracticeNo).Count();
            return count;
        }
        //判断该用户是否已经被企业录取了，只有被企业录取了，才能开始写周记
        public bool CheckVolunStatus(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var volunteer = Db.T_PracticeVolunteer.Where(a => a.PracticeNo == PracticeNo).ToList();
            foreach (var item in volunteer)
            {
                if (item.VolunteerStatus == 5)//学生已经同意企业的录取
                    return true;
            }
            throw new Exception("暂不能进行该项操作");
        }
        //获取周记列表
        public List<Diary> GetDiarys(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var diary = Db.T_WeekRecord.Where(a => a.PracticeNo == PracticeNo).ToList().
                Select(b => new Diary()
                {
                    uid = uid,
                    PracticeNo = PracticeNo,
                    RecordNo = b.RecordNo,
                    RecordTitle = b.RecordTitle,
                    RecordContent = b.RecordContent,
                    RecordComment = b.RecordComment,
                    RecordDate = b.RecordDate,
                    RecordTime = b.RecordDate
                    //RecordScore = b.RecordScore
                }).ToList();
            return diary.OrderBy(c => c.RecordDate).ToList();
        }
        //获取具体的某个周记
        public Diary GetDiary(string uid, int recordno)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var diary = Db.T_WeekRecord.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.RecordNo == recordno);
            var diaryextemsion=Db.T_WeekRecordExtemsion.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.RecordNo == recordno);
            if (diaryextemsion == null)
            {
                return new Diary()
                {
                    PracticeNo = diary.PracticeNo,
                    RecordNo = diary.RecordNo,
                    RecordTitle = diary.RecordTitle,
                    RecordContent = diary.RecordContent,
                    RecordDate = diary.RecordDate,
                    RecordComment = diary.RecordComment,
                    Pic =null
                };
            }
            else
            {
                return new Diary()
                {
                    PracticeNo = diary.PracticeNo,
                    RecordNo = diary.RecordNo,
                    RecordTitle = diary.RecordTitle,
                    RecordContent = diary.RecordContent,
                    RecordDate = diary.RecordDate,
                    RecordComment = diary.RecordComment,
                    Pic = diary.T_WeekRecordExtemsion.pic
                };
            }        
        }
        //获取案例里面的子模块（案例描述，案例分析，心得体会）
        public List<PracticeCase> GetItemNobyUserID(string uid)
        {
            var PracBatchID = GetPracBatchIDByUserID(uid);
            var item = Db.C_PracticeCaseItem.Where(a => a.PracBatchID == PracBatchID).
                Select(b => new PracticeCase
                {
                    ItemNo = b.ItemNo,
                    ItemName = b.ItemName,
                }).ToList();
            if (item.Count() != 0)
            {
                return item;
            }
            else
            {
                throw new Exception("请联系相关人员设立案例内容项");
            }
        }
        //获得案例列表
        public List<PracticeCase> GetPraceticeCase(string PracticeNo)
        {
            var tmp = from a in Db.T_PracticeCase
                      where a.PracticeNo == PracticeNo
                      group a by a.CaseNo into g
                      select g;
            var praceticecase = tmp.ToList().Select(g => new PracticeCase
            {
                CaseTime = Convert.ToDateTime(g.FirstOrDefault().T_PracticeCaseExtension.CaseTime.ToString()),
                // CaseTime=Convert.ToInt32(),
                CaseName = g.FirstOrDefault().CaseName,
                CaseNo = g.Key
            }).ToList();
            return praceticecase.OrderBy(c => c.CaseTime).ToList();
        }

        //获取某个具体的案例，从数据库里读出来
        public List<PracticeCase> GetCase(string uid, int CaseNo)
        {
            var PraceticNo = GetPracticeNoByUserID(uid);
            var caseextension = Db.T_PracticeCaseExtension.Where(a => a.PracticeNo == PraceticNo && a.CaseNo == CaseNo);
            if (!caseextension.Any())
            {
                var Case = Db.T_PracticeCase.Where(a => a.PracticeNo == PraceticNo && a.CaseNo == CaseNo).Select(b => new PracticeCase()
                {
                    CaseName = b.CaseName,
                    CaseNo = b.CaseNo,
                    ItemNo = b.ItemNo,
                    ItemName = b.C_PracticeCaseItem.ItemName,
                    ItemContent = b.ItemContent,
                    Pic =null
                }).ToList();
                return Case;
            }
            else
            {
                var Case = Db.T_PracticeCase.Where(a => a.PracticeNo == PraceticNo && a.CaseNo == CaseNo).Select(b => new PracticeCase()
                {
                    CaseName = b.CaseName,
                    CaseNo = b.CaseNo,
                    ItemNo = b.ItemNo,
                    ItemName = b.C_PracticeCaseItem.ItemName,
                    ItemContent = b.ItemContent,
                    Pic = b.T_PracticeCaseExtension.Pic
                }).ToList();
                return Case;
            }
        
        }
        //删除案例
        public bool DeleteCase(string uid, int CaseNo)//参数必须按表中的主键来删除
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var Case = Db.T_PracticeCase.Where(b => b.PracticeNo == PracticeNo && b.CaseNo == CaseNo).ToList();//3条记录  数组                       
            foreach (var item in Case)
            {
                if (!Db.SaveDelete(item))
                {
                    return false;
                }
            }
            return true;
        }
        //获取用户当前的案例数
        public int GetCaseCount(string uid)
        {
            var count = GetItemNobyUserID(uid).Count();
            var PracticeNo = GetPracticeNoByUserID(uid);
            var Case = Db.T_PracticeCase.Where(a => a.PracticeNo == PracticeNo).Count();
            return Case / count;
        }
        public int CaseCount(string uid)
        {
            var PracticeNo = GetPracticeNoByUserID(uid);
            var Case = Db.T_PracticeCase.Where(a => a.PracticeNo == PracticeNo).Count();
            return Case;
        }
        //找到表里面记录最大的值（案例ID int ）
        public int GetMaxCaseNo()
        {
            try
            {
                var CaseNo = Db.T_PracticeCase.Max(a => a.CaseNo);
                //if (CaseNo == 0)
                //{
                //    return CaseNo = 0;
                //}
                return CaseNo;
            }
            catch
            {
                throw new Exception("数据为空");
            }
        }
        //添加案例
        public bool AddPracticeCase(List<PracticeCase> practicecase)
        {
            foreach (var item in practicecase)
            {
                Db.T_PracticeCase.AddOrUpdate(new T_PracticeCase()
                {
                    PracticeNo = item.PracticeNo,
                    CaseNo = item.CaseNo,
                    CaseName = item.CaseName,
                    ItemNo = item.ItemNo,
                    // ItemName=practicecase.ItemName,
                    ItemContent = item.ItemContent
                });
                Db.T_PracticeCaseExtension.AddOrUpdate(new T_PracticeCaseExtension()
                {
                    PracticeNo = item.PracticeNo,
                    CaseNo = item.CaseNo,
                    ItemNo = item.ItemNo,
                    CaseTime = item.CaseTime,
                    Pic = item.Pic
                });
            }
            return Db.Saved();
        }

        public AppBag Msgs(DataContext dataContext)
        {
            return new AppBag()
            {
                Msgs = new List<Msg>()
              {
                  new Msg()
                  {
                      Content = "测试消息内容",
                      Id = "Msg-01",
                      Time = DateTime.Now,
                      Title = "测试消息标题"
                  } ,
                  new Msg()
                  {
                      Content = "测试消息内容",
                      Id = "Msg-01",
                      Time = DateTime.Now,
                      Title = "测试消息标题"
                  }
              }
            };
        }


    }
}