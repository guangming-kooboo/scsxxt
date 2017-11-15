using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;

namespace ServicePlatform.Models
{
    public class PlateformContext : DbContext 
    {
        public PlateformContext() { 
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//远程服务器
        }
        public DbSet<T_Platformer> T_Platformer { get; set; }
    }
   
}