using System.Collections.Generic;

namespace Qx.Scsxxt.Extentsion.Interfaces
{
    public interface IDisperseServices
    {
        /// <summary>
        /// 一键进入分散实习
        /// </summary>
        /// <param name="stuUserId">学生用户id</param>
        /// <param name="entNo">企业号</param>
        /// <param name="positionId">岗位号</param>
        /// <param name="posDesc">岗位描述</param>
        /// <param name="practiceDept">实习部门</param>
        /// <param name="practiceDivDept">实习分部门</param>
        /// <param name="practiceStartEnd">实习开始结束时间</param>
        /// <returns></returns>
         bool OneKeyToPractice(string stuEnterPriseApplyID);
        bool OneKeyToPractice_RoleBack(string stuEnterPriseApplyID);
        bool AgreeStuEnterprise(string Ent_No, string Ent_Name, string EntCategoryID,
           string EntRank,string EntAddress, string UserID, int RegisterTime,
           int UpdateTime, string Email, string EntResume, string Contectinfo);


    }
}