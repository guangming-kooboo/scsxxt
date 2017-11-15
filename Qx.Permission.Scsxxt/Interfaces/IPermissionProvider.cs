using System.Collections.Generic;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Models;

namespace Qx.Permission.Scsxxt.Interfaces
{
    public interface IPermissionProvider
    {
        List<MenuItem> GetMenuByUserId(string userId);
        List<Button> GetForbidenButtonByUserId(string userId);
        IEnumerable<Navbar> GetNavbarByUserId(string userId);
        List<Menu> FindFather(string menuid);
        bool Login(string userId, string userPwd);

        bool Regist(string userId, string userPwd, 
            string nickName,string email="",string phone="", string userTypeId="0");

        User UserInfo(string userId);
        bool UpdateUser(string userId, string nickName, string userPwd = "", string email = "", string phone = "", string note = "");
    }
}