using System.Configuration;

namespace Qx.Community.Configs
{
   public static class Setting
   {
       public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Repository"].ConnectionString;
   }
}
