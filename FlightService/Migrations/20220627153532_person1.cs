using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class person1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonDetails_Booking_BookingPNRNo",
                table: "PersonDetails");

            migrationBuilder.DropIndex(
                name: "IX_PersonDetails_BookingPNRNo",
                table: "PersonDetails");

            migrationBuilder.DropColumn(
                name: "BookingPNRNo",
                table: "PersonDetails");

            migrationBuilder.AlterColumn<int>(
                name: "PNRno",
                table: "PersonDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonDetails_PNRno",
                table: "PersonDetails",
                column: "PNRno");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonDetails_Booking_PNRno",
                table: "PersonDetails",
                column: "PNRno",
                principalTable: "Booking",
                principalColumn: "PNRNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonDetails_Booking_PNRno",
                table: "PersonDetails");

            migrationBuilder.DropIndex(
                name: "IX_PersonDetails_PNRno",
                table: "PersonDetails");

            migrationBuilder.AlterColumn<string>(
                name: "PNRno",
                table: "PersonDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BookingPNRNo",
                table: "PersonDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonDetails_BookingPNRNo",
                table: "PersonDetails",
                column: "BookingPNRNo");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonDetails_Booking_BookingPNRNo",
                table: "PersonDetails",
                column: "BookingPNRNo",
                principalTable: "Booking",
                principalColumn: "PNRNo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
