IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Restaurants]') 
         AND name = 'ImagePath'
)
BEGIN
	ALTER TABLE Restaurants
	ADD ImagePath nvarchar(255);
END

IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Restaurants]') 
         AND name = 'ImageType'
)
BEGIN
	ALTER TABLE Restaurants
	ADD ImageType nvarchar(255);
END

IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Item]') 
         AND name = 'ImageType'
)
BEGIN
	ALTER TABLE Item
	ADD ImageType nvarchar(255);
END

IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OrderDetails]') 
         AND name = 'OrderDetailId'
)
BEGIN
	ALTER TABLE OrderDetails
	ADD OrderDetailId  int identity(1,1) PRIMARY KEY
END
GO

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OrderDetails]') 
         AND name = 'OrderItemQty'
)
BEGIN
	ALTER TABLE OrderDetails
	DROP COLUMN OrderItemQty;
END
GO

IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OrderDetails]') 
         AND name = 'OrderItemQty'
)
BEGIN
	ALTER TABLE OrderDetails
	ADD OrderItemQty int;
END
GO


DROP TABLE IF EXISTS dbo.Billing
GO

DROP TABLE IF EXISTS dbo.StatusBilling
GO

DROP TABLE IF EXISTS dbo.OrderDetails
GO

create table Billing(
		OrderId int identity(1,1),
		CustomerId int,
		RestaurantId Int,
		BillAmout money,
		OrderDate Date,
		StatusId int

		PRIMARY KEY (OrderId)
)

create table StatusBilling(
		StatusId int identity(1,1),
		StatusName nvarchar(255),

		PRIMARY KEY (StatusId)
)

create table OrderDetail(
		OrderDetailId int identity(1,1),
		OrderId int,
		ItemId int, 
		ItemQty int
		PRIMARY KEY (OrderDetailId)
)

ALTER TABLE Billing
ADD FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerID);

ALTER TABLE Billing
ADD FOREIGN KEY (RestaurantId) REFERENCES Restaurants(RestaurantID);

ALTER TABLE Billing
ADD FOREIGN KEY (StatusId) REFERENCES StatusBilling(StatusId);

ALTER TABLE OrderDetail
ADD FOREIGN KEY (OrderId) REFERENCES Billing(OrderId);

ALTER TABLE OrderDetail
ADD FOREIGN KEY (ItemId) REFERENCES Item(ItemID);


