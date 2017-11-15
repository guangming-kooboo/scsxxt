using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using ServicePlatform.Areas.BBS.ViewModels;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Areas.BBS.ViewModels.Home;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Areas.BBS.Controllers
{
    public class HomeController : BaseController//针对游客
    {

        private IDashboardService _dashboard;
        private IPhotoService _photoService;
        private IFriendService _friendService;
        private IVisitorService _visitorService;
        private IDiaryService _diaryService;
        private IShareService _shareServices;
        private IUserService _userService;
        private IPostService _postService;
        private IMessageService _messageService;
        private IForumService _forumService;
        private IThemeService _themeService;
        public HomeController(IForumService forumService,
            IThemeService themeService, IDashboardService dashboard, 
            IPhotoService photoService, IFriendService friendService, 
            IVisitorService visitorService, IDiaryService diaryService, 
            IShareService shareServices, IUserService userService, 
            IMessageService messageService,IPostService postService)
        {
            _dashboard = dashboard;
            _photoService = photoService;
            _friendService = friendService;
            _visitorService = visitorService;
            _diaryService = diaryService;
            _shareServices = shareServices;
            _userService = userService;
            _messageService = messageService;
            _postService = postService;
            _forumService = forumService;
            _themeService = themeService;
        }

        public ActionResult _Head(string id)
        {
            var model = _userService.GetPersonalData(id);
            return PartialView(_Head_M.ToViewModel(model));
        }

        //调用首页上面那一部分公共的布局
        public ActionResult _Search(string keyword,string type)
        {
            if (!keyword.HasValue())
                keyword = "";
            return PartialView(_Search_M.ToViewModel(keyword,type));
           
        }
        public ActionResult _SearchLeft(string keyword,string type)
        {  
         return PartialView(_SearchLeft_M.ToViewModel(keyword,type));
           
        }
        
        //public ActionResult _Space()
        //{
        //    return PartialView(new _Space());
        //}
        //    BBS/Home/Main
    
        //论坛首页
        public ActionResult Main()
        {
            var userid = "";
            if (DataContext.IsLogin)
            {
                userid = DataContext.UserID;
            }
            InitBBS("论坛首页");
            return View(Main_M.ToViewModel( _forumService.GetZone(),userid));
        }
        //帖子列表

        public ActionResult PostList(string columnid, string themeid, string filter, int pageIndex = 1, int perCount = 10)
        {
            PostList_M model;
            List<Post> posts;
            switch (filter)
            { 
                default:
                    if (themeid.HasValue())
                    {
                        posts = _postService.GetPostsByThemeID(themeid);
                    }
                    else
                    {

                        posts = _postService.GetPostsByColumnID(columnid);
                       
                    }
                    break;
                case "best": if (themeid.HasValue())
                    {
                        posts=_postService.GetCreamPostsByThemeID(themeid);
                    }
                    else
                    {

                        posts = _postService.GetCreamPostsByColumnID(columnid);
                    }
                    break;
                case "time": if (themeid.HasValue())
                    {
                       posts = _postService.GetMostNewPostsByThemeID(themeid);
                    }
                    else
                    {
                        posts = _postService.GetMostNewPostsByColumnID(columnid);

                    }
                    break;
            }
            if (themeid.HasValue())
            {
                model = PostList_M.ToViewModel(_themeService.GetTheme(columnid), InitCutPage(posts, pageIndex, perCount),
                _forumService.GetColumn(columnid), themeid, filter, DataContext.IsLogin);
            }
            else
            {
                model = PostList_M.ToViewModel(_themeService.GetTheme(columnid), InitCutPage(posts, pageIndex, perCount),
                _forumService.GetColumn(columnid), filter, DataContext.IsLogin);
            }
            InitBBS("帖子列表");
            return View(model);
        }
        //帖子页
        public ActionResult Postpage(string id, string flag, int pageIndex = 1, int perCount = 10)
       {
           var post = _postService.GetPost(id);
           var user = _userService.GetPersonalData(post.AuthorID);
           InitBBS("帖子页面");
           if(flag==null)
               return View(PostPage_M.ToViewModel(post, InitCutPage(_postService.GetReply(id), pageIndex, perCount), _postService.GetBestReply(id)));
           else
               return View(PostPage_M.ToViewModel(_postService.GetPost(id), InitCutPage(_postService.GetReply(id, user.ID), pageIndex, perCount), _postService.GetBestReply(id)));
        }

        public ActionResult SearchPosts(string keyword, int pageIndex = 1, int perCount = 9)
       {
           if (keyword == null)
               keyword = "";
           InitBBS("搜帖子");
           return View(SearchPosts_M.ToViewModel(InitCutPage(_postService.GetPostsByKeyWord(keyword), pageIndex, perCount), keyword));
       }
       //BBS/Home/SearchForums
       public ActionResult SearchForums(string keyword, int pageIndex = 1, int perCount = 7)
       {
           if (keyword == null)
               keyword = "";
           InitBBS("搜帖子");
           return View(SearchColumns_M.ToViewModel(InitCutPage(_forumService.GetColumns(keyword), pageIndex, perCount), keyword));
       }
       public ActionResult SearchUsers(string keyword, int pageIndex = 1, int perCount = 6)
       {
           if (keyword == null)
               keyword = "";
           InitBBS("搜用户");
           return View(SearchUsers_M.ToViewModel(InitCutPage(_userService.GetUsers(keyword), pageIndex, perCount), keyword));
       }

       public ActionResult Login()
       {

           InitBBS("------");
           return View();
       }
       public ActionResult PersonalSpace(string id)
       {
           var model=_userService.GetPersonalData(id);
           InitBBS(model.Name+"的个人空间");//注意把这些----成对应标题
          if(DataContext.IsLogin)
          {
              if(DataContext.UserID==id)
                  return RedirectToAction("PersonalSpace", "Usercenter", new { id = id, area = "BBS" });
              else
                  return View(PersonalSpace_M.ToViewModel(model,
                  _dashboard.CalculateDiary(id),
                  _dashboard.CalculateFriend(id),
                  _dashboard.CalculatePhoto(id),
                  _dashboard.GetPostReplyCount(id),
                  _dashboard.CalculateShare(id),
                  _dashboard.CalculateVisitor(id),
                  _photoService.GetPicture(id,3),
                  _friendService.GetFriend(id),
                  _visitorService.GetVisitor(id),
                  _diaryService.GetDiaryList(id, 4),
                  _shareServices.GetShareList(id, 4), id));
          }
          else
          {   
              return View(PersonalSpace_M.ToViewModel(model,
                  _dashboard.CalculateDiary(id),
                  _dashboard.CalculateFriend(id),
                  _dashboard.CalculatePhoto(id),
                  _dashboard.GetPostReplyCount(id),
                  _dashboard.CalculateShare(id),
                  _dashboard.CalculateVisitor(id),
                  _photoService.GetPicture(id,3),
                  _friendService.GetFriend(id),
                  _visitorService.GetVisitor(id),
                  _diaryService.GetDiaryList(id,4),
                  _shareServices.GetShareList(id,4),id));
              //未登录情况
          }

       }

        //留言板
       public ActionResult MessageBoard(string id, int pageIndex = 1, int perCount = 10)
        {

            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的留言吧");
            return View(MessageBoard_M.ToViewModel(InitCutPage( _messageService.GetMessage(id), pageIndex, perCount), _dashboard.CalculateMessage(id), id));

        }
        public ActionResult Diarys(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = _userService.GetPersonalData(id);
            var data = Diarys_M.ToViewModel(InitCutPage(_diaryService.GetDiaryList(id), pageIndex, perCount), _dashboard.GetDiaryReplyCount(id), id);
            InitBBS(model.Name + "的日志");
            return View(data);
        }
        public ActionResult ShowDiary(string id)
        {
            var diary = _diaryService.GetDiary(id);
            var userid = diary.AuthorId;
            var model = _userService.GetPersonalData(userid);
            InitBBS(model.Name + "的查看日志");
            return View(ShowDiary_M.ToViewModel(diary, _diaryService.GetReplyDiarys(id), userid));
        }
        public ActionResult PhotoAlbum(string id, int pageIndex = 1, int perCount = 8)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的相册");
            return View(PhotoAlbum_M.ToViewModel(InitCutPage(_photoService.GetPicture(id), pageIndex, perCount), _userService.GetHeadPicture(id), id));
        }
        public ActionResult Mypost(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的发帖");
            return View(MyPost_M.ToViewModel(InitCutPage(_postService.GetPostsByUserID(id), pageIndex, perCount), model, _dashboard.CalculatePostCount(id), id));
        }

        public ActionResult Search(string found,string sort)
        {
            switch(sort)
            {
                default:
                    return  Alert("非法操作");
                case "搜帖子":
                    return Redirect("/BBS/Home/SearchPosts?keyword=" + found + "&type=SearchPosts");
                case "搜板块":
                    return Redirect("/BBS/Home/SearchForums?keyword=" + found + "&type=SearchForums");
                case "搜用户":
                    return Redirect("/BBS/Home/SearchUsers?keyword=" + found + "&type=SearchUsers");
            }
        }
        //个人资料
        public ActionResult PersonalInformation(string id)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的个人资料");
            return View(PersonalInformation_M.ToViewModel(model, 
                _dashboard.CalculateDiary(id),
                _dashboard.CalculateFriend(id),
                _dashboard.CalculatePhoto(id),
                _dashboard.GetPostReplyCount(id),
                _dashboard.CalculateShare(id),
                _dashboard.CalculateVisitor(id),id));
        }
        //分享
        public ActionResult Share(string id,int pageIndex = 1, int perCount = 10)
        {          
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的个人空间");
            return View(Share_M.ToViewModel(InitCutPage(_shareServices.GetShareList(id), pageIndex, perCount), _dashboard.CalculateSharing(id), id));
        }
    }
}
