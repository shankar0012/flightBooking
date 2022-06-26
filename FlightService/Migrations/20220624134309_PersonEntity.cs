using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class PersonEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightName",
                table: "ScheduleAirline",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlightNo",
                table: "ScheduleAirline",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightName",
                table: "ScheduleAirline");

            migrationBuilder.DropColumn(
                name: "FlightNo",
                table: "ScheduleAirline");
        }
    }
}
