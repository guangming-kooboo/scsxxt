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
    public class FacultyServices : BaseServices, IDML<T_Faculty>
    {
        public string Add(DataContext DataContext, T_Faculty model)
        {
            model.UserID = PrimaryKey;
            if (Find(PrimaryKey) == null)
            {
                return Db.SaveAdd(model) ? PrimaryKey : null;
            }
            else
            {
                return "";
            }
        }

        public T_Faculty Find(object id)
        {
            return Db.T_Faculty.Find(id);
        }
        public List<T_Faculty> All()
        {
            return Db.T_Faculty.ToList();
        }

        public List<T_Faculty> All(DataContext DataContext, string note)
        {
            if (note == "该学校所有教师")
            {
                var SchoolID = Check(DataContext, "SchoolID");
                return All(o => o.SchoolID == SchoolID);
            }
           else  if (note == "负责该学校该评分项的教师")
           {
                var ItemID = Check(DataContext, "ItemID");
                return All(DataContext, "该学校所有教师").Intersect(
                    ServicesFactory.GetSevers<T_SchoolReviewerTask>().All().Where(a=>a.ItemID==ItemID).
                    Select(m=>m.T_Faculty)
                    ).ToList();
            }
            else if (note == "该学校未被安排到该评分项的教师")
            {
                var ItemID = Check(DataContext, "ItemID");
                return All().Where(a =>
                    !(ServicesFactory.GetSevers<T_SchoolReviewerTask>().All(m => m.ItemID == ItemID).Select(b => b.TeacherID).Contains(a.UserID)) &&
                    a.SchoolID == DataContext.UserUnit
                    ).ToList();
            }
            throw new NotImplementedException();
        }

        public List<T_Faculty> All(Func<T_Faculty, bool> filter)
        {
            return Db.T_Faculty.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));   
        }

        public T_Faculty Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Faculty> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(DataContext DataContext, T_Faculty model, string note = "")
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
            return All().Select(o => new SelectListItem() { Value = o.UserID, Text = o.FacName }).ToList().Format(value);
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