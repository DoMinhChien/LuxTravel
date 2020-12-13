using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Models.Migrations
{
    public partial class renametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomtStatuses_RoomStatusId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomtStatuses",
                table: "RoomtStatuses");

            migrationBuilder.RenameTable(
                name: "RoomtStatuses",
                newName: "RoomStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomStatuses",
                table: "RoomStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomStatuses_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId",
                principalTable: "RoomStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomStatuses_RoomStatusId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomStatuses",
                table: "RoomStatuses");

            migrationBuilder.RenameTable(
                name: "RoomStatuses",
                newName: "RoomtStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomtStatuses",
                table: "RoomtStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomtStatuses_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId",
                principalTable: "RoomtStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
