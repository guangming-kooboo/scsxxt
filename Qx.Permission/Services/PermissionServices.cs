using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using Qx.Permission.Entity;
using Qx.Permission.Interfaces;
using Qx.Permission.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Permission.Services
{
    public class PermissionServices : BaseRepository,IPermission
    {

        public List<MenuItem> GetMenuByUserId(string userId)
        {
            var allMenus = Db.Menus.ToList();

            //�ҵ����в˵�
            var myMenus = Db.UserRoles.Where(a => a.UserID == userId).
              SelectMany(b => b.Role.RoleMenus.Select(c => c.Menu)).
              ToList().
              Distinct(CommonExtendMethods.Equality<Menu>.CreateComparer(z => z.MenuID)).//ȥ��
              ToList();

            //�ұ�����ĸ��˵�
            var myRootMenus = myMenus.
                TakeWhile(a => a.FartherID == "MRoot"&&a.RoleMenus.Any(b=>b.IncludeChildren==1)).
                ToList();
            //�ұ�������Ӳ˵�
            var myChildMenus = myMenus.Except(myRootMenus);
                     
            var dest = myRootMenus.Select(a => new MenuItem()
            {
                Father = a,
                Children = allMenus.Where(b=>b.FartherID==a.MenuID).ToList()
            }
             ).ToList();

            //׷���ѷ�����Ӳ˵�
            dest.ForEach(a =>
            {
                a.Children.AddRange(myChildMenus.TakeWhile(b => b.FartherID == a.Father.MenuID));
                a.Children = a.Children.Distinct(CommonExtendMethods.Equality<Menu>.CreateComparer(z => z.MenuID)).ToList();//ȥ��
            });

            return dest;
        }

        public List<Button> GetForbidenButtonByUserId(string userId)
        {
             var dest= Db.UserRoles.Where(a => a.UserID == userId).
              SelectMany(b => b.Role.RoleButtonForbids.Select(c => c.Button)).ToList();
            return dest;
        }

        public List<SelectListItem> GetMenu(string selectedMenuId = "-1", string rootFather = "MRoot")
        {
            throw new System.NotImplementedException();
        }
        public List<Menu> FindFather(string menuid)
        {
            var father = Db.Menus.FirstOrDefault(a=>a.MenuID==menuid);
            List<Menu> fathers = new List<Menu>();
            while (father != null)
            {
                if (father.MenuID == "MRoot")
                    break;
                fathers.Add(father);
                father = Db.Menus.Find(father.FartherID);
            }
            fathers.Reverse();
            return fathers;
        }
    }
}