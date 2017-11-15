using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace Qx.Community.Services
{
    public class EnvelopeServices : BaseService, IEnvelopeService
    {
        //获取私人信息 返回的是消息的列表
        public List<Envelope> GetPersonalMessage()
        {
            return new List<Envelope>();
        }
        //获取公共消息，返回的也是公共消息的列表
        public List<Envelope> GetPublicMessage()
        {
            return new List<Envelope>();
        }
    }
}