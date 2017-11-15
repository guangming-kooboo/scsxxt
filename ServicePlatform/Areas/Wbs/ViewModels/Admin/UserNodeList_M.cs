using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Wbs.Entity;

namespace ServicePlatform.Areas.Wbs.ViewModels.Admin
{
    public class UserNodeList_M
    {
        public static UserNodeList_M ToViewModel(Wbs_Nodes node)
        {
            return new UserNodeList_M() { node = node };
        }
        public Wbs_Nodes node;
    }
}