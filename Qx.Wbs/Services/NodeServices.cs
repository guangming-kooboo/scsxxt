using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Qx.Tools;
using Qx.Wbs.Entity;
using Qx.Wbs.Interfaces;
using Qx.Wbs.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Wbs.Services
{


    public class NodeServices :BaseService, INodeServices
    {
        #region Root
        public string AddRoot(DataContext dataContext, Wbs_Nodes rootNode)
        {
            rootNode.NodeID = SerialId;
            rootNode.FatherNodeID = "0";
            rootNode.NodeWeight = 1;
            rootNode.Owner = dataContext.UserID;          
           
            if (Db.Wbs_Nodes.Find(rootNode.NodeID) == null)
            {
                return Db.SaveAdd(rootNode) ? rootNode.NodeID : null;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateRoot(Wbs_Nodes rootNode)
        {
            var old = Db.Wbs_Nodes.Find(rootNode.NodeID);
            
            old.Decription = rootNode.Decription;
            old.NodeName = rootNode.NodeName;
            old.Place = rootNode.Place;
            old.BeginTime = rootNode.BeginTime;
            old.EndTime = rootNode.EndTime;
            return Db.SaveModified(old);
        }
        #endregion

        #region Child
       public  string AddChild(DataContext dataContext, Wbs_Nodes childNode)
        {
            childNode.NodeID = SerialId;
            childNode.Owner = dataContext.UserID;

            if (Db.Wbs_Nodes.Find(childNode.NodeID) == null)
            {
                return Db.SaveAdd(childNode) ? childNode.NodeID : null;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateChild(Wbs_Nodes childNode)
        {
            var old = Db.Wbs_Nodes.Find(childNode.NodeID);
            if (old != null)
            {
                old.Decription = childNode.Decription;
                old.NodeName = childNode.NodeName;
                old.NodeWeight = childNode.NodeWeight;
                old.Place = childNode.Place;
                old.BeginTime = childNode.BeginTime;
                old.EndTime = childNode.EndTime;
                return Db.SaveModified(old);
            }
            else
            {
                return false;
            }
            
        }
        #endregion

        #region ArrangeNode
        public string ArrangeNode( Wbs_UserNodes userNode)
        {
            userNode.SerialID = userNode.UserID+"-"+userNode.NodeID;
            userNode.FinishTime = DateTime.MaxValue;
            userNode.Materal = "未提交";
            userNode.RealWeight = 0;
            //自动接入用户
            if (Db.Wbs_Users.Find(userNode.UserID) == null)
            {
                Db.SaveAdd(new Wbs_Users() {UserID = userNode.UserID });
            }

            if (Db.Wbs_UserNodes.Find(userNode.SerialID) == null)
            {
                return Db.SaveAdd(userNode) ? userNode.SerialID : null;
            }
            else
            {
                return null;
            }
        }
        public bool ArrangeUpdate(Wbs_UserNodes userNode)
        {
            var old = Db.Wbs_UserNodes.Find(userNode.SerialID);
            if (old != null)
            {
                old.Materal = userNode.Materal;
                old.RealWeight = userNode.RealWeight;
                return Db.SaveModified(old);
            }
            else
            {
                return false;
            }
        }
        public bool ArrangeFinish(Wbs_UserNodes userNode)
        {
            var old = Db.Wbs_UserNodes.Find(userNode.SerialID);
            if (old != null)
            {
                old.FinishTime = userNode.FinishTime;
                old.Materal = userNode.Materal;
                old.RealWeight = userNode.RealWeight;
                return Db.SaveModified(old);
            }
            else
            {
                return false;
            }
        }
        public bool ArrangeDelete(string SerialID)
        {
            var old = Db.Wbs_UserNodes.Find(SerialID);
            if (old != null)
            {
                return Db.SaveDelete(old);
            }
            else
            {
                return false;
            }

        }
        #endregion

        public bool Delete(string SerialID)
        {
            var old = Db.Wbs_Nodes.Find(SerialID);
            if (old != null)
            {
                return Db.SaveDelete(old);
            }
            else
            {
                return false;
            }
            
        }


        public Wbs_UserNodes ArrangeFind(string arrangeId)
        {
          return  Db.Wbs_UserNodes.Find(arrangeId);
        }
        public Wbs_Nodes NodeFind(string nodeId)
        {
            return Db.Wbs_Nodes.Find(nodeId);
        }
        public T_User UserFind(string userId)
        {
            return Db.T_User.Find(userId);
        }

        public ArangeDetail Detail(string userNodeId)
        {
            return Db.Wbs_UserNodes.Where(a => a.SerialID == userNodeId).
                Select(b => new ArangeDetail()
                {
                    SerialID = b.SerialID,
                    UserID = b.UserID,
                    UserName = b.Wbs_Users.T_User.NickName,
                    UserWeight = b.UserWeight,
                    RealWeight = b.RealWeight,
                    Materal = b.Materal,
                    FinishTime = b.FinishTime,
                    NodeID = b.NodeID,
                    NodeName = b.Wbs_Nodes.NodeName,
                    FatherNodeID = b.Wbs_Nodes.FatherNodeID,
                    Decription = b.Wbs_Nodes.Decription,
                    NodeWeight = b.Wbs_Nodes.NodeWeight,
                    Owner = b.Wbs_Nodes.Owner,
                    BeginTime = b.Wbs_Nodes.BeginTime,
                    EndTime = b.FinishTime,
                    Place = b.Wbs_Nodes.Place,
                }).FirstOrDefault();
        }

        public IEnumerable<Wbs_Nodes> FindRootsByOwner(DataContext dataContext)
        {
            return Db.Wbs_Nodes.Where(a => a.FatherNodeID == "0"&&a.Owner==dataContext.UserID);
        }

        public IEnumerable<Wbs_Nodes> FindChildren(DataContext dataContext, string fatherId)
        {
            return Db.Wbs_Nodes.Where(a => a.FatherNodeID == fatherId && a.Owner == dataContext.UserID);
        }

        public IEnumerable<NodeInfo> FindFathers(string nodeId)
        {
            var table = Db.Wbs_Nodes.ToList();
            var father = table.FirstOrDefault(a => a.NodeID == nodeId);
            var result = new List<NodeInfo>();
            while (father.FatherNodeID != "0")
            {
                result.Add(new NodeInfo() { 
                FatherNodeID=father.FatherNodeID ,
                 NodeName=father.NodeName,
                 NodeID=father.NodeID
                });
                father = table.FirstOrDefault(a => a.NodeID == father.FatherNodeID);
            }
            result.Add(new NodeInfo()
            {
                FatherNodeID = father.FatherNodeID,
                NodeName = father.NodeName,
                NodeID = father.NodeID
            });
            return result;
        }
    }
}
