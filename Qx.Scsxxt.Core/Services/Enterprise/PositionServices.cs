using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class PositionServices : BaseServices, IDML<C_Position>
    {
        public string Add(DataContext DataContext, C_Position model)
        {
            throw new NotImplementedException();
        }

        public C_Position Find(object id)
        {
            return Db.C_Position.Find(id);
        }
        public List<C_Position> All()
        {
            return Db.C_Position.ToList();
        }

        public List<C_Position> All(DataContext DataContext, string note)
        {
            
          
            throw new NotImplementedException();
        }

        public List<C_Position> All(Func<C_Position, bool> filter)
        {
            return Db.C_Position.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public C_Position Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<C_Position> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.C_Position.Remove(Db.C_Position.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, C_Position model, string note = "")
        {
            Db.C_Position.AddOrUpdate(model);
            return Db.SaveChanges() > 0;
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.PositionID, Text = o.PositionName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException();
        }


       
    }
}