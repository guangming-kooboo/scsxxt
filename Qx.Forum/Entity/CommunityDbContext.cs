using Qx.Community.Configs;

namespace Qx.Community.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CommunityDbContext : DbContext
    {
        public CommunityDbContext()
            : base(Setting.ConnectionString)
        {
        }


        public virtual DbSet<BBS_C_DiaryState> BBS_C_DiaryState { get; set; }
        public virtual DbSet<BBS_C_Friend> BBS_C_Friend { get; set; }
        public virtual DbSet<BBS_C_PostState> BBS_C_PostState { get; set; }
        public virtual DbSet<BBS_C_PostType> BBS_C_PostType { get; set; }
        public virtual DbSet<BBS_C_Share> BBS_C_Share { get; set; }
        public virtual DbSet<BBS_C_UserGrade> BBS_C_UserGrade { get; set; }
        public virtual DbSet<BBS_C_UserState> BBS_C_UserState { get; set; }
        public virtual DbSet<BBS_Columns> BBS_Columns { get; set; }
        public virtual DbSet<BBS_Diary> BBS_Diary { get; set; }
        public virtual DbSet<BBS_Diary_Report> BBS_Diary_Report { get; set; }
        public virtual DbSet<BBS_DiaryReply> BBS_DiaryReply { get; set; }
        public virtual DbSet<BBS_Forum> BBS_Forum { get; set; }
        public virtual DbSet<BBS_ForumManager> BBS_ForumManager { get; set; }
        public virtual DbSet<BBS_Friend> BBS_Friend { get; set; }
        public virtual DbSet<BBS_Note> BBS_Note { get; set; }
        public virtual DbSet<BBS_Photo> BBS_Photo { get; set; }
        public virtual DbSet<BBS_Post> BBS_Post { get; set; }
        public virtual DbSet<BBS_Post_Report> BBS_Post_Report { get; set; }
        public virtual DbSet<BBS_ReplyParise> BBS_ReplyParise { get; set; }
        public virtual DbSet<BBS_ReplyPost> BBS_ReplyPost { get; set; }
        public virtual DbSet<BBS_ReplyReport> BBS_ReplyReport { get; set; }
        public virtual DbSet<BBS_Share> BBS_Share { get; set; }
        public virtual DbSet<BBS_StepPraise> BBS_StepPraise { get; set; }
        public virtual DbSet<BBS_Theme> BBS_Theme { get; set; }
        public virtual DbSet<BBS_Users> BBS_Users { get; set; }
        public virtual DbSet<BBS_Visitor> BBS_Visitor { get; set; }
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<BBS_SearchKeyWord> BBS_SearchKeyWord { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BBS_C_DiaryState>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_DiaryState>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_Friend>()
                .Property(e => e.StatusID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_Friend>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_PostState>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_PostState>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_PostState>()
                .HasMany(e => e.BBS_Post)
                .WithRequired(e => e.BBS_C_PostState)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_C_PostType>()
                .Property(e => e.PostTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_PostType>()
                .Property(e => e.PostTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_PostType>()
                .HasMany(e => e.BBS_Post)
                .WithOptional(e => e.BBS_C_PostType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BBS_C_Share>()
                .Property(e => e.StatusID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_Share>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_UserGrade>()
                .Property(e => e.UserGradeID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_UserGrade>()
                .Property(e => e.UserGradeName)
                .IsFixedLength();

            modelBuilder.Entity<BBS_C_UserState>()
                .Property(e => e.UserStateID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_C_UserState>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .Property(e => e.ColumnID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .Property(e => e.ColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .Property(e => e.FatherColumnID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .Property(e => e.ForumID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .Property(e => e.ColumnImg)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .Property(e => e.ColumnExplain)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Columns>()
                .HasMany(e => e.BBS_ForumManager)
                .WithRequired(e => e.BBS_Columns)
                .HasForeignKey(e => e.ForumID);

            modelBuilder.Entity<BBS_Diary>()
                .Property(e => e.DiaryTitle)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary>()
                .Property(e => e.Contents)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary>()
                .Property(e => e.DiaryID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary>()
                .Property(e => e.ReportID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary>()
                .HasMany(e => e.BBS_Diary_Report1)
                .WithRequired(e => e.BBS_Diary1)
                .HasForeignKey(e => e.DiaryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Diary>()
                .HasMany(e => e.BBS_DiaryReply)
                .WithRequired(e => e.BBS_Diary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Diary_Report>()
                .Property(e => e.ReportID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary_Report>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary_Report>()
                .Property(e => e.DiaryID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Diary_Report>()
                .HasMany(e => e.BBS_Diary)
                .WithOptional(e => e.BBS_Diary_Report)
                .HasForeignKey(e => e.ReportID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BBS_DiaryReply>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_DiaryReply>()
                .Property(e => e.Contents)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_DiaryReply>()
                .Property(e => e.DiaryID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_DiaryReply>()
                .Property(e => e.DiaryReplyID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Forum>()
                .Property(e => e.ForumID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Forum>()
                .Property(e => e.ForumExplain)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Forum>()
                .Property(e => e.ForumName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ForumManager>()
                .Property(e => e.ForumManagerID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ForumManager>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ForumManager>()
                .Property(e => e.ForumID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Friend>()
                .Property(e => e.FriendID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Friend>()
                .Property(e => e.UserIDA)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Friend>()
                .Property(e => e.UserIDB)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Friend>()
                .Property(e => e.StatusID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Note>()
                .Property(e => e.NoteContent)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Note>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Note>()
                .Property(e => e.NoteID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Note>()
                .Property(e => e.ReceiverUserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Photo>()
                .Property(e => e.Img)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Photo>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Photo>()
                .Property(e => e.PhtotID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.PostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.PostTitle)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.ThemeID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.PostContent)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.StateID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.IsTop)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.IsCream)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.PostTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.BestReplyID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .Property(e => e.Files)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post>()
                .HasMany(e => e.BBS_Post_Report)
                .WithRequired(e => e.BBS_Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Post>()
                .HasMany(e => e.BBS_ReplyPosts)
                .WithRequired(e => e.BBS_Post1)
                .HasForeignKey(e => e.PostID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Post>()
                .HasMany(e => e.BBS_Share)
                .WithRequired(e => e.BBS_Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Post>()
                .HasMany(e => e.BBS_StepPraise)
                .WithRequired(e => e.BBS_Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Post_Report>()
                .Property(e => e.ReportID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post_Report>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Post_Report>()
                .Property(e => e.PostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyParise>()
                .Property(e => e.PraiseID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyParise>()
                .Property(e => e.ReplyPostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyParise>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .Property(e => e.ReplyPostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .Property(e => e.Contents)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .Property(e => e.PostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .Property(e => e.ParentReplyID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .Property(e => e.Files)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .HasMany(e => e.BBS_Post)
                .WithOptional(e => e.BBS_ReplyPost)
                .HasForeignKey(e => e.BestReplyID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BBS_ReplyPost>()
                .HasMany(e => e.BBS_ReplyParise)
                .WithRequired(e => e.BBS_ReplyPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_ReplyPost>()
                .HasMany(e => e.BBS_ReplyReport)
                .WithRequired(e => e.BBS_ReplyPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_ReplyReport>()
                .Property(e => e.ReportID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyReport>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_ReplyReport>()
                .Property(e => e.ReplyPostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Share>()
                .Property(e => e.ShareID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Share>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Share>()
                .Property(e => e.PostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Share>()
                .Property(e => e.StatusID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_StepPraise>()
                .Property(e => e.SetpPraiseID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_StepPraise>()
                .Property(e => e.PostID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_StepPraise>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Theme>()
                .Property(e => e.ThemeID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Theme>()
                .Property(e => e.ThemeName)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Theme>()
                .Property(e => e.ColumnID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Theme>()
                .Property(e => e.ThemeExplain)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Users>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Users>()
                .Property(e => e.UserGradeID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Users>()
                .Property(e => e.UserStateID)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Users>()
                .Property(e => e.UserPoint)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Users>()
                .Property(e => e.HeadImg)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Diary_Report)
                .WithRequired(e => e.BBS_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Friend)
                .WithRequired(e => e.BBS_Users)
                .HasForeignKey(e => e.UserIDA);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Friend1)
                .WithRequired(e => e.BBS_Users1)
                .HasForeignKey(e => e.UserIDB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Note)
                .WithOptional(e => e.BBS_Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Photo)
                .WithOptional(e => e.BBS_Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Post)
                .WithRequired(e => e.BBS_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Post_Report)
                .WithRequired(e => e.BBS_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Visitor)
                .WithRequired(e => e.BBS_Users)
                .HasForeignKey(e => e.UserIDA);

            modelBuilder.Entity<BBS_Users>()
                .HasMany(e => e.BBS_Visitor1)
                .WithRequired(e => e.BBS_Users1)
                .HasForeignKey(e => e.UserIDB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BBS_Visitor>()
                .Property(e => e.UserIDA)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Visitor>()
                .Property(e => e.UserIDB)
                .IsUnicode(false);

            modelBuilder.Entity<BBS_Visitor>()
                .Property(e => e.VisitorID)
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
                .HasOptional(e => e.BBS_Users)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BBS_SearchKeyWord>()
                .Property(e => e.KeyWord)
                .IsUnicode(false);
        }
    }
}
