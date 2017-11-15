using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;

namespace Qx.Scsxxt.Core.Services.Common
{
    public class ReportDataServices : BaseServices, IDML<T_ReportData>
    {
        public string Add(DataContext DataContext, T_ReportData model)
        {
            throw new NotImplementedException();
        }

        public T_ReportData Find(object id)
        {
            return Db.T_ReportData.Find(int.Parse(id+""));
        }
        public List<T_ReportData> All()
        {
            return Db.T_ReportData.ToList();
        }

        public List<T_ReportData> All(DataContext DataContext, string note = "")
        {
          
         
            throw new NotImplementedException();
        }

        public List<T_ReportData> All(Func<T_ReportData, bool> filter)
        {
            return Db.T_ReportData.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_ReportData Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_ReportData> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_ReportData.Remove(Db.T_ReportData.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_ReportData model, string note)
        {
           
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.ReportID+"", Text = o.ReportName }).ToList();
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}