using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class cost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketCoastOneWay",
                table: "ScheduleAirline");

            migrationBuilder.DropColumn(
                name: "TicketCoastRoundTrip",
                table: "ScheduleAirline");

            migrationBuilder.AddColumn<int>(
                name: "TicketCostOneWay",
                table: "ScheduleAirline",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketCostRoundTrip",
                table: "ScheduleAirline",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketCostOneWay",
                table: "ScheduleAirline");

            migrationBuilder.DropColumn(
                name: "TicketCostRoundTrip",
                table: "ScheduleAirline");

            migrationBuilder.AddColumn<int>(
                name: "TicketCoastOneWay",
                table: "ScheduleAirline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketCoastRoundTrip",
                table: "ScheduleAirline",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
