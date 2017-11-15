using System.Data.Entity;
using Qx.Wbs.Hqu.Configs;
using Qx.Wbs.Hqu.Entity;

namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
              : base(Setting.ConnectionString)
        {
        }
        public virtual DbSet<CheckRecord> CheckRecords { get; set; }
        public virtual DbSet<QuantifyTaskCompletion> QuantifyTaskCompletions { get; set; }
        public virtual DbSet<QuantifyTask> QuantifyTasks { get; set; }
        public virtual DbSet<QuotaTask> QuotaTasks { get; set; }
        public virtual DbSet<QuotaTaskStaffDistribution> QuotaTaskStaffDistributions { get; set; }
        public virtual DbSet<ScoringRule> ScoringRules { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<WbsTask> WbsTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.FinishID)
                .IsUnicode(false);

            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.Auditor)
                .IsUnicode(false);

            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.CheckOpinion)
                .IsUnicode(false);

            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.OwerID)
                .IsUnicode(false);

            modelBuilder.Entity<CheckRecord>()
                .Property(e => e.WbsTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.QuantifyTaskCompletionID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.QuantifyTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.ScoringRuleID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.StaffID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.StaffName)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.CompleteNote)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTaskCompletion>()
                .Property(e => e.Certificate)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTask>()
                .Property(e => e.QuantifyTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTask>()
                .Property(e => e.WbsTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTask>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<QuantifyTask>()
                .Property(e => e.TaskContent)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTask>()
                .Property(e => e.QuotaTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTask>()
                .Property(e => e.WbsTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTask>()
                .Property(e => e.FatherID)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTask>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTask>()
                .Property(e => e.TaskContent)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTaskStaffDistribution>()
                .Property(e => e.QuotaTaskStaffDistributionID)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTaskStaffDistribution>()
                .Property(e => e.QuotaTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTaskStaffDistribution>()
                .Property(e => e.StaffID)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTaskStaffDistribution>()
                .Property(e => e.StaffName)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTaskStaffDistribution>()
                .Property(e => e.DistributionExplain)
                .IsUnicode(false);

            modelBuilder.Entity<QuotaTaskStaffDistribution>()
                .Property(e => e.Certificate)
                .IsUnicode(false);

            modelBuilder.Entity<ScoringRule>()
                .Property(e => e.ScoringRuleID)
                .IsUnicode(false);

            modelBuilder.Entity<ScoringRule>()
                .Property(e => e.QuantifyTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<ScoringRule>()
                .Property(e => e.FormName)
                .IsUnicode(false);

            modelBuilder.Entity<ScoringRule>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffID)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffName)
                .IsUnicode(false);

            modelBuilder.Entity<WbsTask>()
                .Property(e => e.WbsTaskID)
                .IsUnicode(false);

            modelBuilder.Entity<WbsTask>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<WbsTask>()
                .Property(e => e.OwnerID)
                .IsUnicode(false);
        }
    }
}
