using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Model.Migrations
{
    public partial class removeroomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room_Id",
                table: "RoomPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Room_Id",
                table: "RoomPrices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
