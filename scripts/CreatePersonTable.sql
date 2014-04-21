USE [AsyncTest]
GO

/****** Object:  Table [dbo].[People]    Script Date: 04/21/2014 17:25:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[People](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](500) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_People] FOREIGN KEY([id])
REFERENCES [dbo].[People] ([id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_People]
GO


