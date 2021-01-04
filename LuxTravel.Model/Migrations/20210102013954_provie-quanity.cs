using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Model.Migrations
{
    public partial class proviequanity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Beds",
                table: "SpGetRoomByHotels",
                newName: "Bed");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BookingDetails");

            migrationBuilder.RenameColumn(
                name: "Bed",
                table: "SpGetRoomByHotels",
                newName: "Beds");
        }
    }
}
