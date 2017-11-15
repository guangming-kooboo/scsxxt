using ServicePlatform.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ServicePlatform.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class BaseActionFilterAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 在执行操作方法前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            string CurrentURL = HttpContext.Current.Request.Url.ToString();
            #region 登录过滤
            if (HttpContext.Current.Session["Vars"] == null)//未登录 记住当前网页 并重定向到登录页面
            {
                HttpContext.Current.Session["return_Url"] = CurrentURL;
                filterContext.Result = new RedirectResult("/Platform/Home/Index");
            }
            else
            {

                #region Url合法性检查（防止出现双问号）

                int count = 0;
                int lastIndex = 0;
                for (int i = 0; i < CurrentURL.Length; i++)
                {
                    if (count == 2)
                    {
                        string newUrl = CurrentURL.Substring(0, lastIndex) + "&" + CurrentURL.Substring(lastIndex + 1, CurrentURL.Length - lastIndex - 1);
                        filterContext.Result = new RedirectResult(newUrl);
                        break;
                    }
                    if (CurrentURL[i] == '?')
                    {
                        count++;
                        lastIndex = i;
                    }
                }
                #endregion

                #region 记录最后一次点击的菜单Id
                var lastClickedMenu = HttpContext.Current.Request.QueryString["pageId"];
                if (lastClickedMenu != null && lastClickedMenu != "")
                {
                    (HttpContext.Current.Session["Vars"] as Vars).lastClickedMenu = lastClickedMenu;
                }

                #endregion
            }
            #endregion

            base.OnActionExecuting(filterContext);

        }
        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }

    }
}