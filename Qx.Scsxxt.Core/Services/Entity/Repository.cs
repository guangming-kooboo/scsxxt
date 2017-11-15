namespace Core.Services.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Repository : DbContext
    {
        public Repository()
            : base("name=Repository")
        {
        }

        public virtual DbSet<C_ApplyStatus> C_ApplyStatus { get; set; }
        public virtual DbSet<C_CareerStatus> C_CareerStatus { get; set; }
        public virtual DbSet<C_City> C_City { get; set; }
        public virtual DbSet<C_ContentColumn> C_ContentColumn { get; set; }
        public virtual DbSet<C_ContentType> C_ContentType { get; set; }
        public virtual DbSet<C_County> C_County { get; set; }
        public virtual DbSet<C_DisperseState> C_DisperseState { get; set; }
        public virtual DbSet<C_EditStatus> C_EditStatus { get; set; }
        public virtual DbSet<C_EntCategory> C_EntCategory { get; set; }
        public virtual DbSet<C_EntEvaluateStuItem> C_EntEvaluateStuItem { get; set; }
        public virtual DbSet<C_EntEvaStuGradeLevelItem> C_EntEvaStuGradeLevelItem { get; set; }
        public virtual DbSet<C_EntRank> C_EntRank { get; set; }
        public virtual DbSet<C_FAQState> C_FAQState { get; set; }
        public virtual DbSet<C_FAQType> C_FAQType { get; set; }
        public virtual DbSet<C_MsgSendType> C_MsgSendType { get; set; }
        public virtual DbSet<C_MsgType> C_MsgType { get; set; }
        public virtual DbSet<C_NewsState> C_NewsState { get; set; }
        public virtual DbSet<C_NewsType> C_NewsType { get; set; }
        public virtual DbSet<C_Position> C_Position { get; set; }
        public virtual DbSet<C_PracAttendanceGradeItem> C_PracAttendanceGradeItem { get; set; }
        public virtual DbSet<C_PracAttendanceItem> C_PracAttendanceItem { get; set; }
        public virtual DbSet<C_PracticeCaseItem> C_PracticeCaseItem { get; set; }
        public virtual DbSet<C_PracticeIdentificationItem> C_PracticeIdentificationItem { get; set; }
        public virtual DbSet<C_ProPosition> C_ProPosition { get; set; }
        public virtual DbSet<C_Province> C_Province { get; set; }
        public virtual DbSet<C_ReadStatus> C_ReadStatus { get; set; }
        public virtual DbSet<C_SchoolEvaStuGradeLevelItem> C_SchoolEvaStuGradeLevelItem { get; set; }
        public virtual DbSet<C_SelfEvaluationItem> C_SelfEvaluationItem { get; set; }
        public virtual DbSet<C_Specialty> C_Specialty { get; set; }
        public virtual DbSet<C_StampType> C_StampType { get; set; }
        public virtual DbSet<C_StuEvaEntGradeLevelItem> C_StuEvaEntGradeLevelItem { get; set; }
        public virtual DbSet<C_StuEvaluateEntItem> C_StuEvaluateEntItem { get; set; }
        public virtual DbSet<C_StuEvaluateSchoolItem> C_StuEvaluateSchoolItem { get; set; }
        public virtual DbSet<C_StuEvaSchoolGradeLevelItem> C_StuEvaSchoolGradeLevelItem { get; set; }
        public virtual DbSet<C_StuInfoType> C_StuInfoType { get; set; }
        public virtual DbSet<C_UnitStatus> C_UnitStatus { get; set; }
        public virtual DbSet<C_UserType> C_UserType { get; set; }
        public virtual DbSet<C_VolPosStatus> C_VolPosStatus { get; set; }
        public virtual DbSet<T_ADs> T_ADs { get; set; }
        public virtual DbSet<T_AnswerQuestion> T_AnswerQuestion { get; set; }
        public virtual DbSet<T_Class> T_Class { get; set; }
        public virtual DbSet<T_Content> T_Content { get; set; }
        public virtual DbSet<T_DownLoadFiles> T_DownLoadFiles { get; set; }
        public virtual DbSet<T_Employee> T_Employee { get; set; }
        public virtual DbSet<T_EntBatchReg> T_EntBatchReg { get; set; }
        public virtual DbSet<T_Enterprise> T_Enterprise { get; set; }
        public virtual DbSet<T_Enterprise_ToCheck> T_Enterprise_ToCheck { get; set; }
        public virtual DbSet<T_EntEvaluateStu> T_EntEvaluateStu { get; set; }
        public virtual DbSet<T_EntReviewerTask> T_EntReviewerTask { get; set; }
        public virtual DbSet<T_EntReviewItem> T_EntReviewItem { get; set; }
        public virtual DbSet<T_EntStudentReviewLink> T_EntStudentReviewLink { get; set; }
        public virtual DbSet<T_Faculty> T_Faculty { get; set; }
        public virtual DbSet<T_FAQ> T_FAQ { get; set; }
        public virtual DbSet<T_FuncBatchOpenSetting> T_FuncBatchOpenSetting { get; set; }
        public virtual DbSet<T_FuncObject> T_FuncObject { get; set; }
        public virtual DbSet<T_HomePageColumn> T_HomePageColumn { get; set; }
        public virtual DbSet<T_HomePageContent> T_HomePageContent { get; set; }
        public virtual DbSet<T_HomePageMenu> T_HomePageMenu { get; set; }
        public virtual DbSet<T_JobWanted> T_JobWanted { get; set; }
        public virtual DbSet<T_Module> T_Module { get; set; }
        public virtual DbSet<T_ModuleBatchOpenSetting> T_ModuleBatchOpenSetting { get; set; }
        public virtual DbSet<T_MsgRec> T_MsgRec { get; set; }
        public virtual DbSet<T_MsgSend> T_MsgSend { get; set; }
        public virtual DbSet<T_News> T_News { get; set; }
        public virtual DbSet<T_PhoneMsg> T_PhoneMsg { get; set; }
        public virtual DbSet<T_Platformer> T_Platformer { get; set; }
        public virtual DbSet<T_PlatformPubToUnit> T_PlatformPubToUnit { get; set; }
        public virtual DbSet<T_PracBatch> T_PracBatch { get; set; }
        public virtual DbSet<T_PracticeAttendance> T_PracticeAttendance { get; set; }
        public virtual DbSet<T_PracticeCase> T_PracticeCase { get; set; }
        public virtual DbSet<T_PracticeCaseExtension> T_PracticeCaseExtension { get; set; }
        public virtual DbSet<T_PracticeIdentification> T_PracticeIdentification { get; set; }
        public virtual DbSet<T_PracticePosition> T_PracticePosition { get; set; }
        public virtual DbSet<T_PracticeVolunteer> T_PracticeVolunteer { get; set; }
        public virtual DbSet<T_RecruitPosition> T_RecruitPosition { get; set; }
        public virtual DbSet<T_ReportData> T_ReportData { get; set; }
        public virtual DbSet<T_Resume> T_Resume { get; set; }
        public virtual DbSet<T_Role> T_Role { get; set; }
        public virtual DbSet<T_RoleFuncObjForbid> T_RoleFuncObjForbid { get; set; }
        public virtual DbSet<T_RoleModule> T_RoleModule { get; set; }
        public virtual DbSet<T_School> T_School { get; set; }
        public virtual DbSet<T_SchoolPubToUnit> T_SchoolPubToUnit { get; set; }
        public virtual DbSet<T_SchoolReviewerTask> T_SchoolReviewerTask { get; set; }
        public virtual DbSet<T_SchoolReviewItem> T_SchoolReviewItem { get; set; }
        public virtual DbSet<T_SchoolStudentReviewLink> T_SchoolStudentReviewLink { get; set; }
        public virtual DbSet<T_SelfEvaluation> T_SelfEvaluation { get; set; }
        public virtual DbSet<T_Signature> T_Signature { get; set; }
        public virtual DbSet<T_Stamps> T_Stamps { get; set; }
        public virtual DbSet<T_StuBatchReg> T_StuBatchReg { get; set; }
        public virtual DbSet<T_StuBatchReg_Extent> T_StuBatchReg_Extent { get; set; }
        public virtual DbSet<T_Student> T_Student { get; set; }
        public virtual DbSet<T_StuEvaluateEnt> T_StuEvaluateEnt { get; set; }
        public virtual DbSet<T_StuEvaluateSchool> T_StuEvaluateSchool { get; set; }
        public virtual DbSet<T_StuFinalEntRecord> T_StuFinalEntRecord { get; set; }
        public virtual DbSet<T_StuInfoPub> T_StuInfoPub { get; set; }
        public virtual DbSet<T_StuPic> T_StuPic { get; set; }
        public virtual DbSet<T_StuResFile> T_StuResFile { get; set; }
        public virtual DbSet<T_StuScoreApply> T_StuScoreApply { get; set; }
        public virtual DbSet<T_StuScoreRecord> T_StuScoreRecord { get; set; }
        public virtual DbSet<T_StuVideo> T_StuVideo { get; set; }
        public virtual DbSet<T_SysMsg> T_SysMsg { get; set; }
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<T_UserRole> T_UserRole { get; set; }
        public virtual DbSet<T_WeekRecord> T_WeekRecord { get; set; }
        public virtual DbSet<T_WeekRecordExtemsion> T_WeekRecordExtemsion { get; set; }
        public virtual DbSet<V3_EnterpriseApply> V3_EnterpriseApply { get; set; }
        public virtual DbSet<V3_StuEnterPriseApply> V3_StuEnterPriseApply { get; set; }
        public virtual DbSet<C_ADColumn> C_ADColumn { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C_ApplyStatus>()
                .Property(e => e.ApplyStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ApplyStatus>()
                .HasMany(e => e.T_EntBatchReg)
                .WithRequired(e => e.C_ApplyStatus)
                .HasForeignKey(e => e.ApplyStatus);

            modelBuilder.Entity<C_CareerStatus>()
                .Property(e => e.CareerStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<C_CareerStatus>()
                .HasMany(e => e.T_StuBatchReg)
                .WithOptional(e => e.C_CareerStatus)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_City>()
                .Property(e => e.ProvinceID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_City>()
                .Property(e => e.CityID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_City>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ContentColumn>()
                .Property(e => e.ContColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ContentColumn>()
                .Property(e => e.SybSystem)
                .IsUnicode(false);

            modelBuilder.Entity<C_ContentColumn>()
                .HasMany(e => e.C_StampType)
                .WithOptional(e => e.C_ContentColumn)
                .HasForeignKey(e => e.StampColumnID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_ContentColumn>()
                .HasMany(e => e.T_ADs)
                .WithRequired(e => e.C_ContentColumn)
                .HasForeignKey(e => e.ADColumnID);

            modelBuilder.Entity<C_ContentColumn>()
                .HasMany(e => e.T_DownLoadFiles)
                .WithRequired(e => e.C_ContentColumn)
                .HasForeignKey(e => e.DLFileColumnID);

            modelBuilder.Entity<C_ContentColumn>()
                .HasMany(e => e.T_News)
                .WithOptional(e => e.C_ContentColumn)
                .HasForeignKey(e => e.NewsColumnID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_ContentType>()
                .Property(e => e.ConTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ContentType>()
                .HasMany(e => e.T_Content)
                .WithRequired(e => e.C_ContentType)
                .HasForeignKey(e => e.ContentTypeID);

            modelBuilder.Entity<C_County>()
                .Property(e => e.CityID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_County>()
                .Property(e => e.CountyID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_County>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<C_County>()
                .HasMany(e => e.T_Enterprise)
                .WithOptional(e => e.C_County)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_DisperseState>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<C_EditStatus>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntCategory>()
                .Property(e => e.EntCategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntCategory>()
                .Property(e => e.EntCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaluateStuItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaluateStuItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaluateStuItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaStuGradeLevelItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaStuGradeLevelItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaStuGradeLevelItem>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaStuGradeLevelItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntEvaStuGradeLevelItem>()
                .HasMany(e => e.T_EntEvaluateStu)
                .WithOptional(e => e.C_EntEvaStuGradeLevelItem)
                .HasForeignKey(e => e.ItemGrade)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_EntRank>()
                .Property(e => e.EntRankID)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntRank>()
                .Property(e => e.EntCategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntRank>()
                .Property(e => e.EntRankName)
                .IsUnicode(false);

            modelBuilder.Entity<C_EntRank>()
                .HasMany(e => e.T_Enterprise)
                .WithRequired(e => e.C_EntRank)
                .HasForeignKey(e => new { e.EntRank, e.EntCategoryID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<C_EntRank>()
                .HasMany(e => e.V3_EnterpriseApply)
                .WithOptional(e => e.C_EntRank)
                .HasForeignKey(e => new { e.EntRankID, e.EntCategoryID })
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_FAQState>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<C_FAQState>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<C_FAQType>()
                .Property(e => e.FAQTypeId)
                .IsUnicode(false);

            modelBuilder.Entity<C_FAQType>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_FAQType>()
                .Property(e => e.FatherID)
                .IsUnicode(false);

            modelBuilder.Entity<C_MsgSendType>()
                .Property(e => e.SendTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_MsgType>()
                .Property(e => e.MsgTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_NewsState>()
                .Property(e => e.NewsStateName)
                .IsUnicode(false);

            modelBuilder.Entity<C_NewsState>()
                .HasMany(e => e.T_News)
                .WithRequired(e => e.C_NewsState)
                .HasForeignKey(e => e.FlowState);

            modelBuilder.Entity<C_NewsType>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_NewsType>()
                .HasMany(e => e.T_News)
                .WithRequired(e => e.C_NewsType)
                .HasForeignKey(e => e.NewsTypeID);

            modelBuilder.Entity<C_Position>()
                .Property(e => e.PositionID)
                .IsUnicode(false);

            modelBuilder.Entity<C_Position>()
                .Property(e => e.PositionName)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceGradeItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceGradeItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceGradeItem>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceGradeItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceGradeItem>()
                .HasMany(e => e.T_PracticeAttendance)
                .WithOptional(e => e.C_PracAttendanceGradeItem)
                .HasForeignKey(e => e.ItemGrade)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_PracAttendanceItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracAttendanceItem>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracticeCaseItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracticeCaseItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracticeCaseItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracticeIdentificationItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracticeIdentificationItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_PracticeIdentificationItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ProPosition>()
                .Property(e => e.ProPosName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ProPosition>()
                .HasMany(e => e.T_Faculty)
                .WithOptional(e => e.C_ProPosition)
                .HasForeignKey(e => e.FacProPos)
                .WillCascadeOnDelete();

            modelBuilder.Entity<C_Province>()
                .Property(e => e.ProvinceID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_Province>()
                .Property(e => e.ProvinceName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ReadStatus>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<C_SchoolEvaStuGradeLevelItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_SchoolEvaStuGradeLevelItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_SchoolEvaStuGradeLevelItem>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<C_SchoolEvaStuGradeLevelItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_SelfEvaluationItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_SelfEvaluationItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_SelfEvaluationItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_Specialty>()
                .Property(e => e.SpeNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_Specialty>()
                .Property(e => e.SchoolID)
                .IsUnicode(false);

            modelBuilder.Entity<C_Specialty>()
                .Property(e => e.SpeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_Specialty>()
                .HasMany(e => e.T_Class)
                .WithRequired(e => e.C_Specialty)
                .HasForeignKey(e => new { e.EntryYear, e.SpeNo, e.SchoolID });

            modelBuilder.Entity<C_StampType>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_StampType>()
                .Property(e => e.SybSystem)
                .IsUnicode(false);

            modelBuilder.Entity<C_StampType>()
                .HasMany(e => e.T_Stamps)
                .WithRequired(e => e.C_StampType)
                .HasForeignKey(e => e.StampsTypeID);

            modelBuilder.Entity<C_StuEvaEntGradeLevelItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaEntGradeLevelItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaEntGradeLevelItem>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaEntGradeLevelItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaEntGradeLevelItem>()
                .HasMany(e => e.T_StuEvaluateEnt)
                .WithRequired(e => e.C_StuEvaEntGradeLevelItem)
                .HasForeignKey(e => e.ItemGrade);

            modelBuilder.Entity<C_StuEvaluateEntItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaluateEntItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaluateEntItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaluateSchoolItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaluateSchoolItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaluateSchoolItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaSchoolGradeLevelItem>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaSchoolGradeLevelItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaSchoolGradeLevelItem>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaSchoolGradeLevelItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_StuEvaSchoolGradeLevelItem>()
                .HasMany(e => e.T_StuEvaluateSchool)
                .WithRequired(e => e.C_StuEvaSchoolGradeLevelItem)
                .HasForeignKey(e => e.ItemGrade);

            modelBuilder.Entity<C_StuInfoType>()
                .Property(e => e.InfoTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_UnitStatus>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<C_UnitStatus>()
                .HasMany(e => e.T_EntBatchReg)
                .WithRequired(e => e.C_UnitStatus)
                .HasForeignKey(e => e.ApplyStatus);

            modelBuilder.Entity<C_UnitStatus>()
                .HasMany(e => e.T_School)
                .WithRequired(e => e.C_UnitStatus)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<C_UserType>()
                .Property(e => e.UserTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<C_VolPosStatus>()
                .Property(e => e.VolStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<C_VolPosStatus>()
                .HasMany(e => e.T_PracticeVolunteer)
                .WithRequired(e => e.C_VolPosStatus)
                .HasForeignKey(e => e.VolunteerStatus);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.ADID)
                .IsUnicode(false);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.ADTitle)
                .IsUnicode(false);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.ADOwner)
                .IsUnicode(false);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.ADPubUser)
                .IsUnicode(false);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.MianPic)
                .IsUnicode(false);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.LinkUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_ADs>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<T_AnswerQuestion>()
                .Property(e => e.AnsQueID)
                .IsUnicode(false);

            modelBuilder.Entity<T_AnswerQuestion>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<T_AnswerQuestion>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<T_AnswerQuestion>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_Class>()
                .Property(e => e.ClassID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Class>()
                .Property(e => e.ClassName)
                .IsFixedLength();

            modelBuilder.Entity<T_Class>()
                .Property(e => e.SpeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Class>()
                .Property(e => e.SchoolID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Class>()
                .HasMany(e => e.T_Student)
                .WithRequired(e => e.T_Class)
                .HasForeignKey(e => e.StuClass);

            modelBuilder.Entity<T_Content>()
                .Property(e => e.ContentID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Content>()
                .Property(e => e.ContentTitle)
                .IsUnicode(false);

            modelBuilder.Entity<T_Content>()
                .Property(e => e.ContentPublisher)
                .IsUnicode(false);

            modelBuilder.Entity<T_Content>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Content>()
                .HasOptional(e => e.T_ADs)
                .WithRequired(e => e.T_Content)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_Content>()
                .HasOptional(e => e.T_DownLoadFiles)
                .WithRequired(e => e.T_Content)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_Content>()
                .HasMany(e => e.T_HomePageContent)
                .WithRequired(e => e.T_Content)
                .HasForeignKey(e => e.HPCID);

            modelBuilder.Entity<T_Content>()
                .HasOptional(e => e.T_News)
                .WithRequired(e => e.T_Content)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_Content>()
                .HasOptional(e => e.T_Signature)
                .WithRequired(e => e.T_Content)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_Content>()
                .HasOptional(e => e.T_Stamps)
                .WithRequired(e => e.T_Content)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_DownLoadFiles>()
                .Property(e => e.DLFileID)
                .IsUnicode(false);

            modelBuilder.Entity<T_DownLoadFiles>()
                .Property(e => e.DLFileUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.EmployeeID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.QQ)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<T_Employee>()
                .HasMany(e => e.T_EntReviewerTask)
                .WithRequired(e => e.T_Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<T_EntBatchReg>()
                .Property(e => e.EntPracNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntBatchReg>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntBatchReg>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.Ent_Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.CountyID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntCategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntRank)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntAddress)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntResume)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntLogo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntAdPics)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntPhotos)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntVideos)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntPPTs)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.EntFiles)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.Contectinfo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise>()
                .HasMany(e => e.T_EntBatchReg)
                .WithRequired(e => e.T_Enterprise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Enterprise>()
                .HasMany(e => e.V3_StuEnterPriseApply)
                .WithOptional(e => e.T_Enterprise)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_Enterprise_ToCheck>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise_ToCheck>()
                .Property(e => e.Ent_Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise_ToCheck>()
                .Property(e => e.EntCategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise_ToCheck>()
                .Property(e => e.EntRank)
                .IsUnicode(false);

            modelBuilder.Entity<T_Enterprise_ToCheck>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntEvaluateStu>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntEvaluateStu>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntEvaluateStu>()
                .Property(e => e.ItemContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntEvaluateStu>()
                .Property(e => e.ItemGrade)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntReviewerTask>()
                .Property(e => e.TaskID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntReviewerTask>()
                .Property(e => e.ItemID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntReviewerTask>()
                .Property(e => e.EmployeeID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntReviewItem>()
                .Property(e => e.ItemID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntReviewItem>()
                .Property(e => e.EntPracNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntReviewItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntStudentReviewLink>()
                .Property(e => e.LinkID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntStudentReviewLink>()
                .Property(e => e.TaskID)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntStudentReviewLink>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_EntStudentReviewLink>()
                .Property(e => e.ReviewComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.FacNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.FacName)
                .IsFixedLength();

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.FacSex)
                .IsFixedLength();

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.FacPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.FacAbstract)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.SchoolID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Faculty>()
                .HasMany(e => e.T_SchoolReviewerTask)
                .WithRequired(e => e.T_Faculty)
                .HasForeignKey(e => e.TeacherID);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.FAQID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.Contents)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.FAQTypeId)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.Video)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.CreatorName)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.Abstract)
                .IsUnicode(false);

            modelBuilder.Entity<T_FAQ>()
                .Property(e => e.Pic)
                .IsUnicode(false);

            modelBuilder.Entity<T_FuncBatchOpenSetting>()
                .Property(e => e.FuncObjID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FuncBatchOpenSetting>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FuncObject>()
                .Property(e => e.FuncObjID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FuncObject>()
                .Property(e => e.ModuleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_FuncObject>()
                .Property(e => e.FuncName)
                .IsUnicode(false);

            modelBuilder.Entity<T_FuncObject>()
                .Property(e => e.FuncID)
                .IsUnicode(false);

            modelBuilder.Entity<T_HomePageColumn>()
                .Property(e => e.HPColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<T_HomePageColumn>()
                .Property(e => e.OnTimeDESC)
                .IsFixedLength();

            modelBuilder.Entity<T_HomePageColumn>()
                .Property(e => e.ContentType)
                .IsFixedLength();

            modelBuilder.Entity<T_HomePageColumn>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<T_HomePageColumn>()
                .HasMany(e => e.T_HomePageContent)
                .WithRequired(e => e.T_HomePageColumn)
                .HasForeignKey(e => e.HPColID);

            modelBuilder.Entity<T_HomePageContent>()
                .Property(e => e.HPCID)
                .IsUnicode(false);

            modelBuilder.Entity<T_HomePageMenu>()
                .Property(e => e.HPMName)
                .IsUnicode(false);

            modelBuilder.Entity<T_HomePageMenu>()
                .Property(e => e.ActionLink)
                .IsUnicode(false);

            modelBuilder.Entity<T_HomePageMenu>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<T_JobWanted>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_JobWanted>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_JobWanted>()
                .Property(e => e.PositionID)
                .IsUnicode(false);

            modelBuilder.Entity<T_JobWanted>()
                .Property(e => e.ResumeURL)
                .IsUnicode(false);

            modelBuilder.Entity<T_JobWanted>()
                .Property(e => e.ReviewRecord)
                .IsUnicode(false);

            modelBuilder.Entity<T_JobWanted>()
                .Property(e => e.Flag)
                .IsUnicode(false);

            modelBuilder.Entity<T_Module>()
                .Property(e => e.ModuleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Module>()
                .Property(e => e.ModuleName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Module>()
                .Property(e => e.FartherModuleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Module>()
                .Property(e => e.PageName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Module>()
                .Property(e => e.ModuleLevel)
                .IsUnicode(false);

            modelBuilder.Entity<T_ModuleBatchOpenSetting>()
                .Property(e => e.ModuleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_ModuleBatchOpenSetting>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_MsgRec>()
                .Property(e => e.MsgID)
                .IsUnicode(false);

            modelBuilder.Entity<T_MsgRec>()
                .Property(e => e.Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<T_MsgSend>()
                .Property(e => e.MsgID)
                .IsUnicode(false);

            modelBuilder.Entity<T_MsgSend>()
                .Property(e => e.Sender)
                .IsUnicode(false);

            modelBuilder.Entity<T_MsgSend>()
                .Property(e => e.Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.NewsID)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.NewsAuthor)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.PicUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.VideoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.Html)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.LinkUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_News>()
                .Property(e => e.Download)
                .IsUnicode(false);

            modelBuilder.Entity<T_PhoneMsg>()
                .Property(e => e.MsgContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_PhoneMsg>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<T_PhoneMsg>()
                .Property(e => e.Flag)
                .IsUnicode(false);

            modelBuilder.Entity<T_PhoneMsg>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Platformer>()
                .Property(e => e.PFNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Platformer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<T_Platformer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<T_Platformer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<T_Platformer>()
                .Property(e => e.PFName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Platformer>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PlatformPubToUnit>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PlatformPubToUnit>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .Property(e => e.BatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .Property(e => e.SchoolID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .Property(e => e.BatchName)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .Property(e => e.StartEnd)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .Property(e => e.IsCurrentBatch)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracBatch>()
                .HasMany(e => e.T_EntBatchReg)
                .WithRequired(e => e.T_PracBatch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_PracBatch>()
                .HasMany(e => e.T_SchoolReviewItem)
                .WithRequired(e => e.T_PracBatch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_PracBatch>()
                .HasMany(e => e.V3_StuEnterPriseApply)
                .WithOptional(e => e.T_PracBatch)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_PracticeAttendance>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeAttendance>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeAttendance>()
                .Property(e => e.ItemGrade)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCase>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCase>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCase>()
                .Property(e => e.ItemContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCase>()
                .Property(e => e.CaseName)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCase>()
                .HasOptional(e => e.T_PracticeCaseExtension)
                .WithRequired(e => e.T_PracticeCase)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_PracticeCaseExtension>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCaseExtension>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeCaseExtension>()
                .Property(e => e.Pic)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeIdentification>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeIdentification>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeIdentification>()
                .Property(e => e.ItemContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticePosition>()
                .Property(e => e.EntPracNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticePosition>()
                .Property(e => e.PositionID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticePosition>()
                .Property(e => e.PosDesc)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeVolunteer>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeVolunteer>()
                .Property(e => e.EntPracNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeVolunteer>()
                .Property(e => e.PosID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeVolunteer>()
                .Property(e => e.InterviewRecord)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeVolunteer>()
                .Property(e => e.Interviewee)
                .IsUnicode(false);

            modelBuilder.Entity<T_PracticeVolunteer>()
                .Property(e => e.PreVolStatus)
                .IsUnicode(false);

            modelBuilder.Entity<T_RecruitPosition>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<T_RecruitPosition>()
                .Property(e => e.PositionID)
                .IsUnicode(false);

            modelBuilder.Entity<T_RecruitPosition>()
                .Property(e => e.PosDesc)
                .IsUnicode(false);

            modelBuilder.Entity<T_RecruitPosition>()
                .Property(e => e.PosRequirement)
                .IsUnicode(false);

            modelBuilder.Entity<T_RecruitPosition>()
                .Property(e => e.PosPay)
                .IsUnicode(false);

            modelBuilder.Entity<T_RecruitPosition>()
                .Property(e => e.Publisher)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.ReportID)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.ReportName)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.SqlStr)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.HeadFields)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.ParaNames)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.ColunmToShow)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.OperateColum)
                .IsUnicode(false);

            modelBuilder.Entity<T_ReportData>()
                .Property(e => e.ReportLog)
                .IsUnicode(false);

            modelBuilder.Entity<T_Resume>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Resume>()
                .Property(e => e.ResumeName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Resume>()
                .Property(e => e.ResumeURL)
                .IsUnicode(false);

            modelBuilder.Entity<T_Role>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Role>()
                .Property(e => e.RoleType)
                .IsUnicode(false);

            modelBuilder.Entity<T_Role>()
                .Property(e => e.subSystem)
                .IsUnicode(false);

            modelBuilder.Entity<T_Role>()
                .Property(e => e.IsDefault)
                .IsFixedLength();

            modelBuilder.Entity<T_RoleFuncObjForbid>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_RoleFuncObjForbid>()
                .Property(e => e.FuncObjID)
                .IsUnicode(false);

            modelBuilder.Entity<T_RoleFuncObjForbid>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<T_RoleModule>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_RoleModule>()
                .Property(e => e.ModuleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_RoleModule>()
                .Property(e => e.DataDomain)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.SchoolID)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.SchoolName)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .Property(e => e.SchoolLogo)
                .IsUnicode(false);

            modelBuilder.Entity<T_School>()
                .HasMany(e => e.T_PracBatch)
                .WithRequired(e => e.T_School)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_SchoolPubToUnit>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolPubToUnit>()
                .Property(e => e.UnitID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolReviewerTask>()
                .Property(e => e.TaskID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolReviewerTask>()
                .Property(e => e.TeacherID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolReviewerTask>()
                .Property(e => e.ItemID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolReviewItem>()
                .Property(e => e.ItemID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolReviewItem>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolReviewItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolStudentReviewLink>()
                .Property(e => e.LinkID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolStudentReviewLink>()
                .Property(e => e.TaskID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolStudentReviewLink>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_SchoolStudentReviewLink>()
                .Property(e => e.ReviewComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_SelfEvaluation>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_SelfEvaluation>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_SelfEvaluation>()
                .Property(e => e.ItemContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_Signature>()
                .Property(e => e.SignatureID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Signature>()
                .Property(e => e.SignatureUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_Signature>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Stamps>()
                .Property(e => e.StampsID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Stamps>()
                .Property(e => e.StampsUrl)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.WeekRecordComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeCaseComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeReport)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeReportFile)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeDept)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeDivDept)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracticeStartEnd)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.PracUnitComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.SchoolComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .Property(e => e.TutorComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg>()
                .HasOptional(e => e.T_StuBatchReg_Extent)
                .WithRequired(e => e.T_StuBatchReg)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_StuBatchReg>()
                .HasOptional(e => e.T_StuScoreApply)
                .WithRequired(e => e.T_StuBatchReg)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_StuBatchReg_Extent>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg_Extent>()
                .Property(e => e.AuthorizedEntNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg_Extent>()
                .Property(e => e.ProofText)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuBatchReg_Extent>()
                .Property(e => e.ProofFile)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuName)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuSex)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuCellphone)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuEMail)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuQQ)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuResume)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.MainPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.Pics)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.Videos)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuClass)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuResourceFile)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.PicMood)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.VideoMood)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .Property(e => e.StuBirthday)
                .IsUnicode(false);

            modelBuilder.Entity<T_Student>()
                .HasMany(e => e.T_StuBatchReg)
                .WithRequired(e => e.T_Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_StuEvaluateEnt>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateEnt>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateEnt>()
                .Property(e => e.ItemContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateEnt>()
                .Property(e => e.ItemGrade)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateSchool>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateSchool>()
                .Property(e => e.ItemNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateSchool>()
                .Property(e => e.ItemContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuEvaluateSchool>()
                .Property(e => e.ItemGrade)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.Ent_Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.EntCategory)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.EntRank)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.EntAddress)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.EntResume)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.Contectinfo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuFinalEntRecord>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuInfoPub>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuInfoPub>()
                .Property(e => e.PubLevel)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuPic>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuPic>()
                .Property(e => e.PicMood)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuPic>()
                .Property(e => e.PicName)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuPic>()
                .Property(e => e.PicLink)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuResFile>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuResFile>()
                .Property(e => e.ResFileMood)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuResFile>()
                .Property(e => e.ResFileName)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuResFile>()
                .Property(e => e.ResFileLink)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.EvidenceFile)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.ApplyContents)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracticeCaseComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.WeekRecordComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracticeContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracticeReport)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracticeReportFile)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracticeStartEnd)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.PracUnitComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.SchoolComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.TutorComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreApply>()
                .Property(e => e.AuditOpinion)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.StuScoreStuScoreID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PraticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.Evidence)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.ApplyContents)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.AuditOpinion)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PracticeCaseComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.WeekRecordComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PracticeContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PracticeReport)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PracticeReportFile)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PracticeStartEnd)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.PracUnitComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.SchoolComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuScoreRecord>()
                .Property(e => e.TutorComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuVideo>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuVideo>()
                .Property(e => e.VideoMood)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuVideo>()
                .Property(e => e.VideoName)
                .IsUnicode(false);

            modelBuilder.Entity<T_StuVideo>()
                .Property(e => e.VideoLink)
                .IsUnicode(false);

            modelBuilder.Entity<T_SysMsg>()
                .Property(e => e.MsgID)
                .IsUnicode(false);

            modelBuilder.Entity<T_SysMsg>()
                .Property(e => e.MsgOwner)
                .IsUnicode(false);

            modelBuilder.Entity<T_SysMsg>()
                .Property(e => e.MsgContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_SysMsg>()
                .Property(e => e.FatherMsgID)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.NickName)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.UserPwd)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .HasOptional(e => e.T_Employee)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_User>()
                .HasOptional(e => e.T_Faculty)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_User>()
                .HasMany(e => e.T_MsgRec)
                .WithRequired(e => e.T_User)
                .HasForeignKey(e => e.Receiver)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_User>()
                .HasMany(e => e.T_MsgSend)
                .WithRequired(e => e.T_User)
                .HasForeignKey(e => e.Sender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_User>()
                .HasOptional(e => e.T_Platformer)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_User>()
                .HasOptional(e => e.T_Student)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_User>()
                .HasMany(e => e.T_SysMsg)
                .WithRequired(e => e.T_User)
                .HasForeignKey(e => e.MsgOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_User>()
                .HasMany(e => e.V3_EnterpriseApply)
                .WithOptional(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_UserRole>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<T_UserRole>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<T_UserRole>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<T_WeekRecord>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_WeekRecord>()
                .Property(e => e.RecordTitle)
                .IsUnicode(false);

            modelBuilder.Entity<T_WeekRecord>()
                .Property(e => e.RecordContent)
                .IsUnicode(false);

            modelBuilder.Entity<T_WeekRecord>()
                .Property(e => e.RecordComment)
                .IsUnicode(false);

            modelBuilder.Entity<T_WeekRecord>()
                .HasOptional(e => e.T_WeekRecordExtemsion)
                .WithRequired(e => e.T_WeekRecord)
                .WillCascadeOnDelete();

            modelBuilder.Entity<T_WeekRecordExtemsion>()
                .Property(e => e.PracticeNo)
                .IsUnicode(false);

            modelBuilder.Entity<T_WeekRecordExtemsion>()
                .Property(e => e.pic)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.Ent_Name)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.EntCategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.EntRankID)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.EntAddress)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.SchoolID)
                .IsFixedLength();

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.Contectinfo)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<V3_EnterpriseApply>()
                .Property(e => e.EntResume)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.StuEnterPriseApplyID)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.Ent_No)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.posDesc)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.practiceDept)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.practiceDivDept)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.positionId)
                .IsUnicode(false);

            modelBuilder.Entity<V3_StuEnterPriseApply>()
                .Property(e => e.PracBatchID)
                .IsUnicode(false);

            modelBuilder.Entity<C_ADColumn>()
                .Property(e => e.ADColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<C_ADColumn>()
                .Property(e => e.SybSystem)
                .IsUnicode(false);
        }
    }
}
