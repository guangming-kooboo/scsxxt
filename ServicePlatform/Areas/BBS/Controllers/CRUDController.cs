using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Qx.Community.Entity;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Areas.BBS.ViewModels.CRUD;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.BBS.Controllers
{
    public class CRUDController : BaseAccountController
    {
        private IForumService _forumService;
        private IThemeService _themeService;
        private IPostService _postService;

        private IRepository<BBS_Forum> _forum;
        private IRepository<BBS_Columns> _columns;
        private IRepository<BBS_Theme> _theme;
        private IRepository<BBS_Post> _post;
        private IRepository<BBS_ForumManager> _forummanager;
        private IRepository<BBS_ReplyPost> _replypost;
        private IRepository<BBS_StepPraise> _praise;
        private IRepository<BBS_Share> _share;
        private IRepository<BBS_Diary> _diary;
        private IRepository<BBS_Note> _message;
        private IRepository<BBS_ReplyParise> _replypraise;
        private IRepository<BBS_Users> _users;
        private IRepository<BBS_Photo> _photo;
        
        public CRUDController(IForumService forumService, IThemeService themeService, IPostService postService,
            IRepository<BBS_Forum> forum, IRepository<BBS_Columns> columns, IRepository<BBS_Theme> theme,
            IRepository<BBS_Post> post, IRepository<BBS_ForumManager> forummanager, IRepository<BBS_ReplyPost> replypost,
            IRepository<BBS_StepPraise> praise, IRepository<BBS_Share> share, IRepository<BBS_Diary> diary,IRepository<BBS_Note> message,
            IRepository<BBS_ReplyParise> replypraise, IRepository<BBS_Users> users, IRepository<BBS_Photo> photo
            )
        {
            _forumService = forumService;
            _themeService = themeService;
            _postService = postService;
            _forum = forum;
            _columns = columns;
            _theme = theme;
            _post = post;
            _forummanager = forummanager;
            _replypost = replypost;
            _praise = praise;
            _share = share;
            _diary = diary;
            _message=message;
            _replypraise = replypraise;
            _users = users;
            _photo = photo;
        }
        //
        // GET: /BBS/CRUD/

        public ActionResult Index()
        {
            InitAdminLayout("测试", CurrentUrl());
            //var uid = "test";
            //var user = new T_User();
            //Db.T_User.Find(uid);//查找数据
            //Db.SaveAdd(user);//插入数据
            //Db.SaveModified(user);//修改数据
            //Db.SaveDelete(user);//删除数据
            return View();
        }
        public ActionResult AddForum()
        {
            InitAdminLayout("添加板块", CurrentUrl());
            return View(new AddForum_M());
        }
        [HttpPost]
        public ActionResult AddForum(BBS_Forum forum)
        {
            if (_forum.Find(forum.ForumID) == null)
            {
                _forum.Add(forum);
                return Redirect(BackToReport);
            }
            else
                return Alert("添加版块失败，请重新添加");
        }
        public ActionResult AddColumn(string id)
        {
            InitAdminLayout("添加子版块", CurrentUrl());
            return View(AddColumn_M.ToViewModel(id));
        }
        [HttpPost]
        public ActionResult AddColumn(BBS_Columns column)
        {
            if (_columns.Find(column.ColumnID) == null)
            {
                _columns.Add(column);
                return Redirect(BackToReport);
            }
            else
                return Alert("添加版块失败，请重新添加");
        }
        public ActionResult AddTheme(string id)
        {
            InitAdminLayout("添加主题", CurrentUrl());
            return View(AddTheme_M.ToViewModel(id));
        }
        [HttpPost]
        public ActionResult AddTheme(BBS_Theme theme)
        {
            if (_theme.Find(theme.ThemeID) == null)
            {
                _theme.Add(theme);
                return Redirect(BackToReport);
            }
            else
                return Alert("添加版块失败，请重新添加");
        }
        public ActionResult ForumEdit(string id)
        {
            InitAdminLayout("编辑板块", CurrentUrl());
            if(!id.HasValue())
                return Alert("非法操作！");
            return View(ForumEdit_M.ToViewModel( _forumService.GetForum(id)));
        }
        [HttpPost]
        public ActionResult ForumEdit(BBS_Forum forum)
        {
            _forum.Update(forum);//修改数据
            return Redirect(BackToReport);
        }
        public ActionResult ColumnEdit(string id)
        {
            InitAdminLayout("编辑子版块", CurrentUrl());
            return View(ColumnEdit_M.ToViewModel( _forumService.GetColumn(id)));
        }
        [HttpPost]
        public ActionResult ColumnEdit(BBS_Columns column)
        {
                _columns.Update(column);
                return Redirect(BackToReport);
        }
        public ActionResult ThemeEdit(string id)
        {
            InitAdminLayout("编辑主题", CurrentUrl());
            return View(ThemeEdit_M.ToViewModel( _themeService.GetThemeByThemeID(id)));
        }
        [HttpPost]
        public ActionResult ThemeEdit(BBS_Theme theme)
        {
                _theme.Update(theme);
                return Redirect(BackToReport);
        }
        public ActionResult ForumDelete(string id)
        {
            var forum = _forum.Find(id);
            InitAdminLayout("删除板块", CurrentUrl());
             _forum.Delete(forum);
            return Alert("删除成功");
        }
        public ActionResult ColumnDelete(string id)
        {
            var column = _columns.Find(id);
            if (column != null)
            {
                _columns.Delete(column);
                return Alert("删除成功");
            }
            else
                return Alert("删除失败");
        }
        public ActionResult ThemeDelete(string id)
        {
            var theme = _theme.Find(id);
            if (theme != null)
            {
                _theme.Delete(theme);
                return Alert("删除成功");
            }
            else
                return Alert("删除失败");
        }
        public ActionResult PostDelete(string id)
        {
            var post = _post.Find(id);
            if (post != null)
            {
                _post.Delete(post);
                return Alert("删除成功");
            }
            else
                return Alert("删除失败");
        }
        public ActionResult AddCream(string id)
        {
            InitAdminLayout("加精", CurrentUrl());
            var post = _post.Find(id);
            post.IsCream =1+"";
            _post.Update(post);
            return Redirect(BackToReport);
        }
        public ActionResult Ban(string id)
        {
            InitAdminLayout("禁贴", CurrentUrl());
            var post =_post.Find(id);
            post.StateID = -2+"";
            _post.Update(post);
            return Redirect(BackToReport);
        }
        public ActionResult SetTop(string id)
        {
            InitAdminLayout("置顶", CurrentUrl());
            var post = _post.Find(id);
            post.IsTop= 1+"";
            _post.Update(post);//修改数据
            return Redirect(BackToReport);
        }
        public ActionResult ChangeForumer(string id)
        {
            InitAdminLayout("更换版主", CurrentUrl());
            var forumer =  _forummanager.Find(id);
            return View(forumer);
        }
        [HttpPost]
        public ActionResult ChangeForumer(BBS_ForumManager forumer)
        {
            forumer.ForumManagerID = forumer.UserID + forumer.ForumID;
            _forummanager.Update(forumer);
            return Redirect(BackToReport);
        }
        public ActionResult DeleteForumer(string id)
        {

            var forumer =_forummanager.Find(id);
            if (forumer != null)
            {
                _forummanager.Delete(forumer);
                return Alert("撤销成功");
            }
            else
                return Alert("撤销失败");
        }
        public ActionResult AddForumer(string id)
        {
            InitAdminLayout("添加版主", CurrentUrl());
            return View(AddForumer_M.ToViewModel(_forumService.GetForum(id)));
        }
        [HttpPost]
        public ActionResult AddForumer(BBS_ForumManager forumer)
        {
            InitAdminLayout("添加版主", CurrentUrl());
            forumer.ForumManagerID = forumer.UserID + forumer.ForumID;
            if (_forummanager.Find(forumer.ForumManagerID)==null)
            {
                _forummanager.Add(forumer);
                return Redirect(BackToReport);
            }
            else
                return Alert("该版主已存在");
        }
        public ActionResult DeletePost(string id)
        {
            return Alert("删除成功");
        }
        public ActionResult SendPost()
        {
            return Alert("登陆成功，");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SendPost(string themename, string posttitle, string postcontent,string sendpost,string file)
        {

            var themeID = _themeService.GetThemeID(themename);
            var post = new BBS_Post()
            {
                ThemeID = themeID,
                PClickCount = 0,
                IsCream = "0",
                IsTop = "0",
                PostContent = postcontent,
                PostID = DateTime.Now.Random().ToString(),
                PostTime = DateTime.Now,
                PostTitle = posttitle,
                UserID = DataContext.UserID,
                 Files=file
            };
            if (sendpost == "发表帖子")
            {
                post.StateID = "1";
                if (_post.Find(post.PostID) == null)
                {
                    _post.Add(post);
                    return Alert("发表成功");
                }
                else
                    return Alert("发表失败,请重新发送");
            }
            else
            {
                post.StateID = "-1";
                if (_post.Find(post.PostID) == null)
                {
                    _post.Add(post);
                    return Alert("保存成功");
                }
                else
                    return Alert("保存失败,请重新保存");
            }
            
        }
        public ActionResult ReplyPost( string PostID)
        {
            return Redirect("/BBS/Home/PostPage?id=" + PostID);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReplyPost(string reply, string PostID,string file,string replyid)
        {
            var replypost = new BBS_ReplyPost()
            {
                Contents = reply,
                PostID = PostID,
                ReplyPostID = DateTime.Now.Random().ToString(),
                Time = DateTime.Now,
                UserID = DataContext.UserID,
                Files=file
            };
            replypost.ParentReplyID = replyid == null ? null : replyid;
            if (_replypost.Find(replypost.ReplyPostID) == null)
            {
                _replypost.Add(replypost);
                return Alert("回复成功");
            }
            else
                return Alert("回复失败，请重新回复");
        }

        public ActionResult PostPraise(string postid)
        {
            var praise = new BBS_StepPraise()
            {
                PostID = postid,
                UserID = DataContext.UserID,
                SetpPraiseID = postid + DataContext.UserID
            };
            if (_praise.Find(praise.SetpPraiseID) == null)
            {
                _praise.Add(praise);
                return Alert("点赞成功");
            }
            else
                return Alert("已赞，不能重复点赞");
        }
        public ActionResult ReplyPraise(string replyid)
        {
            var praise = new BBS_ReplyParise()
            {
                ReplyPostID = replyid,
                UserID = DataContext.UserID,
                PraiseID = replyid + DataContext.UserID,
            };
            if (_replypraise.Find(praise.PraiseID) == null)
            {
                _replypraise.Add(praise);
                return Alert("点赞成功");
            }
            else
                return Alert("已赞，不能重复点赞");
        }
        //帖子页面的分享按钮
        public ActionResult SharePost(string id)
        {
            var share = new BBS_Share()
            {
                PostID = id,
                UserID = DataContext.UserID,
                StatusID = "1",
                Time = DateTime.Now,
                ShareID = DataContext.UserID + id
            };
            if (_share.Find(share.ShareID) == null)
            {
                _share.Add(share);
                return Alert("分享成功");
            }
                
            else
                return Alert("该帖子已分享");
        }
        //写日志
        [HttpPost]
        public ActionResult WriteDiary(string DiaryTitle, string DiaryContent,string writediary,string diaryid,string flag)
        {
            if (flag == "write")
            {
                var diary = new BBS_Diary()
                {
                    Contents = DiaryContent,
                    DiaryTitle = DiaryTitle,
                    DiaryID = DateTime.Now.Random().ToString(),
                    Time = DateTime.Now,
                    UserID = DataContext.UserID
                };
                if (writediary == "发表日志")
                {
                    diary.StateID = "1";
                }
                else
                    diary.StateID = "0";
                if (_diary.Find(diary.DiaryID) == null)
                {
                    _diary.Add(diary);
                    return Redirect("/BBS/UserCenter/Diarys?id=" + DataContext.UserID);
                }
                else
                    return Alert("发送失败，请重新发送");
            }
            else
            {
                var diary=_diary.Find(diaryid);
                diary.DiaryTitle = DiaryTitle;
                diary.Contents = DiaryContent;
                diary.Time = DateTime.Now;
                  if (writediary == "发表日志")
                {
                    diary.StateID = "1";
                }
                else
                    diary.StateID = "0";
                _diary.Update(diary);
                 return Redirect("/BBS/UserCenter/Diarys?id=" + DataContext.UserID);
            
            }
        }
        //举报
        public ActionResult PostReport(string id)
        {
            return Redirect("/BBS/Home/PostPage?id=" + id);
        }
        public ActionResult UploadPhoto(string file, string id)
        {
            var photo = new BBS_Photo()
            {
                PhtotID = DateTime.Now.Random().ToString(),
                Img = file,
                Time = DateTime.Now,
                UserID = id
            };
            if (_photo.Find(photo.PhtotID) == null)
            {
                _photo.Add(photo);
                return Alert("上传成功");
            }
            else
                return Alert("上传失败,请重新上传");
        }
        //上传头像
        public ActionResult UploadHeadImg(string file,string id)
        {
            var user = _users.Find(id);
            user.HeadImg = file;
            _users.Update(user);
            return Alert("上传成功");
        }
        public ActionResult SendMessage(string id,string content)
        {
            var message = new BBS_Note()
            {
                NoteID = DateTime.Now.Random().ToString(),
                NoteTime = DateTime.Now,
                UserID = DataContext.UserID,
                ReceiverUserID = id,
                NoteContent = content
            };
            if(_message.Find(message.NoteID)==null)
            {
             _message.Add(message);
                return Alert("留言成功");
            }
            else
                return Alert("留言失败，请重新留言");
        }
        public ActionResult DeleteDiary(string id)
        {
            var diary=_diary.Find(id);
            if (diary != null)
            {
                _diary.Delete(diary);
                return Alert("删除成功");
            }
            else
            {
                return Alert("删除失败，请重新删除");
            }
        }
        public ActionResult PhotoDelete(string id)
        {
            var photo = _photo.Find(id);
            if (photo != null)
            {
                _photo.Delete(photo);
                return Alert("删除成功");
            }
            else
                return Alert("删除失败，请重新删除");
        }
        public ActionResult DeleteMessage(string id)
        {
            var message = _message.Find(id);
            if(message!=null)
            {
                _message.Delete(message);
                return Alert("删除成功");
            }
            else
            {
                return Alert("删除失败，请重新删除");
            }
        }
        public ActionResult UpdateShare(string id,string select)
        {
            var share = _share.Find(id);
            if (share != null)
            {
                switch (select)
                {
                    default:
                        return Alert("操作失败");
                    case "进行":
                        {
                            share.StatusID = "1";
                            _share.Update(share);
                            return Alert("分享进行中");
                        }
                    case "取消分享":
                        {
                            share.StatusID = "-1";
                            _share.Update(share);
                            return Alert("分享取消");
                        }
                    case "删除":
                        {
                            _share.Delete(share);
                            return Alert("分享删除");
                        }
                };
            }
            else
                return Alert("操作失败");
        }
    }
}
