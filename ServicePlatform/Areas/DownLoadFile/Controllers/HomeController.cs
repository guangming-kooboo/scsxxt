using System.Web.Mvc;
using Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.DownLoadFile.Controllers
{
    public class HomeController : BaseAccountController
    {
        //    /DownLoadFile/Home/Delete?id=&returnUrl=
        public ActionResult Delete(string id,string returnUrl)
        {
            S<Core.Services.Entity.T_DownLoadFiles>().Delete(DataContext, id);
           
            return Alert("删除成功", returnUrl);
        }
    }
}
