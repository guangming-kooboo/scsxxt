using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models

{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["PP2ndConnection"].ConnectionString;
        }
        public DbSet<T_News> T_News { get; set; }
        public DbSet<C_NewsColumn> T_NewsColumn { get; set; }
        public DbSet<C_NewsState> T_NewsState { get; set; }
        public DbSet<C_NewsType> T_NewsType { get; set; }

        

        #region 学生基本信息表
        public DbSet<C_Specialty> C_Specialty { get; set; }
        public DbSet<T_Class> T_Class { get; set; }
        public DbSet<T_Student> T_Student { get; set; }
        #endregion
    }
}