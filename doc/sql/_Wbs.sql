--���������б�
select NodeName,Decription 
from dbo.WBS_Nodes 
where (Owner Like '{0}') and (FatherNodeID Like '0')

--�������б�
select a.NodeName,a.Decription,a.NodeWeight,b.NodeName
from dbo.WBS_Nodes as a,dbo.WBS_Nodes  as b
where (a.FatherNodeID  Like  b.NodeID) and (b.Owner Like '{0}') and (b.FatherNodeID Like '0')

--�ĵ��б�����
select a.UserID,a.UserWeight*b.NodeWeight*c.NodeWeight as "Ԥ���乤����ռ�ܱ�",a.RealWeight*b.NodeWeight*c.NodeWeight as "ʵ�ʹ�����ռ�ܱ�"
from dbo.WBS_UserNodes as a,dbo.WBS_Nodes as b,dbo.WBS_Nodes as c
where (a.NodeID  Like  b.NodeID ) and (b.FatherNodeID  Like  c.NodeID) and (b.Owner Like '{0}')

--�ĵ��б�Ԥ���乤����
select a.UserID,a.UserWeight*b.NodeWeight*c.NodeWeight as "Ԥ���乤����ռ�ܱ�"
from dbo.WBS_UserNodes as a,dbo.WBS_Nodes as b,dbo.WBS_Nodes as c
where (a.NodeID  Like  b.NodeID ) and (b.FatherNodeID  Like  c.NodeID) and (b.Owner Like '{0}')

--�ĵ��б�ʵ�ʹ�����
select a.UserID,a.RealWeight*b.NodeWeight*c.NodeWeight as "ʵ�ʹ�����ռ�ܱ�"
from dbo.WBS_UserNodes as a,dbo.WBS_Nodes as b,dbo.WBS_Nodes as c
where (a.NodeID  Like  b.NodeID ) and (b.FatherNodeID  Like  c.NodeID) and (b.Owner Like '{0}')