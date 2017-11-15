using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Interfaces;
using Qx.Permission.Scsxxt.Models;
using Qx.Tools.Scsxxt.CommonExtendMethods;

namespace Qx.Permission.Scsxxt.Services
{
    public class PermissionProvider : BaseRepository, IPermissionProvider
    {
        private readonly IPermission _permission;

        #region 中转
                public PermissionProvider(IPermission permission)
                {
                    _permission = new PermissionServices();
                }

                public List<MenuItem> GetMenuByUserId(string userId)
                {
                    return _permission.GetMenuByUserId(userId);
                }

                public List<Button> GetForbidenButtonByUserId(string userId)
                {
                    return _permission.GetForbidenButtonByUserId(userId);
                }

                public IEnumerable<Navbar> GetNavbarByUserId(string userId)
                {
                    return _permission.GetNavbarByUserId(userId);
                }

                public List<Menu> FindFather(string menuid)
                {
                    return _permission.FindFather(menuid);
                }
        #endregion

        public bool Login(string userId, string userPwd)
        {
            userPwd = NoneEncrypt(userPwd);
            return Db.Users.Any(a => a.UserID == userId &&
                                a.UserPwd ==userPwd 
                );
        }

        public bool Regist(string userId, string userPwd, 
            string nickName,string email="",string phone="", string userTypeId="0")
        {
            if (Db.Users.Any(a => a.UserID == userId))
                return false;

            //添加用户
            Db.Users.Add(new User()
            {
                UserID = userId,
                UserPwd =NoneEncrypt(userPwd) ,
                NickName = nickName,
                UserTypeId = userTypeId,
                Email = email,
                Phone = phone,
                RegisterDate = DateTime.Now,
                LastLoginDate = DateTime.Now,
            });
            //寻找默认角色
             Db.Roles.Where(a=>a.IsDefault==1).
                 ToList().ForEach(b =>
                 {
                      //添加默认角色
                     Db.UserRoles.Add(new UserRole()
                     {
                         UserID = userId,
                         RoleID = b.RoleID,
                         Note = "新用户注册自动添加默认角色[" + b.Name + "];有效期为永久",
                         ExpireTime = DateTime.MaxValue,
                         UserRoleID = userId + "-" + b.RoleID
                     });
                 });
           
            return Db.SaveChanges() > 0;
        }

        public User UserInfo(string userId)
        {
            var result= Db.Users.Where(a => a.UserID == userId);
            if (result.Count() > 1)
            {
                throw new Exception("找到"+ result.Count()+"个UserID为" + userId + @"的用户，请尝试执行SELECT  [UserID]
                                                                                                              ,[NickName]
                                                                                                              ,[UserPwd]
                                                                                                              ,[Email]
                                                                                                              ,[Phone]
                                                                                                              ,[UserTypeId]
                                                                                                              ,[Note]
                                                                                                              ,[RegisterDate]
                                                                                                              ,[LastLoginDate]
                                                                                                          FROM[Users]
                                                                                                        where[UserID] = '"+ userId+"'检查Permission.Users表中的数据");
            }
            if (result.FirstOrDefault()==null)
            {
                throw new Exception("未找到UserID为" + userId + @"的用户，请尝试执行SELECT  [UserID]
                                                                                          ,[NickName]
                                                                                          ,[UserPwd]
                                                                                          ,[Email]
                                                                                          ,[Phone]
                                                                                          ,[UserTypeId]
                                                                                          ,[Note]
                                                                                          ,[RegisterDate]
                                                                                          ,[LastLoginDate]
                                                                                      FROM[Users]
                                                                                    where [UserID]='"+ userId+"'检查Permission.Users表中的数据");
            }
            return result.FirstOrDefault();
        }

        public bool UpdateUser(string userId, string nickName, string userPwd="", string email="", string phone = "", string note = "")
        {
            var old = UserInfo(userId);
           
            old.NickName = nickName;
            if(userPwd.HasValue())
            {
                old.UserPwd = NoneEncrypt(userPwd);
            }
            if (email.HasValue())
            {
                old.Email = email;
            }
            if (phone.HasValue())
            {
                old.Phone = phone;
            }
            if (note.HasValue())
            {
                old.Note = note;
            }
            Db.Entry(old).State = EntityState.Modified;
            return Db.SaveChanges() > 0;
        }
    }
}