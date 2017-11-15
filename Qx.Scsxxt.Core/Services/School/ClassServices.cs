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
    public class ClassServices : BaseServices, IDML<T_Class>
    {
        public string Add(DataContext DataContext, T_Class model)
        {
            model.ClassID = PrimaryKey;
            if (Find(PrimaryKey) == null)
            {
                return Db.SaveAdd(model) ? PrimaryKey : null;
            }
            else
            {
                return "";
            }
        }

        public T_Class Find(object id)
        {
            return Db.T_Class.Find(id);
        }
        public List<T_Class> All()
        {
            return Db.T_Class.ToList();
        }

        public List<T_Class> All(DataContext DataContext, string note)
        {
            return Db.T_Class.ToList();
        }

        public List<T_Class> All(Func<T_Class, bool> filter)
        {
            return Db.T_Class.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));
        }

        public T_Class Find(object[] id)
        {
            return Db.T_Class.Find(id);
        }

        public List<T_Class> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(DataContext DataContext, T_Class model, string note = "")
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
            var dest = Db.T_Class.Select(o => new SelectListItem() { Value = o.ClassID, Text = o.ClassName }).ToList().Format(value);

            return dest;
        }

    }
}