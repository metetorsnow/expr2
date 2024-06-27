USE [expr]
GO

/****** Object:  Table [dbo].[UsageRequest]    Script Date: 2024/6/27 14:06:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsageRequest](
	[RequestID] [int] NOT NULL,
	[ItemID] [int] NULL,
	[Quantity] [int] NULL,
	[RequestedBy] [nvarchar](100) NULL,
	[RequestDate] [date] NULL,
	[Status] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UsageRequest]  WITH CHECK ADD  CONSTRAINT [FK_UsageRequest_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO

ALTER TABLE [dbo].[UsageRequest] CHECK CONSTRAINT [FK_UsageRequest_Item]
GO

