using Qx.Permission.Configs;

namespace Qx.Permission.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PermissionContext : DbContext
    {
        public PermissionContext()
            : base(Setting.ConnectionString)
        {
        }

        public virtual DbSet<Button> Buttons { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleButtonForbid> RoleButtonForbids { get; set; }
        public virtual DbSet<RoleMenu> RoleMenus { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Button>()
                .Property(e => e.ButtonID)
                .IsUnicode(false);

            modelBuilder.Entity<Button>()
                .Property(e => e.MenuID)
                .IsUnicode(false);

            modelBuilder.Entity<Button>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Button>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<Button>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Button>()
                .HasMany(e => e.RoleButtonForbids)
                .WithRequired(e => e.Button)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.MenuID)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.FartherID)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<Menu>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleType)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.subSystem)
                .IsUnicode(false);

            modelBuilder.Entity<RoleButtonForbid>()
                .Property(e => e.RoleButtonForbidID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleButtonForbid>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleButtonForbid>()
                .Property(e => e.ButtonID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleButtonForbid>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<RoleMenu>()
                .Property(e => e.RoleMenuID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleMenu>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleMenu>()
                .Property(e => e.MenuID)
                .IsUnicode(false);

            modelBuilder.Entity<RoleMenu>()
                .Property(e => e.Note)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.NickName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserPwd)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.UserRoleID)
                .IsFixedLength();

            modelBuilder.Entity<UserRole>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.Note)
                .IsFixedLength();
        }
    }
}
