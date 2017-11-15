using System;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Entity;

namespace Qx.Wbs.Models
{
    public static class NodeExtent
    {
        public static Wbs_Nodes Parse(this RootNode rootNode, string Owner)
        {
            return new Wbs_Nodes()
            {
                NodeID = DateTime.Now.Random().ToString(),//
                Decription = rootNode.Description,
                FatherNodeID = "0",//
                NodeName = rootNode.Name,
                NodeWeight = 1,//
                Owner = Owner//
            };
        }
        public static Wbs_Nodes Parse(this ChildNode childNode, string Owner)
        {
            return new Wbs_Nodes()
            {
                NodeID = DateTime.Now.Random().ToString(),//
                Decription = childNode.Decription,
                FatherNodeID = childNode.FatherNodeID,
                NodeName = childNode.NodeName,
                NodeWeight = childNode.NodeWeight,
                Owner = Owner//
            };
        }
    }
}