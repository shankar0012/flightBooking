using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class cost2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightName",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlightNo",
                table: "Booking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightName",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "FlightNo",
                table: "Booking");
        }
    }
}
