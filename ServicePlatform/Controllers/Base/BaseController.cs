using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core.Services;
using Qx.Report.Scsxxt.Interfaces;
using Qx.Report.Scsxxt.Models;
using Qx.Report.Scsxxt.Services;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Controllers.Base
{
    public class BaseController : Controller
    {
        protected const string PLATE_ALIPAY_ACCOUNT = "plate-alipay";
        private readonly IReportServices _reportServices;
        public BaseController()
        {
            this._reportServices = new ReportServices();
        }

        #region 菜单相关
        private Dictionary<string, string> _menuDictionary;
        private Dictionary<string, string> Menu
        {
            get
            {
                if (_menuDictionary == null)
                {
                    _menuDictionary = new Dictionary<string, string>();
                }
                return _menuDictionary;
            }
        }
        protected void AddUrl(string title, string action, string controller, string area)
        {
            var url = "/" + area + "/" + controller + "/" + action;
            Menu.Add(title, url);
        }
#endregion
        protected DataContext DataContext
        {
            get
            {
                var hsaValue = Session["DataContext"] as DataContext;
                if (hsaValue ==null)
                {
                    Session["DataContext"] = new DataContext();
                }
                return Session["DataContext"] as DataContext;
            }
            set
            {
                
                Session["DataContext"] = value;
            }
        }

        protected ActionResult Alert()
        {
          var content=ModelState.Values.SelectMany(a =>
               a.Errors.Select(b => b.ErrorMessage)).Aggregate((s, t) => s + "/" + t);
            return Alert(content,-1);
        }
        protected string ToPhysicPath(string FilePath)
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName + FilePath;//System.Web.HttpContext.Current.Server.MapPath(SavePath) + fileBase.FileName;
        }
        protected string ReadFile(string FilePath)
        {
            var filePath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName + FilePath;
            var temp = new List<char>();
            if (System.IO.File.Exists(filePath))
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    var br = new BinaryReader(fs, Encoding.UTF8);
                    //判断是否已经读到文件末尾
                    while (br.PeekChar() != -1)//使用while(temp=br.ReadString())!=null 会报异常System.IO.EndOfStreamExceptionl 
                    {
                        temp.Add(br.ReadChar());
                    }
                    br.Close();
                    fs.Close();
                }
            }
            return new string(temp.ToArray());
        }

        protected string GetConnStr()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        protected string CurrentUrl()
        {
            return System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        }
        protected string CurrentFullUrl()
        {
            return System.Web.HttpContext.Current.Request.RawUrl;
        }
        protected string CurrentAction()
        {
            return RouteData.Route.GetRouteData(this.HttpContext).Values["action"].ToString();
        }
        protected string ReturnUrl
        {
            get
            {
                return Session["ReturnUrl"] != null ? Session["ReturnUrl"].ToString() : "/";
            }
            set
            {
                Session["ReturnUrl"] = value;
            }
        }
        protected string BackToReport
        {
            get { return Session["ReturnUrl"] != null ? Session["ReturnUrl"].ToString() : "/"; }
        }
        protected void InitAppStudentLayout(string Title, string AddLink="#")
        {
            ViewData["action"] = CurrentAction();
            ViewData["AddLink"] = AddLink;
            ViewData["Url"] = CurrentUrl();
            ViewBag.Title = Title;
            ViewData["root"] = "/Content/AppStudentV3/";
            ViewData["Layout"] = "~/Views/Shared/_AppStudentLayout.cshtml";
            HttpContext.Items.Add("Layout", ViewData["Layout"]);
        }
        
        protected void InitAdminLayout(string Title, string AddLink)
        {
            ViewData["AddLink"] = AddLink; ViewData["Url"] = CurrentUrl(); ViewBag.Title = Title;
            ViewData["Layout"] = "~/Views/Shared/_PandaLayout.cshtml";
        }
        protected void InitForm(string Title, bool ShowSaveButton = true)
        {
            V("UserID", DataContext.UserID);
            V("Url", CurrentUrl()); V("Title", Title); V("ShowSaveButton", ShowSaveButton);
            HttpContext.Items.Add("Layout", "~/Views/Shared/_PandaFormLayout.cshtml");
            // HttpContext.Items["Layout", "~/Views/Shared/_PandaFormLayout.cshtml"; V("Layout", "~/Views/Shared/_PandaFormLayout.cshtml";
            HttpContext.Items["Layout"] = "~/Views/Shared/_SbFormLayout.cshtml"; V("Layout", "~/Views/Shared/_SbFormLayout.cshtml");
        }
        protected void InitLayout(string Title)
        {
            V("UserID", DataContext.UserID);
            V("Url", CurrentUrl()); V("Title", Title);
            // HttpContext.Items.Add("Layout","~/Views/Shared/_PandaLayout.cshtml");V("Layout", "~/Views/Shared/_PandaLayout.cshtml";
            HttpContext.Items.Add("Layout", "~/Views/Shared/_SbLayout.cshtml"); V("Layout", "~/Views/Shared/_SbLayout.cshtml");
        }
        protected void InitAdminLayout(string Title)
        {
            V("UserID", DataContext.UserID);
            V("Url", CurrentUrl()); V("Title", Title);
           // HttpContext.Items.Add("Layout","~/Views/Shared/_PandaLayout.cshtml");V("Layout", "~/Views/Shared/_PandaLayout.cshtml";
            HttpContext.Items.Add("Layout", "~/Views/Shared/_SbAdminLayout.cshtml"); V("Layout", "~/Views/Shared/_SbAdminLayout.cshtml");
        }
        //protected void InitForm(string Title,bool ShowSaveButton=true)
        //{
        //    V("UserID", DataContext.UserID);
        //    V("Url", CurrentUrl()); V("Title", Title); V("ShowSaveButton", ShowSaveButton);
        //    HttpContext.Items.Add("Layout", "~/Views/Shared/_PandaFormLayout.cshtml");
        //    // HttpContext.Items["Layout", "~/Views/Shared/_PandaFormLayout.cshtml"; V("Layout", "~/Views/Shared/_PandaFormLayout.cshtml";
        //    HttpContext.Items["Layout"]= "~/Views/Shared/_SbFormLayout.cshtml"; V("Layout", "~/Views/Shared/_SbFormLayout.cshtml");
        //}
        //protected void InitTable(List<string> header, List<List<string>> body,string config,string colunmToShow,string Title, string AddLink, int pageIndex, int perCount, string ExtraParam = "", bool showDeafultButton = true)
        //{
        //    V("header"] = header; V("body"] = body; V("config"] = config; V("colunmToShow"] = colunmToShow;
        //    V("UserID"] = DataContext.UserID;
        //    V("showDeafultButton"] = showDeafultButton;
        //    V("ExtraParam"] = ExtraParam;
        //    V("AddLink"] = AddLink; V("Url"] = CurrentUrl(); V("Title"] = Title;
        //    V("ReportID"] = Request.QueryString["ReportID"]; V("Params",Params) = Request.QueryString["Params",Params);
        //    // V("Layout"] = "~/Views/Shared/_PandaReportLayout.cshtml"; HttpContext.Items.Add("Layout", "~/Views/Shared/_PandaReportLayout.cshtml");
        //    V("Layout"] = "~/Views/Shared/_SbTableLayout.cshtml"; HttpContext.Items.Add("Layout", "~/Views/Shared/_SbTableLayout.cshtml");
        //}
        protected void InitReport(string Title, string AddLink, int pageIndex, int perCount, string ExtraParam = "", string dbConnStringKey = null)
        {
            ViewData["dbConnStringKey"] = dbConnStringKey;
            ViewData["ExtraParam"] = ExtraParam;
            ViewData["AddLink"] = AddLink; ViewData["Url"] = CurrentUrl(); ViewBag.Title = Title;
            ViewData["ReportID"] = Request.QueryString["ReportID"]; ViewData["Params"] = Request.QueryString["Params"];
            ViewData["pageIndex"] = Request.QueryString["pageIndex"] ?? "1"; ViewData["perCount"] = Request.QueryString["perCount"] ?? "10";
            ViewData["Layout"] = "~/Views/Shared/_SbReportLayout.cshtml";
        }
        #region 废弃
        [Obsolete(@"使用InitReport(List<List<string>> dataSource, string Title,string AddLink,string ExtraParam = "",bool showDeafultButton = true) 代替")]
        protected void InitReport(List<List<string>> dataSource, string Title, string AddLink,
           int pageIndex, int perCount, string ExtraParam = "", bool showDeafultButton = true)
        {
            //throw new Exception("已过时的方法\r\n"+ @"使用InitReport(List<List<string>> dataSource, string Title,string AddLink,string ExtraParam = "",bool showDeafultButton = true) 代替");
            var ReportID = Q("ReportID"); var Params = Q("Params");
            _InitReport(ReportID, Params, dataSource,
                Title, AddLink,
                ExtraParam, showDeafultButton,
                pageIndex.ToString(), perCount.ToString()
                );
        }
        [Obsolete(@"使用InitReport(string Title, string AddLink, string ExtraParam, bool showDeafultButton, string dbConnStringKey) 代替")]
        protected void InitReport(string Title, string AddLink, int pageIndex, int perCount, string ExtraParam, bool showDeafultButton, string dbConnStringKey)
        {
            //throw new Exception("已过时的方法\r\n"+ @"使用InitReport(string Title, string AddLink, string ExtraParam, bool showDeafultButton, string dbConnStringKey) 代替");
            if (!dbConnStringKey.HasValue())
            {
                throw new Exception("报表数据库配置错误！");
            }
            var ReportID = Q("ReportID"); var Params = V("Params");
            var dataSource = _reportServices.GetDbDataSource(ReportID, Params, dbConnStringKey);
            _InitReport(ReportID, Params, dataSource,
                Title, AddLink,
                ExtraParam, showDeafultButton,
                pageIndex.ToString(), perCount.ToString()
                );
        }
        #endregion

        protected void InitReport(List<CrossDbParam> paramList, string Title, string AddLink, string ExtraParam, bool showDeafultButton, string dbConnStringKey)
        {
            if (!dbConnStringKey.HasValue())
            {
                throw new Exception("报表数据库配置错误！");
            }
            var pageIndex = Q("pageIndex"); var perCount = Q("perCount");
            var ReportID = Q("ReportID"); var Params = V("Params");
            var dataSource = _reportServices.GetDbDataSource(ReportID, Params, dbConnStringKey);
            //垮裤
            dataSource = _reportServices.CrossDb(ReportID, Params, dataSource, paramList);
            _InitReport(ReportID, Params, dataSource,
                Title, AddLink,
                ExtraParam, showDeafultButton,
                pageIndex, perCount
                );
        }


        protected void InitReport(List<List<string>> dataSource,
                                 string Title, string AddLink, string ExtraParam = "", bool showDeafultButton = true)
        {
            var pageIndex = Q("pageIndex"); var perCount = Q("perCount");
            var ReportID = Q("ReportID"); var Params = V("Params");
            dataSource = _reportServices.ToHtml(ReportID, Params, dataSource);
            _InitReport(ReportID, Params, dataSource,
                Title, AddLink,
                ExtraParam, showDeafultButton,
                pageIndex, perCount
                );
        }
        protected void InitReport(string Title, string AddLink, string ExtraParam, bool showDeafultButton, string dbConnStringKey)
        {
            if (!dbConnStringKey.HasValue())
            {
                throw new Exception("报表数据库配置错误！");
            }
            var pageIndex = Q("pageIndex"); var perCount = Q("perCount");
            var ReportID = Q("ReportID"); var Params = V("Params");
            var dataSource = _reportServices.ToHtml(ReportID, Params, dbConnStringKey);
            _InitReport(ReportID, Params, dataSource,
                Title, AddLink,
                ExtraParam, showDeafultButton,
                pageIndex, perCount
                );
        }


        private void _InitReport(string ReportID, string Params, List<List<string>> dataSource,
            string Title, string AddLink,
            string ExtraParam, bool showDeafultButton,
            string pageIndex, string perCount)
        {
            V("ReportID", ReportID); V("Params", Params); V("dataSource", dataSource);
            V("Title", Title); V("AddLink", AddLink);
            V("ExtraParam", ExtraParam); V("showDeafultButton", showDeafultButton);
            V("pageIndex", pageIndex ?? "1"); V("perCount", perCount ?? "10");//分页参数
            V("Url", CurrentUrl()); V("UserID", DataContext.UserID);
            // V("Layout", "~/Views/Shared/_PandaReportLayout.cshtml"; HttpContext.Items.Add("Layout", "~/Views/Shared/_PandaReportLayout.cshtml");
            V("Layout", "~/Views/Shared/_SbReportLayout.cshtml"); HttpContext.Items.Add("Layout", "~/Views/Shared/_SbReportLayout.cshtml");
        }


        protected List<T> InitCutPage<T>(IEnumerable<T> data, int pageIndex, int perCount)
        {
            int maxIndex;
            var model = data.GetPage(pageIndex, perCount, out maxIndex);
            V("currentUrl", CurrentUrl());
            V("maxIndex", maxIndex); V("pageIndex", pageIndex); V("perCount", perCount);
            return model.ToList();
        }
       
        protected string Q(string key)
        {
            return Request.QueryString[key];
        }
        protected string F(string key)
        {
            return Request.Form[key];
        }
        protected object V(string key,object value)
        {
            ViewData[key] = value;
            return value;

        }
        protected string V(string key)
        {
            return ViewData[key] == null ? "" : ViewData[key] as string;
        }
     protected void InitBBS(string Title)
        {
            ViewData["Url"] = CurrentUrl(); ViewBag.Title = Title;
            ViewData["Layout"] = "~/Views/Shared/_BBS_HomeLayout.cshtml";
        }
        protected void AddParam(string Param)
        {
            var Params = V("Params");
            if (Params.Length > 2 && Param[Param.Length - 1] != ';')
            {
                Params += ';';
            }
            var newParams= Params + Param + ";";
            OverWriteParam(newParams) ;
        }
        protected void OverWriteParam(string Param)
        {
            V("Params", Param);
        }
           
         protected void InitMenu(Dictionary<string, string> Menus=null)
        {
            if (Menus == null)
            {
                Menus = _menuDictionary;
            }
            ViewBag.Menus = Menus; ViewBag.Title = "菜单列表";
            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            HttpContext.Items.Add("Layout", ViewData["Layout"]);
        }
        protected string GetProjectDir(string FileName)
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName+ FileName;
        }
        protected void WriteFile(string FilePath, string content, bool isBinaryWriter = true)
        {
            var filePath = GetProjectDir(FilePath);
            var fs = System.IO.File.Exists(filePath) ?
                new FileStream(filePath, FileMode.Append) :
                System.IO.File.Create(filePath);
            using (fs)
            {
                if (isBinaryWriter)
                {
                    var br = new BinaryWriter(fs, Encoding.UTF8);
                    br.Write(true);
                    br.Flush();
                    br.Close();
                }
                else
                {
                    var sw = new StreamWriter(fs);
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }
                fs.Close();
            }
        }


        #region 文件上传 public
        static string TempPath = null;
        [HttpPost]
        public JsonResult UploadHandle(string saveDirectory)
        {
            var fileBase = System.Web.HttpContext.Current.Request.Files[0];
            var SavePath = saveDirectory;// "/UserFiles/Test/";
            var TargetPath =   System.Web.HttpContext.Current.Server.MapPath(SavePath) + fileBase.FileName;
            var ContentRange = Request.Headers["Content-Range"];
            TempPath = TargetPath;
            FileUploadUtility.UploadBigFile(fileBase, TargetPath, ContentRange);
            var result = new { name = "提示:上传成功！", filePath = saveDirectory + fileBase.FileName };
            return Json(result);
        }

        public ActionResult RedoHandle()
        {
            //处理Redo按钮的请求
            int command = Convert.ToInt32(Request["DeleteCommand"]);
            if (command == 1 && TempPath != null)
            {
                if (System.IO.File.Exists(TempPath))//判断文件是否存在
                {
                    System.IO.File.Delete(TempPath);//执行IO文件删除,需引入命名空间System.IO;    
                } //删除物理文件
                TempPath = null;
                return Content("删除成功!");
            }
            else
            {
                return Content("删除异常!");
            }
        }

        public ActionResult ContinueHandle()
        {
            //用于处理续传功能
            //var FileByte = System.IO.File.ReadAllBytes(TempPath);
            //var resp = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ByteArrayContent(FileByte)
            //};
            //var fileStream = new FileStream(TempPath, FileMode.Open);
            string CurrentFileName = Request["file"];
            FileInfo info = new FileInfo(TempPath);
            long InfoSize = info.Length;
            var result = new { FileSize = InfoSize };
            return Json(result);
        }

        protected ActionResult FreshHandle()
        {
            TempPath = null;
            return View();
        }
        #endregion

        protected ActionResult Refresh()
        {
            return Content("<script>window.location.href=document.referrer</script>");
        }
        protected ActionResult Alert(string content)
        {
            return Content(PageHelper.Tip(content,-1));
        }
        protected ActionResult WxAlert(string content,string title)
        {
            return WxAlert(content, title,"");
        }
        protected ActionResult WxAlert(string content)
        {
            return WxAlert(content, "提示","-1");
        }
        protected ActionResult WxAlert(string content,string title,string returnUrl)
        {
            InitAppStudentLayout("提示");
            V("content", content);
            V("title", title);
            V("returnUrl", returnUrl);
            return View("_Tip");
        }
        protected ActionResult Alert(string content, int returnIndex)
        {
            return Content(PageHelper.Tip(content, returnIndex));
        }
        protected ActionResult Alert(string content, string returnUrl)
        {
            return Content(PageHelper.Tip(content, returnUrl));
        }
     
  
  protected IDML<T> S<T>()
        {
            return ServicesFactory.GetSevers<T>();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var item in Request.QueryString)
            {
                if (item == null) continue;
                var key = item.ToString() ?? "";
                ViewData[key] = Request.QueryString[key];
            }
            V("CurrentUrl", filterContext.RequestContext.HttpContext.Request.Url?.AbsolutePath);
            V("CurrentFullUrl", filterContext.RequestContext.HttpContext.Request.RawUrl);
            V("action", RouteData.Route.GetRouteData(this.HttpContext)?.Values["action"]);
         
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }

    }
}
