using Qx.Wbs.Configs;

namespace Qx.Wbs.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WbsDbContext : DbContext
    {
        public WbsDbContext()
            : base(Setting.ConnectionString)
        {
        }

        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<Wbs_Nodes> Wbs_Nodes { get; set; }
        public virtual DbSet<Wbs_UserNodes> Wbs_UserNodes { get; set; }
        public virtual DbSet<Wbs_Users> Wbs_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .HasOptional(e => e.Wbs_Users)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Wbs_Nodes>()
                .Property(e => e.NodeID)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Nodes>()
                .Property(e => e.NodeName)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Nodes>()
                .Property(e => e.FatherNodeID)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Nodes>()
                .Property(e => e.Decription)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Nodes>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Nodes>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Nodes>()
                .HasMany(e => e.Wbs_UserNodes)
                .WithOptional(e => e.Wbs_Nodes)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Wbs_UserNodes>()
                .Property(e => e.SerialID)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_UserNodes>()
                .Property(e => e.NodeID)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_UserNodes>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_UserNodes>()
                .Property(e => e.Materal)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Users>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Wbs_Users>()
                .HasMany(e => e.Wbs_UserNodes)
                .WithOptional(e => e.Wbs_Users)
                .WillCascadeOnDelete();
        }
    }
}
