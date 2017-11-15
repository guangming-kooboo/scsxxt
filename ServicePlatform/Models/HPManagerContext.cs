using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    public class HPManagerContext : DbContext
    {
        public HPManagerContext()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public DbSet<T_HomePageColumn> T_HomePageColumn { get; set; }
        public DbSet<T_HomePageContent> T_HomePageContent { get; set; }
        public DbSet<T_Content> T_Content { get; set; }
        public DbSet<C_ContentType> C_ContentType { get; set; }
        
    }
}