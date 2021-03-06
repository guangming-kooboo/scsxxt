USE [master]
GO
/****** Object:  Database [BBs]    Script Date: 2016/8/2 19:44:04 ******/
CREATE DATABASE [BBs]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BBs', FILENAME = N'E:\SQL\MSSQL11.MSSQLSERVER\MSSQL\DATA\BBs.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BBs_log', FILENAME = N'E:\SQL\MSSQL11.MSSQLSERVER\MSSQL\DATA\BBs_log.ldf' , SIZE = 7040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BBs] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BBs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BBs] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BBs] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BBs] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BBs] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BBs] SET ARITHABORT OFF 
GO
ALTER DATABASE [BBs] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BBs] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BBs] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BBs] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BBs] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BBs] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BBs] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BBs] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BBs] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BBs] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BBs] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BBs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BBs] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BBs] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BBs] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BBs] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BBs] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BBs] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BBs] SET RECOVERY FULL 
GO
ALTER DATABASE [BBs] SET  MULTI_USER 
GO
ALTER DATABASE [BBs] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BBs] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BBs] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BBs] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BBs', N'ON'
GO
USE [BBs]
GO
/****** Object:  Table [dbo].[BBS_C_Friend]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_C_Friend](
	[StatusID] [int] NOT NULL,
	[StatusName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BBS_C_Friend] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_C_PostState]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_C_PostState](
	[StateID] [int] NOT NULL,
	[StateName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BBS_C_Post] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_C_UserGrade]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BBS_C_UserGrade](
	[UserGradeID] [int] NOT NULL,
	[UserGradeName] [nchar](10) NULL,
 CONSTRAINT [PK_BBS_C_UserGrade] PRIMARY KEY CLUSTERED 
(
	[UserGradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BBS_Columns]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Columns](
	[ColumnID] [int] IDENTITY(1,1) NOT NULL,
	[ColumnName] [varchar](20) NOT NULL,
	[FatherColumnID] [int] NULL,
	[ForumID] [int] NOT NULL,
	[ColumnImg] [varchar](50) NULL,
	[ColumnExplain] [varchar](200) NULL,
	[UserID] [varchar](50) NULL,
 CONSTRAINT [PK__Columns__1AA1422FA8E84C9B] PRIMARY KEY CLUSTERED 
(
	[ColumnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_C-Share]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_C-Share](
	[StatusID] [int] NOT NULL,
	[StatusName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BBS_C-Share] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Diary]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Diary](
	[DiaryTitle] [varchar](50) NOT NULL,
	[Contents] [text] NULL,
	[UserID] [varchar](50) NOT NULL,
	[Time] [varchar](20) NULL,
	[DiaryID] [int] NOT NULL,
	[PraiseCount] [int] NULL,
 CONSTRAINT [PK_Log_1] PRIMARY KEY CLUSTERED 
(
	[DiaryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_DiaryReply]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_DiaryReply](
	[UserID] [varchar](50) NOT NULL,
	[Time] [varchar](50) NOT NULL,
	[Contents] [nchar](10) NOT NULL,
	[DiaryID] [int] NOT NULL,
	[DiaryReplyID] [int] NOT NULL,
 CONSTRAINT [PK_BBS_DiaryReply] PRIMARY KEY CLUSTERED 
(
	[DiaryReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Forum]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Forum](
	[ForumID] [int] IDENTITY(1,1) NOT NULL,
	[ForumExplain] [varchar](400) NULL,
	[ForumName] [varchar](20) NOT NULL,
 CONSTRAINT [PK__Forum__E210AC4F4F430F94] PRIMARY KEY CLUSTERED 
(
	[ForumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_ForumManager]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_ForumManager](
	[ForumManagerID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[ForumID] [int] NOT NULL,
 CONSTRAINT [PK_BBS_ForumManager] PRIMARY KEY CLUSTERED 
(
	[ForumManagerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Friend]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Friend](
	[FriendID] [nchar](10) NOT NULL,
	[UserIDA] [varchar](50) NOT NULL,
	[UserIDB] [varchar](50) NOT NULL,
	[StatusID] [int] NULL,
	[Time] [varchar](50) NULL,
 CONSTRAINT [PK_Friend] PRIMARY KEY CLUSTERED 
(
	[FriendID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Note]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Note](
	[NoteContent] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
	[NoteID] [int] NOT NULL,
	[ReceiverUserID] [varchar](50) NULL,
	[NoteTime] [varchar](50) NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Photo]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Photo](
	[Img] [varchar](50) NULL,
	[Time] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
	[PhtotID] [int] NOT NULL,
 CONSTRAINT [PK_BBS_Photo] PRIMARY KEY CLUSTERED 
(
	[PhtotID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Post]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[PostTitle] [varchar](100) NULL,
	[ThemeID] [int] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[PostContent] [text] NOT NULL,
	[PostTime] [varchar](50) NULL,
	[PClickCount] [int] NOT NULL,
	[StateID] [int] NOT NULL,
	[IsTop] [int] NOT NULL,
	[IsCream] [int] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_ReplyPost]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_ReplyPost](
	[ReplyPostID] [int] IDENTITY(1,1) NOT NULL,
	[Time] [varchar](50) NOT NULL,
	[Contents] [varchar](200) NOT NULL,
	[PostID] [int] NOT NULL,
	[UerID] [varchar](50) NOT NULL,
	[ParentReplyID] [int] NULL,
	[IsBest] [int] NOT NULL,
 CONSTRAINT [PK_BBS_ReplyPost_1] PRIMARY KEY CLUSTERED 
(
	[ReplyPostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Share]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Share](
	[ShareID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[Time] [varchar](50) NOT NULL,
	[PostID] [int] NOT NULL,
	[StatusID] [int] NULL,
 CONSTRAINT [PK_BBS_Share] PRIMARY KEY CLUSTERED 
(
	[ShareID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_StepPraise]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_StepPraise](
	[SetpPraiseID] [varchar](50) NOT NULL,
	[PostID] [int] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[StepOrPraise] [int] NOT NULL,
 CONSTRAINT [PK_BBS_StepPraise] PRIMARY KEY CLUSTERED 
(
	[SetpPraiseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Theme]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Theme](
	[ThemeID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeName] [varchar](20) NOT NULL,
	[ColumnID] [int] NOT NULL,
	[ThemeExplain] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
 CONSTRAINT [PK__Theme__FBB3E4B98C28CEE8] PRIMARY KEY CLUSTERED 
(
	[ThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Users]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Users](
	[UserID] [varchar](50) NOT NULL,
	[UserGradeID] [int] NOT NULL,
	[UserState] [varchar](50) NOT NULL,
	[UserPoint] [int] NOT NULL,
	[RegisterDate] [varchar](50) NULL,
	[LastLogin] [varchar](50) NULL,
	[RecentActivite] [varchar](50) NULL,
 CONSTRAINT [PK__Users__1788CCAC181F825C] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BBS_Visitor]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BBS_Visitor](
	[UserIDA] [varchar](50) NOT NULL,
	[UserIDB] [varchar](50) NOT NULL,
	[Time] [varchar](50) NOT NULL,
	[VisitorID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BBS_Visitor_1] PRIMARY KEY CLUSTERED 
(
	[VisitorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SearchKeyWord]    Script Date: 2016/8/2 19:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SearchKeyWord](
	[KeyWord] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BBS_Columns] ADD  CONSTRAINT [DF__Columns__ColumnF__1BFD2C07]  DEFAULT ((-1)) FOR [FatherColumnID]
GO
ALTER TABLE [dbo].[BBS_Post] ADD  CONSTRAINT [DF_Post_AutherID]  DEFAULT ((0)) FOR [UserID]
GO
ALTER TABLE [dbo].[BBS_Post] ADD  CONSTRAINT [DF__Post__ClickCount__24927208]  DEFAULT ((0)) FOR [PClickCount]
GO
ALTER TABLE [dbo].[BBS_Post] ADD  CONSTRAINT [DF_Post_IsTop]  DEFAULT ((0)) FOR [IsTop]
GO
ALTER TABLE [dbo].[BBS_Post] ADD  CONSTRAINT [DF_Post_IsCream]  DEFAULT ((0)) FOR [IsCream]
GO
ALTER TABLE [dbo].[BBS_Users] ADD  CONSTRAINT [DF_Users_UserGrade]  DEFAULT ((0)) FOR [UserGradeID]
GO
ALTER TABLE [dbo].[BBS_Users] ADD  CONSTRAINT [DF__Users__UserPoint__108B795B]  DEFAULT ((0)) FOR [UserPoint]
GO
ALTER TABLE [dbo].[BBS_Columns]  WITH CHECK ADD  CONSTRAINT [FK__Columns__ColumnI__1DE57479] FOREIGN KEY([ForumID])
REFERENCES [dbo].[BBS_Forum] ([ForumID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Columns] CHECK CONSTRAINT [FK__Columns__ColumnI__1DE57479]
GO
ALTER TABLE [dbo].[BBS_Columns]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Columns_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Columns] CHECK CONSTRAINT [FK_BBS_Columns_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_Diary]  WITH CHECK ADD  CONSTRAINT [FK_Log_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Diary] CHECK CONSTRAINT [FK_Log_Users]
GO
ALTER TABLE [dbo].[BBS_DiaryReply]  WITH CHECK ADD  CONSTRAINT [FK_BBS_DiaryReply_BBS_Diary] FOREIGN KEY([DiaryID])
REFERENCES [dbo].[BBS_Diary] ([DiaryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_DiaryReply] CHECK CONSTRAINT [FK_BBS_DiaryReply_BBS_Diary]
GO
ALTER TABLE [dbo].[BBS_DiaryReply]  WITH CHECK ADD  CONSTRAINT [FK_BBS_DiaryReply_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_DiaryReply] CHECK CONSTRAINT [FK_BBS_DiaryReply_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_ForumManager]  WITH CHECK ADD  CONSTRAINT [FK_BBS_ForumManager_BBS_Forum] FOREIGN KEY([ForumID])
REFERENCES [dbo].[BBS_Forum] ([ForumID])
GO
ALTER TABLE [dbo].[BBS_ForumManager] CHECK CONSTRAINT [FK_BBS_ForumManager_BBS_Forum]
GO
ALTER TABLE [dbo].[BBS_ForumManager]  WITH CHECK ADD  CONSTRAINT [FK_BBS_ForumManager_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_ForumManager] CHECK CONSTRAINT [FK_BBS_ForumManager_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_Friend]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Friend_BBS_C_Friend] FOREIGN KEY([StatusID])
REFERENCES [dbo].[BBS_C_Friend] ([StatusID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Friend] CHECK CONSTRAINT [FK_BBS_Friend_BBS_C_Friend]
GO
ALTER TABLE [dbo].[BBS_Friend]  WITH CHECK ADD  CONSTRAINT [FK_Friend_Users] FOREIGN KEY([UserIDA])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Friend] CHECK CONSTRAINT [FK_Friend_Users]
GO
ALTER TABLE [dbo].[BBS_Friend]  WITH CHECK ADD  CONSTRAINT [FK_Friend_Users1] FOREIGN KEY([UserIDB])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Friend] CHECK CONSTRAINT [FK_Friend_Users1]
GO
ALTER TABLE [dbo].[BBS_Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Note] CHECK CONSTRAINT [FK_Note_Users]
GO
ALTER TABLE [dbo].[BBS_Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_Users1] FOREIGN KEY([ReceiverUserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Note] CHECK CONSTRAINT [FK_Note_Users1]
GO
ALTER TABLE [dbo].[BBS_Photo]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Photo_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Photo] CHECK CONSTRAINT [FK_BBS_Photo_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_Post]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Post_BBS_C_Post] FOREIGN KEY([StateID])
REFERENCES [dbo].[BBS_C_PostState] ([StateID])
GO
ALTER TABLE [dbo].[BBS_Post] CHECK CONSTRAINT [FK_BBS_Post_BBS_C_Post]
GO
ALTER TABLE [dbo].[BBS_Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Theme] FOREIGN KEY([ThemeID])
REFERENCES [dbo].[BBS_Theme] ([ThemeID])
GO
ALTER TABLE [dbo].[BBS_Post] CHECK CONSTRAINT [FK_Post_Theme]
GO
ALTER TABLE [dbo].[BBS_Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Post] CHECK CONSTRAINT [FK_Post_Users]
GO
ALTER TABLE [dbo].[BBS_ReplyPost]  WITH CHECK ADD  CONSTRAINT [FK_BBS_ReplyPost_BBS_Post1] FOREIGN KEY([PostID])
REFERENCES [dbo].[BBS_Post] ([PostID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_ReplyPost] CHECK CONSTRAINT [FK_BBS_ReplyPost_BBS_Post1]
GO
ALTER TABLE [dbo].[BBS_ReplyPost]  WITH CHECK ADD  CONSTRAINT [FK_BBS_ReplyPost_BBS_ReplyPost] FOREIGN KEY([ParentReplyID])
REFERENCES [dbo].[BBS_ReplyPost] ([ReplyPostID])
GO
ALTER TABLE [dbo].[BBS_ReplyPost] CHECK CONSTRAINT [FK_BBS_ReplyPost_BBS_ReplyPost]
GO
ALTER TABLE [dbo].[BBS_ReplyPost]  WITH CHECK ADD  CONSTRAINT [FK_BBS_ReplyPost_BBS_Users] FOREIGN KEY([UerID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_ReplyPost] CHECK CONSTRAINT [FK_BBS_ReplyPost_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_Share]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Share_BBS_C-Share] FOREIGN KEY([StatusID])
REFERENCES [dbo].[BBS_C-Share] ([StatusID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Share] CHECK CONSTRAINT [FK_BBS_Share_BBS_C-Share]
GO
ALTER TABLE [dbo].[BBS_Share]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Share_BBS_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[BBS_Post] ([PostID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Share] CHECK CONSTRAINT [FK_BBS_Share_BBS_Post]
GO
ALTER TABLE [dbo].[BBS_Share]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Share_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Share] CHECK CONSTRAINT [FK_BBS_Share_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_StepPraise]  WITH CHECK ADD  CONSTRAINT [FK_BBS_StepPraise_BBS_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[BBS_Post] ([PostID])
GO
ALTER TABLE [dbo].[BBS_StepPraise] CHECK CONSTRAINT [FK_BBS_StepPraise_BBS_Post]
GO
ALTER TABLE [dbo].[BBS_StepPraise]  WITH CHECK ADD  CONSTRAINT [FK_BBS_StepPraise_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_StepPraise] CHECK CONSTRAINT [FK_BBS_StepPraise_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_Theme]  WITH CHECK ADD  CONSTRAINT [FK__Theme__ThemeIn__21B6055D] FOREIGN KEY([ColumnID])
REFERENCES [dbo].[BBS_Columns] ([ColumnID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Theme] CHECK CONSTRAINT [FK__Theme__ThemeIn__21B6055D]
GO
ALTER TABLE [dbo].[BBS_Theme]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Theme_BBS_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Theme] CHECK CONSTRAINT [FK_BBS_Theme_BBS_Users]
GO
ALTER TABLE [dbo].[BBS_Users]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Users_BBS_C_UserGrade] FOREIGN KEY([UserGradeID])
REFERENCES [dbo].[BBS_C_UserGrade] ([UserGradeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Users] CHECK CONSTRAINT [FK_BBS_Users_BBS_C_UserGrade]
GO
ALTER TABLE [dbo].[BBS_Visitor]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Visitor_BBS_Users1] FOREIGN KEY([UserIDB])
REFERENCES [dbo].[BBS_Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BBS_Visitor] CHECK CONSTRAINT [FK_BBS_Visitor_BBS_Users1]
GO
ALTER TABLE [dbo].[BBS_Visitor]  WITH CHECK ADD  CONSTRAINT [FK_BBS_Visitor_BBS_Users2] FOREIGN KEY([UserIDA])
REFERENCES [dbo].[BBS_Users] ([UserID])
GO
ALTER TABLE [dbo].[BBS_Visitor] CHECK CONSTRAINT [FK_BBS_Visitor_BBS_Users2]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目所属版块ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Columns', @level2type=N'COLUMN',@level2name=N'ForumID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Columns', @level2type=N'COLUMN',@level2name=N'ColumnImg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目介绍' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Columns', @level2type=N'COLUMN',@level2name=N'ColumnExplain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目发布人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Columns', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Columns'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Diary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'板块表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Forum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'=UserID+ForumID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_ForumManager', @level2type=N'COLUMN',@level2name=N'ForumManagerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版主ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_ForumManager', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版块ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_ForumManager', @level2type=N'COLUMN',@level2name=N'ForumID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'=UserIDA+UserIDB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Friend', @level2type=N'COLUMN',@level2name=N'FriendID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0待审核  1同意  -1 拒绝' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Friend', @level2type=N'COLUMN',@level2name=N'StatusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请好友的时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Friend', @level2type=N'COLUMN',@level2name=N'Time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Friend'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Note'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Photo', @level2type=N'COLUMN',@level2name=N'Img'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Photo', @level2type=N'COLUMN',@level2name=N'Time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传的人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Photo', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'PostID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'PostTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属主题ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'ThemeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'PostContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发帖时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'PostTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子点击数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'PClickCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'-2为被禁1为发表状态-1为保存在草稿箱0为待审核' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'StateID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1为置顶' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'IsTop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1为精品贴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post', @level2type=N'COLUMN',@level2name=N'IsCream'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Post'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为最佳回答1为最佳回复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_ReplyPost', @level2type=N'COLUMN',@level2name=N'IsBest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'=UserID+PostID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Share', @level2type=N'COLUMN',@level2name=N'ShareID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1为进行中 2为取消' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Share', @level2type=N'COLUMN',@level2name=N'StatusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'=PosrID+UserID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_StepPraise', @level2type=N'COLUMN',@level2name=N'SetpPraiseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被赞或被踩的帖子ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_StepPraise', @level2type=N'COLUMN',@level2name=N'PostID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赞或踩的人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_StepPraise', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1为赞-1为踩' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_StepPraise', @level2type=N'COLUMN',@level2name=N'StepOrPraise'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞 踩的表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_StepPraise'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属栏目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Theme', @level2type=N'COLUMN',@level2name=N'ColumnID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主题介绍' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Theme', @level2type=N'COLUMN',@level2name=N'ThemeExplain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主题发布人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Theme', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主题表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Theme'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户注册时间注册时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Users', @level2type=N'COLUMN',@level2name=N'RegisterDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Users', @level2type=N'COLUMN',@level2name=N'LastLogin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次活动时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Users', @level2type=N'COLUMN',@level2name=N'RecentActivite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Visitor', @level2type=N'COLUMN',@level2name=N'UserIDA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Visitor', @level2type=N'COLUMN',@level2name=N'UserIDB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'=UserIDA+UserIDB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BBS_Visitor', @level2type=N'COLUMN',@level2name=N'VisitorID'
GO
USE [master]
GO
ALTER DATABASE [BBs] SET  READ_WRITE 
GO
