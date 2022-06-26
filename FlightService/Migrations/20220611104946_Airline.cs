using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class Airline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    AirlineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineName = table.Column<string>(nullable: true),
                    Addtime = table.Column<DateTime>(nullable: true),
                    Modtime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    PNRNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    AirlineId = table.Column<int>(nullable: false),
                    FromCity = table.Column<string>(nullable: true),
                    ToCity = table.Column<string>(nullable: true),
                    FromTime = table.Column<DateTime>(nullable: true),
                    ToTime = table.Column<DateTime>(nullable: true),
                    SeatNo = table.Column<string>(nullable: true),
                    NoOfSeats = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Discount = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    TripType = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<string>(nullable: true),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.PNRNo);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleAirline",
                columns: table => new
                {
                    SchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineId = table.Column<int>(nullable: false),
                    FromCity = table.Column<string>(nullable: true),
                    ToCity = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    ScheduledDays = table.Column<string>(nullable: true),
                    InstrumentUsed = table.Column<string>(nullable: true),
                    BCSeats = table.Column<int>(nullable: false),
                    NBCSeats = table.Column<int>(nullable: false),
                    TicketCoastOneWay = table.Column<int>(nullable: false),
                    TicketCoastRoundTrip = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    Meals = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleAirline", x => x.SchId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "ScheduleAirline");
        }
    }
}
