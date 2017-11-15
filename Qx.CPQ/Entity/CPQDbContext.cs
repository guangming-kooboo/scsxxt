using Qx.CPQ.Configs;

namespace Qx.CPQ.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CPQDbContext : DbContext
    {
        public CPQDbContext()
            : base(Setting.ConnectionString)
        {
        }

        public virtual DbSet<CPQ_C_QuestionDomain> CPQ_C_QuestionDomain { get; set; }
        public virtual DbSet<CPQ_C_QuestionnaireDomain> CPQ_C_QuestionnaireDomain { get; set; }
        public virtual DbSet<CPQ_C_QuestionnaireStatus> CPQ_C_QuestionnaireStatus { get; set; }
        public virtual DbSet<CPQ_C_QuestionType> CPQ_C_QuestionType { get; set; }
        public virtual DbSet<CPQ_C_Share> CPQ_C_Share { get; set; }
        public virtual DbSet<CPQ_T_AnswerSheet> CPQ_T_AnswerSheet { get; set; }
        public virtual DbSet<CPQ_T_AttachQuestionToQuestionnaire> CPQ_T_AttachQuestionToQuestionnaire { get; set; }
        public virtual DbSet<CPQ_T_Question> CPQ_T_Question { get; set; }
        public virtual DbSet<CPQ_T_Questionnaire> CPQ_T_Questionnaire { get; set; }
        public virtual DbSet<CPQ_T_QuestionOption> CPQ_T_QuestionOption { get; set; }
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<CPQ_T_QuestionnaireStructure> CPQ_T_QuestionnaireStructure { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CPQ_C_QuestionDomain>()
                .Property(e => e.QuestionDomainName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionDomain>()
                .Property(e => e.QuestionDomainDescribe)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionDomain>()
                .HasMany(e => e.CPQ_T_Question)
                .WithRequired(e => e.CPQ_C_QuestionDomain)
                .HasForeignKey(e => e.QuestionDomain);

            modelBuilder.Entity<CPQ_C_QuestionnaireDomain>()
                .Property(e => e.QuestionnaireDomainName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionnaireDomain>()
                .Property(e => e.QuestionnaireDomainDescribe)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionnaireDomain>()
                .HasMany(e => e.CPQ_T_Questionnaire)
                .WithRequired(e => e.CPQ_C_QuestionnaireDomain)
                .HasForeignKey(e => e.QuestionnaireDomain);

            modelBuilder.Entity<CPQ_C_QuestionnaireStatus>()
                .Property(e => e.QuestionnaireStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionnaireStatus>()
                .Property(e => e.Decription)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionnaireStatus>()
                .HasMany(e => e.CPQ_T_Questionnaire)
                .WithRequired(e => e.CPQ_C_QuestionnaireStatus)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<CPQ_C_QuestionType>()
                .Property(e => e.QuestionTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionType>()
                .Property(e => e.QuestionDescribe)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_QuestionType>()
                .HasMany(e => e.CPQ_T_Question)
                .WithRequired(e => e.CPQ_C_QuestionType)
                .HasForeignKey(e => e.QuestionType);

            modelBuilder.Entity<CPQ_C_Share>()
                .Property(e => e.shareName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_Share>()
                .Property(e => e.Decription)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_C_Share>()
                .HasMany(e => e.CPQ_T_Questionnaire)
                .WithOptional(e => e.CPQ_C_Share)
                .HasForeignKey(e => e.share)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CPQ_T_AnswerSheet>()
                .Property(e => e.AnswerSheetID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AnswerSheet>()
                .Property(e => e.AnswererIP)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AnswerSheet>()
                .Property(e => e.QuestionnaireID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AnswerSheet>()
                .Property(e => e.QuestionID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AnswerSheet>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AttachQuestionToQuestionnaire>()
                .Property(e => e.AttachID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AttachQuestionToQuestionnaire>()
                .Property(e => e.QuestionnaireID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_AttachQuestionToQuestionnaire>()
                .Property(e => e.QuestionID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Question>()
                .Property(e => e.QuestionID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Question>()
                .Property(e => e.QuestionName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Questionnaire>()
                .Property(e => e.QuestionnaireID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Questionnaire>()
                .Property(e => e.QuestionnaireName)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Questionnaire>()
                .Property(e => e.Summarize)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Questionnaire>()
                .Property(e => e.OwnerID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_Questionnaire>()
                .Property(e => e.OwnerCompany)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_QuestionOption>()
                .Property(e => e.QuestionOptionID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_QuestionOption>()
                .Property(e => e.QuestionID)
                .IsUnicode(false);

            modelBuilder.Entity<CPQ_T_QuestionOption>()
                .Property(e => e.QuestionOptionIName)
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
                .HasMany(e => e.CPQ_T_Questionnaire)
                .WithRequired(e => e.T_User)
                .HasForeignKey(e => e.OwnerID);

            modelBuilder.Entity<CPQ_T_QuestionnaireStructure>()
                .Property(e => e.QuestionnaireID)
                .IsUnicode(false);
        }
    }
}
