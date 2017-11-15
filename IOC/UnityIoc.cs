using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Microsoft.Practices.Unity;
using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Community.Repository;
using Qx.Community.Services;
using Qx.Permission.Interfaces;
using Qx.Permission.Services;
using Qx.Report;
using Qx.Report.Interfaces;
using Qx.Report.Services;
using Qx.Scsxxt.Core.Services;
using Qx.Scsxxt.Core.Services.Permission;
using Qx.Scsxxt.Core.Services.School;
using Qx.Tools.Interfaces;
using Qx.Wbs.Interfaces;
using Qx.Wbs.Services;
using T_User = Core.Services.Entity.T_User;
using Qx.CPQ.Repository;
using Qx.CPQ.Entity;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Repository;
using Qx.Scsxxt.Extentsion.Interfaces;
using Qx.Scsxxt.Extentsion.Services;
using Button = Qx.Permission.Entity.Button;
using ButtonRepository = Qx.Permission.Repository.ButtonRepository;
using Menu = Qx.Permission.Entity.Menu;
using MenuRepository = Qx.Permission.Repository.MenuRepository;
using Role = Qx.Permission.Entity.Role;
using RoleButtonForbid = Qx.Permission.Entity.RoleButtonForbid;
using RoleButtonForbidRepository = Qx.Permission.Repository.RoleButtonForbidRepository;
using RoleMenu = Qx.Permission.Entity.RoleMenu;
using RoleMenuRepository = Qx.Permission.Repository.RoleMenuRepository;
using RoleRepository = Qx.Permission.Repository.RoleRepository;
using User = Qx.Permission.Entity.User;
using UserRepository = Qx.Permission.Repository.UserRepository;
using UserRole = Qx.Permission.Entity.UserRole;
using UserRoleRepository = Qx.Permission.Repository.UserRoleRepository;
using Qx.Wbs.Hqu.Interfaces;
using Qx.Wbs.Hqu.Services;
using Qx.Wbs.Hqu.Entity;
using Djk.VipCard.Repository;
using Core.Interfaces;
using Qx.Scsxxt.Extentsion.Repository;

