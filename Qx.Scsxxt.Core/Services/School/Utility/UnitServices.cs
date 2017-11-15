using Core.Services;
using Core.Services.Entity;
using Core.Services.Entity.ViewModels;

namespace Qx.Scsxxt.Core.Services.Utility
{
    public class UnitServices : BaseServices
    {
        private IDML<T_School> SchoolServices;
        private IDML<T_Enterprise> EnterpriseServices;
        public UnitServices()
        {
            SchoolServices = ServicesFactory.GetSevers<T_School>();
            EnterpriseServices = ServicesFactory.GetSevers<T_Enterprise>();
        }

        //获取总分
        public V_UnitCollection GetUnitList()
        {
            var dest =new V_UnitCollection();
            dest.EnterpriseList = EnterpriseServices.All(a => a.EntState == 3);
            dest.SchoolList = SchoolServices.All(a => a.Status == 3);//C_UnitStatus
            return dest;
        }

     

     

    }
}