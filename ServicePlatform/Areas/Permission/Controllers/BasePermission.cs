using ServicePlatform.Areas.Permission.Models;
using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Permission.Controllers
{
    public abstract class BasePermission : BaseAccountController
    {
        protected PermissionContext db = new PermissionContext();
        protected UserContext uc = new UserContext();


        public string getString(int data)
        {
            return data.ToString();
        }
        public static bool UserPswChg(string UserID, string oldPsw, string newPsw)
        {
            UserContext tempUc = new UserContext();
            var old = tempUc.T_User.Find(UserID);
            if (old != null && old.UserPwd == EncryptString.NoneEncrypt(oldPsw))
            {
                old.UserPwd = EncryptString.NoneEncrypt(newPsw);
                tempUc.Entry(old).State = EntityState.Modified;
                tempUc.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
