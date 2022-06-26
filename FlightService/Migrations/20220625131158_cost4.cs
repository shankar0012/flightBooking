using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class cost4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TotalAmount",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalAmount",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
