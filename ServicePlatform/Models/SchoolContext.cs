using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;
using ServicePlatform.Lib;

namespace ServicePlatform.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
            //string cs = EncryptString.Encrypt(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //string re_cs = EncryptString.Decrypt(cs);
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//远程服务器
            // this.Database.Connection.ConnectionString = "Data Source=.;Initial Catalog=F2FDB;Integrated Security=True";//本地服务器
        }
        public DbSet<T_School> School { get; set; }//学校表
        public DbSet<T_PracBatch> PracBatch { get; set; }//批次表
        public DbSet<T_StuBatchReg> StuBatchReg { get; set; }//学生注册批次表
        public DbSet<T_Student> Student { get; set; }//学生表

        public DbSet<T_PracticeCase> PracticeCase { get; set; }//实习案例记录

        public DbSet<T_WeekRecord> WeekRecord { get; set; }//周记记录

        public DbSet<T_DownLoadFiles> DownLoadFiles { get; set; }//文件下载

        public DbSet<T_StuEvaluateEnt> StuEvaluateEnt { get; set; }//学生评价企业

        public DbSet<T_StuEvaluateSchool> StuEvaluateSchool { get; set; }//学生评价学校
        public DbSet<T_Faculty> T_Faculty { get; set; }//学生评价学校


        public DbSet<T_StuInfoPub> StuInfoPub { get; set; }//学生评价学校

        public DbSet<T_Resume> Resume { get; set; }//学生评价学校

        public DbSet<T_StuFinalEntRecord> StuFinalEntRecord { get; set; }//学生终录单位登记

        public DbSet<T_PlatformPubToUnit> PlatformPubToUnit { get; set; }//学生评价学校

        public DbSet<T_SchoolPubToUnit> SchoolPubToUnit { get; set; }//学生评价学校

        public DbSet<T_JobWanted> JobWanted { get; set; }//求职发送简历表


        

        

        

        public DbSet<T_PracticeVolunteer> PracticeVolunteer { get; set; }//实习志愿表

       
        public DbSet<C_Specialty> C_Specialty { get; set; }//专业表
        public DbSet<T_Class> T_Class { get; set; }//班级表
        public DbSet<T_PhoneMsg> T_PhoneMsg { get; set; }//短息表
        public DbSet<T_SysMsg> T_SysMsg { get; set; }//短息表
        public DbSet<T_MsgSend> T_MsgSend { get; set; }//短息表
        public DbSet<T_MsgRec> T_MsgRec { get; set; }//短息表

        public DbSet<T_StuPic> T_StuPic { get; set; }//图片表

        public DbSet<T_StuResFile> T_StuResFile { get; set; }//附件表

        public DbSet<T_StuVideo> T_StuVideo { get; set; }//视频表




        public DbSet<T_SchoolStudentReviewLink> T_SchoolStudentReviewLink { get; set; }//
        public DbSet<T_SchoolReviewerTask> T_SchoolReviewerTask { get; set; }//
        public DbSet<C_SchoolReviewItem> C_SchoolReviewItem { get; set; }//


        public DbSet<T_Enterprise> T_Enterprise { get; set; }
        public DbSet<T_PracticePosition> T_PracticePosition { get; set; }
        public DbSet<C_Position> C_Position { get; set; }
        public DbSet<T_EntBatchReg> T_EntBatchReg { get; set; }



    }
   
       
        
}