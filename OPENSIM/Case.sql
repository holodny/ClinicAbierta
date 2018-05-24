USE [OPENSIM]
GO

/****** Object:  Table [dbo].[Case]    Script Date: 01/06/2014 22:31:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Case](
	[case_id] [int] IDENTITY(1,1) NOT NULL,
	[case_name] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

