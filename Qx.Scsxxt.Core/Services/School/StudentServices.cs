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
    public class StudentServices : BaseServices, IDML<T_Student>
    {
        public string Add(DataContext DataContext, T_Student model)
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

        public T_Student Find(object id)
        {
            return Db.T_Student.Find(id);
        }
        public List<T_Student> All()
        {
            return Db.T_Student.ToList();
        }

        public List<T_Student> All(DataContext DataContext, string note)
        {
            switch (note)
            {
                case "该学校所有学生":
                {
                        var SchoolID = Check(DataContext, "SchoolID");
                        return All(o => o.T_Class.SchoolID == SchoolID);
                }
                case "该学校某班级所有学生":
                    {
                        var ClassId = Check(DataContext, "ClassId");
                        return All(DataContext, "该学校所有学生").
                            Where(o => o.StuClass == ClassId).ToList();
                    }
                case "与某班级某评分项关联的学生":
                    {
                        var ItemID = Check(DataContext, "ItemID");
                        var ClassId = Check(DataContext, "ClassId"); 
                        return ServicesFactory.GetSevers<T_SchoolStudentReviewLink>().All()
                            .Where(a => a.T_SchoolReviewerTask.ItemID == ItemID&&a.T_StuBatchReg.T_Student.StuClass==ClassId)
                            .Select(b => b.T_StuBatchReg.T_Student)
                            .ToList();
                    }
               
                case "与某班级某评分项未关联的学生":
                {
                    return All(DataContext, "该学校某班级所有学生").
                            Except(All(DataContext, "与某班级某评分项关联的学生"), CommonExtendMethods.Equality<T_Student>.CreateComparer(o => o.UserID)
                            ).ToList();
                }
                case "与某班级某评分项某教师关联的学生":
                    {
                        DataContext.SetFiled("TeacherID", DataContext.UserID);
                        var TeacherID = Check(DataContext, "TeacherID");
                        var ClassId = Check(DataContext, "ClassId");
                        return ServicesFactory.GetSevers<T_SchoolStudentReviewLink>().
                            All(a => a.T_SchoolReviewerTask.TeacherID == TeacherID && a.T_StuBatchReg.T_Student.StuClass == ClassId)
                            .Select(b => b.T_StuBatchReg.T_Student)
                            .ToList();
                    }
            }
            throw new NotImplementedException();
        }

        public List<T_Student> All(Func<T_Student, bool> filter)
        {
            return Db.T_Student.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            return Db.SaveDelete(Find(id));   
        }

        public T_Student Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Student> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_Student model, string note = "")
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
            return All().Select(o => new SelectListItem() { Value = o.UserID, Text = o.StuName }).ToList().Format(value);
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