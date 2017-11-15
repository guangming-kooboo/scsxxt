using System;

namespace Qx.Report.Scsxxt.Models
{
   public class CrossDbParam
   {
       public int ParamIndex;
       public int OutIndex;
       // public string OutTitle;
        public Func<string, string> Func;
   }
}
