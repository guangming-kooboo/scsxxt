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
    public class SchoolStudentReviewLinkServices : BaseServices, IDML<T_SchoolStudentReviewLink>
    {
        public string Add(DataContext DataContext, T_SchoolStudentReviewLink model)
        {
            model.LinkID = DateTime.Now.Random().ToString();
            var StudentID = model.PracticeNo;
            var temp = ServicesFactory.GetSevers<T_StuBatchReg>()
                .All(o => o.UserID == StudentID && o.PracBatchID == DataContext.PracBatchID);
           //判断学生是否参加当前批次的实习
           var joined= temp.FirstOrDefault();
            if (joined == null)
            {
                return null;  
            }
            model.PracticeNo = joined.PracticeNo;
            //判断学生是否重复分配
            if (
                All(o =>o.TaskID==model.TaskID&& o.PracticeNo == model.PracticeNo)
                    .Count > 0)
            {
                return null;
            }
           
            Db.T_SchoolStudentReviewLink.Add(model);
            return Db.SaveChanges() > 0? model.TaskID:null;
        }

        public T_SchoolStudentReviewLink Find(object id)
        {
            return Db.T_SchoolStudentReviewLink.Find(id);
        }
        public List<T_SchoolStudentReviewLink> All()
        {
            return Db.T_SchoolStudentReviewLink.ToList();
        }

        public List<T_SchoolStudentReviewLink> All(DataContext DataContext, string note)
        {
            switch (note)
            {
                case "当前批次该评分项已分配情况":
                {
                        var TaskID = Check(DataContext, "TaskID");
                        return All(o => o.TaskID == TaskID);
                }
                case "当前批次该评分项已分配学生情况":
                    {
                        var ItemID = Check(DataContext, "ItemID");
                        return All(o => o.T_SchoolReviewerTask.ItemID == ItemID);
                    }

            }
            throw new NotImplementedException();
        }

        public List<T_SchoolStudentReviewLink> All(Func<T_SchoolStudentReviewLink, bool> filter)
        {
            return Db.T_SchoolStudentReviewLink.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id)); 
        }

        public T_SchoolStudentReviewLink Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_SchoolStudentReviewLink> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_SchoolStudentReviewLink.Remove(Db.T_SchoolStudentReviewLink.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_SchoolStudentReviewLink model, string note = "")
        {
            Db.T_SchoolStudentReviewLink.AddOrUpdate(model);
            return Db.SaveChanges() > 0;      
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            throw new NotImplementedException();
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