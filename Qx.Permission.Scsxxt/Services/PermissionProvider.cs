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

        #region ��ת
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

            //����û�
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
            //Ѱ��Ĭ�Ͻ�ɫ
             Db.Roles.Where(a=>a.IsDefault==1).
                 ToList().ForEach(b =>
                 {
                      //���Ĭ�Ͻ�ɫ
                     Db.UserRoles.Add(new UserRole()
                     {
                         UserID = userId,
                         RoleID = b.RoleID,
                         Note = "���û�ע���Զ����Ĭ�Ͻ�ɫ[" + b.Name + "];��Ч��Ϊ����",
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
                throw new Exception("�ҵ�"+ result.Count()+"��UserIDΪ" + userId + @"���û����볢��ִ��SELECT  [UserID]
                                                                                                              ,[NickName]
                                                                                                              ,[UserPwd]
                                                                                                              ,[Email]
                                                                                                              ,[Phone]
                                                                                                              ,[UserTypeId]
                                                                                                              ,[Note]
                                                                                                              ,[RegisterDate]
                                                                                                              ,[LastLoginDate]
                                                                                                          FROM[Users]
                                                                                                        where[UserID] = '"+ userId+"'���Permission.Users���е�����");
            }
            if (result.FirstOrDefault()==null)
            {
                throw new Exception("δ�ҵ�UserIDΪ" + userId + @"���û����볢��ִ��SELECT  [UserID]
                                                                                          ,[NickName]
                                                                                          ,[UserPwd]
                                                                                          ,[Email]
                                                                                          ,[Phone]
                                                                                          ,[UserTypeId]
                                                                                          ,[Note]
                                                                                          ,[RegisterDate]
                                                                                          ,[LastLoginDate]
                                                                                      FROM[Users]
                                                                                    where [UserID]='"+ userId+"'���Permission.Users���е�����");
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