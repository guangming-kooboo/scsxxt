using System.Configuration;

namespace Qx.Permission.Scsxxt.Configs
{
   public static class Setting
   {
       public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Qx.Permission"].ConnectionString;
   }
}
