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
    public class RoleServices : BaseServices, IDML<T_Role>
    {
        public string Add(DataContext DataContext, T_Role model)
        {
            throw new NotImplementedException();
        }

        public T_Role Find(object id)
        {
            return Db.T_Role.Find(id);
        }
        public List<T_Role> All()
        {
            return Db.T_Role.ToList();
        }

        public List<T_Role> All(DataContext DataContext, string note = "")
        {
            //if (note == "该企业所有员工")
            //{
            //    var Ent_No =Check(DataContext, "Ent_No");
            //    return All(o => o.Ent_No == Ent_No);
            //}
           
            throw new NotImplementedException();
        }

        public List<T_Role> All(Func<T_Role, bool> filter)
        {
            return Db.T_Role.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_Role Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Role> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_Role model, string note)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.RoleID, Text = o.RoleName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决.信息："+this.ToString());
        }


       
    }
}