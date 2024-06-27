USE [expr]
GO

/****** Object:  Table [dbo].[Item]    Script Date: 2024/6/27 14:04:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Item](
	[ItemID] [int] NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[CategoryID] [nvarchar](50) NULL,
	[Origin] [nvarchar](100) NULL,
	[Specification] [nvarchar](100) NULL,
	[Model] [nvarchar](100) NULL,
	[ItemImage] [nvarchar](100) NULL,
	[Amount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Item] ADD  DEFAULT ((0)) FOR [Amount]
GO

