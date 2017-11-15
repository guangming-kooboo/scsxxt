--所有任务列表  
select NodeName,Decription 
from dbo.WBS_Nodes 
where (Owner='10385') and (FatherNodeID='0')

--子任务列表
select a.NodeName,a.Decription,a.NodeWeight,b.NodeName
from dbo.WBS_Nodes as a,dbo.WBS_Nodes  as b
where (a.FatherNodeID = b.NodeID) and (b.Owner='10385') and (b.FatherNodeID='0')


--人员分配 账号;姓名;工作量;
SELECT   dbo.T_User.UserID, dbo.T_User.NickName, dbo.Wbs_UserNodes.UserWeight, dbo.Wbs_UserNodes.SerialID
FROM      dbo.Wbs_Nodes INNER JOIN
                dbo.Wbs_UserNodes ON dbo.Wbs_Nodes.NodeID = dbo.Wbs_UserNodes.NodeID INNER JOIN
                dbo.Wbs_Users ON dbo.Wbs_UserNodes.UserID = dbo.Wbs_Users.UserID INNER JOIN
                dbo.T_User ON dbo.Wbs_UserNodes.UserID = dbo.T_User.UserID
WHERE   (dbo.Wbs_UserNodes.NodeID = '{0}')

--文档列表工作量 账号;姓名;任务名称;预分配工作量占总比;实际工作量占总比;证明材料
select 
a.UserID as "账号",
d.NickName as "姓名",
b.NodeName as "任务名称",
a.UserWeight*b.NodeWeight*c.NodeWeight as "预分配工作量占总比",
a.RealWeight*b.NodeWeight*c.NodeWeight as "实际工作量占总比",
a.Materal as "证明材料"
from 
dbo.WBS_UserNodes as a,
dbo.WBS_Nodes as b,
dbo.WBS_Nodes as c,
dbo.T_User AS d
where 
(a.NodeID = b.NodeID ) and 
(b.FatherNodeID = c.NodeID) and 
(b.Owner LIKE '{0}') AND
d.UserID = a.UserID

--人员负责的所有工作 账号;姓名;预分配工作量占总比;实际工作量占总比
select 
a.UserID as "账号",
d.NickName as "姓名",
a.UserWeight*b.NodeWeight*c.NodeWeight as "预分配工作量占总比",
a.RealWeight*b.NodeWeight*c.NodeWeight as "实际工作量占总比"
from 
dbo.WBS_UserNodes as a,
dbo.WBS_Nodes as b,
dbo.WBS_Nodes as c,
dbo.T_User AS d
where 
(a.NodeID = b.NodeID ) and 
(b.FatherNodeID = c.NodeID) and 
(b.Owner LIKE '{0}') AND
d.UserID = a.UserID AND
a.UserID LIKE '{1}'