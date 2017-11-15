using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services.School;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class EntReviewItemServices : BaseServices, IDML<T_EntReviewItem>
    {
        public string Add(DataContext DataContext, T_EntReviewItem model)
        {
            model.ItemID = DateTime.Now.Random().ToString();
           // var EntPracNo = Check(DataContext, "EntPracNo");
            //model.EntPracNo = EntPracNo;
            if(Db.T_EntReviewItem.Find(model.ItemID)!=null){ return null; }

            Db.T_EntReviewItem.Add(model);
            return Db.SaveChanges() > 0 ? model.ItemID : null;
        }

        public T_EntReviewItem Find(object id)
        {
            return Db.T_EntReviewItem.Find(id);
        }
        public List<T_EntReviewItem> All()
        {
            return Db.T_EntReviewItem.ToList();
        }

        public List<T_EntReviewItem> All(DataContext DataContext, string note)
        {
            
            if (note == "企业实习号关联的评分项")
            {
                var EntPracNo = Check(DataContext, "EntPracNo");
                return All(o => o.EntPracNo == EntPracNo);
            }

           
            throw new NotImplementedException();
        }

        public List<T_EntReviewItem> All(Func<T_EntReviewItem, bool> filter)
        {
            return Db.T_EntReviewItem.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_EntReviewItem Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_EntReviewItem> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_EntReviewItem.Remove(Db.T_EntReviewItem.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_EntReviewItem model, string note = "")
        {
            if (note == "编辑评分项")
            {
                var old = Db.T_EntReviewItem.Find(model.ItemID);
                old.ItemName = model.ItemName;
                old.ItemWeight = model.ItemWeight;
                old.ItemSequence = model.ItemSequence;
                Db.T_EntReviewItem.AddOrUpdate(old);
                return Db.SaveChanges() > 0;
            }
            throw new NotImplementedException();
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