using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Wbs.Entity;

namespace ServicePlatform.Areas.Wbs.ViewModels.Admin
{
    public class ReportList_M
    {
        public static ReportList_M ToViewModel(Wbs_Nodes node)
        {
            return new ReportList_M() { node=node};
        }
        public Wbs_Nodes node;
    }
}