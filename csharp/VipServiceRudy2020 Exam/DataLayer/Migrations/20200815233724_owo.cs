using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class owo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ovenuren",
                table: "Reservatieses");

            migrationBuilder.AddColumn<int>(
                name: "OveruurPerUur",
                table: "Reservatieses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotaalNachtUren",
                table: "Reservatieses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotaalOveruren",
                table: "Reservatieses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OveruurPerUur",
                table: "Reservatieses");

            migrationBuilder.DropColumn(
                name: "TotaalNachtUren",
                table: "Reservatieses");

            migrationBuilder.DropColumn(
                name: "TotaalOveruren",
                table: "Reservatieses");

            migrationBuilder.AddColumn<int>(
                name: "Ovenuren",
                table: "Reservatieses",
                type: "int",
                nullable: true);
        }
    }
}
