using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServicePlatform.Lib;

namespace ServicePlatform.Config
{   
    /// <summary>
    /// 主要存放数据库的相关信息
    /// </summary>
    public static  class DbConfig
    {
        /// <summary>
        /// 主存放主库的信息
        /// 在读取的时候必须使用EncryptString.Decrypt进行解密，否则读取失败
        /// </summary>
        public static string  DB_MASTER
        {
            get{
               string ConnentString ="Data Source=(LocalDb)\v11.0;Initial Catalog=aspnet-ServicePlatform-20150618081736;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\aspnet-ServicePlatform-20150618081736.mdf";
               return EncryptString.Encrypt(ConnentString);
            }
        }
    }
}