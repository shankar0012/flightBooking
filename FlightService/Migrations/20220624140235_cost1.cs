using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class cost1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketCostOneWay",
                table: "ScheduleAirline");

            migrationBuilder.DropColumn(
                name: "TicketCostRoundTrip",
                table: "ScheduleAirline");

            migrationBuilder.AddColumn<int>(
                name: "TicketCost",
                table: "ScheduleAirline",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketCost",
                table: "ScheduleAirline");

            migrationBuilder.AddColumn<int>(
                name: "TicketCostOneWay",
                table: "ScheduleAirline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketCostRoundTrip",
                table: "ScheduleAirline",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
