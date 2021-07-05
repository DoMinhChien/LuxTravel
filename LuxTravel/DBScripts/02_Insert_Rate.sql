	insert into Rates(Id,Room_Id,Rate,RateType_Id)
	select NEWID(),r.id,r.Price,1
	from Rooms r