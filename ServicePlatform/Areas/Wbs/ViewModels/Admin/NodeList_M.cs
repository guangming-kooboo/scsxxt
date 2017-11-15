using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.Admin
{
    public class NodeList_M
    {
        public static NodeList_M ToViewModel(string nodeid)
        {
            return new NodeList_M() {nodeid=nodeid};
        }
        public string nodeid;
    }
}