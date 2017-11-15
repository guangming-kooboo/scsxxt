using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;


namespace ServicePlatform.Models
{
    public class UserContext:DbContext
    {
        public UserContext()
        {

            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        }
    
     public DbSet<T_User> T_User { get; set; }
       }
}