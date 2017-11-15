using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using System.Web.Mvc;
namespace ServicePlatform.Areas.Permission.Models
{
   
 
   
    public   class T_Role
    {
         [Key]
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string subSystem { get; set; }
    }
    public class T_UserRole
    {
         [Key, Column(Order = 0)]
        public string UserID { get; set; }
         [Key, Column(Order = 1)]
        public string RoleID { get; set; }
      
    }
    public class T_RoleModule
    {
         [Key, Column(Order = 0)]
        public string RoleID { get; set; }
         [Key, Column(Order = 1)]
        public string ModuleID { get; set; }
        public int IncludeAllSubMod { get; set; }
        public string DataDomain { get; set; }
    }
    public   class T_Module
    {
         [Key]
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string FartherModuleID { get; set; }
        public string PageName { get; set; }
        public string ModuleLevel { get; set; }
        public int AvailableStatus { get; set; }
        public int ModuleOrder { get; set; }
    }
    public   class T_FuncObject
    {
         [Key, Column(Order = 0)]
        public string FuncObjID { get; set; }
         [Key, Column(Order = 1)]
        public string ModuleID { get; set; }
        public string FuncName { get; set; }
    }
    public class T_RoleFuncObjForbid
    {
        [Key,Column(Order=0)]
        public string RoleID { get; set; }
         [Key, Column(Order = 1)]
        public string FuncObjID { get; set; }
    }
    public class T_ModuleBatchOpenSetting
    {
        [Key, Column(Order = 0)]
        public string ModuleID { get; set; }
        [Key, Column(Order = 1)]
        [Display(Name = "批次")]
        public string PracBatchID { get; set; }
        [Display(Name="关闭时间")]
        public DateTime CloseTime { get; set; }
        [Display(Name = "批次名称")]
        public string PracBatchName { get { return new ServicePlatform.Models.SchoolContext().PracBatch.Find(PracBatchID).BatchName; } }
        public static List<SelectListItem> PracBatchItems()
        {
            return new ServicePlatform.Models.SchoolContext().PracBatch.
                    Where(o => o.IsActive == 1 && o.IsCurrentBatch == "是").ToList().
                    Select(x => new SelectListItem() { Text = x.BatchName, Value = x.PracBatchID }).ToList();
        }
    }
    public class T_FuncBatchOpenSetting
    {
        [Key, Column(Order = 0)]
        public string FuncObjID { get; set; }
        [Key, Column(Order = 1)]
        [Display(Name = "批次")]
        public string PracBatchID { get; set; }
          [Display(Name = "关闭时间")]
        public DateTime CloseTime { get; set; }
          [Display(Name = "批次名称")]
          public string PracBatchName { get { return new ServicePlatform.Models.SchoolContext().PracBatch.Find(PracBatchID).BatchName; } }
          public static List<SelectListItem> PracBatchItems()
          {
              return new ServicePlatform.Models.SchoolContext().PracBatch.
                      Where(o => o.IsActive ==1 && o.IsCurrentBatch == "是").ToList().
                      Select(x => new SelectListItem() { Text = x.BatchName, Value = x.PracBatchID }).ToList();
          }
    }
    #region 注释类
    public class T_Role_Note
    {
        public T_Role_Note()
        {
            RoleID = "角色编号";
            RoleName = "角色名称";
            RoleType = "角色类型";
        }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
    }

    public class T_UserRole_Note
    {
        public T_UserRole_Note()
        {
            UserID = "用户名";
            RoleID = "角色"; 
        }       
        public string UserID { get; set; }  
        public string RoleID { get; set; }
    }

    public class T_RoleModule_Note
    {
        public T_RoleModule_Note()
        {
            RoleID = "角色";
            ModuleID = "菜单";
            IncludeAllSubMod = "包括子菜单";
            DataDomain = "分配理由";
          
        }
        public string RoleID { get; set; }
       
        public string ModuleID { get; set; }
        public string IncludeAllSubMod { get; set; }
        public string DataDomain { get; set; }
    }
    public class T_Module_Note
    {
        public T_Module_Note()
        {
            ModuleID = "菜单编号";
            ModuleName = "菜单名称";
            FartherModuleID = "父菜单";
            PageName = "指向网址";
            ModuleLevel = "菜单级数";
            AvailableStatus = "是否可用";
            ModuleOrder = "菜单顺序";
           
            
        }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string FartherModuleID { get; set; }
        public string PageName { get; set; }
        public string ModuleLevel { get; set; }
        public string AvailableStatus { get; set; }
        public string ModuleOrder { get; set; }
   
    }
    public class T_FuncObject_Note
    {
        public T_FuncObject_Note()
        {
               
            ModuleID = "所属菜单";
            FuncObjID = "按纽名称";
         
        }
     
      
        public string ModuleID { get; set; }
        public string FuncObjID { get; set; }
      
    }
    public class T_RoleFuncObjForbid_Note
    {
        public T_RoleFuncObjForbid_Note()
        {
            RoleID = "角色编号";
            FuncObjID = "按纽编号";
        }
        public string RoleID { get; set; }
      
        public string FuncObjID { get; set; }
    }


    #endregion
}