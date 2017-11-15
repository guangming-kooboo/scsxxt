using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class SearchColumns_M
    {

        public static SearchColumns_M ToViewModel(List<Column> searchColumn, string keyword)
        {
            return new SearchColumns_M() { searchColumn = searchColumn,keyword=keyword };
        }
        public List<Column> searchColumn;
        public string keyword;

    }
}