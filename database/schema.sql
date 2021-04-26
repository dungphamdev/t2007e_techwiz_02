use TESTDB;

IF NOT EXISTS
   (  SELECT [name]
      FROM sys.tables
      WHERE [name] = 'Users'
   )
 create table Users (
        UserId UNIQUEIDENTIFIER NOT NULL,
		FirstName varchar(255),
		LastName varchar(255),
		[Address] varchar(255),
		City varchar(255)

		 PRIMARY KEY (UserId)
    )
go