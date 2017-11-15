using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;

namespace Qx.Scsxxt.Core.Services.Permission
{
    public class UserRoleServices : BaseServices, IDML<T_UserRole>
    {
        public string Add(DataContext DataContext, T_UserRole model)
        {
            throw new NotImplementedException();
        }

        public T_UserRole Find(object id)
        {
            return Db.T_UserRole.Find(id);
        }
        public List<T_UserRole> All()
        {
            return Db.T_UserRole.ToList();
        }

        public List<T_UserRole> All(DataContext DataContext, string note = "")
        {
           
            throw new NotImplementedException();
        }

        public List<T_UserRole> All(Func<T_UserRole, bool> filter)
        {
            return Db.T_UserRole.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_UserRole Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_UserRole> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_UserRole model, string note)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决.信息："+this.ToString());
        }


       
    }
}