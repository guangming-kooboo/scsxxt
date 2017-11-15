﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Report.Configs
{
   public static class Setting
   {
       public static readonly string ReportRuleConnectionString = ConfigurationManager.ConnectionStrings["Repository"].ConnectionString;
       public static readonly string ReportDataConnectionString = ConfigurationManager.ConnectionStrings["Repository"].ConnectionString;
   }
}
