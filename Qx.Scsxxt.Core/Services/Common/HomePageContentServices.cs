using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;

namespace Qx.Scsxxt.Core.Services.Common
{
    public class HomePageContentServices : BaseServices, IDML<T_HomePageContent>
    {
        public string Add(DataContext DataContext, T_HomePageContent model)
        {
            throw new NotImplementedException();
        }

        public T_HomePageContent Find(object id)
        {
            return Db.T_HomePageContent.Find(id);
        }
        public List<T_HomePageContent> All()
        {
            return Db.T_HomePageContent.ToList();
        }
        private List<T_HomePageContent> GetContentById(DataContext dataContext, int hpColId)
        {
            var info = Db.T_HomePageColumn
                .FirstOrDefault(o => o.HPColumnID ==hpColId);
            return Db.T_HomePageContent.Where(o => o.HPColID ==hpColId).
                OrderBy(a => a.HPCSeq).
                Take(info.ColRowCount).ToList();
        }
        public List<T_HomePageContent> All(DataContext DataContext, string note = "")
        {
            if (note == "某栏目显示在首页的内容")
            {
                return GetContentById(DataContext, int.Parse(Check(DataContext, "HPColID")));
            }
           else if (note == "平台栏目801")
            {
                return GetContentById(DataContext, 801);
               
            }
            else if (note == "平台栏目802")
            {
                return GetContentById(DataContext, 802);
           
            }
            else if (note == "平台栏目803")
            {
                return GetContentById(DataContext, 803);
               
            }
            else if (note == "平台栏目804")
            {

                return GetContentById(DataContext, 804);
            }
            else if (note == "平台栏目805")
            {
                return GetContentById(DataContext, 805);
              
            }
            else if (note == "某单位的新闻栏目")
            {
                var UnitID = Check(DataContext, "UnitID");
                var result =
                    Db.T_Content.Where(
                        o => o.UnitID == UnitID && o.ContentTypeID == 11);
                       
                return result.SelectMany(r => r.T_HomePageContent).
                        ToList();
            }
            else if (note == "某单位的下载栏目")
            {
                var UnitID = Check(DataContext, "UnitID");
                return Db.T_HomePageContent.Where(o => o.T_HomePageColumn.UnitID.Trim() == UnitID && o.T_HomePageColumn.ContentType.Trim() == "31").
                  OrderBy(a => a.HPCSeq).
                  ToList();
            }
            else if (note == "某单位的下载栏目")
            {
                var UnitID = Check(DataContext, "UnitID");
                return Db.T_HomePageContent.Where(o => o.T_HomePageColumn.UnitID.Trim() == UnitID && o.T_HomePageColumn.ContentType.Trim() == "31").
                  OrderBy(a => a.HPCSeq).
                  ToList();
            }
           
            throw new NotImplementedException();
        }

        public List<T_HomePageContent> All(Func<T_HomePageContent, bool> filter)
        {
            return Db.T_HomePageContent.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_HomePageContent Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_HomePageContent> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_HomePageContent.Remove(Db.T_HomePageContent.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_HomePageContent model, string note)
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