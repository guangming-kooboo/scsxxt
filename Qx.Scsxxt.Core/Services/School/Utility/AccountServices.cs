using System;
using System.Linq;
using Core.Services;
using Core.Services.Entity;

namespace Qx.Scsxxt.Core.Services.Utility
{
    public class AccountServices : BaseServices
    {
      
        public AccountServices()
        {
         
        }

        //获取总分
        public T_User GetEntAdmin(string Ent_No,string AdminRoleId="3")
        {
           var temp= Db.T_Employee.Where(a => a.Ent_No == Ent_No &&
            a.T_User.T_UserRole.Where(b => b.RoleID == AdminRoleId).Any()).
            Select(c => c.T_User);
            if(temp.Count()!=1)
            {
                throw new Exception("检测到异常");
            }
            return temp.FirstOrDefault();
        }

     

     

    }
}