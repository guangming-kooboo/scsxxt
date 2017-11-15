using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;

namespace Qx.Scsxxt.Core.Services.Permission
{
    public class RoleModuleServices : BaseServices, IDML<T_RoleModule>
    {
        public string Add(DataContext DataContext, T_RoleModule model)
        {
            throw new NotImplementedException();
        }

        public T_RoleModule Find(object id)
        {
            return Db.T_RoleModule.Find(id);
        }
        public List<T_RoleModule> All()
        {
            return Db.T_RoleModule.ToList();
        }

        public List<T_RoleModule> All(DataContext DataContext, string note = "")
        {
            if (note == "该角色具有的所有菜单")
            {
                var RoleID = Check(DataContext, "RoleID");
                return All(o => o.RoleID == RoleID);
            }

            throw new NotImplementedException();
        }

        public List<T_RoleModule> All(Func<T_RoleModule, bool> filter)
        {
            return Db.T_RoleModule.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_RoleModule Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_RoleModule> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_RoleModule model, string note)
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