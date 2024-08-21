using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class _4dekeer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlantenCategories_StaffelKortings_StaffelKortingId",
                table: "KlantenCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffelKortingTypes_StaffelKortingTypes_StaffelKortingTypeId",
                table: "StaffelKortingTypes");

            migrationBuilder.DropIndex(
                name: "IX_StaffelKortingTypes_StaffelKortingTypeId",
                table: "StaffelKortingTypes");

            migrationBuilder.DropIndex(
                name: "IX_KlantenCategories_StaffelKortingId",
                table: "KlantenCategories");

            migrationBuilder.DropColumn(
                name: "StaffelKortingTypeId",
                table: "StaffelKortingTypes");

            migrationBuilder.DropColumn(
                name: "StaffelKortingId",
                table: "KlantenCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffelKortingTypeId",
                table: "StaffelKortingTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffelKortingId",
                table: "KlantenCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffelKortingTypes_StaffelKortingTypeId",
                table: "StaffelKortingTypes",
                column: "StaffelKortingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KlantenCategories_StaffelKortingId",
                table: "KlantenCategories",
                column: "StaffelKortingId");

            migrationBuilder.AddForeignKey(
                name: "FK_KlantenCategories_StaffelKortings_StaffelKortingId",
                table: "KlantenCategories",
                column: "StaffelKortingId",
                principalTable: "StaffelKortings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffelKortingTypes_StaffelKortingTypes_StaffelKortingTypeId",
                table: "StaffelKortingTypes",
                column: "StaffelKortingTypeId",
                principalTable: "StaffelKortingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
