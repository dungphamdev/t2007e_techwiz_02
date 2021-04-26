USE [DB_CloudKitchen]
GO
/****** Object:  Table [dbo].[Billing]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Billing](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[RestaurantID] [int] NULL,
	[BillAmount] [money] NULL,
	[Date] [date] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Billing] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[CustomerEmailID] [nvarchar](200) NULL,
	[CustomerContactPhone] [nvarchar](100) NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[CustomerAddress] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Customer_Active]  DEFAULT ((1)),
	[IsStaff] [bit] NOT NULL CONSTRAINT [DF_Customer_IsStaff]  DEFAULT ((0)),
	[RestaurantID] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Item]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](500) NULL,
	[RestaurantID] [int] NULL,
	[ItemDescription] [nvarchar](1000) NULL,
	[ItemPrice] [money] NULL,
	[ItemCategoryID] [int] NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Item_Active]  DEFAULT ((1)),
	[MainImagePath] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemCategory]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[CategoryName] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_ItemCategory_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_ItemCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemImageCarousel]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemImageCarousel](
	[ItemID] [int] NULL,
	[Title] [nvarchar](1000) NULL,
	[ImagePath] [nvarchar](1000) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[OrderDate] [date] NULL,
	[OrderLocation] [nvarchar](500) NULL,
	[OrderItemName] [nvarchar](500) NULL,
	[OrderItemQty] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Restaurants]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurants](
	[RestaurantID] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantName] [nvarchar](500) NULL,
	[RestaurantAddress] [nvarchar](500) NULL,
	[RestaurantEmail] [nvarchar](500) NULL,
	[RestaurantPhone] [nvarchar](500) NULL,
	[Longitude] [decimal](12, 12) NULL,
	[Latitude] [decimal](12, 12) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Restaurants_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED 
(
	[RestaurantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusBilling]    Script Date: 4/25/2021 10:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusBilling](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[CustomerID] [nchar](10) NULL,
	[Status] [int] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_StatusBilling] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Billing] ADD  CONSTRAINT [DF_Billing_StatusID]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[StatusBilling] ADD  CONSTRAINT [DF_StatusBilling_Status]  DEFAULT ((2)) FOR [Status]
GO
ALTER TABLE [dbo].[Billing]  WITH CHECK ADD  CONSTRAINT [FK_Billing_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Billing] CHECK CONSTRAINT [FK_Billing_Customer]
GO
ALTER TABLE [dbo].[Billing]  WITH CHECK ADD  CONSTRAINT [FK_Billing_Restaurants] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[Restaurants] ([RestaurantID])
GO
ALTER TABLE [dbo].[Billing] CHECK CONSTRAINT [FK_Billing_Restaurants]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemCategory] FOREIGN KEY([ItemCategoryID])
REFERENCES [dbo].[ItemCategory] ([CategoryID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_ItemCategory]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Restaurants] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[Restaurants] ([RestaurantID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Restaurants]
GO
ALTER TABLE [dbo].[ItemImageCarousel]  WITH CHECK ADD  CONSTRAINT [FK_ItemImageCarousel_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemImageCarousel] CHECK CONSTRAINT [FK_ItemImageCarousel_Item]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Billing] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Billing] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Billing]
GO
ALTER TABLE [dbo].[StatusBilling]  WITH CHECK ADD  CONSTRAINT [FK_StatusBilling_Billing] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Billing] ([OrderID])
GO
ALTER TABLE [dbo].[StatusBilling] CHECK CONSTRAINT [FK_StatusBilling_Billing]
GO
