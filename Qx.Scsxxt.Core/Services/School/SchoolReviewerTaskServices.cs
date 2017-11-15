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
    public class SchoolReviewerTaskServices : BaseServices, IDML<T_SchoolReviewerTask>
    {
        public string Add(DataContext DataContext, T_SchoolReviewerTask model)
        {
            model.TaskID = DateTime.Now.Random().ToString();   
            //判断教师是否重复分配
            if (
                All(o => o.ItemID == model.ItemID && o.TeacherID == model.TeacherID)
                    .Count > 0)
            {
                return null;
            }
           
            Db.T_SchoolReviewerTask.Add(model);
            return Db.SaveChanges() > 0? model.TaskID:null;
        }

        public T_SchoolReviewerTask Find(object id)
        {
            return Db.T_SchoolReviewerTask.Find(id);
        }
        public List<T_SchoolReviewerTask> All()
        {
            return Db.T_SchoolReviewerTask.ToList();
        }

        public List<T_SchoolReviewerTask> All(DataContext DataContext, string note)
        {
            switch (note)
            {
                case "当前批次该评分项已分配情况":
                {
                        var ItemID = Check(DataContext, "ItemID");
                        return All(o => o.ItemID == ItemID);
                    }
                    
                case "当前批次某教师已分配情况":
                {
                        var TeacherID = Check(DataContext, "TeacherID");
                        return All(o => o.TeacherID == TeacherID);
                    }
                   
            }
            throw new NotImplementedException();
   
        }

        public List<T_SchoolReviewerTask> All(Func<T_SchoolReviewerTask, bool> filter)
        {
            return Db.T_SchoolReviewerTask.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_SchoolReviewerTask Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_SchoolReviewerTask> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_SchoolReviewerTask.Remove(Db.T_SchoolReviewerTask.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_SchoolReviewerTask model, string note = "")
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
            return All().Select(o => new SelectListItem() { Value = o.TaskID, Text = o.T_Faculty.FacName }).ToList().Format(value);
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