using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Community.Services
{
    public class MessageService : BaseService, IMessageService
    {
        private const string DefaultHeadImg = "/Areas/BBS/Content/Image/默认头像.png";
        //获取留言
        public List<Messages> GetMessage(string id )
        {
          var  messages = Db.BBS_Note.Where(a=>a.ReceiverUserID==id).Select(b=>new Messages()
          {
              ID=b.NoteID,
             Content=b.NoteContent,
             Time=b.NoteTime,
             User=b.BBS_Users.T_User.NickName,
             UserHeadPictureUrl=b.BBS_Users.HeadImg,
             UserID=b.UserID
          }).ToList();
          messages.ForEach(a =>
          {
              a.UserHeadPictureUrl = a.UserHeadPictureUrl.HasValue() ? a.UserHeadPictureUrl : DefaultHeadImg;
          });
          return messages;
        }
        public List<Messages> GetMessage(string id, int count)
        {
            var messages = Db.BBS_Note.Where(a => a.ReceiverUserID == id).Select(b => new Messages()
            {
                ID=b.NoteID,
                Content = b.NoteContent,
                Time = b.NoteTime,
                User = b.BBS_Users.T_User.NickName,
                UserHeadPictureUrl = b.BBS_Users.HeadImg,
                UserID = b.UserID
            }).Take(count).ToList();
            messages.ForEach(a =>
            {
                a.UserHeadPictureUrl = a.UserHeadPictureUrl.HasValue() ? a.UserHeadPictureUrl : DefaultHeadImg;
            });
            return messages;
        }
    }
}