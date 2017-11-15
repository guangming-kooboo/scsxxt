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
    public class EntStudentReviewLinkServices : BaseServices, IDML<T_EntStudentReviewLink>
    {
        public string Add(DataContext DataContext, T_EntStudentReviewLink model)
        {
            model.LinkID = DateTime.Now.Random().ToString();
            //判断学生是否重复分配
            if (
                All(o =>o.TaskID==model.TaskID&& o.PracticeNo == model.PracticeNo)
                    .Count > 0)
            {
                return null;
            }
           
            Db.T_EntStudentReviewLink.Add(model);
            return Db.SaveChanges() > 0? model.TaskID:null;
        }

        public T_EntStudentReviewLink Find(object id)
        {
            return Db.T_EntStudentReviewLink.Find(id);
        }
        public List<T_EntStudentReviewLink> All()
        {
            return Db.T_EntStudentReviewLink.ToList();
        }

        public List<T_EntStudentReviewLink> All(DataContext DataContext, string note)
        {
            switch (note)
            {
                case "该员工已分配情况":
                {
                        var TaskID = Check(DataContext, "TaskID");
                        return All(o => o.TaskID == TaskID);
                }
                case "该员工针对某岗位的已分配情况":
                    {
                        var TaskID = Check(DataContext, "TaskID");
                        var PositionID = Check(DataContext, "PositionID"); var EntPracNo = Check(DataContext, "EntPracNo");
                        //return All(o => o.TaskID == TaskID&&
                        //o.T_StuBatchReg.T_PracticeVolunteer.FirstOrDefault(a=>a.PosID== PositionID&&a.EntPracNo== EntPracNo&&a.VolunteerStatus==5)!=null
                        return All(o=>o.TaskID==TaskID&&
                        Db.T_PracticeVolunteer.Where(a=>a.PracticeNo==o.PracticeNo).FirstOrDefault(a=>a.PosID==PositionID&&a.EntPracNo==EntPracNo&&a.VolunteerStatus==5)!=null
                        );
                    }
            }
            throw new NotImplementedException();
        }

        public List<T_EntStudentReviewLink> All(Func<T_EntStudentReviewLink, bool> filter)
        {
            return Db.T_EntStudentReviewLink.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_EntStudentReviewLink Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_EntStudentReviewLink> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_EntStudentReviewLink.Remove(Db.T_EntStudentReviewLink.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_EntStudentReviewLink model, string note="")
        {
            Db.T_EntStudentReviewLink.AddOrUpdate(model);
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