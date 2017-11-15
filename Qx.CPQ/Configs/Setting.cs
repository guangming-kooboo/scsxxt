using System.Configuration;

namespace Qx.CPQ.Configs
{
   public static class Setting
   {
       public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Repository"].ConnectionString;
     
   }
}
