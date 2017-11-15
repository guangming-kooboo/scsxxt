using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Models;

namespace Qx.Permission.Scsxxt.Interfaces
{
    public interface IPermission
    {
        List<MenuItem>  GetMenuByUserId(string userId);
        List<Button> GetForbidenButtonByUserId(string userId);
        List<SelectListItem> GetMenu(string selectedMenuId = "-1", string rootFather = "MRoot");

        IEnumerable<Navbar> GetNavbarByUserId(string userId);
        List<Menu> FindFather(string menuid);
    }
}