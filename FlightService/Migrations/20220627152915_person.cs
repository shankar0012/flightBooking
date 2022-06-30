using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FromTime",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PersonDetails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    MealType = table.Column<string>(nullable: true),
                    SeatNo = table.Column<string>(nullable: true),
                    PNRno = table.Column<string>(nullable: true),
                    BookingPNRNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_PersonDetails_Booking_BookingPNRNo",
                        column: x => x.BookingPNRNo,
                        principalTable: "Booking",
                        principalColumn: "PNRNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonDetails_BookingPNRNo",
                table: "PersonDetails",
                column: "BookingPNRNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromTime",
                table: "Booking",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
