using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightService.Migrations
{
    public partial class Booking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PNR",
                table: "Booking",
                nullable: true,
                computedColumnSql: "N'PNR' + RIGHT('00000'+CAST(PNRNo as NVARCHAR(5)),5)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PNR",
                table: "Booking");
        }
    }
}
