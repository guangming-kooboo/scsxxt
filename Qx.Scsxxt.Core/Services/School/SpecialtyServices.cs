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
    public class SpecialtyServices : BaseServices, IDML<C_Specialty>
    {
        public string Add(DataContext DataContext, C_Specialty model)
        {
            throw new NotImplementedException();
        }

        public C_Specialty Find(object id)
        {
            return Db.C_Specialty.Find(id);
        }
        public List<C_Specialty> All()
        {
            return Db.C_Specialty.ToList();
        }

        public List<C_Specialty> All(DataContext DataContext, string note)
        {
           
            throw new NotImplementedException();
        }

        public List<C_Specialty> All(Func<C_Specialty, bool> filter)
        {
            return Db.C_Specialty.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));
        }

        public C_Specialty Find(object[] id)
        {
            throw new NotImplementedException();
        }



        public List<C_Specialty> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.C_Specialty.Remove(Db.C_Specialty.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, C_Specialty model, string note = "")
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
            if (this.IsSame<C_Specialty, K>())
            {
                return All().Select(o => new SelectListItem() { Value = o.EntryYear.ToString(), Text = o.EntryYear.ToString() }).ToList().Format("");
            }
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            return All().Select(o => new SelectListItem() { Value = o.SpeNo, Text = o.SpeName }).ToList().Format(value);
        }
    }
}