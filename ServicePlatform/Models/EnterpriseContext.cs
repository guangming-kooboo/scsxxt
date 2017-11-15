using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;


namespace ServicePlatform.Models
{

      public class EnterpriseContext : DbContext
    {
        public EnterpriseContext()
        {
           
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//远程服务器
         }

          
            public DbSet<T_EntBatchReg> T_EntBatchReg { get; set; }
             public DbSet<T_Employee> T_Employee { get; set; }
             public DbSet<T_Enterprise> T_Enterprise { get; set; }

             public DbSet<T_PracticePosition> T_PracticePosition { get; set; }
             public DbSet<C_EntEvaluateStuItem> C_EntEvaluateStuItem { get; set; }
             public DbSet<C_Specialty> C_Specialty { get; set; }
             public DbSet<T_School> School { get; set; }//学校表
             public DbSet<T_PracBatch> PracBatch { get; set; }//批次表
             public DbSet<T_StuBatchReg> T_StuBatchReg { get; set; }//学生注册批次表
             public DbSet<T_Student> Student { get; set; }//学生表

             public DbSet<T_PracticeVolunteer> PracticeVolunteer { get; set; }//实习志愿表



             public DbSet<T_Class> T_Class { get; set; }//班级表

             public DbSet<T_User> T_User { get; set; }

             public DbSet<T_EntEvaluateStu> T_EntEvaluateStu { get; set; }
             public DbSet<T_EntReviewerTask> T_EntReviewerTask { get; set; }
             public DbSet<T_EntStudentReviewLink> T_EntStudentReviewLink { get; set; }
             public DbSet<C_EntReviewItem> C_EntReviewItem { get; set; }

             public DbSet<T_RecruitPosition> RecruitPosition { get; set; }

             public DbSet<T_Faculty> T_Faculty { get; set; }
             public DbSet<T_PracticeIdentification> T_PracticeIdentification { get; set; }
          
          
    }
    
}