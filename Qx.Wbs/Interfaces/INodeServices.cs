using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qx.Tools;
using Qx.Wbs.Entity;
using Qx.Wbs.Models;

namespace Qx.Wbs.Interfaces
{
    public interface INodeServices
    {
        string AddRoot(DataContext dataContext, Wbs_Nodes rootNode);
        bool UpdateRoot(Wbs_Nodes rootNode);
        string AddChild(DataContext dataContext, Wbs_Nodes childNode);
        bool UpdateChild(Wbs_Nodes childNode);
        string ArrangeNode(Wbs_UserNodes userNode);
        bool ArrangeUpdate(Wbs_UserNodes userNode);
        bool ArrangeFinish(Wbs_UserNodes userNode);
        bool ArrangeDelete(string id);
        bool Delete(string id);
        Wbs_UserNodes ArrangeFind(string arrangeId);
        Wbs_Nodes NodeFind(string nodeId);
        IEnumerable<NodeInfo> FindFathers(string nodeId);
        T_User UserFind(string userId);
        ArangeDetail Detail(string userNodeId);
        IEnumerable<Wbs_Nodes> FindRootsByOwner(DataContext dataContext);
        IEnumerable<Wbs_Nodes> FindChildren(DataContext dataContext, string fatherId);
    }
}
