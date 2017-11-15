using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServicePlatform.Areas.Permission.Models;
using ServicePlatform.Models;
using xyj.Plugs;
using ServicePlatform.Lib;
namespace ServicePlatform.Config
{
    /// <summary>
    /// 该类用于存放各种常量或者静态变量
    /// </summary>
    public  class Vars :IList
    {
        //例子
        public  double Pi = 3.14159;
        public  string UserID ;
        public int UserType ;//用户类型  1 企业   2学校  3平台
        public bool  IsSchoolAdmin=false;
        public bool IsEntAdmin = false;
        public bool IsPlateAdmin = false;
        public string UserUnit;
        public string SchoolID;
        private List<T_FuncObject> buttons;//需要禁用的按钮
        private  List<T_Module> menus;
        private List<MenuItem> menuItems;
        public string lastClickedMenu;//最后一次点击的菜单
        public int msgCount=0;
        private string subSystem;//用户登陆的子系统，分为"平台端","学校端","企业端"

        //构造函数
         public Vars(string UserID)
         {
             this.UserID = UserID;
         }
         //用户信息初始化
         public void Init ()
         {
             //获取用户组织
             searchUserUnit();
             //获取菜单数据源
             getMenuSource();
             //判断是否为管理员
             JudgeAdmin();
         }



         #region 获取用户组织 和用户类型
         public string getUserUnit()
         {
             return UserUnit;
         }
         public int getUserType()
         {
             return UserType;
         }
         public string getSubSystem()
         {
             return subSystem;
         }
         private void searchUserUnit()
         {
             EnterpriseContext db = new EnterpriseContext();
             //员工表  企业表
             //学生表 学校表
             //教师表 学校表
             var E = (from a in db.T_User
                      join b in db.T_Employee on a.UserID equals b.UserID
                      where b.UserID==UserID
                      select b).FirstOrDefault();

             var S = (from a in db.T_User
                      join b in db.Student on a.UserID equals b.UserID
                      join c in db.T_Class on b.StuClass equals c.ClassID
                      where b.UserID == UserID
                      select c).FirstOrDefault();

             var T = (from a in db.T_User
                      join b in db.T_Faculty on a.UserID equals b.UserID
                      where b.UserID == UserID
                      select b).FirstOrDefault();

             if (E != null)
             {
                 UserUnit = E.Ent_No;
                 UserType = 1;
                 //subSystem = "企业端";
                 subSystem = SubSystem.ENTERPRISE;
             }
                 
             else if (S != null)
             {
                 UserUnit = S.SchoolID;
                 this.SchoolID = S.SchoolID;
                 UserType = 2;
                 //subSystem = "学校端";
                 subSystem = SubSystem.SCHOOL;
             }
                
             else if (T != null)
             {
                 UserUnit = T.SchoolID;
                 this.SchoolID = T.SchoolID;
                 UserType = 2;
                 //subSystem = "学校端";
                 subSystem = SubSystem.SCHOOL;
             }
             else
             {
                 UserUnit = "Platform";
                 UserType = 3;
                 //subSystem = "平台端";
                 subSystem = SubSystem.PLATFORM;
                // UserUnit = "未找到改用户的单位,请检查数据库信息完整性";
                // UserType = -1;
             }
                 
         }
         #endregion

         #region 判断是否为管理员
         /*
RoleID	RoleName	RoleType
3	企业管理员	2
31	人力资源	3
32	企业导师	3
33	企业普通员工	3
5	高校管理员	3
51	高校新闻管理员	5
52	高校导师	5
53	实习管理老师	5
61	当前批次学生	6
62	过往批次学生	6
7	平台管理员	7
71	平台普通员工	7
72	平台新闻管理员	7
C1	普通用户	默认
TTTT	测试角色	默认
         */
         private void JudgeAdmin()
         {
             PermissionContext db = new PermissionContext();
             var roles=db.UserRole.Where(m => m.UserID == UserID).ToList();
            if(roles.Count>0)
            {
                for(int i=0;i<roles.Count;i++)
                {
                    //企业管理员
                    if (roles[i].RoleID == "3")
                        IsEntAdmin = true;
                    //高校管理员
                    if (roles[i].RoleID == "5")
                        IsSchoolAdmin = true;
                    //平台管理员
                    if (roles[i].RoleID == "7")
                        IsPlateAdmin = true;
                }
            }

         }
         #endregion

