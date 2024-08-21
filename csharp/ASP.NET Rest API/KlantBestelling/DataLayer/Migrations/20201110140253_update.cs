using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestllingen_Klanten_KlantId",
                table: "Bestllingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Bestllingen_Producten_ProductId",
                table: "Bestllingen");

            migrationBuilder.DropTable(
                name: "Producten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klanten",
                table: "Klanten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bestllingen",
                table: "Bestllingen");

            migrationBuilder.DropIndex(
                name: "IX_Bestllingen_ProductId",
                table: "Bestllingen");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Bestllingen");

            migrationBuilder.RenameTable(
                name: "Klanten",
                newName: "Klant");

            migrationBuilder.RenameTable(
                name: "Bestllingen",
                newName: "Bestelling");

            migrationBuilder.RenameColumn(
                name: "KlantId",
                table: "Klant",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "KlantId",
                table: "Bestelling",
                newName: "KlantID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bestelling",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Bestllingen_KlantId",
                table: "Bestelling",
                newName: "IX_Bestelling_KlantID");

            migrationBuilder.AlterColumn<string>(
                name: "Naam",
                table: "Klant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "Klant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KlantID",
                table: "Bestelling",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Product",
                table: "Bestelling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klant",
                table: "Klant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bestelling",
                table: "Bestelling",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestelling_Klant_KlantID",
                table: "Bestelling",
                column: "KlantID",
                principalTable: "Klant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestelling_Klant_KlantID",
                table: "Bestelling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klant",
                table: "Klant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bestelling",
                table: "Bestelling");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Bestelling");

            migrationBuilder.RenameTable(
                name: "Klant",
                newName: "Klanten");

            migrationBuilder.RenameTable(
                name: "Bestelling",
                newName: "Bestllingen");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Klanten",
                newName: "KlantId");

            migrationBuilder.RenameColumn(
                name: "KlantID",
                table: "Bestllingen",
                newName: "KlantId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Bestllingen",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bestelling_KlantID",
                table: "Bestllingen",
                newName: "IX_Bestllingen_KlantId");

            migrationBuilder.AlterColumn<string>(
                name: "Naam",
                table: "Klanten",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "Klanten",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Bestllingen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Bestllingen",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klanten",
                table: "Klanten",
                column: "KlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bestllingen",
                table: "Bestllingen",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Producten",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijs = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producten", x => x.ProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestllingen_ProductId",
                table: "Bestllingen",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestllingen_Klanten_KlantId",
                table: "Bestllingen",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "KlantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestllingen_Producten_ProductId",
                table: "Bestllingen",
                column: "ProductId",
                principalTable: "Producten",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
