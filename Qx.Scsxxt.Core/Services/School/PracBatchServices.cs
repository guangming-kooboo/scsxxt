using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.School
{
    public class PracBatchServices : BaseServices, IDML<T_PracBatch>
    {
        public string Add(DataContext DataContext, T_PracBatch model)
        {
            model.PracBatchID= PrimaryKey;
            if (Find(PrimaryKey) == null)
            {
                return Db.SaveAdd(model) ? PrimaryKey : null;
            }
            else
            {
                return "";
            };
        }

        public T_PracBatch Find(object id)
        {
            return Db.T_PracBatch.Find(id);
        }
        public List<T_PracBatch> All()
        {
            return Db.T_PracBatch.ToList();
        }

        public List<T_PracBatch> All(DataContext DataContext, string note)
        {
            var SchoolID = Check(DataContext, "SchoolID");
            if (note == "该学校当前批次信息")
            {
                return Db.T_PracBatch.Where(a => a.SchoolID == SchoolID && a.IsCurrentBatch == "是").ToList();
            }
            throw new NotImplementedException();
        }

        public List<T_PracBatch> All(Func<T_PracBatch, bool> filter)
        {
            return Db.T_PracBatch.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));
        }

        public T_PracBatch Find(object[] id)
        {
            throw new NotImplementedException();
        }



        public List<T_PracBatch> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_PracBatch.Remove(Db.T_PracBatch.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_PracBatch model, string note = "")
        {

            switch (note)
            {
                case "":
                    {


                    }; break;

                default: throw new NotImplementedException();
            }
            return Db.SaveModified(model);
        }

  

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            return All().Select(o => new SelectListItem() { Value = o.PracBatchID, Text = o.BatchName }).ToList().Format(value);
        }
    }
}