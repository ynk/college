using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arragements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(maxLength: 50, nullable: true),
                    Aantal_uur = table.Column<int>(nullable: false),
                    Min_start = table.Column<int>(nullable: false),
                    Max_start = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arragements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locaties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocatieNaam = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locaties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffelKortingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(maxLength: 50, nullable: true),
                    StaffelKortingTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffelKortingTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffelKortingTypes_StaffelKortingTypes_StaffelKortingTypeId",
                        column: x => x.StaffelKortingTypeId,
                        principalTable: "StaffelKortingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voertuigen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EersteUur = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voertuigen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Klantnummer = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(maxLength: 100, nullable: true),
                    btw_nummer = table.Column<int>(nullable: false),
                    Adres = table.Column<string>(maxLength: 500, nullable: true),
                    StaffelKortingTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klanten_StaffelKortingTypes_StaffelKortingTypeId",
                        column: x => x.StaffelKortingTypeId,
                        principalTable: "StaffelKortingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffelKortings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aantal = table.Column<int>(nullable: false),
                    Korting = table.Column<decimal>(nullable: false),
                    StaffelKortingTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffelKortings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffelKortings_StaffelKortingTypes_StaffelKortingTypeId",
                        column: x => x.StaffelKortingTypeId,
                        principalTable: "StaffelKortingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservatieses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantId = table.Column<int>(nullable: false),
                    Adress = table.Column<string>(maxLength: 500, nullable: true),
                    StartDatum = table.Column<DateTime>(nullable: false),
                    TotaalExclusiefBtw = table.Column<decimal>(nullable: true),
                    StartLocatieId = table.Column<int>(nullable: true),
                    EindLocatieId = table.Column<int>(nullable: true),
                    VoertuigId = table.Column<int>(nullable: true),
                    ArragementId = table.Column<int>(nullable: true),
                    TotaalInclusiefBtw = table.Column<decimal>(nullable: false),
                    TotaalBedrag = table.Column<decimal>(nullable: false),
                    Korting = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservatieses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservatieses_Arragements_ArragementId",
                        column: x => x.ArragementId,
                        principalTable: "Arragements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservatieses_Locaties_EindLocatieId",
                        column: x => x.EindLocatieId,
                        principalTable: "Locaties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservatieses_Locaties_StartLocatieId",
                        column: x => x.StartLocatieId,
                        principalTable: "Locaties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservatieses_Voertuigen_VoertuigId",
                        column: x => x.VoertuigId,
                        principalTable: "Voertuigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voertuigprijzen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArragementId = table.Column<int>(nullable: true),
                    VoertuigId = table.Column<int>(nullable: true),
                    Prijs = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voertuigprijzen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voertuigprijzen_Arragements_ArragementId",
                        column: x => x.ArragementId,
                        principalTable: "Arragements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voertuigprijzen_Voertuigen_VoertuigId",
                        column: x => x.VoertuigId,
                        principalTable: "Voertuigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KlantenCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<int>(maxLength: 50, nullable: false),
                    StaffelKortingTypeId = table.Column<int>(nullable: true),
                    StaffelKortingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlantenCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KlantenCategories_StaffelKortings_StaffelKortingId",
                        column: x => x.StaffelKortingId,
                        principalTable: "StaffelKortings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_StaffelKortingTypeId",
                table: "Klanten",
                column: "StaffelKortingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KlantenCategories_StaffelKortingId",
                table: "KlantenCategories",
                column: "StaffelKortingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservatieses_ArragementId",
                table: "Reservatieses",
                column: "ArragementId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservatieses_EindLocatieId",
                table: "Reservatieses",
                column: "EindLocatieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservatieses_StartLocatieId",
                table: "Reservatieses",
                column: "StartLocatieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservatieses_VoertuigId",
                table: "Reservatieses",
                column: "VoertuigId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffelKortings_StaffelKortingTypeId",
                table: "StaffelKortings",
                column: "StaffelKortingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffelKortingTypes_StaffelKortingTypeId",
                table: "StaffelKortingTypes",
                column: "StaffelKortingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Voertuigprijzen_ArragementId",
                table: "Voertuigprijzen",
                column: "ArragementId");

            migrationBuilder.CreateIndex(
                name: "IX_Voertuigprijzen_VoertuigId",
                table: "Voertuigprijzen",
                column: "VoertuigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropTable(
                name: "KlantenCategories");

            migrationBuilder.DropTable(
                name: "Reservatieses");

            migrationBuilder.DropTable(
                name: "Voertuigprijzen");

            migrationBuilder.DropTable(
                name: "StaffelKortings");

            migrationBuilder.DropTable(
                name: "Locaties");

            migrationBuilder.DropTable(
                name: "Arragements");

            migrationBuilder.DropTable(
                name: "Voertuigen");

            migrationBuilder.DropTable(
                name: "StaffelKortingTypes");
        }
    }
}
