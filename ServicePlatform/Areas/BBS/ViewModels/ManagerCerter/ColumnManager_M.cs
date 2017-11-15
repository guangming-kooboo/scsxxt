using System.Collections.Generic;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.ManagerCerter
{
    public class ColumnManager_M
    {
        public static ColumnManager_M ToViewModel(Forum forum)
        {
            return new ColumnManager_M() { forum=forum };
        }
        public Forum forum;
    }
}