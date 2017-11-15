namespace Qx.Scsxxt.Extentsion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSet1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BBS_C_DiaryState",
                c => new
                    {
                        StateID = c.String(nullable: false, maxLength: 10, unicode: false),
                        StateName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.BBS_Diary",
                c => new
                    {
                        DiaryID = c.String(nullable: false, maxLength: 10, unicode: false),
                        DiaryTitle = c.String(nullable: false, maxLength: 50, unicode: false),
                        Contents = c.String(unicode: false, storeType: "text"),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Time = c.DateTime(precision: 7, storeType: "datetime2"),
                        StateID = c.String(maxLength: 10, unicode: false),
                        ReportID = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.DiaryID)
                .ForeignKey("dbo.BBS_C_DiaryState", t => t.StateID)
                .ForeignKey("dbo.BBS_Diary_Report", t => t.ReportID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.StateID)
                .Index(t => t.ReportID);
            
            CreateTable(
                "dbo.BBS_Diary_Report",
                c => new
                    {
                        ReportID = c.String(nullable: false, maxLength: 10, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        DiaryID = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID)
                .ForeignKey("dbo.BBS_Diary", t => t.DiaryID)
                .Index(t => t.UserID)
                .Index(t => t.DiaryID);
            
            CreateTable(
                "dbo.BBS_Users",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserGradeID = c.String(nullable: false, maxLength: 10, unicode: false),
                        UserStateID = c.String(nullable: false, maxLength: 10, unicode: false),
                        UserPoint = c.String(nullable: false, maxLength: 10, unicode: false),
                        RegisterDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastLogin = c.DateTime(precision: 7, storeType: "datetime2"),
                        RecentActivite = c.DateTime(precision: 7, storeType: "datetime2"),
                        HeadImg = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.BBS_C_UserGrade", t => t.UserGradeID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_C_UserState", t => t.UserStateID, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.UserGradeID)
                .Index(t => t.UserStateID);
            
            CreateTable(
                "dbo.BBS_C_UserGrade",
                c => new
                    {
                        UserGradeID = c.String(nullable: false, maxLength: 10, unicode: false),
                        UserGradeName = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.UserGradeID);
            
            CreateTable(
                "dbo.BBS_C_UserState",
                c => new
                    {
                        UserStateID = c.String(nullable: false, maxLength: 10, unicode: false),
                        StateName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.UserStateID);
            
            CreateTable(
                "dbo.BBS_DiaryReply",
                c => new
                    {
                        DiaryReplyID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Contents = c.String(nullable: false, unicode: false, storeType: "text"),
                        DiaryID = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.DiaryReplyID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Diary", t => t.DiaryID)
                .Index(t => t.UserID)
                .Index(t => t.DiaryID);
            
            CreateTable(
                "dbo.BBS_ForumManager",
                c => new
                    {
                        ForumManagerID = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        ForumID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ForumManagerID)
                .ForeignKey("dbo.BBS_Columns", t => t.ForumID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ForumID);
            
            CreateTable(
                "dbo.BBS_Columns",
                c => new
                    {
                        ColumnID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ColumnName = c.String(nullable: false, maxLength: 20, unicode: false),
                        FatherColumnID = c.String(maxLength: 20, unicode: false),
                        ForumID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ColumnImg = c.String(maxLength: 50, unicode: false),
                        ColumnExplain = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.ColumnID)
                .ForeignKey("dbo.BBS_Forum", t => t.ForumID, cascadeDelete: true)
                .Index(t => t.ForumID);
            
            CreateTable(
                "dbo.BBS_Forum",
                c => new
                    {
                        ForumID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ForumExplain = c.String(maxLength: 400, unicode: false),
                        ForumName = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ForumID);
            
            CreateTable(
                "dbo.BBS_Theme",
                c => new
                    {
                        ThemeID = c.String(nullable: false, maxLength: 20, unicode: false),
                        ThemeName = c.String(nullable: false, maxLength: 20, unicode: false),
                        ColumnID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ThemeExplain = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ThemeID)
                .ForeignKey("dbo.BBS_Columns", t => t.ColumnID, cascadeDelete: true)
                .Index(t => t.ColumnID);
            
            CreateTable(
                "dbo.BBS_Post",
                c => new
                    {
                        PostID = c.String(nullable: false, maxLength: 20, unicode: false),
                        PostTitle = c.String(nullable: false, maxLength: 100, unicode: false),
                        ThemeID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        PostContent = c.String(nullable: false, unicode: false, storeType: "text"),
                        PostTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PClickCount = c.Int(nullable: false),
                        StateID = c.String(nullable: false, maxLength: 10, unicode: false),
                        IsTop = c.String(nullable: false, maxLength: 10, unicode: false),
                        IsCream = c.String(nullable: false, maxLength: 10, unicode: false),
                        PostTypeID = c.String(maxLength: 10, unicode: false),
                        BestReplyID = c.String(maxLength: 20, unicode: false),
                        Files = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.BBS_C_PostState", t => t.StateID)
                .ForeignKey("dbo.BBS_C_PostType", t => t.PostTypeID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_ReplyPost", t => t.BestReplyID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Theme", t => t.ThemeID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Users", t => t.UserID)
                .Index(t => t.ThemeID)
                .Index(t => t.UserID)
                .Index(t => t.StateID)
                .Index(t => t.PostTypeID)
                .Index(t => t.BestReplyID);
            
            CreateTable(
                "dbo.BBS_C_PostState",
                c => new
                    {
                        StateID = c.String(nullable: false, maxLength: 10, unicode: false),
                        StateName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.BBS_C_PostType",
                c => new
                    {
                        PostTypeID = c.String(nullable: false, maxLength: 10, unicode: false),
                        PostTypeName = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.PostTypeID);
            
            CreateTable(
                "dbo.BBS_Post_Report",
                c => new
                    {
                        ReportID = c.String(nullable: false, maxLength: 10, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        PostID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.BBS_Post", t => t.PostID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.BBS_ReplyPost",
                c => new
                    {
                        ReplyPostID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Contents = c.String(nullable: false, maxLength: 200, unicode: false),
                        PostID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        ParentReplyID = c.String(maxLength: 20, unicode: false),
                        Files = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.ReplyPostID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Post", t => t.PostID)
                .Index(t => t.PostID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.BBS_ReplyParise",
                c => new
                    {
                        PraiseID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ReplyPostID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.PraiseID)
                .ForeignKey("dbo.BBS_ReplyPost", t => t.ReplyPostID)
                .Index(t => t.ReplyPostID);
            
            CreateTable(
                "dbo.BBS_ReplyReport",
                c => new
                    {
                        ReportID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        ReplyPostID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_ReplyPost", t => t.ReplyPostID)
                .Index(t => t.UserID)
                .Index(t => t.ReplyPostID);
            
            CreateTable(
                "dbo.BBS_Share",
                c => new
                    {
                        ShareID = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PostID = c.String(nullable: false, maxLength: 20, unicode: false),
                        StatusID = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ShareID)
                .ForeignKey("dbo.BBS_C_Share", t => t.StatusID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Post", t => t.PostID)
                .Index(t => t.UserID)
                .Index(t => t.PostID)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.BBS_C_Share",
                c => new
                    {
                        StatusID = c.String(nullable: false, maxLength: 20, unicode: false),
                        StatusName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.BBS_StepPraise",
                c => new
                    {
                        SetpPraiseID = c.String(nullable: false, maxLength: 50, unicode: false),
                        PostID = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.SetpPraiseID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Post", t => t.PostID)
                .Index(t => t.PostID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.BBS_Friend",
                c => new
                    {
                        FriendID = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserIDA = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserIDB = c.String(nullable: false, maxLength: 20, unicode: false),
                        StatusID = c.String(maxLength: 20, unicode: false),
                        Time = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.FriendID)
                .ForeignKey("dbo.BBS_C_Friend", t => t.StatusID)
                .ForeignKey("dbo.BBS_Users", t => t.UserIDA, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Users", t => t.UserIDB)
                .Index(t => t.UserIDA)
                .Index(t => t.UserIDB)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.BBS_C_Friend",
                c => new
                    {
                        StatusID = c.String(nullable: false, maxLength: 20, unicode: false),
                        StatusName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.BBS_Note",
                c => new
                    {
                        NoteID = c.String(nullable: false, maxLength: 20, unicode: false),
                        NoteContent = c.String(maxLength: 50, unicode: false),
                        UserID = c.String(maxLength: 20, unicode: false),
                        ReceiverUserID = c.String(maxLength: 50, unicode: false),
                        NoteTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.NoteID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.BBS_Photo",
                c => new
                    {
                        PhtotID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Img = c.String(maxLength: 50, unicode: false),
                        Time = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserID = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.PhtotID)
                .ForeignKey("dbo.BBS_Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.BBS_Visitor",
                c => new
                    {
                        VisitorID = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserIDA = c.String(nullable: false, maxLength: 20, unicode: false),
                        UserIDB = c.String(nullable: false, maxLength: 20, unicode: false),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VisitorID)
                .ForeignKey("dbo.BBS_Users", t => t.UserIDA, cascadeDelete: true)
                .ForeignKey("dbo.BBS_Users", t => t.UserIDB)
                .Index(t => t.UserIDA)
                .Index(t => t.UserIDB);
            
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        NickName = c.String(maxLength: 100, unicode: false),
                        UserPwd = c.String(nullable: false, maxLength: 100, unicode: false),
                        UserType = c.Int(nullable: false),
                        Note = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.CPQ_T_Questionnaire",
                c => new
                    {
                        QuestionnaireID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionnaireName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Summarize = c.String(nullable: false, maxLength: 255, unicode: false),
                        QuestionnaireDomain = c.Int(nullable: false),
                        CreateTime = c.Int(nullable: false),
                        OwnerID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Status = c.Int(nullable: false),
                        OwnerCompany = c.String(maxLength: 50, unicode: false),
                        share = c.Int(),
                        Reference = c.Int(),
                        CollectSeting = c.Int(),
                        CollectNumber = c.Int(),
                        IsLock = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionnaireID)
                .ForeignKey("dbo.CPQ_C_QuestionnaireDomain", t => t.QuestionnaireDomain, cascadeDelete: true)
                .ForeignKey("dbo.CPQ_C_QuestionnaireStatus", t => t.Status, cascadeDelete: true)
                .ForeignKey("dbo.CPQ_C_Share", t => t.share, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.QuestionnaireDomain)
                .Index(t => t.OwnerID)
                .Index(t => t.Status)
                .Index(t => t.share);
            
            CreateTable(
                "dbo.CPQ_C_QuestionnaireDomain",
                c => new
                    {
                        QuestionnaireDomainID = c.Int(nullable: false),
                        QuestionnaireDomainName = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionnaireDomainDescribe = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.QuestionnaireDomainID);
            
            CreateTable(
                "dbo.CPQ_C_QuestionnaireStatus",
                c => new
                    {
                        QuestionnaireStatusID = c.Int(nullable: false),
                        QuestionnaireStatusName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Decription = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.QuestionnaireStatusID);
            
            CreateTable(
                "dbo.CPQ_C_Share",
                c => new
                    {
                        shareID = c.Int(nullable: false),
                        shareName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Decription = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.shareID);
            
            CreateTable(
                "dbo.CPQ_T_AnswerSheet",
                c => new
                    {
                        AnswerSheetID = c.String(nullable: false, maxLength: 150, unicode: false),
                        AnswererIP = c.String(nullable: false, maxLength: 50, unicode: false),
                        AnswerTime = c.Int(nullable: false),
                        QuestionnaireID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Answer = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.AnswerSheetID)
                .ForeignKey("dbo.CPQ_T_Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.CPQ_T_Questionnaire", t => t.QuestionnaireID, cascadeDelete: true)
                .Index(t => t.QuestionnaireID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.CPQ_T_Question",
                c => new
                    {
                        QuestionID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionType = c.Int(nullable: false),
                        QuestionName = c.String(nullable: false, maxLength: 255, unicode: false),
                        QuestionDomain = c.Int(nullable: false),
                        share = c.Int(),
                        Reference = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.CPQ_C_QuestionDomain", t => t.QuestionDomain, cascadeDelete: true)
                .ForeignKey("dbo.CPQ_C_QuestionType", t => t.QuestionType, cascadeDelete: true)
                .Index(t => t.QuestionType)
                .Index(t => t.QuestionDomain);
            
            CreateTable(
                "dbo.CPQ_C_QuestionDomain",
                c => new
                    {
                        QuestionDomainID = c.Int(nullable: false),
                        QuestionDomainName = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionDomainDescribe = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.QuestionDomainID);
            
            CreateTable(
                "dbo.CPQ_C_QuestionType",
                c => new
                    {
                        QuestionTypeID = c.Int(nullable: false),
                        QuestionTypeName = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionDescribe = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.QuestionTypeID);
            
            CreateTable(
                "dbo.CPQ_T_AttachQuestionToQuestionnaire",
                c => new
                    {
                        AttachID = c.String(nullable: false, maxLength: 70, unicode: false),
                        QuestionnaireID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionSequenceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttachID)
                .ForeignKey("dbo.CPQ_T_Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.CPQ_T_Questionnaire", t => t.QuestionnaireID, cascadeDelete: true)
                .Index(t => t.QuestionnaireID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.CPQ_T_QuestionOption",
                c => new
                    {
                        QuestionOptionID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionID = c.String(nullable: false, maxLength: 50, unicode: false),
                        QuestionOptionIName = c.String(nullable: false, maxLength: 50, unicode: false),
                        SequenID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionOptionID)
                .ForeignKey("dbo.CPQ_T_Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.T_Employee",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        EmployeeID = c.String(nullable: false, maxLength: 20, unicode: false),
                        EmployeeName = c.String(nullable: false, maxLength: 100, unicode: false),
                        Phone = c.String(maxLength: 20, unicode: false),
                        QQ = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 20, unicode: false),
                        Address = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.T_Enterprise", t => t.Ent_No, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.Ent_No);
            
            CreateTable(
                "dbo.T_Enterprise",
                c => new
                    {
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        Ent_Name = c.String(nullable: false, maxLength: 1024, unicode: false),
                        CountyID = c.String(maxLength: 6, fixedLength: true, unicode: false),
                        EntCategoryID = c.String(nullable: false, maxLength: 6, unicode: false),
                        EntRank = c.String(nullable: false, maxLength: 6, unicode: false),
                        EntAddress = c.String(maxLength: 1024, unicode: false),
                        EntResume = c.String(unicode: false, storeType: "text"),
                        EntLogo = c.String(maxLength: 100, unicode: false),
                        EntAdPics = c.String(unicode: false, storeType: "text"),
                        EntPhotos = c.String(unicode: false, storeType: "text"),
                        EntVideos = c.String(unicode: false, storeType: "text"),
                        EntPPTs = c.String(unicode: false, storeType: "text"),
                        EntFiles = c.String(unicode: false, storeType: "text"),
                        EntState = c.Int(nullable: false),
                        RegisterTime = c.Int(),
                        UpdateTime = c.Int(),
                        Email = c.String(maxLength: 50, unicode: false),
                        Contectinfo = c.String(maxLength: 1024, unicode: false),
                        UserID = c.String(maxLength: 20, unicode: false),
                        InfoLocked = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Ent_No)
                .ForeignKey("dbo.C_County", t => t.CountyID)
                .ForeignKey("dbo.C_EntRank", t => new { t.EntRank, t.EntCategoryID })
                .ForeignKey("dbo.C_UnitStatus", t => t.EntState)
                .Index(t => t.CountyID)
                .Index(t => new { t.EntRank, t.EntCategoryID })
                .Index(t => t.EntState);
            
            CreateTable(
                "dbo.C_County",
                c => new
                    {
                        CountyID = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        CityID = c.String(maxLength: 6, fixedLength: true, unicode: false),
                        CountyName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CountyID);
            
            CreateTable(
                "dbo.C_EntRank",
                c => new
                    {
                        EntRankID = c.String(nullable: false, maxLength: 6, unicode: false),
                        EntCategoryID = c.String(nullable: false, maxLength: 6, unicode: false),
                        EntRankName = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => new { t.EntRankID, t.EntCategoryID })
                .ForeignKey("dbo.C_EntCategory", t => t.EntCategoryID)
                .Index(t => t.EntCategoryID);
            
            CreateTable(
                "dbo.C_EntCategory",
                c => new
                    {
                        EntCategoryID = c.String(nullable: false, maxLength: 6, unicode: false),
                        EntCategoryName = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.EntCategoryID);
            
            CreateTable(
                "dbo.V3_EnterpriseApply",
                c => new
                    {
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        Ent_Name = c.String(maxLength: 1024, unicode: false),
                        EntCategoryID = c.String(maxLength: 6, unicode: false),
                        EntRankID = c.String(maxLength: 6, unicode: false),
                        EntAddress = c.String(maxLength: 1024, unicode: false),
                        UserID = c.String(maxLength: 20, unicode: false),
                        SchoolID = c.String(maxLength: 10, fixedLength: true),
                        RegisterTime = c.Int(),
                        UpdateTime = c.Int(),
                        ApplyState = c.Int(),
                        Contectinfo = c.String(maxLength: 1024, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                        EntResume = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Ent_No)
                .ForeignKey("dbo.T_User", t => t.UserID)
                .ForeignKey("dbo.C_EntRank", t => new { t.EntRankID, t.EntCategoryID }, cascadeDelete: true)
                .Index(t => new { t.EntRankID, t.EntCategoryID })
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.V3_StuEnterPriseApply",
                c => new
                    {
                        StuEnterPriseApplyID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ent_No = c.String(maxLength: 20, unicode: false),
                        UserID = c.String(maxLength: 20, unicode: false),
                        ApplyState = c.Int(),
                        ApplyTime = c.Int(),
                        posDesc = c.String(unicode: false, storeType: "text"),
                        practiceDept = c.String(maxLength: 50, unicode: false),
                        practiceDivDept = c.String(maxLength: 50, unicode: false),
                        practiceStart = c.Int(),
                        practiceEnd = c.Int(),
                        positionId = c.String(maxLength: 6, unicode: false),
                    })
                .PrimaryKey(t => t.StuEnterPriseApplyID)
                .ForeignKey("dbo.V3_EnterpriseApply", t => t.Ent_No)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.Ent_No)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.C_UnitStatus",
                c => new
                    {
                        StatusID = c.Int(nullable: false),
                        StatusName = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.T_School",
                c => new
                    {
                        SchoolID = c.String(nullable: false, maxLength: 20, unicode: false),
                        SchoolName = c.String(maxLength: 100, unicode: false),
                        Address = c.String(maxLength: 200, unicode: false),
                        Contact = c.String(maxLength: 200, unicode: false),
                        Email = c.String(maxLength: 200, unicode: false),
                        UserID = c.String(maxLength: 20, unicode: false),
                        Status = c.Int(nullable: false),
                        InfoLocked = c.Int(nullable: false),
                        SchoolLogo = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.SchoolID)
                .ForeignKey("dbo.C_UnitStatus", t => t.Status)
                .Index(t => t.Status);
            
            CreateTable(
                "dbo.C_Specialty",
                c => new
                    {
                        EntryYear = c.Int(nullable: false),
                        SpeNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        SchoolID = c.String(nullable: false, maxLength: 20, unicode: false),
                        SpeName = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.EntryYear, t.SpeNo, t.SchoolID })
                .ForeignKey("dbo.T_School", t => t.SchoolID, cascadeDelete: true)
                .Index(t => t.SchoolID);
            
            CreateTable(
                "dbo.T_Class",
                c => new
                    {
                        ClassID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ClassName = c.String(maxLength: 100, fixedLength: true),
                        SpeNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        EntryYear = c.Int(nullable: false),
                        SchoolID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.C_Specialty", t => new { t.EntryYear, t.SpeNo, t.SchoolID }, cascadeDelete: true)
                .ForeignKey("dbo.T_School", t => t.SchoolID)
                .Index(t => new { t.EntryYear, t.SpeNo, t.SchoolID });
            
            CreateTable(
                "dbo.T_Student",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        StuNo = c.String(nullable: false, maxLength: 15, unicode: false),
                        StuName = c.String(nullable: false, maxLength: 100, unicode: false),
                        StuSex = c.String(maxLength: 2, unicode: false),
                        StuHeight = c.Int(),
                        StuWeight = c.Int(),
                        StuCellphone = c.String(maxLength: 20, unicode: false),
                        StuEMail = c.String(maxLength: 100, unicode: false),
                        StuQQ = c.String(maxLength: 20, unicode: false),
                        StuResume = c.String(unicode: false, storeType: "text"),
                        MainPhoto = c.String(maxLength: 100, unicode: false),
                        Pics = c.String(unicode: false, storeType: "text"),
                        Videos = c.String(maxLength: 500, unicode: false),
                        StuClass = c.String(nullable: false, maxLength: 50, unicode: false),
                        StuResourceFile = c.String(unicode: false, storeType: "text"),
                        PicMood = c.String(unicode: false, storeType: "text"),
                        VideoMood = c.String(unicode: false, storeType: "text"),
                        StuBirthday = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.T_Class", t => t.StuClass, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.StuClass);
            
            CreateTable(
                "dbo.T_StuBatchReg",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        WeekRecordScore = c.Double(),
                        PracticeCaseScore = c.Double(),
                        WeekRecordComment = c.String(maxLength: 1024, unicode: false),
                        PracticeCaseComment = c.String(maxLength: 1024, unicode: false),
                        PracticeContent = c.String(unicode: false, storeType: "text"),
                        PracticeReport = c.String(unicode: false, storeType: "text"),
                        PracticeReportFile = c.String(maxLength: 200, unicode: false),
                        PracticeDept = c.String(maxLength: 50, unicode: false),
                        PracticeDivDept = c.String(maxLength: 50, unicode: false),
                        PracticeStartEnd = c.String(maxLength: 50, unicode: false),
                        ReviewScore = c.Double(),
                        PracUnitComment = c.String(maxLength: 1024, unicode: false),
                        SchoolComment = c.String(maxLength: 1024, unicode: false),
                        TutorComment = c.String(maxLength: 1024, unicode: false),
                        CareerStatusID = c.Int(),
                        StuEvaEntScore = c.Double(),
                        StuEvaSchoolScore = c.Double(),
                    })
                .PrimaryKey(t => t.PracticeNo)
                .ForeignKey("dbo.C_CareerStatus", t => t.CareerStatusID, cascadeDelete: true)
                .ForeignKey("dbo.T_PracBatch", t => t.PracBatchID)
                .ForeignKey("dbo.T_Student", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PracBatchID)
                .Index(t => t.CareerStatusID);
            
            CreateTable(
                "dbo.C_CareerStatus",
                c => new
                    {
                        CareerStatusID = c.Int(nullable: false),
                        CareerStatusName = c.String(maxLength: 50, unicode: false),
                        C_CareerStatus2_CareerStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CareerStatusID)
                .ForeignKey("dbo.C_CareerStatus", t => t.C_CareerStatus2_CareerStatusID)
                .Index(t => t.C_CareerStatus2_CareerStatusID);
            
            CreateTable(
                "dbo.T_EntEvaluateStu",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemContent = c.String(maxLength: 1024, unicode: false),
                        ItemGrade = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ItemNo })
                .ForeignKey("dbo.C_EntEvaluateStuItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.C_EntEvaStuGradeLevelItem", t => t.ItemGrade, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo)
                .Index(t => t.ItemGrade);
            
            CreateTable(
                "dbo.C_EntEvaluateStuItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_EntEvaStuGradeLevelItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        Note = c.String(maxLength: 20, unicode: false),
                        Weight = c.Int(),
                        PracBatchID = c.String(maxLength: 40, unicode: false),
                        ItemSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_EntStudentReviewLink",
                c => new
                    {
                        LinkID = c.String(nullable: false, maxLength: 60, unicode: false),
                        TaskID = c.String(nullable: false, maxLength: 60, unicode: false),
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ReviewScore = c.Double(),
                        ReviewComment = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.LinkID)
                .ForeignKey("dbo.T_EntReviewerTask", t => t.TaskID)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo)
                .Index(t => t.TaskID)
                .Index(t => t.PracticeNo);
            
            CreateTable(
                "dbo.T_EntReviewerTask",
                c => new
                    {
                        TaskID = c.String(nullable: false, maxLength: 60, unicode: false),
                        ItemID = c.String(nullable: false, maxLength: 60, unicode: false),
                        EmployeeID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.T_EntReviewItem", t => t.ItemID)
                .ForeignKey("dbo.T_Employee", t => t.EmployeeID)
                .Index(t => t.ItemID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.T_EntReviewItem",
                c => new
                    {
                        ItemID = c.String(nullable: false, maxLength: 60, unicode: false),
                        EntPracNo = c.String(nullable: false, maxLength: 100, unicode: false),
                        ItemName = c.String(maxLength: 1024, unicode: false),
                        ItemWeight = c.Int(),
                        ItemSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.T_EntBatchReg", t => t.EntPracNo)
                .Index(t => t.EntPracNo);
            
            CreateTable(
                "dbo.T_EntBatchReg",
                c => new
                    {
                        EntPracNo = c.String(nullable: false, maxLength: 100, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        RegistTime = c.Int(),
                        ApplyStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntPracNo)
                .ForeignKey("dbo.C_ApplyStatus", t => t.ApplyStatus)
                .ForeignKey("dbo.T_PracBatch", t => t.PracBatchID, cascadeDelete: true)
                .ForeignKey("dbo.T_Enterprise", t => t.Ent_No, cascadeDelete: true)
                .Index(t => t.PracBatchID)
                .Index(t => t.Ent_No)
                .Index(t => t.ApplyStatus);
            
            CreateTable(
                "dbo.C_ApplyStatus",
                c => new
                    {
                        ApplyStatusID = c.Int(nullable: false),
                        ApplyStatusName = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ApplyStatusID);
            
            CreateTable(
                "dbo.T_PlatformPubToUnit",
                c => new
                    {
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        UnitID = c.String(nullable: false, maxLength: 20, unicode: false),
                        StartTime = c.Int(),
                        EndTime = c.Int(),
                        ApplyStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PracBatchID, t.UnitID })
                .ForeignKey("dbo.C_ApplyStatus", t => t.ApplyStatusID, cascadeDelete: true)
                .ForeignKey("dbo.T_PracBatch", t => t.PracBatchID)
                .Index(t => t.PracBatchID)
                .Index(t => t.ApplyStatusID);
            
            CreateTable(
                "dbo.T_PracBatch",
                c => new
                    {
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        BatchID = c.String(nullable: false, maxLength: 20, unicode: false),
                        SchoolID = c.String(nullable: false, maxLength: 20, unicode: false),
                        BatchName = c.String(nullable: false, maxLength: 100, unicode: false),
                        StartEnd = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsCurrentBatch = c.String(nullable: false, maxLength: 10, unicode: false),
                        IsActive = c.Int(nullable: false),
                        SchoolReviewWeight = c.Int(),
                    })
                .PrimaryKey(t => t.PracBatchID)
                .ForeignKey("dbo.T_School", t => t.SchoolID)
                .Index(t => t.SchoolID);
            
            CreateTable(
                "dbo.C_PracticeCaseItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo)
                .ForeignKey("dbo.T_PracBatch", t => t.PracBatchID)
                .Index(t => t.PracBatchID);
            
            CreateTable(
                "dbo.T_PracticeCase",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        CaseNo = c.Int(nullable: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemContent = c.String(unicode: false, storeType: "text"),
                        CaseName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.CaseNo, t.ItemNo })
                .ForeignKey("dbo.C_PracticeCaseItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_PracticeCaseExtension",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        CaseNo = c.Int(nullable: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Pic = c.String(maxLength: 500, unicode: false),
                        CaseTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.CaseNo, t.ItemNo })
                .ForeignKey("dbo.T_PracticeCase", t => new { t.PracticeNo, t.CaseNo, t.ItemNo }, cascadeDelete: true)
                .Index(t => new { t.PracticeNo, t.CaseNo, t.ItemNo });
            
            CreateTable(
                "dbo.T_SchoolReviewItem",
                c => new
                    {
                        ItemID = c.String(nullable: false, maxLength: 60, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemWeight = c.Int(nullable: false),
                        ItemSequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.T_PracBatch", t => t.PracBatchID, cascadeDelete: true)
                .Index(t => t.PracBatchID);
            
            CreateTable(
                "dbo.T_SchoolReviewerTask",
                c => new
                    {
                        TaskID = c.String(nullable: false, maxLength: 60, unicode: false),
                        TeacherID = c.String(nullable: false, maxLength: 20, unicode: false),
                        ItemID = c.String(nullable: false, maxLength: 60, unicode: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.T_Faculty", t => t.TeacherID)
                .ForeignKey("dbo.T_SchoolReviewItem", t => t.ItemID)
                .Index(t => t.TeacherID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.T_Faculty",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        FacNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        FacName = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        FacSex = c.String(maxLength: 10, fixedLength: true),
                        FacProPos = c.Int(),
                        PhoneNo = c.String(maxLength: 20, unicode: false),
                        EmailAddress = c.String(maxLength: 100, unicode: false),
                        FacPhoto = c.String(maxLength: 100, unicode: false),
                        FacAbstract = c.String(unicode: false, storeType: "text"),
                        FacStatus = c.Int(nullable: false),
                        SchoolID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.C_ProPosition", t => t.FacProPos)
                .ForeignKey("dbo.T_School", t => t.SchoolID, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.FacProPos)
                .Index(t => t.SchoolID);
            
            CreateTable(
                "dbo.C_ProPosition",
                c => new
                    {
                        ProPosID = c.Int(nullable: false),
                        ProPosName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ProPosID);
            
            CreateTable(
                "dbo.T_SchoolStudentReviewLink",
                c => new
                    {
                        LinkID = c.String(nullable: false, maxLength: 60, unicode: false),
                        TaskID = c.String(nullable: false, maxLength: 60, unicode: false),
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ReviewScore = c.Double(),
                        ReviewComment = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.LinkID)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .ForeignKey("dbo.T_SchoolReviewerTask", t => t.TaskID)
                .Index(t => t.TaskID)
                .Index(t => t.PracticeNo);
            
            CreateTable(
                "dbo.T_SchoolPubToUnit",
                c => new
                    {
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        UnitID = c.String(nullable: false, maxLength: 20, unicode: false),
                        StartTime = c.Int(),
                        EndTime = c.Int(),
                        ApplyStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PracBatchID, t.UnitID })
                .ForeignKey("dbo.C_ApplyStatus", t => t.ApplyStatusID, cascadeDelete: true)
                .Index(t => t.ApplyStatusID);
            
            CreateTable(
                "dbo.T_PracticePosition",
                c => new
                    {
                        EntPracNo = c.String(nullable: false, maxLength: 100, unicode: false),
                        PositionID = c.String(nullable: false, maxLength: 6, unicode: false),
                        PubDate = c.Int(nullable: false),
                        PosDesc = c.String(nullable: false, unicode: false, storeType: "text"),
                        PosQuantity = c.Int(nullable: false),
                        PosExpireDate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EntPracNo, t.PositionID })
                .ForeignKey("dbo.C_Position", t => t.PositionID)
                .ForeignKey("dbo.T_EntBatchReg", t => t.EntPracNo, cascadeDelete: true)
                .Index(t => t.EntPracNo)
                .Index(t => t.PositionID);
            
            CreateTable(
                "dbo.C_Position",
                c => new
                    {
                        PositionID = c.String(nullable: false, maxLength: 6, unicode: false),
                        PositionName = c.String(nullable: false, maxLength: 1024, unicode: false),
                    })
                .PrimaryKey(t => t.PositionID);
            
            CreateTable(
                "dbo.T_PracticeAttendance",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemContent = c.Int(),
                        ItemGrade = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ItemNo })
                .ForeignKey("dbo.C_PracAttendanceGradeItem", t => t.ItemGrade, cascadeDelete: true)
                .ForeignKey("dbo.C_PracAttendanceItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo)
                .Index(t => t.ItemGrade);
            
            CreateTable(
                "dbo.C_PracAttendanceGradeItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        Note = c.String(maxLength: 20, unicode: false),
                        Weight = c.Int(),
                        PracBatchID = c.String(maxLength: 40, unicode: false),
                        ItemSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_PracAttendanceItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        Note = c.String(maxLength: 20, unicode: false),
                        ItemSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_PracticeIdentification",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemContent = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ItemNo })
                .ForeignKey("dbo.C_PracticeIdentificationItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_PracticeIdentificationItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_SelfEvaluation",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemStars = c.Int(),
                        ItemContent = c.String(maxLength: 1024, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ItemNo })
                .ForeignKey("dbo.C_SelfEvaluationItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_SelfEvaluationItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_StuBatchReg_Extent",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsEffect = c.Int(),
                        AuthorizedEntNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        ProofText = c.String(unicode: false, storeType: "text"),
                        ProofFile = c.String(maxLength: 50, unicode: false),
                        DisperseStateID = c.Int(),
                    })
                .PrimaryKey(t => t.PracticeNo)
                .ForeignKey("dbo.C_DisperseState", t => t.DisperseStateID, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .ForeignKey("dbo.T_Enterprise", t => t.AuthorizedEntNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.AuthorizedEntNo)
                .Index(t => t.DisperseStateID);
            
            CreateTable(
                "dbo.C_DisperseState",
                c => new
                    {
                        DisperseStateID = c.Int(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DisperseStateID);
            
            CreateTable(
                "dbo.T_StuEvaluateEnt",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemContent = c.String(maxLength: 1024, unicode: false),
                        ItemGrade = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ItemNo })
                .ForeignKey("dbo.C_StuEvaEntGradeLevelItem", t => t.ItemGrade, cascadeDelete: true)
                .ForeignKey("dbo.C_StuEvaluateEntItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo)
                .Index(t => t.ItemGrade);
            
            CreateTable(
                "dbo.C_StuEvaEntGradeLevelItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        Note = c.String(maxLength: 20, unicode: false),
                        Weight = c.Int(),
                        PracBatchID = c.String(maxLength: 40, unicode: false),
                        ItemSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_StuEvaluateEntItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                        ItemPower = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_StuEvaluateSchool",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemContent = c.String(maxLength: 1024, unicode: false),
                        ItemGrade = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ItemNo })
                .ForeignKey("dbo.C_StuEvaluateSchoolItem", t => t.ItemNo, cascadeDelete: true)
                .ForeignKey("dbo.C_StuEvaSchoolGradeLevelItem", t => t.ItemGrade, cascadeDelete: true)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo)
                .Index(t => t.ItemNo)
                .Index(t => t.ItemGrade);
            
            CreateTable(
                "dbo.C_StuEvaluateSchoolItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                        ItemPower = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_StuEvaSchoolGradeLevelItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        Note = c.String(maxLength: 20, unicode: false),
                        Weight = c.Int(nullable: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        ItemSequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.T_StuScoreApply",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserID = c.String(maxLength: 50, unicode: false),
                        EvidenceFile = c.String(maxLength: 500, unicode: false),
                        ApplyReviewScore = c.Double(),
                        ApplyContents = c.String(maxLength: 500, unicode: false),
                        StateID = c.String(maxLength: 50, unicode: false),
                        PracticeCaseComment = c.String(maxLength: 1024, unicode: false),
                        WeekRecordScore = c.Double(),
                        PracticeCaseScore = c.Double(),
                        WeekRecordComment = c.String(maxLength: 1024, unicode: false),
                        PracticeContent = c.String(unicode: false, storeType: "text"),
                        PracticeReport = c.String(unicode: false, storeType: "text"),
                        PracticeReportFile = c.String(maxLength: 200, unicode: false),
                        PracticeStartEnd = c.String(maxLength: 50, unicode: false),
                        PracUnitComment = c.String(maxLength: 1024, unicode: false),
                        SchoolComment = c.String(maxLength: 1024, unicode: false),
                        TutorComment = c.String(maxLength: 1024, unicode: false),
                        StuEvaEntScore = c.Double(),
                        StuEvaSchoolScore = c.Double(),
                        ReviewScore = c.Double(),
                        AuditOpinion = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.PracticeNo)
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo);
            
            CreateTable(
                "dbo.T_WeekRecord",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        RecordNo = c.Int(nullable: false),
                        RecordDate = c.Int(nullable: false),
                        RecordTitle = c.String(nullable: false, maxLength: 1024, unicode: false),
                        RecordContent = c.String(nullable: false, unicode: false, storeType: "text"),
                        RecordComment = c.String(unicode: false, storeType: "text"),
                        RecordScore = c.Double(),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.RecordNo })
                .ForeignKey("dbo.T_StuBatchReg", t => t.PracticeNo, cascadeDelete: true)
                .Index(t => t.PracticeNo);
            
            CreateTable(
                "dbo.T_WeekRecordExtemsion",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        RecordNo = c.Int(nullable: false),
                        pic = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.RecordNo })
                .ForeignKey("dbo.T_WeekRecord", t => new { t.PracticeNo, t.RecordNo }, cascadeDelete: true)
                .Index(t => new { t.PracticeNo, t.RecordNo });
            
            CreateTable(
                "dbo.T_AnswerQuestion",
                c => new
                    {
                        AnsQueID = c.String(nullable: false, maxLength: 100, unicode: false),
                        Answer = c.String(maxLength: 500, unicode: false),
                        Question = c.String(maxLength: 100, unicode: false),
                        QuestionTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        AnswerTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.AnsQueID)
                .ForeignKey("dbo.T_Enterprise", t => t.Ent_No, cascadeDelete: true)
                .Index(t => t.Ent_No);
            
            CreateTable(
                "dbo.T_MsgRec",
                c => new
                    {
                        MsgID = c.String(nullable: false, maxLength: 100, unicode: false),
                        Receiver = c.String(nullable: false, maxLength: 20, unicode: false),
                        ReadTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MsgID, t.Receiver })
                .ForeignKey("dbo.T_SysMsg", t => t.MsgID, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.Receiver)
                .Index(t => t.MsgID)
                .Index(t => t.Receiver);
            
            CreateTable(
                "dbo.T_SysMsg",
                c => new
                    {
                        MsgID = c.String(nullable: false, maxLength: 100, unicode: false),
                        MsgOwner = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreateTime = c.Int(nullable: false),
                        MsgContent = c.String(nullable: false, unicode: false, storeType: "text"),
                        MsgTypeID = c.Int(nullable: false),
                        FatherMsgID = c.String(nullable: false, maxLength: 100, unicode: false),
                        IsLocked = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MsgID)
                .ForeignKey("dbo.C_MsgType", t => t.MsgTypeID, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.MsgOwner, cascadeDelete: true)
                .Index(t => t.MsgOwner)
                .Index(t => t.MsgTypeID);
            
            CreateTable(
                "dbo.C_MsgType",
                c => new
                    {
                        MsgTypeID = c.Int(nullable: false),
                        MsgTypeName = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.MsgTypeID);
            
            CreateTable(
                "dbo.T_MsgSend",
                c => new
                    {
                        MsgID = c.String(nullable: false, maxLength: 100, unicode: false),
                        Sender = c.String(nullable: false, maxLength: 20, unicode: false),
                        SendTime = c.Int(nullable: false),
                        SendTypeID = c.Int(nullable: false),
                        Receiver = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.MsgID, t.Sender })
                .ForeignKey("dbo.C_MsgSendType", t => t.SendTypeID, cascadeDelete: true)
                .ForeignKey("dbo.T_SysMsg", t => t.MsgID, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.Sender)
                .Index(t => t.MsgID)
                .Index(t => t.Sender)
                .Index(t => t.SendTypeID);
            
            CreateTable(
                "dbo.C_MsgSendType",
                c => new
                    {
                        SendTypeID = c.Int(nullable: false),
                        SendTypeName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.SendTypeID);
            
            CreateTable(
                "dbo.T_Platformer",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        PFNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        Phone = c.String(maxLength: 20, unicode: false),
                        Address = c.String(maxLength: 200, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        PFName = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.T_UserRole",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        RoleID = c.String(nullable: false, maxLength: 6, unicode: false),
                        Note = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.T_Role", t => t.RoleID)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.T_Role",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 6, unicode: false),
                        RoleName = c.String(nullable: false, maxLength: 100, unicode: false),
                        RoleType = c.String(nullable: false, maxLength: 10, unicode: false),
                        subSystem = c.String(maxLength: 20, unicode: false),
                        IsDefault = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.T_RoleFuncObjForbid",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 6, unicode: false),
                        FuncObjID = c.String(nullable: false, maxLength: 60, unicode: false),
                        Note = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.RoleID, t.FuncObjID })
                .ForeignKey("dbo.T_FuncObject", t => t.FuncObjID, cascadeDelete: true)
                .ForeignKey("dbo.T_Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.FuncObjID);
            
            CreateTable(
                "dbo.T_FuncObject",
                c => new
                    {
                        FuncObjID = c.String(nullable: false, maxLength: 60, unicode: false),
                        ModuleID = c.String(nullable: false, maxLength: 10, unicode: false),
                        FuncName = c.String(maxLength: 40, unicode: false),
                        FuncID = c.String(nullable: false, maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => t.FuncObjID)
                .ForeignKey("dbo.T_Module", t => t.ModuleID, cascadeDelete: true)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.T_FuncBatchOpenSetting",
                c => new
                    {
                        FuncObjID = c.String(nullable: false, maxLength: 60, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        CloseTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.FuncObjID, t.PracBatchID })
                .ForeignKey("dbo.T_FuncObject", t => t.FuncObjID, cascadeDelete: true)
                .Index(t => t.FuncObjID);
            
            CreateTable(
                "dbo.T_Module",
                c => new
                    {
                        ModuleID = c.String(nullable: false, maxLength: 10, unicode: false),
                        ModuleName = c.String(nullable: false, maxLength: 100, unicode: false),
                        FartherModuleID = c.String(nullable: false, maxLength: 10, unicode: false),
                        PageName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        ModuleLevel = c.String(nullable: false, maxLength: 10, unicode: false),
                        AvailableStatus = c.Int(nullable: false),
                        ModuleOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ModuleID);
            
            CreateTable(
                "dbo.T_ModuleBatchOpenSetting",
                c => new
                    {
                        ModuleID = c.String(nullable: false, maxLength: 10, unicode: false),
                        PracBatchID = c.String(nullable: false, maxLength: 40, unicode: false),
                        CloseTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.ModuleID, t.PracBatchID })
                .ForeignKey("dbo.T_Module", t => t.ModuleID, cascadeDelete: true)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.T_RoleModule",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 6, unicode: false),
                        ModuleID = c.String(nullable: false, maxLength: 10, unicode: false),
                        IncludeAllSubMod = c.Int(nullable: false),
                        DataDomain = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.RoleID, t.ModuleID })
                .ForeignKey("dbo.T_Module", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.T_Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.Wbs_Users",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.T_User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Wbs_UserNodes",
                c => new
                    {
                        SerialID = c.String(nullable: false, maxLength: 50, unicode: false),
                        NodeID = c.String(maxLength: 30, unicode: false),
                        UserID = c.String(maxLength: 20, unicode: false),
                        UserWeight = c.Double(),
                        RealWeight = c.Double(),
                        Materal = c.String(maxLength: 50, unicode: false),
                        FinishTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SerialID)
                .ForeignKey("dbo.Wbs_Nodes", t => t.NodeID, cascadeDelete: true)
                .ForeignKey("dbo.Wbs_Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.NodeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Wbs_Nodes",
                c => new
                    {
                        NodeID = c.String(nullable: false, maxLength: 30, unicode: false),
                        NodeName = c.String(maxLength: 30, unicode: false),
                        FatherNodeID = c.String(maxLength: 30, unicode: false),
                        Decription = c.String(maxLength: 200, unicode: false),
                        NodeWeight = c.Double(),
                        Owner = c.String(maxLength: 50, unicode: false),
                        BeginTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Place = c.String(maxLength: 30, unicode: false),
                        NodeSequence = c.Int(),
                    })
                .PrimaryKey(t => t.NodeID);
            
            CreateTable(
                "dbo.BBS_SearchKeyWord",
                c => new
                    {
                        KeyWord = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.KeyWord);
            
            CreateTable(
                "dbo.C_ADColumn",
                c => new
                    {
                        ADColumnID = c.Int(nullable: false),
                        ADColumnName = c.String(nullable: false, maxLength: 100, unicode: false),
                        SybSystem = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.ADColumnID);
            
            CreateTable(
                "dbo.C_City",
                c => new
                    {
                        CityID = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        ProvinceID = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        CityName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.C_Province", t => t.ProvinceID)
                .Index(t => t.ProvinceID);
            
            CreateTable(
                "dbo.C_Province",
                c => new
                    {
                        ProvinceID = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                        ProvinceName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ProvinceID);
            
            CreateTable(
                "dbo.C_ContentColumn",
                c => new
                    {
                        ContColumnID = c.Int(nullable: false),
                        ContColumnName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ContTypeID = c.Int(nullable: false),
                        SybSystem = c.String(nullable: false, maxLength: 10, unicode: false),
                        ColSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ContColumnID);
            
            CreateTable(
                "dbo.C_StampType",
                c => new
                    {
                        TypeID = c.Int(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 100, unicode: false),
                        StampColumnID = c.Int(),
                        SybSystem = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.TypeID)
                .ForeignKey("dbo.C_ContentColumn", t => t.StampColumnID, cascadeDelete: true)
                .Index(t => t.StampColumnID);
            
            CreateTable(
                "dbo.T_Stamps",
                c => new
                    {
                        StampsID = c.String(nullable: false, maxLength: 50, unicode: false),
                        StampsTypeID = c.Int(nullable: false),
                        StampsUrl = c.String(nullable: false, maxLength: 100, unicode: false),
                        InnerID = c.Int(),
                    })
                .PrimaryKey(t => t.StampsID)
                .ForeignKey("dbo.T_Content", t => t.StampsID, cascadeDelete: true)
                .ForeignKey("dbo.C_StampType", t => t.StampsTypeID, cascadeDelete: true)
                .Index(t => t.StampsID)
                .Index(t => t.StampsTypeID);
            
            CreateTable(
                "dbo.T_Content",
                c => new
                    {
                        ContentID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ContentTypeID = c.Int(nullable: false),
                        ContentTitle = c.String(maxLength: 100, unicode: false),
                        ContentPublisher = c.String(maxLength: 50, unicode: false),
                        PubDate = c.Int(),
                        UnitID = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ContentID)
                .ForeignKey("dbo.C_ContentType", t => t.ContentTypeID, cascadeDelete: true)
                .Index(t => t.ContentTypeID);
            
            CreateTable(
                "dbo.C_ContentType",
                c => new
                    {
                        ConTypeID = c.Int(nullable: false),
                        ConTypeName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ConTypeID);
            
            CreateTable(
                "dbo.T_ADs",
                c => new
                    {
                        ADID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ADTitle = c.String(nullable: false, maxLength: 100, unicode: false),
                        ADTypeID = c.Int(),
                        ADOwner = c.String(maxLength: 50, unicode: false),
                        ADPubUser = c.String(nullable: false, maxLength: 50, unicode: false),
                        PubTime = c.Int(),
                        StartTime = c.Int(),
                        EndTime = c.Int(),
                        IsForbid = c.Int(nullable: false),
                        ADColumnID = c.Int(nullable: false),
                        MianPic = c.String(maxLength: 100, unicode: false),
                        LinkUrl = c.String(maxLength: 100, unicode: false),
                        InnerID = c.Int(),
                        UnitID = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.ADID)
                .ForeignKey("dbo.T_Content", t => t.ADID, cascadeDelete: true)
                .ForeignKey("dbo.C_ContentColumn", t => t.ADColumnID, cascadeDelete: true)
                .Index(t => t.ADID)
                .Index(t => t.ADColumnID);
            
            CreateTable(
                "dbo.T_DownLoadFiles",
                c => new
                    {
                        DLFileID = c.String(nullable: false, maxLength: 50, unicode: false),
                        DLFileColumnID = c.Int(nullable: false),
                        DLFileUrl = c.String(nullable: false, maxLength: 200, unicode: false),
                        InnerID = c.Int(),
                    })
                .PrimaryKey(t => t.DLFileID)
                .ForeignKey("dbo.T_Content", t => t.DLFileID, cascadeDelete: true)
                .ForeignKey("dbo.C_ContentColumn", t => t.DLFileColumnID, cascadeDelete: true)
                .Index(t => t.DLFileID)
                .Index(t => t.DLFileColumnID);
            
            CreateTable(
                "dbo.T_HomePageContent",
                c => new
                    {
                        HPCID = c.String(nullable: false, maxLength: 50, unicode: false),
                        HPColID = c.Int(nullable: false),
                        HPCSeq = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HPCID, t.HPColID })
                .ForeignKey("dbo.T_HomePageColumn", t => t.HPColID, cascadeDelete: true)
                .ForeignKey("dbo.T_Content", t => t.HPCID, cascadeDelete: true)
                .Index(t => t.HPCID)
                .Index(t => t.HPColID);
            
            CreateTable(
                "dbo.T_HomePageColumn",
                c => new
                    {
                        HPColumnID = c.Int(nullable: false),
                        HPColumnName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ColRowCount = c.Int(nullable: false),
                        OnTimeDESC = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        ContentType = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        ContentFrom = c.Int(),
                        UnitID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.HPColumnID);
            
            CreateTable(
                "dbo.T_News",
                c => new
                    {
                        NewsID = c.String(nullable: false, maxLength: 50, unicode: false),
                        NewsTypeID = c.Int(nullable: false),
                        NewsAuthor = c.String(unicode: false),
                        FlowState = c.Int(nullable: false),
                        IsShow = c.Int(nullable: false),
                        IsLocked = c.Int(nullable: false),
                        NewsColumnID = c.Int(),
                        PicUrl = c.String(unicode: false),
                        VideoUrl = c.String(unicode: false),
                        Html = c.String(unicode: false, storeType: "text"),
                        LinkUrl = c.String(unicode: false),
                        Download = c.String(unicode: false),
                        InnerID = c.Int(),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.C_NewsState", t => t.FlowState)
                .ForeignKey("dbo.C_NewsType", t => t.NewsTypeID)
                .ForeignKey("dbo.T_Content", t => t.NewsID, cascadeDelete: true)
                .ForeignKey("dbo.C_ContentColumn", t => t.NewsColumnID, cascadeDelete: true)
                .Index(t => t.NewsID)
                .Index(t => t.NewsTypeID)
                .Index(t => t.FlowState)
                .Index(t => t.NewsColumnID);
            
            CreateTable(
                "dbo.C_NewsState",
                c => new
                    {
                        NewsStateID = c.Int(nullable: false),
                        NewsStateName = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.NewsStateID);
            
            CreateTable(
                "dbo.C_NewsType",
                c => new
                    {
                        TypeID = c.Int(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TypeID);
            
            CreateTable(
                "dbo.T_Signature",
                c => new
                    {
                        SignatureID = c.String(nullable: false, maxLength: 50, unicode: false),
                        SignatureUrl = c.String(nullable: false, maxLength: 100, unicode: false),
                        InnerID = c.Int(),
                        UserID = c.String(maxLength: 50, unicode: false),
                        IsDefault = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SignatureID)
                .ForeignKey("dbo.T_Content", t => t.SignatureID, cascadeDelete: true)
                .Index(t => t.SignatureID);
            
            CreateTable(
                "dbo.C_EditStatus",
                c => new
                    {
                        StatusID = c.Int(nullable: false),
                        StatusName = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.C_ReadStatus",
                c => new
                    {
                        StatusID = c.Int(nullable: false),
                        StatusName = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.C_SchoolEvaStuGradeLevelItem",
                c => new
                    {
                        ItemNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ItemName = c.String(nullable: false, maxLength: 1024, unicode: false),
                        Note = c.String(maxLength: 20, unicode: false),
                        Weight = c.Int(),
                        PracBatchID = c.String(maxLength: 40, unicode: false),
                        ItemSequence = c.Int(),
                    })
                .PrimaryKey(t => t.ItemNo);
            
            CreateTable(
                "dbo.C_StuInfoType",
                c => new
                    {
                        InfoTypeID = c.Int(nullable: false),
                        InfoTypeName = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.InfoTypeID);
            
            CreateTable(
                "dbo.T_StuInfoPub",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        InfoTypeID = c.Int(nullable: false),
                        PubLevel = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.InfoTypeID })
                .ForeignKey("dbo.C_StuInfoType", t => t.InfoTypeID, cascadeDelete: true)
                .Index(t => t.InfoTypeID);
            
            CreateTable(
                "dbo.C_UserType",
                c => new
                    {
                        UserTypeID = c.Int(nullable: false),
                        UserTypeName = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.UserTypeID);
            
            CreateTable(
                "dbo.C_VolPosStatus",
                c => new
                    {
                        VolStatusID = c.Int(nullable: false),
                        VolStatusName = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.VolStatusID);
            
            CreateTable(
                "dbo.T_PracticeVolunteer",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        EntPracNo = c.String(nullable: false, maxLength: 100, unicode: false),
                        PosID = c.String(nullable: false, maxLength: 6, unicode: false),
                        VolunteerSequence = c.Int(nullable: false),
                        PosSequence = c.Int(nullable: false),
                        VolunteerStatus = c.Int(nullable: false),
                        InterviewTime = c.Int(nullable: false),
                        InterviewRecord = c.String(unicode: false, storeType: "text"),
                        InterviewScore = c.Double(),
                        Interviewee = c.String(maxLength: 20, unicode: false),
                        PreVolStatus = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.EntPracNo, t.PosID })
                .ForeignKey("dbo.C_VolPosStatus", t => t.VolunteerStatus)
                .Index(t => t.VolunteerStatus);
            
            CreateTable(
                "dbo.CPQ_T_QuestionnaireStructure",
                c => new
                    {
                        QuestionnaireID = c.String(nullable: false, maxLength: 50, unicode: false),
                        hasQuesionType = c.Int(),
                        hasQuestionNumber = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionnaireID);
            
            CreateTable(
                "dbo.T_Enterprise_ToCheck",
                c => new
                    {
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        EntState = c.Int(nullable: false),
                        Ent_Name = c.String(nullable: false, maxLength: 1024, unicode: false),
                        EntCategoryID = c.String(nullable: false, maxLength: 6, unicode: false),
                        EntRank = c.String(nullable: false, maxLength: 6, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Ent_No);
            
            CreateTable(
                "dbo.T_HomePageMenu",
                c => new
                    {
                        HPMID = c.Int(nullable: false),
                        HPMName = c.String(nullable: false, maxLength: 50, unicode: false),
                        FatherID = c.Int(nullable: false),
                        ActionLink = c.String(maxLength: 100, unicode: false),
                        UnitID = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.HPMID);
            
            CreateTable(
                "dbo.T_JobWanted",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        PositionID = c.String(nullable: false, maxLength: 6, unicode: false),
                        ResumeURL = c.String(maxLength: 200, unicode: false),
                        ReviewTime = c.Int(),
                        ReviewRecord = c.String(unicode: false, storeType: "text"),
                        ReviewScore = c.Int(),
                        JobStatus = c.Int(),
                        Flag = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.Ent_No, t.PositionID });
            
            CreateTable(
                "dbo.T_PhoneMsg",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        MsgContent = c.String(unicode: false, storeType: "text"),
                        Phone = c.String(unicode: false, storeType: "text"),
                        Flag = c.String(maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.T_RecruitPosition",
                c => new
                    {
                        Ent_No = c.String(nullable: false, maxLength: 20, unicode: false),
                        PositionID = c.String(nullable: false, maxLength: 6, unicode: false),
                        PubDate = c.Int(nullable: false),
                        PosDesc = c.String(nullable: false, unicode: false, storeType: "text"),
                        PosQuantity = c.Int(nullable: false),
                        PosExpireDate = c.Int(nullable: false),
                        PosRequirement = c.String(nullable: false, unicode: false, storeType: "text"),
                        PosPay = c.String(nullable: false, unicode: false, storeType: "text"),
                        Publisher = c.String(maxLength: 50, unicode: false),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ent_No, t.PositionID });
            
            CreateTable(
                "dbo.T_ReportData",
                c => new
                    {
                        ReportID = c.String(nullable: false, maxLength: 50, unicode: false),
                        ReportName = c.String(nullable: false, maxLength: 500, unicode: false),
                        SqlStr = c.String(nullable: false, unicode: false, storeType: "text"),
                        HeadFields = c.String(nullable: false, maxLength: 500, unicode: false),
                        RecordsPerPage = c.Int(nullable: false),
                        ParaNames = c.String(unicode: false, storeType: "text"),
                        ColunmToShow = c.String(maxLength: 500, unicode: false),
                        OperateColum = c.String(unicode: false, storeType: "text"),
                        ReportLog = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.ReportID);
            
            CreateTable(
                "dbo.T_Resume",
                c => new
                    {
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ResumeName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ResumeURL = c.String(nullable: false, maxLength: 200, unicode: false),
                        PubTime = c.Int(),
                        IsDefault = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PracticeNo, t.ResumeName });
            
            CreateTable(
                "dbo.T_StuFinalEntRecord",
                c => new
                    {
                        Ent_Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        PracticeNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        EntCategory = c.String(maxLength: 50, unicode: false),
                        EntRank = c.String(maxLength: 50, unicode: false),
                        EntAddress = c.String(nullable: false, maxLength: 500, unicode: false),
                        EntResume = c.String(maxLength: 2000, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Contectinfo = c.String(nullable: false, maxLength: 1000, unicode: false),
                    })
                .PrimaryKey(t => new { t.Ent_Name, t.PracticeNo });
            
            CreateTable(
                "dbo.T_StuPic",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        InnerID = c.Int(nullable: false),
                        PicMood = c.String(maxLength: 100, unicode: false),
                        PicName = c.String(maxLength: 100, unicode: false),
                        PicLink = c.String(nullable: false, maxLength: 100, unicode: false),
                        Secquence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.InnerID });
            
            CreateTable(
                "dbo.T_StuResFile",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        InnerID = c.Int(nullable: false),
                        ResFileMood = c.String(maxLength: 100, unicode: false),
                        ResFileName = c.String(maxLength: 100, unicode: false),
                        ResFileLink = c.String(nullable: false, maxLength: 100, unicode: false),
                        Secquence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.InnerID });
            
            CreateTable(
                "dbo.T_StuScoreRecord",
                c => new
                    {
                        StuScoreStuScoreID = c.String(nullable: false, maxLength: 50, unicode: false),
                        PraticeNo = c.String(maxLength: 50, unicode: false),
                        UserID = c.String(maxLength: 50, unicode: false),
                        Evidence = c.String(maxLength: 500, unicode: false),
                        ApplyReviewScore = c.String(maxLength: 50, unicode: false),
                        ApplyContents = c.String(maxLength: 500, unicode: false),
                        StateID = c.String(maxLength: 50, unicode: false),
                        ReviewScore = c.Double(),
                        AuditOpinion = c.String(maxLength: 500, unicode: false),
                        PracticeCaseComment = c.String(maxLength: 1024, unicode: false),
                        WeekRecordScore = c.Double(),
                        PracticeCaseScore = c.Double(),
                        WeekRecordComment = c.String(maxLength: 1024, unicode: false),
                        PracticeContent = c.String(unicode: false, storeType: "text"),
                        PracticeReport = c.String(unicode: false, storeType: "text"),
                        PracticeReportFile = c.String(maxLength: 200, unicode: false),
                        PracticeStartEnd = c.String(maxLength: 50, unicode: false),
                        PracUnitComment = c.String(maxLength: 1024, unicode: false),
                        SchoolComment = c.String(maxLength: 1024, unicode: false),
                        TutorComment = c.String(maxLength: 1024, unicode: false),
                        StuEvaEntScore = c.Double(),
                        StuEvaSchoolScore = c.Double(),
                        CreateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.StuScoreStuScoreID);
            
            CreateTable(
                "dbo.T_StuVideo",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 20, unicode: false),
                        InnerID = c.Int(nullable: false),
                        VideoMood = c.String(maxLength: 100, unicode: false),
                        VideoName = c.String(maxLength: 100, unicode: false),
                        VideoLink = c.String(nullable: false, maxLength: 100, unicode: false),
                        Secquence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.InnerID });
            
            CreateTable(
                "dbo.Wbs_TaskNodes",
                c => new
                    {
                        TaskNodeId = c.String(nullable: false, maxLength: 30, unicode: false),
                        TaskNodeTypeId = c.Int(nullable: false),
                        FatherId = c.String(nullable: false, maxLength: 30, unicode: false),
                        TaskId = c.String(nullable: false, maxLength: 30, unicode: false),
                        IsLeaf = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30, unicode: false),
                        BeginTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Place = c.String(nullable: false, maxLength: 30, unicode: false),
                        Decription = c.String(nullable: false, maxLength: 200, unicode: false),
                        NodeSequence = c.Int(nullable: false),
                        ArrangedUserId = c.String(maxLength: 20, unicode: false),
                        RealPercent = c.Double(),
                    })
                .PrimaryKey(t => new { t.TaskNodeId, t.TaskNodeTypeId, t.FatherId, t.TaskId, t.IsLeaf, t.Weight, t.Name, t.BeginTime, t.EndTime, t.Place, t.Decription, t.NodeSequence })
                .ForeignKey("dbo.Wbs_Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Wbs_Tasks",
                c => new
                    {
                        TaskId = c.String(nullable: false, maxLength: 30, unicode: false),
                        Name = c.String(nullable: false, maxLength: 30, unicode: false),
                        BeginTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Decription = c.String(nullable: false, maxLength: 200, unicode: false),
                        OwnerId = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wbs_TaskNodes", "TaskId", "dbo.Wbs_Tasks");
            DropForeignKey("dbo.T_PracticeVolunteer", "VolunteerStatus", "dbo.C_VolPosStatus");
            DropForeignKey("dbo.T_StuInfoPub", "InfoTypeID", "dbo.C_StuInfoType");
            DropForeignKey("dbo.T_News", "NewsColumnID", "dbo.C_ContentColumn");
            DropForeignKey("dbo.T_DownLoadFiles", "DLFileColumnID", "dbo.C_ContentColumn");
            DropForeignKey("dbo.T_ADs", "ADColumnID", "dbo.C_ContentColumn");
            DropForeignKey("dbo.C_StampType", "StampColumnID", "dbo.C_ContentColumn");
            DropForeignKey("dbo.T_Stamps", "StampsTypeID", "dbo.C_StampType");
            DropForeignKey("dbo.T_Stamps", "StampsID", "dbo.T_Content");
            DropForeignKey("dbo.T_Signature", "SignatureID", "dbo.T_Content");
            DropForeignKey("dbo.T_News", "NewsID", "dbo.T_Content");
            DropForeignKey("dbo.T_News", "NewsTypeID", "dbo.C_NewsType");
            DropForeignKey("dbo.T_News", "FlowState", "dbo.C_NewsState");
            DropForeignKey("dbo.T_HomePageContent", "HPCID", "dbo.T_Content");
            DropForeignKey("dbo.T_HomePageContent", "HPColID", "dbo.T_HomePageColumn");
            DropForeignKey("dbo.T_DownLoadFiles", "DLFileID", "dbo.T_Content");
            DropForeignKey("dbo.T_ADs", "ADID", "dbo.T_Content");
            DropForeignKey("dbo.T_Content", "ContentTypeID", "dbo.C_ContentType");
            DropForeignKey("dbo.C_City", "ProvinceID", "dbo.C_Province");
            DropForeignKey("dbo.BBS_DiaryReply", "DiaryID", "dbo.BBS_Diary");
            DropForeignKey("dbo.BBS_Diary_Report", "DiaryID", "dbo.BBS_Diary");
            DropForeignKey("dbo.Wbs_Users", "UserID", "dbo.T_User");
            DropForeignKey("dbo.Wbs_UserNodes", "UserID", "dbo.Wbs_Users");
            DropForeignKey("dbo.Wbs_UserNodes", "NodeID", "dbo.Wbs_Nodes");
            DropForeignKey("dbo.V3_StuEnterPriseApply", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_UserRole", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_UserRole", "RoleID", "dbo.T_Role");
            DropForeignKey("dbo.T_RoleFuncObjForbid", "RoleID", "dbo.T_Role");
            DropForeignKey("dbo.T_RoleFuncObjForbid", "FuncObjID", "dbo.T_FuncObject");
            DropForeignKey("dbo.T_RoleModule", "RoleID", "dbo.T_Role");
            DropForeignKey("dbo.T_RoleModule", "ModuleID", "dbo.T_Module");
            DropForeignKey("dbo.T_ModuleBatchOpenSetting", "ModuleID", "dbo.T_Module");
            DropForeignKey("dbo.T_FuncObject", "ModuleID", "dbo.T_Module");
            DropForeignKey("dbo.T_FuncBatchOpenSetting", "FuncObjID", "dbo.T_FuncObject");
            DropForeignKey("dbo.T_SysMsg", "MsgOwner", "dbo.T_User");
            DropForeignKey("dbo.T_Student", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_Platformer", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_MsgSend", "Sender", "dbo.T_User");
            DropForeignKey("dbo.T_MsgRec", "Receiver", "dbo.T_User");
            DropForeignKey("dbo.T_MsgSend", "MsgID", "dbo.T_SysMsg");
            DropForeignKey("dbo.T_MsgSend", "SendTypeID", "dbo.C_MsgSendType");
            DropForeignKey("dbo.T_MsgRec", "MsgID", "dbo.T_SysMsg");
            DropForeignKey("dbo.T_SysMsg", "MsgTypeID", "dbo.C_MsgType");
            DropForeignKey("dbo.T_Faculty", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_Employee", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_EntReviewerTask", "EmployeeID", "dbo.T_Employee");
            DropForeignKey("dbo.T_StuBatchReg_Extent", "AuthorizedEntNo", "dbo.T_Enterprise");
            DropForeignKey("dbo.T_Employee", "Ent_No", "dbo.T_Enterprise");
            DropForeignKey("dbo.T_AnswerQuestion", "Ent_No", "dbo.T_Enterprise");
            DropForeignKey("dbo.T_School", "Status", "dbo.C_UnitStatus");
            DropForeignKey("dbo.T_PracBatch", "SchoolID", "dbo.T_School");
            DropForeignKey("dbo.T_Class", "SchoolID", "dbo.T_School");
            DropForeignKey("dbo.C_Specialty", "SchoolID", "dbo.T_School");
            DropForeignKey("dbo.T_Class", new[] { "EntryYear", "SpeNo", "SchoolID" }, "dbo.C_Specialty");
            DropForeignKey("dbo.T_Student", "StuClass", "dbo.T_Class");
            DropForeignKey("dbo.T_WeekRecordExtemsion", new[] { "PracticeNo", "RecordNo" }, "dbo.T_WeekRecord");
            DropForeignKey("dbo.T_WeekRecord", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_StuScoreApply", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_StuEvaluateSchool", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_StuEvaluateSchool", "ItemGrade", "dbo.C_StuEvaSchoolGradeLevelItem");
            DropForeignKey("dbo.T_StuEvaluateSchool", "ItemNo", "dbo.C_StuEvaluateSchoolItem");
            DropForeignKey("dbo.T_StuEvaluateEnt", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_StuEvaluateEnt", "ItemNo", "dbo.C_StuEvaluateEntItem");
            DropForeignKey("dbo.T_StuEvaluateEnt", "ItemGrade", "dbo.C_StuEvaEntGradeLevelItem");
            DropForeignKey("dbo.T_StuBatchReg", "UserID", "dbo.T_Student");
            DropForeignKey("dbo.T_StuBatchReg_Extent", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_StuBatchReg_Extent", "DisperseStateID", "dbo.C_DisperseState");
            DropForeignKey("dbo.T_SelfEvaluation", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_SelfEvaluation", "ItemNo", "dbo.C_SelfEvaluationItem");
            DropForeignKey("dbo.T_PracticeIdentification", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_PracticeIdentification", "ItemNo", "dbo.C_PracticeIdentificationItem");
            DropForeignKey("dbo.T_PracticeAttendance", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_PracticeAttendance", "ItemNo", "dbo.C_PracAttendanceItem");
            DropForeignKey("dbo.T_PracticeAttendance", "ItemGrade", "dbo.C_PracAttendanceGradeItem");
            DropForeignKey("dbo.T_EntStudentReviewLink", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_EntStudentReviewLink", "TaskID", "dbo.T_EntReviewerTask");
            DropForeignKey("dbo.T_EntReviewerTask", "ItemID", "dbo.T_EntReviewItem");
            DropForeignKey("dbo.T_PracticePosition", "EntPracNo", "dbo.T_EntBatchReg");
            DropForeignKey("dbo.T_PracticePosition", "PositionID", "dbo.C_Position");
            DropForeignKey("dbo.T_EntReviewItem", "EntPracNo", "dbo.T_EntBatchReg");
            DropForeignKey("dbo.T_EntBatchReg", "Ent_No", "dbo.T_Enterprise");
            DropForeignKey("dbo.T_SchoolPubToUnit", "ApplyStatusID", "dbo.C_ApplyStatus");
            DropForeignKey("dbo.T_StuBatchReg", "PracBatchID", "dbo.T_PracBatch");
            DropForeignKey("dbo.T_SchoolReviewerTask", "ItemID", "dbo.T_SchoolReviewItem");
            DropForeignKey("dbo.T_SchoolStudentReviewLink", "TaskID", "dbo.T_SchoolReviewerTask");
            DropForeignKey("dbo.T_SchoolStudentReviewLink", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_SchoolReviewerTask", "TeacherID", "dbo.T_Faculty");
            DropForeignKey("dbo.T_Faculty", "SchoolID", "dbo.T_School");
            DropForeignKey("dbo.T_Faculty", "FacProPos", "dbo.C_ProPosition");
            DropForeignKey("dbo.T_SchoolReviewItem", "PracBatchID", "dbo.T_PracBatch");
            DropForeignKey("dbo.T_PlatformPubToUnit", "PracBatchID", "dbo.T_PracBatch");
            DropForeignKey("dbo.T_EntBatchReg", "PracBatchID", "dbo.T_PracBatch");
            DropForeignKey("dbo.C_PracticeCaseItem", "PracBatchID", "dbo.T_PracBatch");
            DropForeignKey("dbo.T_PracticeCase", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_PracticeCaseExtension", new[] { "PracticeNo", "CaseNo", "ItemNo" }, "dbo.T_PracticeCase");
            DropForeignKey("dbo.T_PracticeCase", "ItemNo", "dbo.C_PracticeCaseItem");
            DropForeignKey("dbo.T_PlatformPubToUnit", "ApplyStatusID", "dbo.C_ApplyStatus");
            DropForeignKey("dbo.T_EntBatchReg", "ApplyStatus", "dbo.C_ApplyStatus");
            DropForeignKey("dbo.T_EntEvaluateStu", "PracticeNo", "dbo.T_StuBatchReg");
            DropForeignKey("dbo.T_EntEvaluateStu", "ItemGrade", "dbo.C_EntEvaStuGradeLevelItem");
            DropForeignKey("dbo.T_EntEvaluateStu", "ItemNo", "dbo.C_EntEvaluateStuItem");
            DropForeignKey("dbo.T_StuBatchReg", "CareerStatusID", "dbo.C_CareerStatus");
            DropForeignKey("dbo.C_CareerStatus", "C_CareerStatus2_CareerStatusID", "dbo.C_CareerStatus");
            DropForeignKey("dbo.T_Enterprise", "EntState", "dbo.C_UnitStatus");
            DropForeignKey("dbo.V3_EnterpriseApply", new[] { "EntRankID", "EntCategoryID" }, "dbo.C_EntRank");
            DropForeignKey("dbo.V3_StuEnterPriseApply", "Ent_No", "dbo.V3_EnterpriseApply");
            DropForeignKey("dbo.V3_EnterpriseApply", "UserID", "dbo.T_User");
            DropForeignKey("dbo.T_Enterprise", new[] { "EntRank", "EntCategoryID" }, "dbo.C_EntRank");
            DropForeignKey("dbo.C_EntRank", "EntCategoryID", "dbo.C_EntCategory");
            DropForeignKey("dbo.T_Enterprise", "CountyID", "dbo.C_County");
            DropForeignKey("dbo.CPQ_T_Questionnaire", "OwnerID", "dbo.T_User");
            DropForeignKey("dbo.CPQ_T_AnswerSheet", "QuestionnaireID", "dbo.CPQ_T_Questionnaire");
            DropForeignKey("dbo.CPQ_T_QuestionOption", "QuestionID", "dbo.CPQ_T_Question");
            DropForeignKey("dbo.CPQ_T_AttachQuestionToQuestionnaire", "QuestionnaireID", "dbo.CPQ_T_Questionnaire");
            DropForeignKey("dbo.CPQ_T_AttachQuestionToQuestionnaire", "QuestionID", "dbo.CPQ_T_Question");
            DropForeignKey("dbo.CPQ_T_AnswerSheet", "QuestionID", "dbo.CPQ_T_Question");
            DropForeignKey("dbo.CPQ_T_Question", "QuestionType", "dbo.CPQ_C_QuestionType");
            DropForeignKey("dbo.CPQ_T_Question", "QuestionDomain", "dbo.CPQ_C_QuestionDomain");
            DropForeignKey("dbo.CPQ_T_Questionnaire", "share", "dbo.CPQ_C_Share");
            DropForeignKey("dbo.CPQ_T_Questionnaire", "Status", "dbo.CPQ_C_QuestionnaireStatus");
            DropForeignKey("dbo.CPQ_T_Questionnaire", "QuestionnaireDomain", "dbo.CPQ_C_QuestionnaireDomain");
            DropForeignKey("dbo.BBS_Users", "UserID", "dbo.T_User");
            DropForeignKey("dbo.BBS_Visitor", "UserIDB", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Visitor", "UserIDA", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Post_Report", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Post", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Photo", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Note", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Friend", "UserIDB", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Friend", "UserIDA", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Friend", "StatusID", "dbo.BBS_C_Friend");
            DropForeignKey("dbo.BBS_ForumManager", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Post", "ThemeID", "dbo.BBS_Theme");
            DropForeignKey("dbo.BBS_StepPraise", "PostID", "dbo.BBS_Post");
            DropForeignKey("dbo.BBS_StepPraise", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Share", "PostID", "dbo.BBS_Post");
            DropForeignKey("dbo.BBS_Share", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Share", "StatusID", "dbo.BBS_C_Share");
            DropForeignKey("dbo.BBS_ReplyPost", "PostID", "dbo.BBS_Post");
            DropForeignKey("dbo.BBS_ReplyPost", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_ReplyReport", "ReplyPostID", "dbo.BBS_ReplyPost");
            DropForeignKey("dbo.BBS_ReplyReport", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_ReplyParise", "ReplyPostID", "dbo.BBS_ReplyPost");
            DropForeignKey("dbo.BBS_Post", "BestReplyID", "dbo.BBS_ReplyPost");
            DropForeignKey("dbo.BBS_Post_Report", "PostID", "dbo.BBS_Post");
            DropForeignKey("dbo.BBS_Post", "PostTypeID", "dbo.BBS_C_PostType");
            DropForeignKey("dbo.BBS_Post", "StateID", "dbo.BBS_C_PostState");
            DropForeignKey("dbo.BBS_Theme", "ColumnID", "dbo.BBS_Columns");
            DropForeignKey("dbo.BBS_ForumManager", "ForumID", "dbo.BBS_Columns");
            DropForeignKey("dbo.BBS_Columns", "ForumID", "dbo.BBS_Forum");
            DropForeignKey("dbo.BBS_DiaryReply", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Diary_Report", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Diary", "UserID", "dbo.BBS_Users");
            DropForeignKey("dbo.BBS_Users", "UserStateID", "dbo.BBS_C_UserState");
            DropForeignKey("dbo.BBS_Users", "UserGradeID", "dbo.BBS_C_UserGrade");
            DropForeignKey("dbo.BBS_Diary", "ReportID", "dbo.BBS_Diary_Report");
            DropForeignKey("dbo.BBS_Diary", "StateID", "dbo.BBS_C_DiaryState");
            DropIndex("dbo.Wbs_TaskNodes", new[] { "TaskId" });
            DropIndex("dbo.T_PracticeVolunteer", new[] { "VolunteerStatus" });
            DropIndex("dbo.T_StuInfoPub", new[] { "InfoTypeID" });
            DropIndex("dbo.T_Signature", new[] { "SignatureID" });
            DropIndex("dbo.T_News", new[] { "NewsColumnID" });
            DropIndex("dbo.T_News", new[] { "FlowState" });
            DropIndex("dbo.T_News", new[] { "NewsTypeID" });
            DropIndex("dbo.T_News", new[] { "NewsID" });
            DropIndex("dbo.T_HomePageContent", new[] { "HPColID" });
            DropIndex("dbo.T_HomePageContent", new[] { "HPCID" });
            DropIndex("dbo.T_DownLoadFiles", new[] { "DLFileColumnID" });
            DropIndex("dbo.T_DownLoadFiles", new[] { "DLFileID" });
            DropIndex("dbo.T_ADs", new[] { "ADColumnID" });
            DropIndex("dbo.T_ADs", new[] { "ADID" });
            DropIndex("dbo.T_Content", new[] { "ContentTypeID" });
            DropIndex("dbo.T_Stamps", new[] { "StampsTypeID" });
            DropIndex("dbo.T_Stamps", new[] { "StampsID" });
            DropIndex("dbo.C_StampType", new[] { "StampColumnID" });
            DropIndex("dbo.C_City", new[] { "ProvinceID" });
            DropIndex("dbo.Wbs_UserNodes", new[] { "UserID" });
            DropIndex("dbo.Wbs_UserNodes", new[] { "NodeID" });
            DropIndex("dbo.Wbs_Users", new[] { "UserID" });
            DropIndex("dbo.T_RoleModule", new[] { "ModuleID" });
            DropIndex("dbo.T_RoleModule", new[] { "RoleID" });
            DropIndex("dbo.T_ModuleBatchOpenSetting", new[] { "ModuleID" });
            DropIndex("dbo.T_FuncBatchOpenSetting", new[] { "FuncObjID" });
            DropIndex("dbo.T_FuncObject", new[] { "ModuleID" });
            DropIndex("dbo.T_RoleFuncObjForbid", new[] { "FuncObjID" });
            DropIndex("dbo.T_RoleFuncObjForbid", new[] { "RoleID" });
            DropIndex("dbo.T_UserRole", new[] { "RoleID" });
            DropIndex("dbo.T_UserRole", new[] { "UserID" });
            DropIndex("dbo.T_Platformer", new[] { "UserID" });
            DropIndex("dbo.T_MsgSend", new[] { "SendTypeID" });
            DropIndex("dbo.T_MsgSend", new[] { "Sender" });
            DropIndex("dbo.T_MsgSend", new[] { "MsgID" });
            DropIndex("dbo.T_SysMsg", new[] { "MsgTypeID" });
            DropIndex("dbo.T_SysMsg", new[] { "MsgOwner" });
            DropIndex("dbo.T_MsgRec", new[] { "Receiver" });
            DropIndex("dbo.T_MsgRec", new[] { "MsgID" });
            DropIndex("dbo.T_AnswerQuestion", new[] { "Ent_No" });
            DropIndex("dbo.T_WeekRecordExtemsion", new[] { "PracticeNo", "RecordNo" });
            DropIndex("dbo.T_WeekRecord", new[] { "PracticeNo" });
            DropIndex("dbo.T_StuScoreApply", new[] { "PracticeNo" });
            DropIndex("dbo.T_StuEvaluateSchool", new[] { "ItemGrade" });
            DropIndex("dbo.T_StuEvaluateSchool", new[] { "ItemNo" });
            DropIndex("dbo.T_StuEvaluateSchool", new[] { "PracticeNo" });
            DropIndex("dbo.T_StuEvaluateEnt", new[] { "ItemGrade" });
            DropIndex("dbo.T_StuEvaluateEnt", new[] { "ItemNo" });
            DropIndex("dbo.T_StuEvaluateEnt", new[] { "PracticeNo" });
            DropIndex("dbo.T_StuBatchReg_Extent", new[] { "DisperseStateID" });
            DropIndex("dbo.T_StuBatchReg_Extent", new[] { "AuthorizedEntNo" });
            DropIndex("dbo.T_StuBatchReg_Extent", new[] { "PracticeNo" });
            DropIndex("dbo.T_SelfEvaluation", new[] { "ItemNo" });
            DropIndex("dbo.T_SelfEvaluation", new[] { "PracticeNo" });
            DropIndex("dbo.T_PracticeIdentification", new[] { "ItemNo" });
            DropIndex("dbo.T_PracticeIdentification", new[] { "PracticeNo" });
            DropIndex("dbo.T_PracticeAttendance", new[] { "ItemGrade" });
            DropIndex("dbo.T_PracticeAttendance", new[] { "ItemNo" });
            DropIndex("dbo.T_PracticeAttendance", new[] { "PracticeNo" });
            DropIndex("dbo.T_PracticePosition", new[] { "PositionID" });
            DropIndex("dbo.T_PracticePosition", new[] { "EntPracNo" });
            DropIndex("dbo.T_SchoolPubToUnit", new[] { "ApplyStatusID" });
            DropIndex("dbo.T_SchoolStudentReviewLink", new[] { "PracticeNo" });
            DropIndex("dbo.T_SchoolStudentReviewLink", new[] { "TaskID" });
            DropIndex("dbo.T_Faculty", new[] { "SchoolID" });
            DropIndex("dbo.T_Faculty", new[] { "FacProPos" });
            DropIndex("dbo.T_Faculty", new[] { "UserID" });
            DropIndex("dbo.T_SchoolReviewerTask", new[] { "ItemID" });
            DropIndex("dbo.T_SchoolReviewerTask", new[] { "TeacherID" });
            DropIndex("dbo.T_SchoolReviewItem", new[] { "PracBatchID" });
            DropIndex("dbo.T_PracticeCaseExtension", new[] { "PracticeNo", "CaseNo", "ItemNo" });
            DropIndex("dbo.T_PracticeCase", new[] { "ItemNo" });
            DropIndex("dbo.T_PracticeCase", new[] { "PracticeNo" });
            DropIndex("dbo.C_PracticeCaseItem", new[] { "PracBatchID" });
            DropIndex("dbo.T_PracBatch", new[] { "SchoolID" });
            DropIndex("dbo.T_PlatformPubToUnit", new[] { "ApplyStatusID" });
            DropIndex("dbo.T_PlatformPubToUnit", new[] { "PracBatchID" });
            DropIndex("dbo.T_EntBatchReg", new[] { "ApplyStatus" });
            DropIndex("dbo.T_EntBatchReg", new[] { "Ent_No" });
            DropIndex("dbo.T_EntBatchReg", new[] { "PracBatchID" });
            DropIndex("dbo.T_EntReviewItem", new[] { "EntPracNo" });
            DropIndex("dbo.T_EntReviewerTask", new[] { "EmployeeID" });
            DropIndex("dbo.T_EntReviewerTask", new[] { "ItemID" });
            DropIndex("dbo.T_EntStudentReviewLink", new[] { "PracticeNo" });
            DropIndex("dbo.T_EntStudentReviewLink", new[] { "TaskID" });
            DropIndex("dbo.T_EntEvaluateStu", new[] { "ItemGrade" });
            DropIndex("dbo.T_EntEvaluateStu", new[] { "ItemNo" });
            DropIndex("dbo.T_EntEvaluateStu", new[] { "PracticeNo" });
            DropIndex("dbo.C_CareerStatus", new[] { "C_CareerStatus2_CareerStatusID" });
            DropIndex("dbo.T_StuBatchReg", new[] { "CareerStatusID" });
            DropIndex("dbo.T_StuBatchReg", new[] { "PracBatchID" });
            DropIndex("dbo.T_StuBatchReg", new[] { "UserID" });
            DropIndex("dbo.T_Student", new[] { "StuClass" });
            DropIndex("dbo.T_Student", new[] { "UserID" });
            DropIndex("dbo.T_Class", new[] { "EntryYear", "SpeNo", "SchoolID" });
            DropIndex("dbo.C_Specialty", new[] { "SchoolID" });
            DropIndex("dbo.T_School", new[] { "Status" });
            DropIndex("dbo.V3_StuEnterPriseApply", new[] { "UserID" });
            DropIndex("dbo.V3_StuEnterPriseApply", new[] { "Ent_No" });
            DropIndex("dbo.V3_EnterpriseApply", new[] { "UserID" });
            DropIndex("dbo.V3_EnterpriseApply", new[] { "EntRankID", "EntCategoryID" });
            DropIndex("dbo.C_EntRank", new[] { "EntCategoryID" });
            DropIndex("dbo.T_Enterprise", new[] { "EntState" });
            DropIndex("dbo.T_Enterprise", new[] { "EntRank", "EntCategoryID" });
            DropIndex("dbo.T_Enterprise", new[] { "CountyID" });
            DropIndex("dbo.T_Employee", new[] { "Ent_No" });
            DropIndex("dbo.T_Employee", new[] { "UserID" });
            DropIndex("dbo.CPQ_T_QuestionOption", new[] { "QuestionID" });
            DropIndex("dbo.CPQ_T_AttachQuestionToQuestionnaire", new[] { "QuestionID" });
            DropIndex("dbo.CPQ_T_AttachQuestionToQuestionnaire", new[] { "QuestionnaireID" });
            DropIndex("dbo.CPQ_T_Question", new[] { "QuestionDomain" });
            DropIndex("dbo.CPQ_T_Question", new[] { "QuestionType" });
            DropIndex("dbo.CPQ_T_AnswerSheet", new[] { "QuestionID" });
            DropIndex("dbo.CPQ_T_AnswerSheet", new[] { "QuestionnaireID" });
            DropIndex("dbo.CPQ_T_Questionnaire", new[] { "share" });
            DropIndex("dbo.CPQ_T_Questionnaire", new[] { "Status" });
            DropIndex("dbo.CPQ_T_Questionnaire", new[] { "OwnerID" });
            DropIndex("dbo.CPQ_T_Questionnaire", new[] { "QuestionnaireDomain" });
            DropIndex("dbo.BBS_Visitor", new[] { "UserIDB" });
            DropIndex("dbo.BBS_Visitor", new[] { "UserIDA" });
            DropIndex("dbo.BBS_Photo", new[] { "UserID" });
            DropIndex("dbo.BBS_Note", new[] { "UserID" });
            DropIndex("dbo.BBS_Friend", new[] { "StatusID" });
            DropIndex("dbo.BBS_Friend", new[] { "UserIDB" });
            DropIndex("dbo.BBS_Friend", new[] { "UserIDA" });
            DropIndex("dbo.BBS_StepPraise", new[] { "UserID" });
            DropIndex("dbo.BBS_StepPraise", new[] { "PostID" });
            DropIndex("dbo.BBS_Share", new[] { "StatusID" });
            DropIndex("dbo.BBS_Share", new[] { "PostID" });
            DropIndex("dbo.BBS_Share", new[] { "UserID" });
            DropIndex("dbo.BBS_ReplyReport", new[] { "ReplyPostID" });
            DropIndex("dbo.BBS_ReplyReport", new[] { "UserID" });
            DropIndex("dbo.BBS_ReplyParise", new[] { "ReplyPostID" });
            DropIndex("dbo.BBS_ReplyPost", new[] { "UserID" });
            DropIndex("dbo.BBS_ReplyPost", new[] { "PostID" });
            DropIndex("dbo.BBS_Post_Report", new[] { "PostID" });
            DropIndex("dbo.BBS_Post_Report", new[] { "UserID" });
            DropIndex("dbo.BBS_Post", new[] { "BestReplyID" });
            DropIndex("dbo.BBS_Post", new[] { "PostTypeID" });
            DropIndex("dbo.BBS_Post", new[] { "StateID" });
            DropIndex("dbo.BBS_Post", new[] { "UserID" });
            DropIndex("dbo.BBS_Post", new[] { "ThemeID" });
            DropIndex("dbo.BBS_Theme", new[] { "ColumnID" });
            DropIndex("dbo.BBS_Columns", new[] { "ForumID" });
            DropIndex("dbo.BBS_ForumManager", new[] { "ForumID" });
            DropIndex("dbo.BBS_ForumManager", new[] { "UserID" });
            DropIndex("dbo.BBS_DiaryReply", new[] { "DiaryID" });
            DropIndex("dbo.BBS_DiaryReply", new[] { "UserID" });
            DropIndex("dbo.BBS_Users", new[] { "UserStateID" });
            DropIndex("dbo.BBS_Users", new[] { "UserGradeID" });
            DropIndex("dbo.BBS_Users", new[] { "UserID" });
            DropIndex("dbo.BBS_Diary_Report", new[] { "DiaryID" });
            DropIndex("dbo.BBS_Diary_Report", new[] { "UserID" });
            DropIndex("dbo.BBS_Diary", new[] { "ReportID" });
            DropIndex("dbo.BBS_Diary", new[] { "StateID" });
            DropIndex("dbo.BBS_Diary", new[] { "UserID" });
            DropTable("dbo.Wbs_Tasks");
            DropTable("dbo.Wbs_TaskNodes");
            DropTable("dbo.T_StuVideo");
            DropTable("dbo.T_StuScoreRecord");
            DropTable("dbo.T_StuResFile");
            DropTable("dbo.T_StuPic");
            DropTable("dbo.T_StuFinalEntRecord");
            DropTable("dbo.T_Resume");
            DropTable("dbo.T_ReportData");
            DropTable("dbo.T_RecruitPosition");
            DropTable("dbo.T_PhoneMsg");
            DropTable("dbo.T_JobWanted");
            DropTable("dbo.T_HomePageMenu");
            DropTable("dbo.T_Enterprise_ToCheck");
            DropTable("dbo.CPQ_T_QuestionnaireStructure");
            DropTable("dbo.T_PracticeVolunteer");
            DropTable("dbo.C_VolPosStatus");
            DropTable("dbo.C_UserType");
            DropTable("dbo.T_StuInfoPub");
            DropTable("dbo.C_StuInfoType");
            DropTable("dbo.C_SchoolEvaStuGradeLevelItem");
            DropTable("dbo.C_ReadStatus");
            DropTable("dbo.C_EditStatus");
            DropTable("dbo.T_Signature");
            DropTable("dbo.C_NewsType");
            DropTable("dbo.C_NewsState");
            DropTable("dbo.T_News");
            DropTable("dbo.T_HomePageColumn");
            DropTable("dbo.T_HomePageContent");
            DropTable("dbo.T_DownLoadFiles");
            DropTable("dbo.T_ADs");
            DropTable("dbo.C_ContentType");
            DropTable("dbo.T_Content");
            DropTable("dbo.T_Stamps");
            DropTable("dbo.C_StampType");
            DropTable("dbo.C_ContentColumn");
            DropTable("dbo.C_Province");
            DropTable("dbo.C_City");
            DropTable("dbo.C_ADColumn");
            DropTable("dbo.BBS_SearchKeyWord");
            DropTable("dbo.Wbs_Nodes");
            DropTable("dbo.Wbs_UserNodes");
            DropTable("dbo.Wbs_Users");
            DropTable("dbo.T_RoleModule");
            DropTable("dbo.T_ModuleBatchOpenSetting");
            DropTable("dbo.T_Module");
            DropTable("dbo.T_FuncBatchOpenSetting");
            DropTable("dbo.T_FuncObject");
            DropTable("dbo.T_RoleFuncObjForbid");
            DropTable("dbo.T_Role");
            DropTable("dbo.T_UserRole");
            DropTable("dbo.T_Platformer");
            DropTable("dbo.C_MsgSendType");
            DropTable("dbo.T_MsgSend");
            DropTable("dbo.C_MsgType");
            DropTable("dbo.T_SysMsg");
            DropTable("dbo.T_MsgRec");
            DropTable("dbo.T_AnswerQuestion");
            DropTable("dbo.T_WeekRecordExtemsion");
            DropTable("dbo.T_WeekRecord");
            DropTable("dbo.T_StuScoreApply");
            DropTable("dbo.C_StuEvaSchoolGradeLevelItem");
            DropTable("dbo.C_StuEvaluateSchoolItem");
            DropTable("dbo.T_StuEvaluateSchool");
            DropTable("dbo.C_StuEvaluateEntItem");
            DropTable("dbo.C_StuEvaEntGradeLevelItem");
            DropTable("dbo.T_StuEvaluateEnt");
            DropTable("dbo.C_DisperseState");
            DropTable("dbo.T_StuBatchReg_Extent");
            DropTable("dbo.C_SelfEvaluationItem");
            DropTable("dbo.T_SelfEvaluation");
            DropTable("dbo.C_PracticeIdentificationItem");
            DropTable("dbo.T_PracticeIdentification");
            DropTable("dbo.C_PracAttendanceItem");
            DropTable("dbo.C_PracAttendanceGradeItem");
            DropTable("dbo.T_PracticeAttendance");
            DropTable("dbo.C_Position");
            DropTable("dbo.T_PracticePosition");
            DropTable("dbo.T_SchoolPubToUnit");
            DropTable("dbo.T_SchoolStudentReviewLink");
            DropTable("dbo.C_ProPosition");
            DropTable("dbo.T_Faculty");
            DropTable("dbo.T_SchoolReviewerTask");
            DropTable("dbo.T_SchoolReviewItem");
            DropTable("dbo.T_PracticeCaseExtension");
            DropTable("dbo.T_PracticeCase");
            DropTable("dbo.C_PracticeCaseItem");
            DropTable("dbo.T_PracBatch");
            DropTable("dbo.T_PlatformPubToUnit");
            DropTable("dbo.C_ApplyStatus");
            DropTable("dbo.T_EntBatchReg");
            DropTable("dbo.T_EntReviewItem");
            DropTable("dbo.T_EntReviewerTask");
            DropTable("dbo.T_EntStudentReviewLink");
            DropTable("dbo.C_EntEvaStuGradeLevelItem");
            DropTable("dbo.C_EntEvaluateStuItem");
            DropTable("dbo.T_EntEvaluateStu");
            DropTable("dbo.C_CareerStatus");
            DropTable("dbo.T_StuBatchReg");
            DropTable("dbo.T_Student");
            DropTable("dbo.T_Class");
            DropTable("dbo.C_Specialty");
            DropTable("dbo.T_School");
            DropTable("dbo.C_UnitStatus");
            DropTable("dbo.V3_StuEnterPriseApply");
            DropTable("dbo.V3_EnterpriseApply");
            DropTable("dbo.C_EntCategory");
            DropTable("dbo.C_EntRank");
            DropTable("dbo.C_County");
            DropTable("dbo.T_Enterprise");
            DropTable("dbo.T_Employee");
            DropTable("dbo.CPQ_T_QuestionOption");
            DropTable("dbo.CPQ_T_AttachQuestionToQuestionnaire");
            DropTable("dbo.CPQ_C_QuestionType");
            DropTable("dbo.CPQ_C_QuestionDomain");
            DropTable("dbo.CPQ_T_Question");
            DropTable("dbo.CPQ_T_AnswerSheet");
            DropTable("dbo.CPQ_C_Share");
            DropTable("dbo.CPQ_C_QuestionnaireStatus");
            DropTable("dbo.CPQ_C_QuestionnaireDomain");
            DropTable("dbo.CPQ_T_Questionnaire");
            DropTable("dbo.T_User");
            DropTable("dbo.BBS_Visitor");
            DropTable("dbo.BBS_Photo");
            DropTable("dbo.BBS_Note");
            DropTable("dbo.BBS_C_Friend");
            DropTable("dbo.BBS_Friend");
            DropTable("dbo.BBS_StepPraise");
            DropTable("dbo.BBS_C_Share");
            DropTable("dbo.BBS_Share");
            DropTable("dbo.BBS_ReplyReport");
            DropTable("dbo.BBS_ReplyParise");
            DropTable("dbo.BBS_ReplyPost");
            DropTable("dbo.BBS_Post_Report");
            DropTable("dbo.BBS_C_PostType");
            DropTable("dbo.BBS_C_PostState");
            DropTable("dbo.BBS_Post");
            DropTable("dbo.BBS_Theme");
            DropTable("dbo.BBS_Forum");
            DropTable("dbo.BBS_Columns");
            DropTable("dbo.BBS_ForumManager");
            DropTable("dbo.BBS_DiaryReply");
            DropTable("dbo.BBS_C_UserState");
            DropTable("dbo.BBS_C_UserGrade");
            DropTable("dbo.BBS_Users");
            DropTable("dbo.BBS_Diary_Report");
            DropTable("dbo.BBS_Diary");
            DropTable("dbo.BBS_C_DiaryState");
        }
    }
}
