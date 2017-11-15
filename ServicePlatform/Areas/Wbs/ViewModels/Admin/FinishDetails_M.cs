using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Wbs.Entity;

namespace ServicePlatform.Areas.Wbs.ViewModels.Admin
{
    public class FinishDetails_M
    {
        public static FinishDetails_M ToViewModel( Wbs_Nodes node)
        {
            return new FinishDetails_M() {node = node };
        }
        public Wbs_UserNodes usernode;
        public Wbs_Nodes node;
    }
}