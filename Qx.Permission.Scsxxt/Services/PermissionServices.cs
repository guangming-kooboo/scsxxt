using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Interfaces;
using Qx.Permission.Scsxxt.Models;
using Qx.Tools.Scsxxt.CommonExtendMethods;

namespace Qx.Permission.Scsxxt.Services
{
    public class PermissionServices : BaseRepository,IPermission
    {

        public List<MenuItem> GetMenuByUserId(string userId)
        {
            var allMenus = Db.Menus.ToList();

        var myAllMenus=new List<Menu>();

            //我的所有不包含孩子的菜单
            var myMenus0 = Db.UserRoles.Where(a => a.UserID == userId).
              SelectMany(b => b.Role.RoleMenus.Where(z => z.IncludeChildren == 0).Select(c => c.Menu)).
              //  ToList().
              // Distinct(CommonExtendMethods.Equality<Menu>.CreateComparer(z => z.MenuID)).//去重
              ToList();

            //踢出根节点
          var myMenus0ExceptRoot= myMenus0.Where(a => a.FartherID != "MRoot").Select(b => b).ToList();

            //我的所有包含孩子的菜单
            var myMenus1 = Db.UserRoles.Where(a => a.UserID == userId).
              SelectMany(b => b.Role.RoleMenus.Where(z=>z.IncludeChildren==1).Select(c => c.Menu)).
              ToList().
              Distinct(CommonExtendMethods.Equality<Menu>.CreateComparer(z => z.MenuID)).//去重
              ToList();

            //补全缺失子菜单
            var myMenus1ChildMenus =
                myMenus1.SelectMany(a => allMenus.
                Where(b => b.FartherID == a.MenuID).
                Select(c => c)).
               // OrderBy(c => c.Sequence).//根菜单排序
                ToList();




            ////我被分配的包含孩子的根菜单
            //var myRootMenus = myMenus.
            //    Where(a => a.FartherID == "MRoot"&&a.RoleMenus.Any(b=>b.IncludeChildren==1)).
            //    OrderBy(c=>c.Sequence).//根菜单排序
            //    ToList();



            ////我被分配的子菜单[]
            //var myChildMenus = myMenus.Except(myRootMenus).ToList();

            //myMenus0ExceptRoot+myMenus1ChildMenus
            myMenus0ExceptRoot.AddRange(myMenus1ChildMenus);

           //去重后的所有子菜单
            var myAllChilren =
                myMenus0ExceptRoot.Distinct(CommonExtendMethods.Equality<Menu>.
                CreateComparer(z => z.MenuID)).//去重
                OrderBy(c => c.Sequence).//排序
                ToList();

            //分组后找父亲

          var dest=  myAllChilren.GroupBy(a => a.FartherID).Select(g => new MenuItem()
            {
                Father = allMenus.FirstOrDefault(b => b.MenuID == g.Key),
                Children = g.AsEnumerable().Select(c => c).ToList()
            }).ToList();

            //var dest = myRootMenus.Select(a => new MenuItem()
            //{
            //    Father = a,
            //    Children = allMenus.Where(b=>b.FartherID==a.MenuID).ToList()
            //}
            // ).ToList();

            ////追加已分配的子菜单
            //dest.ForEach(a =>
            //{
            //    a.Children.AddRange(myChildMenus.Where(b => b.FartherID == a.Father.MenuID));
            //    a.Children = a.Children.Distinct(CommonExtendMethods.Equality<Menu>.CreateComparer(z => z.MenuID)).//去重
            //    OrderBy(c=>c.Sequence).//子菜单排序
            //    ToList();
            //});

            return dest;
        }

        public List<Button> GetForbidenButtonByUserId(string userId)
        {
            //var roles = Db.UserRoles.Where(a => a.UserID == userId).Select(b => b.Role);
            //var dest =
            //    Db.RoleButtonForbids.Where(a => roles.Any(b => b.RoleID == a.RoleID)).Select(c => c.Button).ToList();

            var dest = Db.UserRoles.Where(a => a.UserID == userId).
              SelectMany(b => b.Role.RoleButtonForbids.Select(c => c.Button)).ToList();
            return dest;
        }

        public List<SelectListItem> GetMenu(string selectedMenuId = "-1", string rootFather = "MRoot")
        {
            throw new System.NotImplementedException();
        }

        private Navbar Parse(Menu a)
        {
            var bar = new Navbar();
            try
            {
                bar=(new Navbar()
                {
                    Id = a.MenuID,
                    Name = a.Name,
                    ParentId = a.FartherID,
                    IsParent = a.FartherID == "MRoot" ? true : false, 
                    Status = a.MenuExtension.Status == "true" ? true : false,
                    Action = a.MenuExtension.Action,
                    Controller = a.MenuExtension.Controller,
                    Area = a.MenuExtension.Area,
                    Activeli = a.MenuExtension.ActiveLi,
                    ImageClass = a.MenuExtension.ImageClass,
                });
            }
            catch (Exception)
            {

                bar=(new Navbar()
                {
                    Id = a.MenuID,
                    Name = "[Error]" + a.Name,
                    ParentId = a.FartherID,
                    Status = true,
                    IsParent = a.FartherID == "MRoot" ? true : false, //a.Father.MenuExtension.IsParent
                    Action = "#",
                    Controller = "#",
                    Area = "#",
                    Activeli = "",
                    ImageClass = "fa fa-bar-chart-o fa-fw",
                    //    menu.Add(new Navbar { Id = 2, nameOption = "车辆管理(管理员)", imageClass = "fa fa-bar-chart-o fa-fw", status = true, isParent = true, parentId = 0 });
                    // menu.Add(new Navbar { Id = 21, nameOption = "车辆列表(管理员)", area = "FastCar", controller = "Admin", action = "CarList", status = true, isParent = false, parentId = 2 });
                });
            }
            return bar;
        }

        public IEnumerable<Navbar> GetNavbarByUserId(string userId)
        {
            var dest = new List<Navbar>();
            var myMenus = GetMenuByUserId(userId);
            myMenus.ForEach(a =>
            {
                //添加 Father
                dest.Add(Parse(a.Father));
                //添加 Children
                if (a.Children.Count > 0)
                {
                    a.Children.ForEach(b =>
                    {
                        dest.Add(Parse(b));
                    });
                }
            });
            return dest;
        }

        public List<Menu> FindFather(string menuid)
        {
            var father = Db.Menus.FirstOrDefault(a => a.MenuID == menuid);
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