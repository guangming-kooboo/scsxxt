using System.Configuration;

namespace Qx.Report.Scsxxt.Configs
{
    public static class Setting
    {
        public static readonly string ReportRuleConnectionString =
            ConfigurationManager.ConnectionStrings["Qx.System"].ConnectionString;
    }
}