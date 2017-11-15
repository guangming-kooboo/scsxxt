using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Platform.Controllers
{
    public class DownLoadFilesController : BaseAccountController
    {
        //
        // GET: /Platform/DownLoadFiles/

        public ActionResult Index(int id=1)
        {
            var service = S<T_DownLoadFiles>();
            if (id == 1)
            {
                return View(service.All(DataContext, "平台所有公开文件"));
            }
            else if (id == 2)
            {
                DataContext.SetFiled("SchoolID", DataContext.UserUnit);
                return View(service.All(DataContext, "该学校所有公开文件"));
            }
            else if (id == 3)
            {
                DataContext.SetFiled("Ent_No", DataContext.UserUnit);
                return View(service.All(DataContext, "该企业所有公开文件"));
            }
            return Alert("请勿盗链");
        }
    
    }
}
