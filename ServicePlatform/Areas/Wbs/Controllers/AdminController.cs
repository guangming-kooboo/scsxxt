using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;
using Qx.Wbs.Interfaces;
using ServicePlatform.Areas.Wbs.ViewModels.Admin;

namespace ServicePlatform.Areas.Wbs.Controllers
{
    public class AdminController : BaseAccountController
    {
        //
        // GET: /Wbs/Admin/Index
          private INodeServices _nodeServices;

          public AdminController(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        public ActionResult Index()
        {
            InitMenu(new Dictionary<string, string>() { 
                {"所有任务列表","/Wbs/Admin/AllNodeList?ReportID=Wbs_所有任务列表&Params=;"},
                {"工作量列表","/Wbs/Admin/NodeList?ReportID=Wbs_工作量列表&Params=10385"},
                {"子任务列表","/Wbs/Admin/ChildNodeList?ReportID=Wbs_子任务列表&Params=;"},
                {"人员分配列表","/Wbs/Admin/UserNodeList?ReportID=Wbs_人员分配&Params=7"},
                {"完成情况报表","/Wbs/Admin/ReportList?ReportID=Wbs_完成情况报表&Params=10385"},
                {"完成情况详情","/Wbs/Admin/FinishDetails?ReportID=Wbs_完成情况详情&Params=10385"},
                {"用户列表","/Wbs/Admin/FinishDetails?ReportID=Wbs_完成情况详情&Params=10385"}
            });
            return View();
        }
        //Wbs/Admin/NodeList?ReportID=Wbs_工作量列表&Params=
        public ActionResult NodeList(string ReportID, string Params, int pageIndex=1, int perCount = 10)
        {
          
            InitReport("工作量列表", "/Wbs/CRUD/NodeList_Add",pageIndex,perCount);
            ViewData["Params"] = DataContext.UserID;//必须放在后面！
            return View(NodeList_M.ToViewModel(Params));
        }
       

        //Wbs/Admin/ChildNodeList?ReportID=Wbs_子任务列表&Params=;
        public ActionResult ChildNodeList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
          
            if (Params == "0")
                return RedirectToAction("NodeList", new { ReportID = "Wbs_工作量列表" });
            InitReport("子任务列表", "/Wbs/CRUD/ChildNodeList_Add?id=" + Params, pageIndex, perCount);
            return View(ChildNodeList_M.ToViewModel(_nodeServices.FindFathers(Params)));
        }
        //Wbs/Admin/AllNodeList?ReportID=Wbs_所有任务列表&Params=;
        public ActionResult AllNodeList(string ReportID = "Wbs_所有任务列表", string Params = "1", int pageIndex = 1, int perCount = 10)
        {
            InitReport("所有任务列表", "/Wbs/CRUD/AllNodeList_Add?id=" + Params, pageIndex, perCount);
            return View(AllNodeList_M.ToViewModel(_nodeServices.NodeFind(Params)));
        }
        //Wbs/Admin/UserNodeList?ReportID=Wbs_人员分配&Params=7
        public ActionResult UserNodeList(string ReportID = "Wbs_人员分配", string Params = "7", int pageIndex = 1, int perCount = 10)
        {
            
            InitReport("人员分配列表", "/Wbs/CRUD/UserNodeList_Add?id=" + Params, pageIndex, perCount);
            return View(UserNodeList_M.ToViewModel(_nodeServices.NodeFind(Params)));
        }
        //Wbs/Admin/ReportList?ReportID=Wbs_完成情况报表&Params=10385
        public ActionResult ReportList(string ReportID, string Params, string Onwer, int pageIndex = 1, int perCount = 10)
        {
            var node=_nodeServices.NodeFind(Params);
            InitReport("完成情况报表", "#", pageIndex, perCount,"NodeID="+node.NodeID);
            return View(ReportList_M.ToViewModel(node));
        }
        //Wbs/Admin/FinishDetails?ReportID=Wbs_完成情况详情&Params=10385
        public ActionResult FinishDetails(string ReportID, string Params,int pageIndex = 1, int perCount = 10)
        {
            
            InitReport("完成情况详情", "#", pageIndex, perCount);
            return View();
        }
        //Wbs/Admin/UserList?ReportID=1&Params=;
        public ActionResult UserList(string ReportID = "1", string Params = ";", int pageIndex = 1, int perCount = 10)
        {
            InitReport("用户列表", "#", pageIndex, perCount);
            return View();
        }

        //Wbs/Admin/test?ReportID=test_01&Params=;
        public ActionResult test(string ReportID = "test_01", string Params = "", int pageIndex = 1, int perCount = 10)
        {
            InitReport("测试", "/Wbs/CRUD/---", pageIndex, perCount);
            return View();
        }
        public ActionResult UserInformation(string id)
        {
            return View();
        }
        public ActionResult Materal(string id)
        {
            return View();
        }
      

   
    }
}
