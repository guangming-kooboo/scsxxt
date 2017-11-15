using System.Collections.Generic;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.ManagerCerter
{
    public class ThemeManager_M
    {
       public static ThemeManager_M ToViewModel(Forum forum,Column column)
       {
           return new ThemeManager_M() { forum=forum,column=column};
       }
       public Forum forum;
       public Column column;
    }
}