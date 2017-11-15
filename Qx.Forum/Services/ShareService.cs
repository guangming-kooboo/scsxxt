using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using System.Web.Mvc;

namespace Qx.Community.Services
{
    public class ShareService : BaseService, IShareService
    {
       
        //获取分享列表  返回值为Share
        public List<ShareAndDiary> GetShareList(string userid)
        {
           var share = Db.BBS_Share.Where(a=>a.UserID==userid&&a.StatusID=="1").Select(b=>new ShareAndDiary(){
              PostID=b.PostID,
              ID=b.ShareID,
              Time=b.Time,
              Title=b.BBS_Post.PostTitle
           }).ToList();
           return share;
        }
        public List<ShareAndDiary> GetAllShare(string userid)
        {
            var share = Db.BBS_Share.Where(a => a.UserID == userid).Select(b => new ShareAndDiary()
            {
                PostID = b.PostID,
                ID = b.ShareID,
                Time = b.Time,
                State = b.StatusID,
                Title = b.BBS_Post.PostTitle
            }).ToList();
            share.ForEach(a => a.items = a.State == "1" ?
                new List<SelectListItem>(){
                   new SelectListItem(){Text="取消分享",Value="取消分享"},
                   new SelectListItem(){Text="进行",Value="进行",Selected=true},
                   new SelectListItem(){Text="删除",Value="删除"}} :
               new List<SelectListItem>(){
                  new SelectListItem(){Text="取消分享",Value="取消分享",Selected=true},
                  new SelectListItem(){Text="进行",Value="进行"},
                  new SelectListItem(){Text="删除",Value="删除"}});
            return share;
        }
        public List<ShareAndDiary> GetShareList(string userid,int count)
        {
            var share = Db.BBS_Share.Where(a => a.UserID == userid && a.StatusID == "1").Select(b => new ShareAndDiary()
            {
                PostID = b.PostID,
                ID = b.ShareID,
                Time = b.Time,
                Title = b.BBS_Post.PostTitle
            }).Take(count).ToList();
            return share;
        }
    }
}