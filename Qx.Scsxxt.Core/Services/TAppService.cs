using Core.Interfaces;
using Core.Model;
using Core.Services.Entity;
using Qx.Tools.CommonExtendMethods;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Qx.Tools;

namespace Core.Services
{
    public  class TAppService:BaseServices, TAppInterface
    {
        #region teacherInfo
        public T_Faculty TeacherInfo(string uid)
        {
            return Db.T_Faculty.FirstOrDefault(a => a.UserID == uid);
        }
        #endregion

        #region FindTeacher
        public T_User FindTeacher(string uid)
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
        #endregion

        #region Login
        public bool Login(string UserID, string Psw)
        {
            var psw = NoneEncrypt(Psw);
            return Db.T_User.Any(a => a.UserID == UserID && a.UserPwd == psw&&a.UserType==11);
        }
        #endregion

        #region RevisePsw
        public bool ForgetPsw(string uid, string psw)
        {
            var user = Db.T_User.Find(uid);
            user.UserPwd = NoneEncrypt(psw);
            Db.Entry(user).State = EntityState.Modified;
            return Db.Saved();
        }
        #endregion

        #region 学校列表GetSchoolIItems
        public List<SelectListItem> GetSchoolIItems()
        {
            return S<T_School>().ToSelectItems();
        }
        #endregion

        #region 企业列表EntpriseList
        private string GetEntPic(string Ent_No)
        {
            var context = new DataContext();
            // 获取企业的轮播图
            context.SetFiled("UnitID", Ent_No);
            context.SetFiled("Ent_No", Ent_No);
            var adList = S<T_DownLoadFiles>().All(context, "某单位的介绍图集");
            return adList.Select(a => a.DLFileUrl).ToList().PackString(';');

        }
        public List<Enterprise> EntpriseList(string SchoolID,string EntCategoryID)
        {
           
            var entprise = Db.T_PracBatch.Where(a => a.SchoolID == SchoolID && a.IsCurrentBatch == "是").
                SelectMany(b => b.T_EntBatchReg.
                Where(m=>m.ApplyStatus==2).Select(c => c.T_Enterprise).
                Where(g=>g.EntState==3)).ToList().Select(b => new Enterprise()
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
                    EntPracNo = b.T_EntBatchReg.FirstOrDefault().EntPracNo
                }).ToList();
            if (EntCategoryID.HasValue())
            {
                 entprise = entprise.Where(
                    a=>a.EntCategoryID== EntCategoryID).ToList();
           
            }
                return entprise;
            

        }
        #endregion

        #region 企业详情EnterpriseDetail
        public T_Enterprise EnterpriseDetail(string Ent_No)
        {
            var Enterprise = Db.T_EntBatchReg.Where(a => a.Ent_No == Ent_No).Select
               (b => b.T_Enterprise).FirstOrDefault();
            return Enterprise;
        }
        #endregion

        #region 企业类型EntCategory
        public List<SelectListItem> EntCategory()
        {
            return Db.C_EntCategory.ToList().Select(b => new SelectListItem
            {
                Value = b.EntCategoryID,
                Text = b.EntCategoryName
            }).ToList().Format("");
        }
        #endregion

        #region 获取入学年份列表GetEntryYear
        public List<SelectListItem> GetEntryYear(string schoolid)
        {
            return Db.C_Specialty.Where(a=>a.SchoolID== schoolid).GroupBy(b=>b.EntryYear).ToList().Select(s => new SelectListItem
            {
                Value=s.Key.ToString(),
                Text=s.FirstOrDefault().EntryYear.ToString()
            }).ToList();
        }
        #region 获取班级名字（同年份的）
        public List<SelectListItem>GetClass(int EntryYear)
        {
            return Db.T_Class.Where(a => a.EntryYear == EntryYear).ToList().Select(s => new SelectListItem
            {
                Value=s.ClassName,
                Text=s.ClassName
            }).ToList();
        }
        #endregion
        #endregion

        #region 学生列表StudentList
        public List<Student>StudentList(string classid)
        {
            if (classid == null)
            {
                var student = Db.T_Student.Select(s => new Student
                {
                    StuNo = s.StuNo,
                    StuName = s.StuName,
                    Phone = s.StuCellphone,
                    UserID = s.UserID
                }).ToList();
                return student;
            }
            else
            {
                //var Class = Db.T_Class.Where(a => a.EntryYear == EntryYear && a.SpeNo == SpeNo).FirstOrDefault();
                var student = Db.T_Student.Where(b => b.StuClass == classid).ToList().Select(s => new Student
                {
                    StuNo = s.StuNo,
                    StuName = s.StuName,
                    Phone = s.StuCellphone,
                    UserID = s.UserID
                }).ToList();
                return student;
            }
        }
        #endregion

