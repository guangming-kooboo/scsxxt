using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Tools;using Qx.Tools.CommonExtendMethods;

namespace Core.Services
{
    public interface IDML<T>
    {
        List<SelectListItem> GetSelectItems<K>();
        List<SelectListItem> ToSelectItems(string value="");
        string Add(DataContext DataContext, T model);
        bool Delete(DataContext DataContext, object id);
        bool Update(DataContext DataContext, T model, string note = "");
        T Find(object id);
        List<T> All();
        List<T> All(Func<T, bool> filter);
        List<T> All(DataContext DataContext);
        List<T> All(DataContext DataContext, string note);  
        bool Delete(DataContext DataContext, object[] id);
        T Find(object[] id);

    }
}

