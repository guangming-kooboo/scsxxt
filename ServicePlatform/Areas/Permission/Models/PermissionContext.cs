using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;
using ServicePlatform.Lib;
using ServicePlatform.Models;

namespace ServicePlatform.Areas.Permission.Models
{
    public class PermissionContext : DbContext
    {
        public PermissionContext()
        {

            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
           
          
        }
            
      
        public DbSet<T_UserRole> UserRole { get; set; }
        public DbSet<T_Role> Role { get; set; }
        public DbSet<T_RoleModule> RoleModule { get; set; }
         
        public DbSet<T_Module> Module { get; set; }
        public DbSet<T_FuncObject> FuncObject { get; set; }
        public DbSet<T_RoleFuncObjForbid> RoleFuncObjForbid { get; set; }
        public DbSet<T_ModuleBatchOpenSetting> ModuleBatchOpenSetting { get; set; }
        public DbSet<T_FuncBatchOpenSetting> FuncBatchOpenSetting { get; set; }


        //非权限系统数据表
        public DbSet<T_User> T_User { get; set; }

        public DbSet<T_Employee> T_Employee { get; set; }
        public DbSet<T_Enterprise> T_Enterprise { get; set; }

        public DbSet<T_Faculty> T_Faculty { get; set; }
        public DbSet<T_School> T_School { get; set; }


    }
}