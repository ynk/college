using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class help : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestelling_Klant_KlantID",
                table: "Bestelling");

            migrationBuilder.RenameColumn(
                name: "KlantID",
                table: "Bestelling",
                newName: "KlantId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Bestelling",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bestelling_KlantID",
                table: "Bestelling",
                newName: "IX_Bestelling_KlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestelling_Klant_KlantId",
                table: "Bestelling",
                column: "KlantId",
                principalTable: "Klant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestelling_Klant_KlantId",
                table: "Bestelling");

            migrationBuilder.RenameColumn(
                name: "KlantId",
                table: "Bestelling",
                newName: "KlantID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bestelling",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Bestelling_KlantId",
                table: "Bestelling",
                newName: "IX_Bestelling_KlantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestelling_Klant_KlantID",
                table: "Bestelling",
                column: "KlantID",
                principalTable: "Klant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
