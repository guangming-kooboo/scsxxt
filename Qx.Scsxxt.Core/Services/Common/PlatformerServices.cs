using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Common
{
    public class PlatformerServices : BaseServices, IDML<T_Platformer>
    {
        public string Add(DataContext DataContext, T_Platformer model)
        {
            throw new NotImplementedException();
        }

        public T_Platformer Find(object id)
        {
            return Db.T_Platformer.Find(id);
        }
        public List<T_Platformer> All()
        {
            return Db.T_Platformer.ToList();
        }

        public List<T_Platformer> All(DataContext DataContext, string note = "")
        {
            throw new NotImplementedException();
        }

        public List<T_Platformer> All(Func<T_Platformer, bool> filter)
        {
            return Db.T_Platformer.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_Platformer Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Platformer> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_Platformer.Remove(Db.T_Platformer.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_Platformer model, string note)
        {
           
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.UserID, Text = o.PFName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}