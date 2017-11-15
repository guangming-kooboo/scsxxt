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
    public class ModuleServices : BaseServices, IDML<T_Module>
    {
        public string Add(DataContext DataContext, T_Module model)
        {
            throw new NotImplementedException();
        }

        public T_Module Find(object id)
        {
            return Db.T_Module.Find(id);
        }
        public List<T_Module> All()
        {
            return Db.T_Module.ToList();
        }

        public List<T_Module> All(DataContext DataContext, string note = "")
        {
            //if (note == "该企业所有员工")
            //{
            //    var Ent_No =Check(DataContext, "Ent_No");
            //    return All(o => o.Ent_No == Ent_No);
            //}
          
            throw new NotImplementedException();
        }

        public List<T_Module> All(Func<T_Module, bool> filter)
        {
            return Db.T_Module.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_Module Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Module> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_Module model, string note)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.ModuleID, Text = o.ModuleName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决.信息："+this.ToString());
        }


       
    }
}