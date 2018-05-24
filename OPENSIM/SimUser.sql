USE [OPENSIM]
GO

/****** Object:  Table [dbo].[SimUser]    Script Date: 01/06/2014 22:32:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SimUser](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[last_name] [varchar](50) NULL,
	[first_name] [varchar](50) NULL,
	[user_type_id] [varchar](3) NULL,
	[class] [smallint] NULL,
	[class_id] [smallint] NULL,
	[gender] [char](1) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

