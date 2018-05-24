USE [OPENSIM]
GO

/****** Object:  Table [dbo].[Encounter]    Script Date: 01/06/2014 22:32:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Encounter](
	[encounter_id] [bigint] NULL,
	[room_id] [bigint] NULL,
	[team_id] [bigint] NULL,
	[student_id] [bigint] NULL,
	[faculty_id] [bigint] NULL,
	[sp_id] [bigint] NULL,
	[case_id] [bigint] NULL,
	[session_id] [bigint] NULL
) ON [PRIMARY]

GO

