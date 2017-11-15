using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class PracticeVolunteerServices : BaseServices, IDML<T_PracticeVolunteer>
    {
        public string Add(DataContext DataContext, T_PracticeVolunteer model)
        {
            throw new NotImplementedException();
        }

        public T_PracticeVolunteer Find(object id)
        {
            return Db.T_PracticeVolunteer.Find(id);
        }
        public List<T_PracticeVolunteer> All()
        {
            return Db.T_PracticeVolunteer.ToList();
        }

        public List<T_PracticeVolunteer> All(DataContext DataContext, string note = "")
        {

            if (note == "企业提供给某个学校的岗位")
            {
                var EntPracNo = Check(DataContext, "EntPracNo");
                var PosID = Check(DataContext, "PosID");
                return Db.T_PracticeVolunteer.
                    Where(o => o.EntPracNo == EntPracNo &&
                    o.PosID == PosID)
                    .ToList();
            }
            if (note == "参加该岗位的所有学生")
            {
                var EntPracNo = Check(DataContext, "EntPracNo");
                var PosID = Check(DataContext, "PosID");
                return Db.T_PracticeVolunteer.
                    Where(o => o.EntPracNo == EntPracNo &&
                    o.PosID == PosID)
                    .ToList();
            }
            if (note == "搜索参加某企业某岗位的所有学生-面试管理")
            {
                var EntPracNo = Check(DataContext, "EntPracNo");
                var PosID = Check(DataContext, "PosID");
                var keyword = Check(DataContext, "keyword");
                return Db.T_PracticeVolunteer.
                    Where(o => o.EntPracNo == EntPracNo &&
                    o.PosID == PosID)
                    .ToList();

                return Db.T_PracticeVolunteer.
                    Where(c => (c.PreVolStatus == "1" &&
                    c.EntPracNo == EntPracNo &&
                     c.PosID == PosID &&
                     Db.T_StuBatchReg.FirstOrDefault(a=>a.PracticeNo==c.PracticeNo).T_PracBatch.IsCurrentBatch=="是"&&
                     Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == c.PracticeNo).T_Student.StuNo.Contains(keyword)||
                      Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == c.PracticeNo).T_Student.StuName.Contains(keyword)||
                     Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == c.PracticeNo).T_Student.T_Class.ClassName.Contains(keyword)||
                      Db.T_StuBatchReg.FirstOrDefault(a => a.PracticeNo == c.PracticeNo).T_Student.T_Class.C_Specialty.SpeName.Contains(keyword)
               //c.T_StuBatchReg.T_PracBatch.IsCurrentBatch == "是" &&
               //(c.T_StuBatchReg.T_Student.StuNo.Contains(keyword) ||
               //c.T_StuBatchReg.T_Student.StuName.Contains(keyword) ||
               //c.T_StuBatchReg.T_Student.T_Class.ClassName.Contains(keyword) ||
               //c.T_StuBatchReg.T_Student.T_Class.C_Specialty.SpeName.Contains(keyword))
               )).ToList();

               
            }
            throw new NotImplementedException();
        }

        public List<T_PracticeVolunteer> All(Func<T_PracticeVolunteer, bool> filter)
        {
            return Db.T_PracticeVolunteer.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_PracticeVolunteer Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_PracticeVolunteer> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_PracticeVolunteer model, string note)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}