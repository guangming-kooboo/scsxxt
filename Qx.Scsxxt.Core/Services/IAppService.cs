using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Tools;
using Core.Services.Entity;

namespace Qx.Scsxxt.Core.Services
{
    public interface IAppService
    {
        #region 管理中心

        string FindFatherID(string childID);
        bool SavePic(string faqid, string picpath, string uid, string faqtypeid);
        C_FAQType FindFAQType(string id);
        List<SelectListItem> FAQFatherType(string fatherid);
        List<SelectListItem> FAQChildType(string fatherid);
        List<SelectListItem> ChildTypeNotNULL(string typeid);
        List<SelectListItem> FAQType();
        List<SelectListItem> ChildType(string fatherid);
        List<SelectListItem> FAQState();
        List<FAQ> FAQList(string faqtypeid);
        FAQ DetailFAQ(string faqid);
        bool AddorEditDraftFAQ(FAQ faq);
        bool AddorEditFormFAQ(FAQ faq);
        bool TurnForm(string faqid);
        bool DeleteFAQ(string faqid);
        bool AddorEditFAQType(C_FAQType type);
        bool DeleteFAQType(string faqtypeid);
        #endregion

        T_PracticeVolunteer IsEnroll(string uid);
        int PractPositionCount(string EntPracNo);
        string GetEntPracNoByEntNo_AllPrac(string Ent_No);
        bool OverBaoming(string uid);
        T_User UserInfo(string uid);
        int CaseCount(string uid);
        string GetSchoolName(string schoolid);
        //List<AnswerQuestion> GetAnswerQuestions(string Ent_No);
        List<JobWanted> GetEntFindStu(string uid);
        List<JobWanted> GetStuFindEnt(string uid);
        List<Resume> GetResumes(string PracticeNo);
        int GetFinalVolunteerCount(string uid);
        List<SelectListItem> YetSubmit(string uid);
        int CheckAgreeCount(string PracticeNo);
        List<SelectListItem> EntCategory();
        int GrogshopsCount(string EntCategoryID,string schoolid);
        bool CheckEntBatch( string Ent_No, DataContext dataContext);
        List<DownLoadFiles> GetDownLoadFiles(string UnitID, int DLFileColumnID);
        string ReportContent(string uid);
        bool AddReport(Report report);
        int GetSucceedEnter(string EntPracNo, string PosID);
        int GetCountByPosID(string EntPracNo, string PosID);
        List<AllEnterprise> GetPositionCount();
        string GetStuName(string uid);
        bool ForgetPsw(string uid, string psw);
        //拒绝录取
        bool DisagreePosition(string PracticeNo, string EntPracNo, string PosID);
        //同意录取
        bool AgreePosition(string PracticeNo, string EntPracNo, string PosID);
        //志愿详情
        Volunteer GetVolunteerDetail(string PracticeNo, string EntPracNo, string PosID);
        //获取企业评价学生详情
        List<EntEvaluateStu> GetEntEvaluateStu(string uid);
        //获取实习鉴定表里面的评论项
        T_StuBatchReg AllComment(string uid);
        UserInfo StuAllInfo(string uid);
        //获取最终的实习志愿
        Volunteer GetFormaVolunteer(string uid);
        //学生报名结束，改变学生的生涯状态，报名已结束
        bool ChangeCareerStatus(string uid);
        //获取学生评价学校详情
        List<StuEvaluateSchool> GetStuEvaluateSchool(string uid);
        //获取学生评价企业详情
        List<StuEvaluateEnt> GetStuEvaluateEnt(string uid);
        int GetEvaluateEntCount(string uid);
        int GetEvaluateSchoolCount(string uid);
        bool AddStuEvaluateSchool(List<StuEvaluateSchool> EvaluateSchool);
        //获取学校评价等级项
        List<SelectListItem> GetStuEvaSchoolGradeLevelItem(string PraBatchID);
        //获取学校评价项
        List<StuEvaluateSchoolItem> GetEvaluateSchoolItem(string PraBatchID);
        bool AddStuEvaluateEnt(List<StuEvaluateEnt> EvaluateEnt);
        //获取学生实习企业的名字
        string GetEntName(string PracticeNo);
        //通过用户id查找实习批次号
        string GetPracBatchIDByUserID(string uid);
        //通过uid查找详细信息
        UserInfo GetPersonalInfo(string uid);
        //获取具体的某个周记
        Diary GetDiary(string uid, int RecordNo);
        //获取预报名志愿数量
        int GetPerparVolunteerCount(string uid);
        //获取自己的正式志愿数量
        int GetFormalVolunteerCount(string uid);
        //获取企业数量
        int GetEnterpriseCount();
        //获取志愿数量
        int GetVolunteerCount(string uid);
        //获取用户当前的案例数
        int  GetCaseCount(string uid);
        //判断给用户是否已经有写周记了,获得他的周记数量
        int GetDiaryCount(string uid);
        //找到表里面记录最大的值（周记ID int ）
        int GetMaxRecordNo();
        //找到表里面记录最大的值（案例ID int ）
        int GetMaxCaseNo();
        //通过uid查看学生生涯状态
        int GetCareerStatus(string uid);
        //获取所有企业的列表
        List<AllEnterprise> GetEnterprises(string EntCategoryID);
        //单个的企业详细信息
        T_Enterprise SingleEnterprise(string Ent_No);
        //获取招聘企业列表
        List<Grogshop> GetGrogshops(string EntCategoryID, string schoolid);
       // Grogshop Enterprise(string Ent_No);
       //单个招聘企业详情
       T_Enterprise Enterprise(string EntPracNo);
        //获取岗位详情
        List<Job> GetJobs(string EntPracNo);
        List<SelectListItem> GetSchoolIItems();
        //通过学生ID找到学生实习号
        string GetPracticeNoByUserID(string uid);
        //通过企业号查找企业实习号
        string GetEntPracNoByEntNo(string Ent_No);
        //通过企业实习号查找企业号
        string GetEntNoByEntPracNo(string EntPracNo);
        //通过企业实习号查找岗位ID
        string GetPositionID(string EntPracNo);
        //find，通过uid查找学生表的详情
        T_Student FindStudent(string uid);
        //find，通过uid查找用户表的详情
         T_User FindStu(string uid);
        //查志愿
        T_PracticeVolunteer FindVolunteer(string uid);
        //查找具体的一个志愿
        T_PracticeVolunteer GetVolunteer(string PracticeNo, string EntPracNo, string PosID);
        //检查密码是否匹配
        bool CheckPsw(string NewPsw, string uid);
        //修改密码
        bool RevisePsw(DataContext dataContext, UserInfo userInfo);
        //检查是否已经添加志愿
        bool GetPrepareVolunteer(string PracticeNo, string EntPracNo, string PosID);
        bool Login(DataContext dataContext,string uid,string psw);
        bool UpdateInfo(DataContext dataContext,UserInfo userInfo);
        bool UpdatePhoto(DataContext dataContext, UserInfo userInfo);
        //添加志愿
        bool AddVolunteer(string PracticeNo, string EntPracNo, string PosID, int VolunteerSequence, int PosSequence);
        //编辑修改志愿
        bool EditVolunteer(string PracticeNo, string EntPracNo, string PosID, int VolunteerSequence, int PosSequence);
        //确定志愿
        bool ConfirVolunteer(string uid, string EntPracNo, string PosID);
        //删除志愿
        bool DeleteVolunteer(string PracticeNo, string EntPracNo, string PosID);
        //判断该用户是否已经被企业录取了，只有被企业录取了，才能开始写周记
        bool CheckVolunStatus(string uid);
        bool AddDiary(Diary diary);
        bool EditDiary(Diary diary);
        bool DeleteDiary(string PracticeNo, int recordno);
        //添加，编辑案例
        bool AddPracticeCase(List<PracticeCase> practicecase);
       
        //删除案例
        bool DeleteCase(string uid, int CaseNo);
        //获取某个具体的案例，从数据库里读出来
        List<PracticeCase> GetCase(string uid, int CaseNo);
        //获得案例列表
        List<PracticeCase> GetPraceticeCase(string PracticeNo);
        //获取案例里面的子模块（案例描述，案例分析，心得体会）
        List<PracticeCase> GetItemNobyUserID(string uid);
        //志愿列表
        List<Volunteer> GetVolunteers(string uid);
        //周记列表
        List<Diary> GetDiarys(string uid);
        //获取企业评价项
        List<StuEvaluateEntItem> GetEvaluateEntItem(string PraBatchID);
        //获取企业评价等级项
        List<SelectListItem> GetStuEvaEntGradeLevelItem(string PraBatchID);
        AppBag Msgs(DataContext dataContext);
        //AppBag Diarys(DataContext dataContext);
        //AppBag Reports(DataContext dataContext);
      //  AppBag Volunteers(DataContext dataContext);
        //企业咨询
      //  AppBag Grogshops(DataContext dataContext);
        //招聘信息
     //   AppBag Jobs(DataContext dataContext);
    }
}