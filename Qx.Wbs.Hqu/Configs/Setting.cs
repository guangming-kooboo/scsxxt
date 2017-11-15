using System.Configuration;

namespace Qx.Wbs.Hqu.Configs
{
   public static class Setting
   {
       public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Qx.Wbs.Hqu"].ConnectionString;
   }
}
