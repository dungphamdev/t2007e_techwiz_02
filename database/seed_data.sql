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