namespace Qx.Tools.Ioc.Unity
{
    public static class UnityIoc
    {
        public static void Register(List<Type> controllers)
        {
            //Container
            IUnityContainer container = new UnityContainer();
            //Register controllers
            controllers.ForEach(c => container.RegisterType(c));
            //Register Services
            RegisterServices(container);
            //Resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IWbsProvider, WbsProvider>();
            container.RegisterType<IWbsService, WbsService>();
            container.RegisterType<IRepository<QuantifyTaskCompletion>, QuantifyTaskCompletionRepository>();
            container.RegisterType<IRepository<QuantifyTask>, QuantifyTaskRepository>();
            container.RegisterType<IRepository<QuotaTask>, QuotaTaskRepository>();
            container.RegisterType<IRepository<QuotaTaskStaffDistribution>, QuotaTaskStaffDistributionRepository>();
            container.RegisterType<IRepository<ScoringRule>, ScoringRuleRepository>();
            container.RegisterType<IRepository<WbsTask>, WbsTaskRepository>();
            container.RegisterType<IRepository<Staff>, StaffRepository>();
            container.RegisterType<INodeServices, NodeServices>();
            container.RegisterType<IRepository<CheckRecord>, CheckRecordRepository>();
            #region v3
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.C_Position>, C_PositionRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.V3_EnterpriseApply>, V3_EnterpriseApplyRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.V3_StuEnterPriseApply>, V3_StuEnterPriseApplyRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.C_EntCategory>, C_EntCategoryRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.C_EntRank>, C_EntRankRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_StuScoreApply>, StuScoreApplyRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_StuScoreRecord>, StuScoreRecordRepository>();
            container.RegisterType<IV3Services, V3Services>();
            #endregion
            #region BBS Service
            container.RegisterType<IDashboardService, DashboardService>();
            container.RegisterType<IDiaryService, DiaryService>();
            container.RegisterType<IEnvelopeService, EnvelopeService>();
            container.RegisterType<IForumService, ForumService>();
            container.RegisterType<IFriendService, FriendService>();
            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IPhotoService, PhotoService>();
            container.RegisterType<IPostService, PostService>();
            container.RegisterType<IShareService, ShareService>();
            container.RegisterType<IThemeService, ThemeService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IVisitorService, VisitorService>();
            #endregion

            #region BBS Repository
            container.RegisterType<IRepository<BBS_C_Friend>, C_FriendRepository>();
            container.RegisterType<IRepository<BBS_C_PostState>, C_PostStateRepository>();
            container.RegisterType<IRepository<BBS_C_Share>, C_ShareRepository>();
            container.RegisterType<IRepository<BBS_C_UserGrade>, C_UserGradeRepository>();
            container.RegisterType<IRepository<BBS_C_UserState>, C_UserStateRepository>();
            container.RegisterType<IRepository<BBS_Columns>, ColumnsRepository>();
            container.RegisterType<IRepository<BBS_DiaryReply>, DiaryReplyRepository>();
            container.RegisterType<IRepository<BBS_Diary>, DiaryRepository>();
            container.RegisterType<IRepository<BBS_ForumManager>, ForumManagerRepository>();
            container.RegisterType<IRepository<BBS_Forum>, ForumRepository>();
            container.RegisterType<IRepository<BBS_Friend>, FriendRepository>();
            container.RegisterType<IRepository<BBS_Note>, NoteRepository>();
            container.RegisterType<IRepository<BBS_Photo>, PhotoRepository>();
            container.RegisterType<IRepository<BBS_Post>, PostRepository>();
            container.RegisterType<IRepository<BBS_ReplyPost>, ReplyPostRepository>();
            container.RegisterType<IRepository<BBS_SearchKeyWord>, SearchKeyWordRepository>();
            container.RegisterType<IRepository<BBS_Share>, ShareRepository>();
            container.RegisterType<IRepository<BBS_StepPraise>, StepPraiseRepository>();
            container.RegisterType<IRepository<BBS_Theme>, ThemeRepository>();
            container.RegisterType<IRepository<BBS_Users>, UsersRepository>();
            container.RegisterType<IRepository<BBS_Visitor>, VisitorRepository>();
            container.RegisterType<IRepository<BBS_ReplyParise>, ReplyPraiseRepository>();

            
            #endregion

            #region Permission Repository
            container.RegisterType<IRepository<Button>, ButtonRepository>();
            container.RegisterType<IRepository<Menu>, MenuRepository>();
            container.RegisterType<IRepository<RoleButtonForbid>, RoleButtonForbidRepository>();
            container.RegisterType<IRepository<RoleMenu>, RoleMenuRepository>();
            container.RegisterType<IRepository<Role>, RoleRepository>();
            container.RegisterType<IRepository<User>, UserRepository>();
            container.RegisterType<IRepository<UserRole>, UserRoleRepository>();


            #endregion

        

            #region Cpq Repository
            container.RegisterType<IRepository<CPQ_T_AnswerSheet>, AnswerSheetRespository>();
            container.RegisterType<IRepository<CPQ_T_AttachQuestionToQuestionnaire>, AttachQuestionToQuestionnaireRespository>();
            container.RegisterType<IRepository<CPQ_C_QuestionDomain>, C_QuestionDomainRespository>();
            container.RegisterType<IRepository<CPQ_C_QuestionnaireDomain>, C_QuestionnaireDomainRespository>();
            container.RegisterType<IRepository<CPQ_C_QuestionnaireStatus>, C_QuestionnaireStatusRespository>();
            container.RegisterType<IRepository<CPQ_C_QuestionType>, C_QuestionTypeRespository>();
            container.RegisterType<IRepository<CPQ_C_Share>, C_ShareRespository>();
            container.RegisterType<IRepository<CPQ_T_Questionnaire>, QuestionnaireRespository>();
            container.RegisterType<IRepository<CPQ_T_QuestionOption>, QuestionOptionRespository>();
            container.RegisterType<IRepository<CPQ_T_Question>, QuestionRespository>();

            #endregion

        
            container.RegisterType<IAppService, AppService>();
            container.RegisterType<TAppInterface, TAppService>();

            container.RegisterType<INodeServices, NodeServices>();
            
            container.RegisterType<IReportServices, ReportServices>();
            container.RegisterType<Qx.Report.Scsxxt.Interfaces.IReportServices, Qx.Report.Scsxxt.Services.ReportServices>();
           
            container.RegisterType<IPermission, PermissionServices>();
            container.RegisterType<Qx.Permission.Scsxxt.Interfaces.IPermission, Qx.Permission.Scsxxt.Services.PermissionServices>();

            container.RegisterType<IDisperseServices, DisperseServices>();

            #region Scsxxt Repository
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_User>, Qx.Scsxxt.Extentsion.Repository.UserRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_Enterprise_ToCheck>, Qx.Scsxxt.Extentsion.Repository.EnterpriseToCheckRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_StuBatchReg_Extent>, Qx.Scsxxt.Extentsion.Repository.StuBatchRegExtentRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_StuBatchReg>, Qx.Scsxxt.Extentsion.Repository.StuBatchRegRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_Student>, Qx.Scsxxt.Extentsion.Repository.StudentRepository>();
            container.RegisterType<IRepository<Qx.Scsxxt.Extentsion.Entity.T_Enterprise>, Qx.Scsxxt.Extentsion.Repository.EnterpriseRepository>();

            #endregion

        }

    }
}
