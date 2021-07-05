
declare @count int = (select count(*) from Ward)
declare @Id int =1

while @Id <= 10
begin
insert into [dbo].[DistrictWardMapping]([District_Id],[Ward_Id])
values (2,@Id)
set @Id = @Id +1
end


--while @id < 12
--begin
--	insert into Ward(Id,WardName) 
--	values (@id,N'P.' + CONVERT(nvarchar(10),@id))
--	set @id =@id +1
--end
