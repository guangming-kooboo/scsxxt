--所有任务列表
select NodeName,Decription 
from dbo.WBS_Nodes 
where (Owner Like '{0}') and (FatherNodeID Like '0')

--子任务列表
select a.NodeName,a.Decription,a.NodeWeight,b.NodeName
from dbo.WBS_Nodes as a,dbo.WBS_Nodes  as b
where (a.FatherNodeID  Like  b.NodeID) and (b.Owner Like '{0}') and (b.FatherNodeID Like '0')

--文档列表工作量
select a.UserID,a.UserWeight*b.NodeWeight*c.NodeWeight as "预分配工作量占总比",a.RealWeight*b.NodeWeight*c.NodeWeight as "实际工作量占总比"
from dbo.WBS_UserNodes as a,dbo.WBS_Nodes as b,dbo.WBS_Nodes as c
where (a.NodeID  Like  b.NodeID ) and (b.FatherNodeID  Like  c.NodeID) and (b.Owner Like '{0}')

--文档列表预分配工作量
select a.UserID,a.UserWeight*b.NodeWeight*c.NodeWeight as "预分配工作量占总比"
from dbo.WBS_UserNodes as a,dbo.WBS_Nodes as b,dbo.WBS_Nodes as c
where (a.NodeID  Like  b.NodeID ) and (b.FatherNodeID  Like  c.NodeID) and (b.Owner Like '{0}')

--文档列表实际工作量
select a.UserID,a.RealWeight*b.NodeWeight*c.NodeWeight as "实际工作量占总比"
from dbo.WBS_UserNodes as a,dbo.WBS_Nodes as b,dbo.WBS_Nodes as c
where (a.NodeID  Like  b.NodeID ) and (b.FatherNodeID  Like  c.NodeID) and (b.Owner Like '{0}')