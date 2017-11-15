using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class PracticePositionServices : BaseServices, IDML<T_PracticePosition>
    {
        public string Add(DataContext DataContext, T_PracticePosition model)
        {
            throw new NotImplementedException();
        }

        public T_PracticePosition Find(object id)
        {
            return Db.T_PracticePosition.Find(id);
        }
        public List<T_PracticePosition> All()
        {
            return Db.T_PracticePosition.ToList();
        }

        public List<T_PracticePosition> All(DataContext DataContext, string note = "")
        {
            if (note == "企业提供给某个学校的岗位")
           {
                var EntPracNo = Check(DataContext, "EntPracNo");
                return Db.T_PracticePosition.
                    Where(o=>o.EntPracNo== EntPracNo)
                    .ToList();
            }
            if (note == "企业提供的所有岗位")
            {
                var Ent_No = Check(DataContext, "Ent_No");
                return Db.T_PracticePosition.Where(a=>a.T_EntBatchReg.Ent_No== Ent_No&&a.T_EntBatchReg.T_PracBatch.IsCurrentBatch=="是").ToList().
                    Distinct(CommonExtendMethods.Equality<T_PracticePosition>.CreateComparer(a=>a.PositionID))
                    .ToList();
            }
            throw new NotImplementedException();
        }

        public List<T_PracticePosition> All(Func<T_PracticePosition, bool> filter)
        {
            return Db.T_PracticePosition.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_PracticePosition Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_PracticePosition> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DataContext DataContext, T_PracticePosition model, string note)
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