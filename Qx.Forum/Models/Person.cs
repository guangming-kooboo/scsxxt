using System;
namespace Qx.Community.Models
{
    public class Person
    {
        public string ID;
        public string NickName;//备注名
        public string Name;
        public string Grade;
        public string Sex;
        public string HeadImg;//头像
        public string Signature;//个性签名
        public DateTime? RegisterDate;//注册日期
        public DateTime? LastLogin;//最后登录日期
    }
}