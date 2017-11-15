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
    public class SchoolServices : BaseServices, IDML<T_School>
    {
        public string Add(DataContext DataContext, T_School model)
        {
            model.SchoolID = PrimaryKey;
            if (Find(PrimaryKey) == null)
            {
                return Db.SaveAdd(model) ? PrimaryKey : null;
            }
            else
            {
                return "";
            }
        }

        public T_School Find(object id)
        {
            return Db.T_School.Find(id);
        }
        public List<T_School> All()
        {
            return Db.T_School.ToList();
        }

        public List<T_School> All(DataContext DataContext, string note)
        {
            throw new NotImplementedException();
        }

        public List<T_School> All(Func<T_School, bool> filter)
        {
            return Db.T_School.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_School Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_School> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(DataContext DataContext, T_School model, string note = "")
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

        public List<SelectListItem> ToSelectItems(string value="")
        {
            var dest= Db.T_School.Select(o => new SelectListItem() { Value = o.SchoolID, Text = o.SchoolName }).ToList().Format(value);
          
            return dest;
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            if (this.IsSame<T_PracBatch, K>())
            {
                return new PracBatchServices().ToSelectItems();
            }
            else
            {
                throw new NotImplementedException("联系开发人员解决");
            }
        }


       
    }
}