using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Model.Migrations
{
    public partial class createspgetroombyhotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	//		var sp = @"   IF OBJECT_ID('GetRoomByHotelId', 'P') IS NOT NULL
 //           DROP PROC GetRoomByHotelId
 //           GO
 
 //            CREATE PROC [GetRoomByHotelId]
	//            @HotelId uniqueidentifier

 //           as 
	//            BEGIN
 //               declare @room as table(
	//            [Id] [uniqueidentifier] ,
	//            [Name] [nvarchar](max) ,
	//            [RoomTypeId] [uniqueidentifier] ,
	//            [RoomFloor] [int] ,
	//            [RoomNumber] [int] ,
	//            [RoomStatusId] [uniqueidentifier],
	//            Num int identity(1,1))

	//		insert into @room(Id, Name, RoomTypeId, RoomFloor, RoomNumber, RoomStatusId)
	//		select Id, Name, RoomTypeId, RoomFloor, RoomNumber, RoomStatusId from Rooms where HotelId = @HotelId

	//		select r.Id, r.Name, price.Price, rType.Capacity, rType.Beds as bed, rType.Name as RoomTypeName 
			
	//		from RoomPrices price 
	//		Join @room r on price.RoomId = r.Id
	//		join RoomTypes rType on r.RoomTypeId = rType.Id
	//END";

 //           migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC GetRoomByHotelId");
		}
    }
}
