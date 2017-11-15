using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Lib
{
    public class UnitStatus
    {
        public const int UnComitted = 1;
        public const int WaitingAudit = 2;
        public const int Approved = 3;
        public const int Suspend = 4;
        public const int Respring = 5;
    }

    public class EditStatus
    {
        public const int UnLock = -1;
        public const int Locked = 1;
    }


#region 内容子系统相关常量
    public class ContentType
    {
        public const int News = 11;//新闻
        public const int AD = 21;//广告
        public const int DownLoadFile = 31;//下载文件
        public const int Stamp = 41;//下载文件
        public const int Signature = 51;//下载文件
    }

    public class NewsState
    {
        private const int STATE_WAITCHECK = 1;//新闻FlowState属性 1=>待审核
        private const int STATE_PASSCHECK = 2;//新闻FlowState属性 2=>通过
        private const int STATE_BACKCHECK = 3;//新闻FlowState属性 3=>驳回
    }

#endregion 内容子系统相关常量

#region 权限子系统相关常量

    public class SubSystem
    {
        public const string PLATFORM = "平台端";//平台端
        public const string SCHOOL = "学校端";//广告
        public const string ENTERPRISE = "企业端";//下载文件

    }

    public class UserTypeConst
    {
        public const int SchoolUser = 5;
        public const int EnterpriseUser = 3;
 
    }

    public class UserRoleConst
    {
        public const string SchoolAdmin = "5";
        public const string EnterpriseAdmin = "3";
        public const string CommonUser = "C1";
        public const string CurrentStudent = "61";
        public const string PastStudent = "62";

    }

#endregion 权限子系统相关常量

    public class DefaultValueConst 
    {
        public const string CountyID = "-1";
        public const int EntState = 1;
        public const string EntRank = "-1";
        public const string EntCategoryID = "-1";
        public const int FacStatus = 0;
        public const int FacProPos = 0;

    }

}