        #region 学生信息StudentInfomation
        public Student StudentInfo(string uid)
        {
            var PracticeNo = GetPracticeNo(uid);
            var student = Db.T_Student.FirstOrDefault(a => a.UserID == uid);
            return new Student
            {
                UserID=student.UserID,
                StuName=student.StuName,
                StuSex=student.StuSex,
                EntryYear=student.T_Class.EntryYear,
                Class=student.T_Class.ClassName,
                StuNo=student.StuNo,
                Phone = student.StuCellphone,
                QQ = student.StuQQ,
                Photo=student.Pics,
                EntEnterprise= GetPracEntName(PracticeNo)
            };
        }
        #endregion

        #region 获取实习生的实习单位GetPracEntName
        public string GetPracEntName(string PracticeNo)
        {
            //学生同意,录取成功
            var volunteer = Db.T_PracticeVolunteer.FirstOrDefault(a => a.PracticeNo == PracticeNo && a.VolunteerStatus == 5);
            if (volunteer == null)
            {
                return  null;
                // throw new Exception("该学生还没有实习的企业");
            }
            else
            {
                return Db.T_EntBatchReg.FirstOrDefault(a => a.EntPracNo == volunteer.EntPracNo).T_Enterprise.Ent_Name;
            }
        }
        #endregion

        #region 获取实习生的实习号GetPracticeNo
        public string GetPracticeNo(string uid)
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
        #endregion
        
        #region 获取每个企业的招聘岗位数量
        public List<Enterprise> GetPositionCount()
        {
            var tmp = from a in Db.T_PracticePosition
                          //where a.EntPracNo == EntPracNo
                      group a by a.EntPracNo into g
                      select g;
            var count = tmp.ToList().Select(g => new Enterprise()
            {
                EntPracNo = g.FirstOrDefault().EntPracNo,
                PositionCount = g.Sum(a => a.PosQuantity),
                YetAddVolunteerCount = GetYetAddVolunteerCount(g.FirstOrDefault().EntPracNo),
                RemainRecruitingCount = g.Sum(a => a.PosQuantity) - GetYetAddVolunteerSucceedEnter(g.FirstOrDefault().EntPracNo),
                LastTime =  g.Max(a => a.PosExpireDate).ToDateTime()
            }).ToList();
            return count;
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
        #endregion

        #region 工作量
        #endregion

        #region 获取企业实习号
        public string GetEntPracNoByEntNo(string Ent_No)
        {
            var entpracno= Db.T_EntBatchReg
                .Where(a => a.ApplyStatus == 2 &&a.Ent_No==Ent_No&& a.T_PracBatch.IsCurrentBatch == "是")
                .FirstOrDefault().EntPracNo;
            if (!string.IsNullOrEmpty(entpracno))
            {
                return entpracno;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取已经报了该岗位的人数,并且已经被录取了
        public int GetSucceedEnter(string EntPracNo, string PosID)
        {
            return Db.T_PracticeVolunteer.Where(a => a.EntPracNo == EntPracNo && a.PosID == PosID && a.PreVolStatus == "5").Count();
        }
        #endregion

        #region 获取岗位详情
        public List<Position> PositionList(string Ent_No)//企业实习号
        {
            var EntPracNo = GetEntPracNoByEntNo(Ent_No);
            var Job = Db.T_PracticePosition.Where
                  (a => a.EntPracNo == EntPracNo).ToList().
                  Select(b => new Position()
                  {
                      Ent_No=Ent_No,
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
        #endregion

        #region 文件下载
        //文件下载
        public List<DownLoadFiles> GetDownLoadFiles(string Ent_No, int DLFileColumnID)
        {
            //var file = Convert.ToInt32(DLFileColumnID);
            var DownLoadFile = Db.T_DownLoadFiles.Where(o => o.T_Content.UnitID == Ent_No && o.DLFileColumnID == DLFileColumnID)
                .ToList().Select(b => new DownLoadFiles()
                {
                    DLFileID = b.DLFileID,
                    DLFileColumnID = b.DLFileColumnID,
                    DLFileUrl = b.DLFileUrl
                }).ToList();
            return DownLoadFile;
        }
        #endregion
    }
}
