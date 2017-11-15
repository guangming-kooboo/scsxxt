using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class SearchUsers_M
    {
        public static SearchUsers_M ToViewModel(List<Person> searchUser, string keyword)
        {
            return new SearchUsers_M (){searchUser=searchUser,keyword=keyword};
        }
        public List<Person> searchUser;
        public string keyword;
    }
}