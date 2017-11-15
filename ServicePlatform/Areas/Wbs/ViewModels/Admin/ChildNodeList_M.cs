using Qx.Wbs.Entity;
using Qx.Wbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.Admin
{
    public class ChildNodeList_M
    {
        public static ChildNodeList_M ToViewModel(IEnumerable<NodeInfo> nodelist)
        {
            nodelist=nodelist.Reverse();
            return new ChildNodeList_M() { nodes = nodelist.ToList() };
        }
        public List<NodeInfo> nodes;
    }
}