using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Hqu.Interfaces;

using ServicePlatform.Areas.WbsHqu.ViewModels;
using ServicePlatform.Areas.WbsHqu.ViewModels.FAQ;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    //WbsHqu/Admin
    public class AdminController : BaseWbsHquController
    {
        private IAppService _app;
        private IWbsService _wbsService;
        public AdminController(IWbsService wbsService, IAppService app)
        {
            _wbsService = wbsService;
            _app = app;
        }

        // GET: WbsHqu/Admin
        public ActionResult Index()
        {
            InitMenu(new Dictionary<string, string>() {
                {"工作量列表", "/WbsHqu/WbsTask/Index?Params="+DataContext.UserID},
            });
            return View();
        }
        public ActionResult FaqTypeList(string reportId, string Params=";0",  int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("FaqTypeList", new { reportId = "FAQ.FAQ类型", Params = Params,  pageIndex = 1, perCount = 10});
            }
            InitReport("帮助类型", "/WbsHqu/Admin/TypeAdd?id="+Params.UnPackString(';')[1], "", true, "Qx.Scsxxt.Extentsion");
            return View(List_M.ToViewModel(Params.UnPackString(';')[1]));
        }
        public ActionResult TypeAdd(string id)
        {
            InitForm("添加帮助类型");
            return View(new FAQType_M());
        }

        [HttpPost]
        public ActionResult TypeAdd(FAQType_M viewModel, string id)
        {
            if (ModelState.IsValid)
            {
                viewModel._FAQTypeId = DateTime.Now.Random().ToString();
                viewModel.fatherId = id;
                _app.AddorEditFAQType(viewModel.ToModel());
                return RedirectToAction("FaqTypeList",new {Params=";"+viewModel.fatherId });
            }
            else
            {
                return View(viewModel);

            }
        }

        public ActionResult TypeEdit(string id)
        {
            InitForm("编辑帮助类型");
            var type = _app.FindFAQType(id);
            return View(FAQType_M.ToViewModel(type.TypeName, type.FAQTypeId,type.FatherID));
        }

        [HttpPost]
        public ActionResult TypeEdit(FAQType_M viewModel)
        {
            if (ModelState.IsValid)
            {
                _app.AddorEditFAQType(viewModel.ToModel());
                return RedirectToAction("FaqTypeList",new {Params=";"+viewModel.fatherId});
            }
            else
            {
                return View(viewModel);

            }
        }

        public ActionResult FaqList(string reportId, string Params=";;", int pageIndex = 1, int perCount = 10)
        {
            if (!reportId.HasValue())
            {
                return RedirectToAction("FaqList", new { reportId = "FAQ.FAQ列表", Params = Params, pageIndex = 1, perCount = 10 });
            }
            InitReport("文章列表", "/WbsHqu/Admin/AddFAQ", "", true, "Qx.Scsxxt.Extentsion");
            var FAQState = _app.FAQState();
            FAQState.Add(new SelectListItem()
            {
                Value =";",
                Text = "全部"
            });
            var stateId = Params.UnPackString(';')[1];
            return View(FAQList_M.ToViewModel(FAQState, stateId==""?";":stateId));
        }

        [HttpPost]
        public JsonResult jsonfathertype(string fatherid)
        {
            return Json(_app.FAQFatherType(fatherid));
        }
        [HttpPost]
        public JsonResult jsonchildtype(string fatherid)
        {
            return Json(_app.ChildTypeNotNULL(fatherid));
        }
        [HttpPost]
        public JsonResult childtype(string fatherid)
        {
            return Json(_app.ChildType(fatherid));
        }

        public ActionResult AddFAQ(string typeid,string fatherid)
        {
            InitForm("添加文章");
            var child = _app.ChildType(fatherid);
            return View(AddFAQ_M.ToViewModel(_app.FAQType(), child, typeid));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFAQ(AddFAQ_M model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model._ChildFAQTypeId))
                {
                    FAQ faq = new FAQ();
                    model._FAQID = DateTime.Now.Random().ToString();
                    model.UserID = DataContext.UserID;                 
                    model._CreatTime = DateTime.Now;
                    model._StateID = "001"; //草稿\
                    model._FAQTypeId=model._ChildFAQTypeId; 
                    faq = model.ToModel();
                    if (_app.AddorEditDraftFAQ(faq))
                    {
                        return RedirectToAction("FAQList");
                    }
                    else
                    {
                        return Alert("添加失败", -1);
                    }
                }
                else
                {
                    return Alert("请选择子级分类", -1);
                    //FAQ faq = new FAQ();
                    //Random random = new Random();
                    //model._FAQID = random.Next().ToString();
                    //model.UserID = DataContext.UserID;
                    //model._CreatTime = DateTime.Now;
                    //model._StateID = "001"; //草稿
                    //faq = model.ToModel();
                    //if (_app.AddorEditDraftFAQ(faq))
                    //{
                    //    return RedirectToAction("FAQList");
                    //}
                    //else
                    //{
                    //    return Alert("添加失败", -1);
                    //}
                }           
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult SavePic(string faqid,string filepath,string faqtypeid)
        {
           //Random ansRandom=new Random();
           // var faqid = ansRandom.Next().ToString();
            if (_app.SavePic(faqid, filepath,DataContext.UserID, faqtypeid))
            {
                return Alert("添加成功");
            }
            else
            {
                return Alert("添加失败");
            }
        }

        public ActionResult EditFAQ(string id, string fatherid)
        {
            InitForm("编辑文章");
        
            var typeid=_app.DetailFAQ(id).FAQTypeId;//当前FAQ的类型id             
            var fathertypeid = _app.FindFatherID(typeid);//查找他是否存在父级类型           
            if (fathertypeid != "0")//有父级类型
            {
                var child = _app.FAQChildType(fathertypeid);
                return View(EditFaq_M.ToViewModel(_app.DetailFAQ(id), fathertypeid, typeid, _app.FAQType(), child));
            }
            else//自己就是父级的类型，没有孩子类型
            {
                List<SelectListItem> child = new List<SelectListItem>()
                {
                    new SelectListItem() {Text = "", Value = ""}
                };
                return View(EditFaq_M.ToViewModel(_app.DetailFAQ(id),"0",typeid, _app.FAQType(), child));
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFAQ(EditFaq_M model, string id,string oldpic)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model._ChildFAQTypeId))
                {
                    //if (!string.IsNullOrEmpty(model.Pic))
                    //{
                        FAQ faq = new FAQ();
                        model.UserID = DataContext.UserID;
                        model._CreatTime = DateTime.Now;
                        model._FAQTypeId = model._ChildFAQTypeId;
                        faq = model.ToModel();
                        if (_app.AddorEditFormFAQ(faq))
                        {
                            return RedirectToAction("FAQList");
                        }
                        else
                        {
                            return Alert("添加失败", -1);
                        }
                    //}
                    //else
                    //{
                    //    return Alert("请选择子级分类", -1);
                        //FAQ faq = new FAQ();
                        //model.UserID = DataContext.UserID;
                        //model._CreatTime = DateTime.Now;
                        //model.Pic = oldpic;
                        //model._FAQTypeId = model._ChildFAQTypeId;
                        //faq = model.ToModel();
                        //if (_app.AddorEditFormFAQ(faq))
                        //{
                        //    return RedirectToAction("FAQList");
                        //}
                        //else
                        //{
                        //    return Alert("添加失败", -1);
                        //}
                                 
                }
                else
                {
                    return Alert("请选择子级分类", -1);
                    //if (!string.IsNullOrEmpty(model.Pic))
                    //{
                    //    FAQ faq = new FAQ();
                    //    model.UserID = DataContext.UserID;
                    //    model._CreatTime = DateTime.Now;
                    //    faq = model.ToModel();
                    //    if (_app.AddorEditFormFAQ(faq))
                    //    {
                    //        return RedirectToAction("FAQList");
                    //    }
                    //    else
                    //    {
                    //        return Alert("添加失败", -1);
                    //    }
                    //}
                    //else
                    //{
                    //    FAQ faq = new FAQ();
                    //    model.UserID = DataContext.UserID;
                    //    model._CreatTime = DateTime.Now;
                    //    model.Pic = oldpic;
                    //    faq = model.ToModel();
                    //    if (_app.AddorEditFormFAQ(faq))
                    //    {
                    //        return RedirectToAction("FAQList");
                    //    }
                    //    else
                    //    {
                    //        return Alert("添加失败", -1);
                    //    }
                    //}                   
                }
            }
            else
            {

                return View(model);
            }
        }

        public ActionResult Publish(string id)
        {
            if (id.HasValue())
            {
                _app.TurnForm(id);
                return Alert("发布成功！", "/WbsHqu/Admin/FaqList");
            }
            else
            {
                return Alert("发布失败！", "/WbsHqu/Admin/FaqList");
            }

        }

        public ActionResult DeleteFAQ(string id)
        {
            if (id.HasValue())
            {
                _app.DeleteFAQ(id);
                return Alert("删除成功！", "/WbsHqu/Admin/FaqList");
            }
            else
            {
                return Alert("删除失败！", "/WbsHqu/Admin/FaqList");
            }
        }
        public ActionResult TypeDelete(string id)
        {
            if (id.HasValue())
            {
                var type = _app.FindFAQType(id);
                _app.DeleteFAQType(id);
                //return Alert("删除成功！");
                return RedirectToAction("FaqTypeList", new { Params = ";" + type.FatherID });
            }
            else
            {
                return Alert("删除失败！",-1);
            }
        }
        #region 前端展示
        //WbsHqu/Admin/ShowFAQList
        public ActionResult ShowFAQList(string typeid)
        {
            var type = _app.FAQType();
            return View(FAQList_M.ToViewModel(type, _app.FAQList(typeid)));
        }
        [HttpPost]
        public JsonResult FaqList(string typeid)
        {
            return Json(_app.FAQList(typeid));
        }
        [HttpPost]
        public JsonResult FaqDetail(string faqid)
        {
            return Json(_app.DetailFAQ(faqid)); 
        }
        public ActionResult ShowFAQDetail(string faqid)
        {
            var faq = _app.DetailFAQ(faqid);
            var type = _app.FAQType();
            return View(FAQDetail_M.ToViewModel(type, faq));
        }

        #endregion

    }
}