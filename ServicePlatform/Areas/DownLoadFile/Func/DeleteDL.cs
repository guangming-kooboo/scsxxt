using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.DownLoadFile.Func
{
    public class DeleteDL
    {
        public void Delete(string ContentId, ContentContext ne,string filePath)
        {
            if (System.IO.File.Exists(filePath))//判断文件是否存在
            {
                System.IO.File.Delete(filePath);//执行IO文件删除,需引入命名空间System.IO;    
            } //删除物理文件
            string ContentInsert = "delete from T_Content where ContentID='" + ContentId + "'";
            ne.Database.ExecuteSqlCommand(ContentInsert);//删除数据库中数据
        }
    }
}