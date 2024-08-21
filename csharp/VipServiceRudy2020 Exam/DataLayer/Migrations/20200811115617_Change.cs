using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klanten_StaffelKortingTypes_StaffelKortingTypeId",
                table: "Klanten");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_StaffelKortingTypeId",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "StaffelKortingTypeId",
                table: "Klanten");

            migrationBuilder.AddColumn<int>(
                name: "KlantenCategorieId",
                table: "Klanten",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KlantenCategories_StaffelKortingTypeId",
                table: "KlantenCategories",
                column: "StaffelKortingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_KlantenCategorieId",
                table: "Klanten",
                column: "KlantenCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klanten_KlantenCategories_KlantenCategorieId",
                table: "Klanten",
                column: "KlantenCategorieId",
                principalTable: "KlantenCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KlantenCategories_StaffelKortingTypes_StaffelKortingTypeId",
                table: "KlantenCategories",
                column: "StaffelKortingTypeId",
                principalTable: "StaffelKortingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klanten_KlantenCategories_KlantenCategorieId",
                table: "Klanten");

            migrationBuilder.DropForeignKey(
                name: "FK_KlantenCategories_StaffelKortingTypes_StaffelKortingTypeId",
                table: "KlantenCategories");

            migrationBuilder.DropIndex(
                name: "IX_KlantenCategories_StaffelKortingTypeId",
                table: "KlantenCategories");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_KlantenCategorieId",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "KlantenCategorieId",
                table: "Klanten");

            migrationBuilder.AddColumn<int>(
                name: "StaffelKortingTypeId",
                table: "Klanten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_StaffelKortingTypeId",
                table: "Klanten",
                column: "StaffelKortingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klanten_StaffelKortingTypes_StaffelKortingTypeId",
                table: "Klanten",
                column: "StaffelKortingTypeId",
                principalTable: "StaffelKortingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
