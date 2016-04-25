USE [PokerTracker]
GO
/****** Object:  Table [dbo].[CardRooms]    Script Date: 4/23/2016 2:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CardRooms](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[Address] [varchar](64) NULL,
	[City] [varchar](32) NULL,
	[State] [char](2) NULL,
	[ZipCode] [char](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_CardRooms_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Games]    Script Date: 4/23/2016 2:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Games](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](64) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Games_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 4/23/2016 2:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [uniqueidentifier] NOT NULL,
	[CardRoomId] [uniqueidentifier] NOT NULL,
	[SmallBlind] [int] NOT NULL,
	[BigBlind] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
	[Notes] [text] NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[HoursActive] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeEntries]    Script Date: 4/23/2016 2:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeEntries](
	[Id] [uniqueidentifier] NOT NULL, 
	[SessionId] [uniqueidentifier] NOT NULL,
	[RecordedAt] [datetime] NOT NULL,
	[StackSize] [int] NOT NULL,
	[StackDifferential] [int] NULL,
	[DealerTokes] [int] NOT NULL,
	[ServerTips] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_Summary]    Script Date: 4/23/2016 2:26:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[vw_Summary]
AS
SELECT
	s.Id As SessionId,
	cr.Name As Cardroom,
	g.Name As Game,
	CONCAT('$', s.BigBlind, '/$', s.BigBlind*2) As Limit,
	HoursActive As HoursPlayed,
	DATEPART(day, s.StartTime) As [DayOfMonth],
	CASE DATEPART(weekday, s.StartTime)
		WHEN 1 THEN 'U'
		WHEN 2 THEN 'M'
		WHEN 3 THEN 'T'
		WHEN 4 THEN 'W'
		WHEN 5 THEN 'R'
		WHEN 6 THEN 'F'
		WHEN 7 THEN 'S'
	END As [DayOfWeek],
	s.StartTime,
	s.EndTime,
	SUM(te.StackDifferential) As WinLoss,
	ROUND(SUM(COALESCE(te.StackDifferential,0)) / CAST(s.BigBlind AS FLOAT), 2) As WinLossBB,
	ROUND(SUM(COALESCE(te.StackDifferential,0)) / CAST(s.HoursActive AS FLOAT), 2) As HourlyRate,
	ROUND(SUM(COALESCE(te.StackDifferential,0)) / CAST(s.HoursActive AS FLOAT) / s.BigBlind, 2) As HourlyRateBB
FROM
	dbo.[Sessions] s
	INNER JOIN dbo.Games g ON s.GameId = g.Id
	INNER JOIN dbo.CardRooms cr ON s.CardRoomId = cr.Id
	INNER JOIN dbo.TimeEntries te ON te.SessionId = s.Id
WHERE
	s.EndTime IS NOT NULL
GROUP BY
	s.Id, cr.Name, g.Name, s.BigBlind, s.HoursActive, s.StartTime, s.EndTime





GO
ALTER TABLE [dbo].[TimeEntries]  WITH CHECK ADD  CONSTRAINT [FK_TimeEntries_Sessions] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([Id])
GO
ALTER TABLE [dbo].[TimeEntries] CHECK CONSTRAINT [FK_TimeEntries_Sessions]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [CK_BigBlind] CHECK  (([BigBlind]>(0)))
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [CK_BigBlind]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [CK_Sessions_HoursActive] CHECK  ((dateadd(minute,coalesce([HoursActive],(0))*(60),[StartTime])<=[EndTime]))
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [CK_Sessions_HoursActive]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [CK_SmallBlind] CHECK  (([SmallBlind]>(0)))
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [CK_SmallBlind]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [CK_SmallBlind_BigBlind] CHECK  (([SmallBlind]<=[BigBlind]))
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [CK_SmallBlind_BigBlind]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [CK_StartTime_EndTime] CHECK  (([StartTime]<[EndTime]))
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [CK_StartTime_EndTime]
GO
ALTER TABLE [dbo].[TimeEntries]  WITH CHECK ADD  CONSTRAINT [CK_DealerTokes] CHECK  (([DealerTokes]>=(0)))
GO
ALTER TABLE [dbo].[TimeEntries] CHECK CONSTRAINT [CK_DealerTokes]
GO
ALTER TABLE [dbo].[TimeEntries]  WITH CHECK ADD  CONSTRAINT [CK_ServerTips] CHECK  (([ServerTips]>=(0)))
GO
ALTER TABLE [dbo].[TimeEntries] CHECK CONSTRAINT [CK_ServerTips]
GO