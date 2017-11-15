using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;
namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
     public  class MessageBoard_M
    {

        public static MessageBoard_M ToViewModel(List<Messages> messages,int messagecount,string id)
        {
            return new MessageBoard_M() { Messages = messages, MessageCount = messagecount,userid=id };
        }
        public List<Messages> Messages;// 留言列表
        public int MessageCount;
        public string userid;
    }
}
