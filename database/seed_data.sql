--ADD admin user
if not EXISTS(select * from Customer where CustomerID = 1) 
Begin
	insert into Customer(CustomerName,CustomerEmailID, CustomerContactPhone, Username, [Password], CustomerAddress, Active
		, IsStaff, RestaurantID)
values
	(
	'Admin',
	'dungphamdev@gmail.com',
	'0387928697',
	'admin',
	'1',
	'Ha Noi, Viet Nam',
	1,
	1,
	null
	)
end

-- ADD Billing Status
if not EXISTS(select * from StatusBilling where StatusName = 'New') 
Begin
	insert into StatusBilling(StatusName)
	values ('New')
end

if not EXISTS(select * from StatusBilling where StatusName = 'Approved') 
Begin
	insert into StatusBilling(StatusName)
	values ('Approved')
end

if not EXISTS(select * from StatusBilling where StatusName = 'Canceled') 
Begin
	insert into StatusBilling(StatusName)
	values ('Canceled')
end