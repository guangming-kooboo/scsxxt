using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.Lib
{
    public static class DataBaseHandler
    {
        public static bool Execute(ContentContext Data,string TargetSQL) {
            try
            {
                Data.Database.ExecuteSqlCommand(TargetSQL);
            }
            catch (SqlException ex)
            {
                //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
            }
            return true;
        }
    }
}