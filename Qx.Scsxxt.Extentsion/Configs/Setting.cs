using Qx.Tools;
using System.Configuration;

namespace Qx.Scsxxt.Extentsion.Configs
{
    public static class Setting
    {
        public static string ConnectionString
        {
            get
            {
                return QxConfigs.IsUnitTest ? "name=Qx.Scsxxt.Extentsion" :
                    ConfigurationManager.ConnectionStrings["Qx.Scsxxt.Extentsion"].ConnectionString;
            }
        }
}
}