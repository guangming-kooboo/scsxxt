using Qx.Wbs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Qx.Community.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Interfaces;
using ServicePlatform.Areas.Wbs.ViewModels.CRUD;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Wbs.Controllers
{
    public class CRUDController : BaseAccountController
    {


        private INodeServices _nodeServices;

        public CRUDController(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        //
        // GET: /BBS/CRUD/Test
        public ActionResult Test(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = new Test() { 
            Name="测试名字",
            TestBool=true
            };
            InitReport("工作量增加", CurrentUrl(), pageIndex, perCount);
     
            return View(model);
        }
        public ActionResult Index()
        {
            //var uid = "test";
            //var user=new T_User();
            //Db.T_User.Find(uid);//查找数据
            //Db.SaveAdd(user);//插入数据
            //Db.SaveModified(user);//修改数据
            //Db.SaveDelete(user);//删除数据
            return View();
        }
        //工作量  添加
        public ActionResult NodeList_Add( int pageIndex = 1, int perCount = 10)
        {
            InitReport("工作量增加", CurrentUrl(), pageIndex, perCount);
            return View(new NodeList_Add_M());
        }
        [HttpPost]
        public ActionResult NodeList_Add(Wbs_Nodes node)
        {
            if (_nodeServices.AddRoot(DataContext, node).HasValue())
            {
                return Redirect(BackToReport);
            }
            else
                return Alert("添加失败，请重试！");
        }
        //工作量列表 编辑
        public ActionResult NodeList_Edit(string id, int pageIndex = 1, int perCount = 10)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            InitReport("工作量列表编辑", CurrentUrl(), pageIndex, perCount);
            return View(NodeList_Edit_M.ToViewModel(_nodeServices.NodeFind(id)));
        }
        [HttpPost]
        public ActionResult NodeList_Edit(Wbs_Nodes node)
        {

            if (_nodeServices.UpdateRoot(node))
            {
                return Redirect(BackToReport);
            }
            else
                return Alert("保存失败，请重试！");
        }
        public ActionResult NodeList_Delete(string id)
        {
            if (_nodeServices.Delete(id))
            {
                return Alert("删除成功！");
            }
            else
                return Alert("删除失败！");
        }


        //子任务列表 删除
        public ActionResult ChildNodeList_Delete(string id)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            return NodeList_Delete(id);
        }
        //子任务列表查看
        public ActionResult ChildNodeList_Check(string id, int pageIndex = 1, int perCount = 10)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            InitReport("子任务列表编辑", CurrentUrl(), pageIndex, perCount);  
            return View(ChildNodeList_Check_M.ToViewModel(_nodeServices.NodeFind(id)));
        }
        //子任务列表  添加
        public ActionResult ChildNodeList_Add(string id, int pageIndex = 1, int perCount = 10)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            InitReport("子任务列表增加", CurrentUrl(), pageIndex, perCount);
            return View(ChildNodeList_Add_M.ToViewModel(_nodeServices.NodeFind(id)));
        }
        [HttpPost]
        public ActionResult ChildNodeList_Add(Wbs_Nodes childnode)
        {
            if (_nodeServices.AddChild(DataContext, childnode).HasValue())
            {
                return Redirect(BackToReport);
            }
            else
                return Alert("添加失败，请重试！");
        }

   
        //子任务列表 编辑
        public ActionResult ChildNodeList_Edit(string id, int pageIndex = 1, int perCount = 10)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            InitReport("子任务列表编辑", CurrentUrl(), pageIndex, perCount);
            return View(ChildNodeList_Edit_M.ToViewModel(_nodeServices.NodeFind(id)));
        }
        [HttpPost]
        public ActionResult ChildNodeList_Edit(Wbs_Nodes node)
        {
            if (_nodeServices.UpdateChild(node))
            {
                return Redirect(BackToReport);
            }
            else
                return Alert("保存失败，请重试！");
           
        }
        //人员分配列表  增加

        public ActionResult UserNodeList_Add(string id)
        {
            if (!id.HasValue())
                return Alert("非法操作！");

            InitAdminLayout("人员分配列表增加", CurrentUrl());
            return View(UserNodeList_Add_M.ToViewModel(_nodeServices.NodeFind(id)));
        }
        [HttpPost]
        public ActionResult UserNodeList_Add(Wbs_UserNodes usernode)
        {
            if (_nodeServices.ArrangeNode(usernode).HasValue())
            {
                return Redirect(BackToReport);
            }
            else
                return Alert("添加失败，请重试！");
        }
       
        //人员分配列表编辑
        public ActionResult UserNodeList_Edit(string id, int pageIndex = 1, int perCount = 10)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            InitReport("编辑完成情况", CurrentUrl(), pageIndex, perCount);

            return View(UserNodeList_Edit_M.ToViewModel(_nodeServices.Detail(id)));
        }
        [HttpPost]
        public ActionResult UserNodeList_Edit(UserNodeList_Edit_M arange)
        {
            var usernode = new Wbs_UserNodes()
            {
                SerialID=arange.SerialID,
                RealWeight = arange.RealWeight,
                FinishTime = arange.FinshTime,
                Materal = arange.Materal

            };
            if (_nodeServices.ArrangeFinish(usernode))
            {
                return Redirect(BackToReport);
            }
            else
                return Alert("保存失败，请重试！");
           
        }
        public ActionResult UserNodeList_Check(string id, int pageIndex = 1, int perCount = 10)
        {
            if (!id.HasValue())
                return Alert("非法操作！");
            InitReport("编辑完成情况", CurrentUrl(), pageIndex, perCount);

            return View(UserNodeList_Edit_M.ToViewModel(_nodeServices.Detail(id)));
        }
        //人员分配列表 删除

        public ActionResult UserNodeList_Delete(string id)
        {
            if (_nodeServices.ArrangeDelete(id))
            {
                return Alert("删除成功！");
            }
            else
                return Alert("删除失败！");
        }
    
    }
}
