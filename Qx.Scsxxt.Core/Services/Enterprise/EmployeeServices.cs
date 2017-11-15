using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Enterprise
{
    public class EmployeeServices : BaseServices, IDML<T_Employee>
    {
        public string Add(DataContext DataContext, T_Employee model)
        {
            var Ent_No = Check(DataContext, "Ent_No");//企业号
            var RoleID = Check(DataContext, "RoleID");//角色编号

            var UserPwd = model.EmployeeID; UserPwd = NoneEncrypt(UserPwd);//用户密码默认为员工号【密文存储 】 
            var UserID = Ent_No + "-" + model.EmployeeID;//用户编号
            //用户表
            var User = new T_User();
            User.UserID = UserID;
            User.NickName = model.EmployeeName;//员工姓名
            User.UserPwd = UserPwd;
            User.Note = DateTime.Now.ToLongDateString() + "企业管理员为用户注册了账号";

            //员工表
            var Employee = model;
            Employee.UserID = UserID;
            Employee.Ent_No = Ent_No;

            //用户角色表
            //31 人力资源
            //32 企业导师
            //33 企业普通员工         
            var UserRole = new T_UserRole() { UserID = UserID, RoleID = RoleID };
            Db.T_User.Add(User);
            Db.T_Employee.Add(Employee);
            Db.T_UserRole.Add(UserRole);
            
            return Db.SaveChanges()>0? model.UserID:"";
        }

        public T_Employee Find(object id)
        {
            return Db.T_Employee.Find(id);
        }
        public List<T_Employee> All()
        {
            return Db.T_Employee.ToList();
        }

        public List<T_Employee> All(DataContext DataContext, string note = "")
        {
            if (note == "该企业所有员工")
            {
                var Ent_No =Check(DataContext, "Ent_No");
                return All(o => o.Ent_No == Ent_No);
            }
           
            throw new NotImplementedException();
        }

        public List<T_Employee> All(Func<T_Employee, bool> filter)
        {
            return Db.T_Employee.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_Employee Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_Employee> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_Employee.Remove(Db.T_Employee.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_Employee model, string note)
        {
            if (note == "编辑员工资料")
            {
                Db.T_Employee.AddOrUpdate(model);
                return Db.SaveChanges()>0;
            }
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.UserID, Text = o.EmployeeName }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}