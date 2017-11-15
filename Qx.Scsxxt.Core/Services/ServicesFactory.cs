using System;
using Qx.Scsxxt.Core.Services.Common;
using Qx.Scsxxt.Core.Services.Enterprise;
using Qx.Scsxxt.Core.Services.Permission;
using Qx.Scsxxt.Core.Services.School;
using UserServices = Qx.Scsxxt.Core.Services.Permission.UserServices;

namespace Core.Services
{
    public static class ServicesFactory
    {
        public static IDML<T> GetSevers<T>()
        {
            switch (typeof(T).Name)
            {
                case "T_SchoolReviewItem":
                    return (IDML<T>)new SchoolReviewItemServices();
                case "T_PracBatch":
                    return (IDML<T>)new PracBatchServices();
                case "T_SchoolReviewerTask":
                    return (IDML<T>)new SchoolReviewerTaskServices();
                case "T_Faculty":
                    return (IDML<T>)new FacultyServices();
                case "T_Student":
                    return (IDML<T>)new StudentServices();
                case "T_StuBatchReg":
                    return (IDML<T>)new StuBatchRegServices();
                case "T_School":
                    return (IDML<T>)new SchoolServices();
                case "C_Specialty":
                    return (IDML<T>)new SpecialtyServices();
                case "T_Class":
                    return (IDML<T>)new ClassServices();


                case "T_Employee":
                    return (IDML<T>)new EmployeeServices();
                case "T_EntReviewerTask":
                    return (IDML<T>)new EntReviewerTaskServices();
                case "T_EntReviewItem":
                    return (IDML<T>)new EntReviewItemServices();
                case "T_EntStudentReviewLink":
                    return (IDML<T>)new EntStudentReviewLinkServices();
                case "T_SchoolStudentReviewLink":
                    return (IDML<T>)new SchoolStudentReviewLinkServices();
                case "T_EntBatchReg":
                    return (IDML<T>)new EntBatchRegServices();    
                case "T_PracticeVolunteer":
                    return (IDML<T>)new PracticeVolunteerServices();
                case "T_PracticePosition":
                    return (IDML<T>)new PracticePositionServices();
                case "T_Enterprise":
                    return (IDML<T>)new EnterpriseServices();
                case "C_Position":
                    return (IDML<T>)new PositionServices();

                case "T_DownLoadFiles":
                    return (IDML<T>)new DownLoadFileServices();
                case "T_HomePageContent":
                    return (IDML<T>)new HomePageContentServices();
                case "T_HomePageColumn":
                    return (IDML<T>)new HomePageColumnServices();
                case "T_ReportData":
                    return (IDML<T>)new ReportDataServices();
                    



                case "T_FuncObject":
                    return (IDML<T>)new FuncObjectServices();
                case "T_Module":
                    return (IDML<T>)new ModuleServices();
                case "T_RoleModule":
                    return (IDML<T>)new RoleModuleServices();
                case "T_Role":
                    return (IDML<T>)new RoleServices();
                case "T_UserRole":
                    return (IDML<T>)new UserRoleServices();
                case "T_User":
                    return (IDML<T>)new UserServices();
                case "T_Platformer":
                    return (IDML<T>)new PlatformerServices();

                

                default: throw new NotImplementedException("请联系开发人员解决：位置Core.Services.ServicesFactory\n信息："+ typeof(T).Name);
            }

        }
    }
}