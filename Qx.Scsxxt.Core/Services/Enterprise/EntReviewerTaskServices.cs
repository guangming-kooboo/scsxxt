using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services.School;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class EntReviewerTaskServices : BaseServices, IDML<T_EntReviewerTask>
    {
        public string Add(DataContext DataContext, T_EntReviewerTask model)
        {
            model.TaskID = DateTime.Now.Random().ToString();   
            //判断员工是否重复分配
            if (
                All(o => o.ItemID == model.ItemID && o.EmployeeID == model.EmployeeID)
                    .Count > 0)
            {
                return null;
            }
           
            Db.T_EntReviewerTask.Add(model);
            return Db.SaveChanges() > 0? model.TaskID:null;
        }

        public T_EntReviewerTask Find(object id)
        {
            return Db.T_EntReviewerTask.Find(id);
        }
        public List<T_EntReviewerTask> All()
        {
            return Db.T_EntReviewerTask.ToList();
        }

        public List<T_EntReviewerTask> All(DataContext DataContext, string note)
        {
            switch (note)
            {
                case "该评分项已分配情况":
                {
                        var ItemID = Check(DataContext, "ItemID");
                        return All(o => o.ItemID == ItemID);
                 }

                case "该员工负责某批次的评分项":
                    {
                        var EntPracNo = Check(DataContext, "EntPracNo");
                        var EmployeeID = Check(DataContext, "EmployeeID");
                        return All(o => o.EmployeeID == EmployeeID&&
                        o.T_EntReviewItem.EntPracNo== EntPracNo
                        );
                    }

            }
            throw new NotImplementedException();
   
        }

        public List<T_EntReviewerTask> All(Func<T_EntReviewerTask, bool> filter)
        {
            return Db.T_EntReviewerTask.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_EntReviewerTask Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_EntReviewerTask> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_EntReviewerTask.Remove(Db.T_EntReviewerTask.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_EntReviewerTask model, string note = "")
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.TaskID, Text = o.T_Employee.EmployeeName }).ToList().Format(value);
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