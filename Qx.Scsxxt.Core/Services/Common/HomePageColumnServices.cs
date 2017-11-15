using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;

namespace Qx.Scsxxt.Core.Services.Common
{
    public class HomePageColumnServices : BaseServices, IDML<T_HomePageColumn>
    {
        public string Add(DataContext DataContext, T_HomePageColumn model)
        {
            throw new NotImplementedException();
        }

        public T_HomePageColumn Find(object id)
        {
            return Db.T_HomePageColumn.Find(id);
        }
        public List<T_HomePageColumn> All()
        {
            return Db.T_HomePageColumn.ToList();
        }

        public List<T_HomePageColumn> All(DataContext DataContext, string note = "")
        {
            throw new NotImplementedException();
        }

        public List<T_HomePageColumn> All(Func<T_HomePageColumn, bool> filter)
        {
            return Db.T_HomePageColumn.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_HomePageColumn Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_HomePageColumn> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_HomePageColumn.Remove(Db.T_HomePageColumn.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_HomePageColumn model, string note)
        {
           
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}