using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using ServicePlatform.Areas.BBS.ViewModels.UserCenter;
using ServicePlatform.Controllers.Base;
using Qx.Tools.CommonExtendMethods;



namespace ServicePlatform.Areas.BBS.Controllers
{
    public class UserCenterController : BaseAccountController//针对会员BaseAccountController
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
        public UserCenterController(IForumService forumService,
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


        //    BBS/UserCenter/Index
        public ActionResult Index()
        {
            ViewBag.UserID = DataContext.UserID;
            return View();
        }
        public ActionResult _Head(string id)
        {       
            return PartialView(_Head_M.ToViewModel(_userService.GetPersonalData(id)));   
        }
        //个人空间
        public ActionResult PersonalSpace(string id)
        {
            if (!id.HasValue())
            {
                id = DataContext.UserID;
            }
            if (id == DataContext.UserID)
            {
                ViewBag.UserID = DataContext.UserID;
                var model = _userService.GetPersonalData(id);
                InitBBS(model.Name + "的个人空间");
                return View(PersonalSpace_M.ToViewModel(_messageService.GetMessage(id, 3),
                    model,
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
                    _shareServices.GetShareList(id, 4),
                    id));
            }
            else
                return RedirectToAction("PersonalSpace", "Home", new { id = id, area = "BBS" });
        }
        //日志
        public ActionResult Diarys(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的日志");
            var data=Diarys_M.ToViewModel(_dashboard.CalculateDiary(id),_dashboard.CalculateDiaryDraft(id), InitCutPage(_diaryService.GetDiaryList(id), pageIndex, perCount),id);
            return View(data);
        }
        //我发布的帖子
        public ActionResult MyPost(string id, int pageIndex = 1, int perCount = 6)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的发帖");
            return View(MyPost_M.ToViewModel(_forumService.GetForums(),
                _forumService.GetColumnsByForumID("1"),
                _themeService.GetTheme("1"),
                InitCutPage(_postService.GetPostsByUserID(id), pageIndex, perCount),
                model,
                _dashboard.CalculatePostCount(id),
                _dashboard.CalculatePostDraft(id),
                id
                ));
        }
        //留言板
        public ActionResult MessageBoard(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的留言板");
            return View(MessageBoard_M.ToViewModel(InitCutPage(_messageService.GetMessage(id), pageIndex, perCount), _dashboard.CalculateMessage(id), id));
        }
        //帖子草稿箱
        public ActionResult PostDrafts(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的帖子草稿箱");
            return View(PostDrafts_M.ToViewModel(model,
                 InitCutPage(_postService.GetPostDrafts(id), pageIndex, perCount),
                _forumService.GetForums(),
                _forumService.GetColumnsByForumID("1"),
                _themeService.GetTheme("1"),
                _dashboard.CalculatePostCount(id),
                _dashboard.CalculatePostDraft(id),id));
        }
        //日志草稿箱
        public ActionResult DiaryDrafts(string id, int pageIndex = 1, int perCount = 10)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的日志草稿箱");
            return View(DiaryDrafts_M.ToViewModel(InitCutPage(_diaryService.GetDiaryDrafts(id), pageIndex, perCount),               
                _dashboard.CalculateDiary(id),
                _dashboard.CalculateDiaryDraft(id), id));

        }
        //写日志
        public ActionResult WriteDiary(string id,string diaryid)
        {
            InitBBS("写日志");
            if (diaryid != null)
            {
                var diary = _diaryService.GetDiary(diaryid);
                return View(WriteDiary_M.ToViewModel(id, diary.DiaryTitle, diary.Content,diaryid));
            }
            else
                return View(WriteDiary_M.ToViewModel(id));
        }
        //查看日志
        public ActionResult ShowDiary(string id)
        {
            var diary = _diaryService.GetDiary(id);
            var model = _userService.GetPersonalData(diary.AuthorId);
            InitBBS(model.Name + "的查看日志");
            return View(ShowDiarys_M.ToViewModel(diary,_diaryService.GetReplyDiarys(id),diary.AuthorId));
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
        //个人资料修改
        public ActionResult UpdateInformation(string id)
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
            InitBBS(model.Name + "的分享");
            return View(Share_M.ToViewModel(InitCutPage(_shareServices.GetAllShare(id), pageIndex, perCount),_dashboard.CalculateSharing(id),_dashboard.CalculateCancelShare(id),id));
        }
        //相册
        public ActionResult PhotoAlbum(string id, int pageIndex = 1, int perCount = 8)
        {
            var model = _userService.GetPersonalData(id);
            InitBBS(model.Name + "的相册");
            return View(PhotoAlbum_M.ToViewModel(InitCutPage(_photoService.GetPicture(id), pageIndex, perCount),_userService.GetHeadPicture(id),id));
        }

        public ActionResult Friend()
        {
            return View();
        }
        public ActionResult ReportDiary()
        {
            return Alert("这里是举报日志");
        }
        public ActionResult LastDiary()
        {
            return Alert("这里是上一篇日志");
        }
        public ActionResult NextDiary()
        {
            return Alert("这里是下一篇日志");
        }


    }
}

