using System;
using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class MessageBoard_M
    {
        
        public static MessageBoard_M ToViewModel(List<Messages> messages, int MessageCount,string id)
        { 
           return new MessageBoard_M(){messages=messages,MessageCount=MessageCount,userid=id};
        }
        public List<Messages> messages;
        public int MessageCount;
        public string userid;
    }
}