         #region 获取菜单和按钮数据
         //获取菜单数据源 和禁用按钮数据源
         private void getMenuSource(bool foreceUpdate = false)
        {
            if (foreceUpdate || menus == null || buttons == null)
            {
                 menus = new ServicePlatform.Areas.Permission.Controllers.HomeController().GetModuleByUserID(UserID);//缓存
                 buttons = new ServicePlatform.Areas.Permission.Controllers.HomeController().GetForbidenButtonByUserID(UserID);//缓存
                
            }
               
        }
        //获取菜单[排序后]
        public List<MenuItem> getmenuItems(bool foreceUpdate = false)
        {
            //结果集
            List<MenuItem> tempList = new List<MenuItem>();
            //防止数据源为空
            if (menus == null || foreceUpdate)
                getMenuSource(foreceUpdate);

            //找出一级菜单
            List<T_Module> fathers = (from t in menus
                                      where t.FartherModuleID == "MRoot"
                                      orderby t.ModuleOrder
                                      select t).ToList();
            fathers=this.Distinct<T_Module>(fathers, "ModuleID");          
            //找出二级菜单
            for (int i = 0; i < fathers.Count; i++)
            {
                //该父亲的孩子
                string fatherID = fathers[i].ModuleID;
                List<T_Module> children = (from t in menus
                                           where t.FartherModuleID == fatherID
                                           orderby t.ModuleOrder
                                           select t).ToList();
                this.Distinct<T_Module>(children, "ModuleID");
                tempList.Add(new MenuItem(fathers[i], children));
            }

          
            menuItems=tempList;
            return tempList;
        }
        
         //获取禁用按钮
        public List<string> getButtons()
        {
            #region 构造测试数据
            //buttons=new List<T_FuncObject>()
            //{
            //    new T_FuncObject()
            //    {
            //         ModuleID="M14-8",
            //         FuncObjID="_form_newrep",
            //          FuncName=""
            //    }, 
            //    new T_FuncObject()
            //    {
            //         ModuleID="M14-8",
            //         FuncObjID="_form_weekrec",
            //          FuncName=""
            //    },  
            //    new T_FuncObject()
            //    {
            //         ModuleID="M14-8",
            //         FuncObjID="_form_practicecase",
            //         FuncName=""
            //    }
            //    ,  
            //    new T_FuncObject()
            //    {
            //         ModuleID="M14-2",
            //         FuncObjID="_form_newrep",
            //          FuncName=""
            //    }, 
            //    new T_FuncObject()
            //    {
            //         ModuleID="M14-2",
            //         FuncObjID="_form_weekrec",
            //          FuncName=""
            //    },  
            //    new T_FuncObject()
            //    {
            //         ModuleID="M14-2",
            //         FuncObjID="_form_practicecase",
            //         FuncName=""
            //    }
            //    ,  
            //    new T_FuncObject()
            //    {
            //         ModuleID="M16-2",
            //         FuncObjID="_form_newrep",
            //          FuncName=""
            //    }, 
            //    new T_FuncObject()
            //    {
            //         ModuleID="M16-2",
            //         FuncObjID="_form_weekrec",
            //          FuncName=""
            //    },  
            //    new T_FuncObject()
            //    {
            //         ModuleID="M16-2",
            //         FuncObjID="_form_practicecase",
            //         FuncName=""
            //    }



            //};
            #endregion
            var q=buttons.Where(m=>m.ModuleID==lastClickedMenu).Select(n=>n.FuncName.Trim()).ToList();
            return q;
        }

       }

    public class MenuItem
    {
        public T_Module father;
        public List<T_Module> children;
        public MenuItem(T_Module father, List<T_Module> children)
        {
            this.father = father;
            this.children = children;
        }
    }
    #endregion
}
/*
 错误代码：
 * L1001    列表视图原始数据和代码转换后数据行数不一致        清理数据库不规范数据
 * L1002    列表视图标题栏出现错误                            查看向应标题栏代码Limit类型是否和控制器一致
 * L1003    列表视图数据显示出现错误                          
 */
