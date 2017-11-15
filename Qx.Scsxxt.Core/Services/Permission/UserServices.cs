using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Permission
{
    public class UserServices : BaseServices, IDML<T_User>
    {
        public string Add(DataContext DataContext, T_User model)
        {
            throw new NotImplementedException();
        }

        public T_User Find(object id)
        {
            return Db.T_User.Find(id);
        }
        public List<T_User> All()
        {
            return Db.T_User.ToList();
        }

        public List<T_User> All(DataContext DataContext, string note = "")
        {
            if (note == "登录")
            {
                var UserID = Check(DataContext, "UserID");
                var UserPwd = Check(DataContext, "UserPwd");
                return All(o => o.UserID == UserID&&
                (o.UserPwd == NoneEncrypt(UserPwd)| o.UserPwd == UserPwd)
                );
            }
            else if (note == "开发者登录")
            {
                var UserID = Check(DataContext, "UserID");
                return All(o => o.UserID == UserID);
            }
            else
            {
                throw new NotImplementedException(note);
            }
            //if (note == "超管登陆")
            //{
            //    var UserID = Check(DataContext, "UserID");
            //    return All(o => o.UserID == UserID );
            //}
           // throw new NotImplementedException();
        }

        public List<T_User> All(Func<T_User, bool> filter)
        {
            return Db.T_User.Where(filter).ToList();
        }

     

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_User Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_User> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_User model, string note)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.UserID, Text = o.NickName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决.信息："+this.ToString());
        }


       
    }
}