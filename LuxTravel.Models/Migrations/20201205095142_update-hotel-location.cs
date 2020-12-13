using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Models.Migrations
{
    public partial class updatehotellocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "HotelLocations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "HotelLocations");
        }
    }
}
