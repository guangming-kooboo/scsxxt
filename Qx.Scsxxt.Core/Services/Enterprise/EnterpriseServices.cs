using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class EnterpriseServices : BaseServices, IDML<T_Enterprise>
    {
        public string Add(DataContext DataContext, T_Enterprise model)
        {
            throw new NotImplementedException();
        }

        public T_Enterprise Find(object id)
        {
            return Db.T_Enterprise.Find(id);
        }
        public List<T_Enterprise> All()
        {
            return Db.T_Enterprise.ToList();
        }

        public List<T_Enterprise> All(DataContext DataContext, string note = "")
        {
            throw new NotImplementedException();
        }

        public List<T_Enterprise> All(Func<T_Enterprise, bool> filter)
        {
            return Db.T_Enterprise.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_Enterprise Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Enterprise> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_Enterprise.Remove(Db.T_Enterprise.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_Enterprise model, string note)
        {
           
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest= Db.T_Enterprise.Select(a => new SelectListItem() {Text = a.Ent_Name, Value = a.Ent_No}).ToList().Format(value);
           
            return dest;
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}