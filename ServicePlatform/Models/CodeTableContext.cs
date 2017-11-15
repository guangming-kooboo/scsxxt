using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;


namespace ServicePlatform.Models
{

      public class CodeTableContext : DbContext
    {
          public CodeTableContext()
        {
           
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//远程服务器
         }

        public DbSet<C_ApplyStatus> C_ApplyStatus { get; set; }
        public DbSet<C_City> C_City { get; set; }
        public DbSet<C_County> C_County { get; set; }
        public DbSet<C_EditStatus> C_EditStatus { get; set; }
        public DbSet<C_CareerStatus> C_CareerStatus { get; set; }
          
        public DbSet<C_EntCategory> C_EntCategory { get; set; }
        public DbSet<C_EntEvaluateStuItem> C_EntEvaluateStuItem { get; set; }
        public DbSet<C_EntEvaStuGradeLevelItem> C_EntEvaStuGradeLevelItem { get; set; }
        public DbSet<C_EntRank> C_EntRank { get; set; }
        public DbSet<C_Position> C_Position { get; set; }
        public DbSet<C_PracAttendanceItem> C_PracAttendanceItem { get; set; }
        public DbSet<C_PracticeCaseItem> C_PracticeCaseItem { get; set; }
        public DbSet<C_PracticeIdentificationItem> C_PracticeIdentificationItem { get; set; }
        public DbSet<C_ProPosition> C_ProPosition { get; set; }
        public DbSet<C_Province> C_Province { get; set; }
        public DbSet<C_ReadStatus> C_ReadStatus { get; set; }
        public DbSet<C_SelfEvaluationItem> C_SelfEvaluationItem { get; set; }
        public DbSet<C_Specialty> C_Specialty { get; set; }
        public DbSet<C_StuEvaluateEntItem> C_StuEvaluateEntItem { get; set; }
        public DbSet<C_StuEvaEntGradeLevelItem> C_StuEvaEntGradeLevelItem { get; set; }//学生评价企业条目等级
        public DbSet<C_StuEvaluateSchoolItem> C_StuEvaluateSchoolItem { get; set; }
        public DbSet<C_StuEvaSchoolGradeLevelItem> C_StuEvaSchoolGradeLevelItem { get; set; }//学生评价学校条目等级
        public DbSet<C_UnitStatus> C_UnitStatus { get; set; }
        public DbSet<C_UserType> C_UserType { get; set; }
        public DbSet<C_VolPosStatus> C_VolPosStatus { get; set; }
        public DbSet<C_DownLoadFileColumn> C_DownLoadFileColumn { get; set; }

          
        public DbSet<C_SchoolReviewItem> C_SchoolReviewItem { get; set; }
        public DbSet<C_EntReviewItem> C_EntReviewItem { get; set; }

        public DbSet<C_StuInfoType> C_StuInfoType { get; set; }
        public DbSet<C_SchoolEvaStuGradeLevelItem> C_SchoolEvaStuGradeLevelItem { get; set; }
        public DbSet<C_PracAttendanceGradeItem> C_PracAttendanceGradeItem { get; set; }

        public DbSet<C_ADColumn> C_ADColumn { get; set; }
        public DbSet<C_MsgSendType> C_MsgSendType { get; set; }
        public DbSet<C_MsgType> C_MsgType { get; set; } 
    }
    
}