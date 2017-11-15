using Qx.Wbs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.Admin
{
    public class AllNodeList_M
    {
        public static AllNodeList_M ToViewModel(Wbs_Nodes node)
        {
            return new AllNodeList_M() { node = node };
        }
        public Wbs_Nodes node;
    }
}