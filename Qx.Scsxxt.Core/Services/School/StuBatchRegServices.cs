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
    public class StuBatchRegServices : BaseServices, IDML<T_StuBatchReg>
    {
        public string Add(DataContext DataContext, T_StuBatchReg model)
        {
            model.PracticeNo = PrimaryKey;
            if (Find(PrimaryKey) == null)
            {
                return Db.SaveAdd(model) ? PrimaryKey : null;
            }
            else
            {
                return "";
            }
        }

        public T_StuBatchReg Find(object id)
        {
            return Db.T_StuBatchReg.Find(id);
        }
        public List<T_StuBatchReg> All()
        {
            return Db.T_StuBatchReg.ToList();
        }

        public List<T_StuBatchReg> All(DataContext DataContext, string note)
        {
            
            if (note == "该学生的实习成绩")
            {
                var UserID = Check(DataContext, "UserID");
                return All(o => o.UserID == UserID&&
                 o.T_PracBatch.IsActive == 1 && o.T_PracBatch.IsCurrentBatch == "是"
                 );
            }
            if (note == "某班级所有学生的实习成绩")
            {
                var ClassId = Check(DataContext, "ClassId");
                return All(o => o.T_Student.StuClass == ClassId&&
                o.T_PracBatch.IsActive==1&&o.T_PracBatch.IsCurrentBatch=="是"
                );
            }
            throw new NotImplementedException();
        }

        public List<T_StuBatchReg> All(Func<T_StuBatchReg, bool> filter)
        {
            return Db.T_StuBatchReg.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));   
        }

        public T_StuBatchReg Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_StuBatchReg> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_StuBatchReg model, string note = "")
        {
            if (note == "更新分数")
            {
                var old = Find(model.PracticeNo);
                old.ReviewScore = model.ReviewScore;
                Db.T_StuBatchReg.AddOrUpdate(old);
                return Db.SaveChanges() > 0;
            }
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.PracticeNo, Text = o.T_Student.StuName }).ToList().Format(value);
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