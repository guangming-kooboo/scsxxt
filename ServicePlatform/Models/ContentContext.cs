using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;

namespace ServicePlatform.Models
{
    public class ContentContext : DbContext
    {

        public ContentContext()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//远程服务器
        }

        public DbSet<T_HomePageColumn> T_HomePageColumn { get; set; }
        public DbSet<T_HomePageContent> T_HomePageContent { get; set; }
        public DbSet<T_Content> T_Content { get; set; }
        public DbSet<C_ContentType> C_ContentType { get; set; }

        public DbSet<T_News> T_News { get; set; }
        public DbSet<C_ContentColumn> C_ContentColumn { get; set; }
        public DbSet<C_NewsState> C_NewsState { get; set; }
        public DbSet<C_NewsType> C_NewsType { get; set; }
        public DbSet<T_DownLoadFiles> T_DownLoadFiles { get; set; }
        public DbSet<C_DownLoadFileColumn> C_DownLoadFileColumn { get; set; }
        public DbSet<T_Stamps> T_Stamps { get; set; }
        public DbSet<C_StampType> C_StampType { get; set; }
        public DbSet<T_Signature> T_Signature { get; set; }
       


    }

}

    