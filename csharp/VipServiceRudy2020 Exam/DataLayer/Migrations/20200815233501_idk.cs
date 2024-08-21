using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Reservatieses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Eerste_uren",
                table: "Reservatieses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EindDateTime",
                table: "Reservatieses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Nachtuur",
                table: "Reservatieses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ovenuren",
                table: "Reservatieses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservatieses_KlantId",
                table: "Reservatieses",
                column: "KlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservatieses_Klanten_KlantId",
                table: "Reservatieses",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservatieses_Klanten_KlantId",
                table: "Reservatieses");

            migrationBuilder.DropIndex(
                name: "IX_Reservatieses_KlantId",
                table: "Reservatieses");

            migrationBuilder.DropColumn(
                name: "Eerste_uren",
                table: "Reservatieses");

            migrationBuilder.DropColumn(
                name: "EindDateTime",
                table: "Reservatieses");

            migrationBuilder.DropColumn(
                name: "Nachtuur",
                table: "Reservatieses");

            migrationBuilder.DropColumn(
                name: "Ovenuren",
                table: "Reservatieses");

            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Reservatieses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
