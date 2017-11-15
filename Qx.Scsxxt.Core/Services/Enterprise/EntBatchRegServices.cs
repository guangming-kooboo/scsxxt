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
    public class EntBatchRegServices : BaseServices, IDML<T_EntBatchReg>
    {
        public string Add(DataContext DataContext, T_EntBatchReg model)
        {
            throw new NotImplementedException();
        }

        public T_EntBatchReg Find(object id)
        {
            return Db.T_EntBatchReg.Find(id);
        }
        public List<T_EntBatchReg> All()
        {
            return Db.T_EntBatchReg.ToList();
        }

        public List<T_EntBatchReg> All(DataContext DataContext, string note)
        {
            if (note == "企业通过审核的批次[当前]")
            {
                var Ent_No = Check(DataContext, "Ent_No");   
                return All(o =>o.T_PracBatch.IsActive==1&&o.T_PracBatch.IsCurrentBatch=="是"&&
                o.Ent_No == Ent_No
               ).ToList();
            }
          
            throw new NotImplementedException();
        }

        public List<T_EntBatchReg> All(Func<T_EntBatchReg, bool> filter)
        {
            return Db.T_EntBatchReg.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_EntBatchReg Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_EntBatchReg> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_EntBatchReg.Remove(Db.T_EntBatchReg.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_EntBatchReg model, string note = "")
        {
            Db.T_EntBatchReg.AddOrUpdate(model);
            return Db.SaveChanges() > 0;
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.EntPracNo, Text = o.T_PracBatch.BatchName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException();
        }


       
    }
}