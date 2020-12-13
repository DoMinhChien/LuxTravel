using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Models.Migrations
{
    public partial class changewardname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WardName",
                table: "Wards",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Wards",
                newName: "WardName");
        }
    }
}
