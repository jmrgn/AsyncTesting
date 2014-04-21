USE [AsyncTest]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 04/21/2014 17:25:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [varchar](50) NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_People] FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([id])
GO

ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_People]
GO


