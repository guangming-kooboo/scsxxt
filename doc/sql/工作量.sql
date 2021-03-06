USE [SCSXXT_DEV]
GO
/****** Object:  Table [dbo].[Wds_Nodes]    Script Date: 2016/8/1 16:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Wds_Nodes](
	[NodeID] [int] NOT NULL,
	[NodeName] [varchar](30) NULL,
	[FatherNodeID] [int] NULL,
	[Decription] [varchar](200) NULL,
	[NodeWeight] [float] NULL,
 CONSTRAINT [PK_WBS Nodes] PRIMARY KEY CLUSTERED 
(
	[NodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Wds_UserNodes]    Script Date: 2016/8/1 16:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Wds_UserNodes](
	[SerialID] [int] NOT NULL,
	[NodeID] [int] NULL,
	[UserID] [varchar](50) NULL,
	[UserWeight] [float] NULL,
	[RealWeight] [float] NULL,
	[Materal] [varchar](50) NULL,
 CONSTRAINT [PK_WBS UserNodes] PRIMARY KEY CLUSTERED 
(
	[SerialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Wds_Users]    Script Date: 2016/8/1 16:40:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Wds_Users](
	[UserID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_WBS Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Wds_UserNodes]  WITH CHECK ADD  CONSTRAINT [FK_WBS UserNodes_WBS Nodes] FOREIGN KEY([NodeID])
REFERENCES [dbo].[Wds_Nodes] ([NodeID])
GO
ALTER TABLE [dbo].[Wds_UserNodes] CHECK CONSTRAINT [FK_WBS UserNodes_WBS Nodes]
GO
ALTER TABLE [dbo].[Wds_UserNodes]  WITH CHECK ADD  CONSTRAINT [FK_WBS UserNodes_WBS Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Wds_Users] ([UserID])
GO
ALTER TABLE [dbo].[Wds_UserNodes] CHECK CONSTRAINT [FK_WBS UserNodes_WBS Users]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点名字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_Nodes', @level2type=N'COLUMN',@level2name=N'NodeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父节点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_Nodes', @level2type=N'COLUMN',@level2name=N'FatherNodeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_Nodes', @level2type=N'COLUMN',@level2name=N'Decription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_Nodes', @level2type=N'COLUMN',@level2name=N'NodeWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_UserNodes', @level2type=N'COLUMN',@level2name=N'SerialID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_UserNodes', @level2type=N'COLUMN',@level2name=N'NodeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_UserNodes', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预分配' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_UserNodes', @level2type=N'COLUMN',@level2name=N'UserWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_UserNodes', @level2type=N'COLUMN',@level2name=N'RealWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证明材料' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Wds_UserNodes', @level2type=N'COLUMN',@level2name=N'Materal'
GO
