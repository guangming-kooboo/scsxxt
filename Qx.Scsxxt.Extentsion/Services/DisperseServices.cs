using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Qx.Scsxxt.Extentsion.Configs;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Scsxxt.Extentsion.Exceptions;
using Qx.Scsxxt.Extentsion.Interfaces;
using Qx.Scsxxt.Extentsion.Repository;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Services
{
  
    public class DisperseServices : BaseRepository, IDisperseServices
    {
        public bool AgreeStuEnterprise(string Ent_No, string Ent_Name, string EntCategoryID,
            string EntRank, string EntAddress, string UserID, int RegisterTime,
            int UpdateTime, string Email, string EntResume, string Contectinfo)
        {
            var enterprise = new T_Enterprise()
            {
                Ent_No = Ent_No,
                Ent_Name = Ent_Name,
                EntCategoryID = EntCategoryID,
                EntRank = EntRank,
                InfoLocked = -1,
                EntState = 3,
                EntAddress = EntAddress,
                UserID = UserID,
                RegisterTime = RegisterTime,
                UpdateTime = UpdateTime,
                Email = Email,
                EntResume = EntResume,
                Contectinfo = Contectinfo
            };
            Db.T_Enterprise.Add(enterprise);
            return Db.Saved();
        }

        public bool OneKeyToPractice_RoleBack(string stuEnterPriseApplyID)
        {
            var V3 = Db.V3_StuEnterPriseApply.Find(stuEnterPriseApplyID);
            if (V3 == null)
            {
                throw new StuEnterPriseApplyNotFoundException();
            }
            string stuUserId = V3.UserID;
            string entNo = V3.Ent_No;
            string positionId = V3.positionId.ToString();
            string posDesc = V3.posDesc;
            string practiceDept = V3.practiceDept;
            string practiceDivDept = V3.practiceDivDept;
            string practiceStartEnd = V3.practiceStart.Value.ToDateTime() + "到" + V3.practiceEnd.Value.ToDateTime();


            const string DisperseRoleId = "81";
            #region 前置条件检测
            //查找学生角色
            var role = Db.T_Role.FirstOrDefault(a => a.RoleID == DisperseRoleId);
            if (role == null)
            {
                throw new RoleNotFoundException();
            }


            //查找岗位
            var position = Db.C_Position.FirstOrDefault(a => a.PositionID == positionId);
            if (position == null)
            {
                throw new PositionNotFoundException();
            }

            //查找企业
            var enterprise = Db.T_Enterprise.FirstOrDefault(a => a.Ent_No == entNo);
            if (enterprise == null)
            {
                throw new EnterpriseNotFoundException();
            }

            //查找学生
            var student = Db.T_Student.FirstOrDefault(a => a.UserID == stuUserId);
            if (student == null || !student.T_Class.SchoolID.HasValue())
            {
                throw new StudentNotFoundException();
            }

            //查找学生所在学校
            var school = Db.T_School.FirstOrDefault(a => a.SchoolID == student.T_Class.SchoolID);
            if (school == null)
            {
                throw new SchoolNotFoundException();
            }

            //查找当前批次号
            var pracBatch = Db.T_PracBatch.FirstOrDefault(a =>
            a.SchoolID == school.SchoolID &&
            a.IsCurrentBatch == "是" &&
            a.IsActive == 1);
            if (pracBatch == null)
            {
                throw new PracBatchNotFoundException();
            }

            //查找企业注册批次
            var entPracBatch = Db.T_EntBatchReg.Find(pracBatch.PracBatchID
                + "-" + enterprise.Ent_No);
            if (entPracBatch == null)
            {
                throw new EntPracBatchNotFoundException();
            }

            //查找企业提供岗位
            var practicePosition = Db.T_PracticePosition.FirstOrDefault(a =>
             a.EntPracNo == entPracBatch.EntPracNo &&
             a.PositionID == position.PositionID);
            if (practicePosition == null)
            {
               throw new PracticePositionNotFoundException();
            }

            //查找学生实习号
            var stuPracBatch = Db.T_StuBatchReg.FirstOrDefault(a =>
            a.UserID == stuUserId &&
            a.PracBatchID == pracBatch.PracBatchID);
            if (stuPracBatch == null)
            {
                throw new StuPracBatchNotExsitException();
            }

            //查找学生企业实习号(志愿号)
            var practiceVolunteer = Db.T_PracticeVolunteer.FirstOrDefault(a =>
            a.PracticeNo == stuPracBatch.PracticeNo &&
            a.EntPracNo == practicePosition.EntPracNo &&
            a.PosID == practicePosition.PositionID);
            if (practiceVolunteer == null)
            {
                throw new PracticeVolunteerNotExsitException();
            }
            #endregion
            //1.给学生分配角色(角色:分散实习)
            //var t_UserRole = Db.T_UserRole.Find(stuUserId, role.RoleID); 
            //Db.T_UserRole.Remove(t_UserRole);
            //2.调用获取当前批次的接口获取学校的当前批次号（参数：学校代码）
            //3.企业自动注册学校当前批次，生成企业实习号
            //var t_EntBatchReg = entPracBatch;
            //Db.T_EntBatchReg.Remove(t_EntBatchReg);
            //4.学生自动注册学校当前批次，生成学生实习号
            var t_StuBatchReg = stuPracBatch;
            Db.T_StuBatchReg.Remove(t_StuBatchReg);
            //5.企业利用企业实习号自动提供岗位, 生成企业岗位号
            //var t_PracticePosition = practicePosition;
            //Db.T_PracticePosition.Remove(t_PracticePosition);
            //6.学生利用企业岗位号自动报名企业岗位，生成学生志愿号
            var t_PracticeVolunteer = practiceVolunteer;
            Db.T_PracticeVolunteer.Remove(t_PracticeVolunteer);

            //8.改变学生分散实习申请状态并设置申请的批次
            V3.ApplyState = 0;
            V3.PracBatchID =null;
            Db.Entry(V3).State = EntityState.Modified;

            return Db.SaveChanges() > 0;
        }

        public bool OneKeyToPractice(string stuEnterPriseApplyID)
        {
            /*
            string stuUserId, string entNo,
            string positionId,string posDesc,string practiceDept,
            string practiceDivDept,string practiceStartEnd
            */
            //查找学生申请分散实习号
            var t_uapplyDisperseRecord = Db.V3_StuEnterPriseApply.Find(stuEnterPriseApplyID);
            if(t_uapplyDisperseRecord == null)
            {
                throw new StuEnterPriseApplyNotFoundException();
            }
        
            string stuUserId = t_uapplyDisperseRecord.UserID;
            string entNo = t_uapplyDisperseRecord.Ent_No;
            string positionId = t_uapplyDisperseRecord.positionId.ToString();
            string posDesc = t_uapplyDisperseRecord.posDesc;
            string practiceDept = t_uapplyDisperseRecord.practiceDept;
            string practiceDivDept = t_uapplyDisperseRecord.practiceDivDept;
            string practiceStartEnd = t_uapplyDisperseRecord.practiceStart.Value.ToDateTime()+ "到" + t_uapplyDisperseRecord.practiceEnd.Value.ToDateTime();
            /*
             * 分散实习学生企业端比重为0，分数由学校端决定！！！
             * 
             * 1.给学生分配角色(角色:分散实习)
             *  -提前在[权限系统]建立分散实习角色和分散实习菜单
             *  -在[权限系统]中加一个分配角色的接口（参数：userid,roleid）
             * 2.调用获取当前批次的接口获取学校的当前批次号（参数：学校代码）
             *  -如果没有则终止
             * 3.企业自动注册学校当前批次，生成企业实习号
             *  -如果已存在则直接读取
             * 4.学生自动注册学校当前批次，生成学生实习号
             *  -如果已存在则终止
             * 5.企业利用企业实习号自动提供岗位, 生成企业岗位号
             *  -如果已存在则直接读取
             * 6.学生利用企业岗位号自动报名企业岗位，生成学生志愿号
             *  -如果已存在则直接读取
             * 7.利用学生志愿号自动更改志愿状态为双选,同时自动填写部门,分部门信息 
             * 8.改变学生分散实习申请状态并设置申请的批次
             * 
             * flag    note
             * a       企业未加入过分散实习
             * b       企业已加入过分散实习 
             * c       企业未提供过该分散实习岗位 
             */
            var keys = new List<string>();

            const string DisperseRoleId = "81";
            #region 前置条件检测
            //查找学生角色
            var role = Db.T_Role.FirstOrDefault(a => a.RoleID == DisperseRoleId);
            if (role == null)
            {
                throw new RoleNotFoundException();
            }


            //查找岗位
            var position = Db.C_Position.FirstOrDefault(a => a.PositionID == positionId);
            if (position == null)
            {
                throw new PositionNotFoundException();
            }

            //查找企业
            var enterprise = Db.T_Enterprise.FirstOrDefault(a => a.Ent_No == entNo);
            if (enterprise == null)
            {
                throw new EnterpriseNotFoundException();
            }

            //查找学生
            var student = Db.T_Student.FirstOrDefault(a => a.UserID == stuUserId);
            if (student == null || !student.T_Class.SchoolID.HasValue())
            {
                throw new StudentNotFoundException();
            }

            //查找学生所在学校
            var school = Db.T_School.FirstOrDefault(a => a.SchoolID == student.T_Class.SchoolID);
            if (school == null)
            {
                throw new SchoolNotFoundException();
            }

            //查找当前批次号
            var pracBatch = Db.T_PracBatch.FirstOrDefault(a =>
            a.SchoolID == school.SchoolID &&
            a.IsCurrentBatch == "是" &&
            a.IsActive == 1);
            if (pracBatch == null)
            {
                throw new PracBatchNotFoundException();
            }

            //查找企业注册批次
            var entPracBatch = Db.T_EntBatchReg.FirstOrDefault(a =>
            a.Ent_No == enterprise.Ent_No &&
            a.PracBatchID == pracBatch.PracBatchID);
            if (entPracBatch == null)
            {
                //未找到则自动加入
                entPracBatch= new T_EntBatchReg()
                {
                    EntPracNo = pracBatch.PracBatchID + "-" + enterprise.Ent_No,
                    PracBatchID = pracBatch.PracBatchID,
                    Ent_No = enterprise.Ent_No,
                    RegistTime = DateTime.Now.ToInt(),
                    ApplyStatus = 2
                };
                //throw new EntPracBatchNotFoundException();
            }

            //查找企业提供岗位
            var practicePosition = Db.T_PracticePosition.FirstOrDefault(a =>
             a.EntPracNo == entPracBatch.EntPracNo &&
             a.PositionID == position.PositionID);
            if (practicePosition == null)
            {
                //未找到则自动提供
                practicePosition= new T_PracticePosition()
                {
                    EntPracNo = entPracBatch.EntPracNo,
                    PositionID = position.PositionID,
                    PubDate = DateTime.Now.ToInt(),
                    PosDesc = posDesc,
                    PosQuantity = 1,
                    PosExpireDate = DateTime.Now.AddDays(20).ToInt()
                }; 
                //throw new PracticePositionNotFoundException();
            }

            //查找学生实习号
            var stuPracBatch = Db.T_StuBatchReg.FirstOrDefault(a =>
            a.UserID == stuUserId &&
            a.PracBatchID == pracBatch.BatchID);
            if (stuPracBatch != null)
            {
                throw new StuPracBatchAlreadyExsitException();
            }

            //查找学生企业实习号(志愿号)
            var practiceVolunteer = Db.T_PracticeVolunteer.FirstOrDefault(a =>
            a.PracticeNo == pracBatch.PracBatchID &&
            a.EntPracNo == practicePosition.EntPracNo &&
            a.PosID == practicePosition.PositionID);
            if (practiceVolunteer != null)
            {
                throw new PracticeVolunteerAlreadyExsitException();
            }

         
            #endregion
            try
            {
                //1.给学生分配角色(角色:分散实习)
                var t_UserRole = new T_UserRole()
                {
                    UserID = stuUserId,
                    RoleID = role.RoleID,
                    Note = "分散实习自动分配"
                };
                var t_UserRole2 = new T_UserRole
                {
                    UserID = stuUserId,
                    RoleID = "61",
                    Note = "分散实习自动分配"
                };

                T_UserRole t_UserRole3 = new T_UserRole
                {
                    UserID = stuUserId,
                    RoleID = "C1",
                    Note = "分散实习自动分配"

                };

                Db.T_UserRole.AddOrUpdate(t_UserRole);
                Db.T_UserRole.AddOrUpdate(t_UserRole2);
                Db.T_UserRole.AddOrUpdate(t_UserRole3);
                //2.调用获取当前批次的接口获取学校的当前批次号（参数：学校代码）
                //3.企业自动注册学校当前批次，生成企业实习号
                var t_EntBatchReg = entPracBatch;
                Db.T_EntBatchReg.AddOrUpdate(t_EntBatchReg);
                //4. (7)学生自动注册学校当前批次，生成学生实习号
                var t_StuBatchReg = new T_StuBatchReg()
                {
                    PracticeNo = student.UserID + pracBatch.PracBatchID,
                    UserID = student.UserID,
                    PracBatchID = pracBatch.PracBatchID,
                    PracticeReport = practiceDept,
                    PracticeDivDept = practiceDivDept,
                    PracticeStartEnd = practiceStartEnd,
                    PracticeCaseScore=0,
                    ReviewScore = 0,
                    StuEvaEntScore=0,
                    StuEvaSchoolScore=0,
                    WeekRecordScore =0,
                    PracUnitComment = "分散实习",
                    CareerStatusID = 13
                };
                Db.T_StuBatchReg.AddOrUpdate(t_StuBatchReg);
                //5.企业利用企业实习号自动提供岗位, 生成企业岗位号
                var t_PracticePosition = practicePosition;
                Db.T_PracticePosition.AddOrUpdate(t_PracticePosition);
                //6.学生利用企业岗位号自动报名企业岗位，生成学生志愿号
                var t_PracticeVolunteer = new T_PracticeVolunteer()
                {
                    PracticeNo = t_StuBatchReg.PracticeNo,
                    EntPracNo = t_PracticePosition.EntPracNo,
                    PosID = t_PracticePosition.PositionID,
                    VolunteerSequence = 1,
                    PosSequence = 1,
                    VolunteerStatus = 5,
                    InterviewTime = DateTime.Now.ToInt(),
                    InterviewRecord = "分散实习",
                    InterviewScore = 100,
                    PreVolStatus = "1"
                };
                Db.T_PracticeVolunteer.AddOrUpdate(t_PracticeVolunteer);

                //8.改变学生分散实习申请状态并设置申请的批次
                t_uapplyDisperseRecord.ApplyState = 1;
                t_uapplyDisperseRecord.PracBatchID = pracBatch.PracBatchID;
                Db.Entry(t_uapplyDisperseRecord).State=EntityState.Modified;

                return Db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}