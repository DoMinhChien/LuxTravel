using Microsoft.EntityFrameworkCore.Migrations;

namespace LuxTravel.Model.Migrations
{
    public partial class updatepaymentintent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Bookings");
        }
    }
}
