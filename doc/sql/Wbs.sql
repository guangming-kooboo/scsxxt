--���������б�  
select NodeName,Decription 
from dbo.WBS_Nodes 
where (Owner='10385') and (FatherNodeID='0')

--�������б�
select a.NodeName,a.Decription,a.NodeWeight,b.NodeName
from dbo.WBS_Nodes as a,dbo.WBS_Nodes  as b
where (a.FatherNodeID = b.NodeID) and (b.Owner='10385') and (b.FatherNodeID='0')


--��Ա���� �˺�;����;������;
SELECT   dbo.T_User.UserID, dbo.T_User.NickName, dbo.Wbs_UserNodes.UserWeight, dbo.Wbs_UserNodes.SerialID
FROM      dbo.Wbs_Nodes INNER JOIN
                dbo.Wbs_UserNodes ON dbo.Wbs_Nodes.NodeID = dbo.Wbs_UserNodes.NodeID INNER JOIN
                dbo.Wbs_Users ON dbo.Wbs_UserNodes.UserID = dbo.Wbs_Users.UserID INNER JOIN
                dbo.T_User ON dbo.Wbs_UserNodes.UserID = dbo.T_User.UserID
WHERE   (dbo.Wbs_UserNodes.NodeID = '{0}')

--�ĵ��б����� �˺�;����;��������;Ԥ���乤����ռ�ܱ�;ʵ�ʹ�����ռ�ܱ�;֤������
select 
a.UserID as "�˺�",
d.NickName as "����",
b.NodeName as "��������",
a.UserWeight*b.NodeWeight*c.NodeWeight as "Ԥ���乤����ռ�ܱ�",
a.RealWeight*b.NodeWeight*c.NodeWeight as "ʵ�ʹ�����ռ�ܱ�",
a.Materal as "֤������"
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

--��Ա��������й��� �˺�;����;Ԥ���乤����ռ�ܱ�;ʵ�ʹ�����ռ�ܱ�
select 
a.UserID as "�˺�",
d.NickName as "����",
a.UserWeight*b.NodeWeight*c.NodeWeight as "Ԥ���乤����ռ�ܱ�",
a.RealWeight*b.NodeWeight*c.NodeWeight as "ʵ�ʹ�����ռ�ܱ�"
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