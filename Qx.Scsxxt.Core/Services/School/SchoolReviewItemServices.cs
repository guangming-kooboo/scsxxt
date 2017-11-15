using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.School
{
    public class SchoolReviewItemServices : BaseServices, IDML<T_SchoolReviewItem>
    {
        public string Add(DataContext DataContext, T_SchoolReviewItem model)
        {
            model.ItemID = DateTime.Now.Random().ToString();
            model.PracBatchID = DataContext.PracBatchID;
            Db.T_SchoolReviewItem.Add(model);
            return Db.SaveChanges() > 0 ? model.ItemID : null;
        }

        public T_SchoolReviewItem Find(object id)
        {
            return Db.T_SchoolReviewItem.Find(id);
        }
        public List<T_SchoolReviewItem> All()
        {
            return Db.T_SchoolReviewItem.ToList();
        }

        public List<T_SchoolReviewItem> All(DataContext DataContext, string note)
        {
            if (note == "该批次所有评分项")
            {
                DataContext.SetFiled("PracBatchID", DataContext.PracBatchID);
                var PracBatchID = Check(DataContext, "PracBatchID");
                return All(o => o.PracBatchID == PracBatchID);
            }
            else if (note == "某教师关联的评分项")
            {
                DataContext.SetFiled("TeacherID", DataContext.UserID);
                var TeacherID = Check(DataContext, "TeacherID");
                return All(DataContext, "该批次所有评分项").
                    Intersect(
                        ServicesFactory.GetSevers<T_SchoolReviewerTask>()
                            .All(a => a.TeacherID == TeacherID)
                            .Select(a => a.T_SchoolReviewItem),
                        CommonExtendMethods.Equality<T_SchoolReviewItem>.CreateComparer(o => o.ItemID))
                    .ToList();
            }
            throw new NotImplementedException();
        }

        public List<T_SchoolReviewItem> All(Func<T_SchoolReviewItem, bool> filter)
        {
            return Db.T_SchoolReviewItem.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));   
        }

        public T_SchoolReviewItem Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_SchoolReviewItem> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_SchoolReviewItem.Remove(Db.T_SchoolReviewItem.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_SchoolReviewItem model, string note = "")
        {
            Db.T_SchoolReviewItem.AddOrUpdate(model);
            return Db.SaveChanges() > 0;
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.ItemID, Text = o.ItemName }).ToList().Format(value);
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