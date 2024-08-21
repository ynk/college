﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(VipServiceContext))]
    [Migration("20200811115958_Edit")]
    partial class Edit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.StaffelKortingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("StaffelKortingTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffelKortingTypeId");

                    b.ToTable("StaffelKortingTypes");
                });

            modelBuilder.Entity("DataLayer.VoertuigPrijs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArragementId")
                        .HasColumnType("int");

                    b.Property<int>("Prijs")
                        .HasColumnType("int");

                    b.Property<int?>("VoertuigId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArragementId");

                    b.HasIndex("VoertuigId");

                    b.ToTable("Voertuigprijzen");
                });

            modelBuilder.Entity("Entiteiten.Arragement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Aantal_uur")
                        .HasColumnType("int");

                    b.Property<int>("Max_start")
                        .HasColumnType("int");

                    b.Property<int>("Min_start")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Arragements");
                });

            modelBuilder.Entity("Entiteiten.Klant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adres")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("KlantenCategorieId")
                        .HasColumnType("int");

                    b.Property<int>("Klantnummer")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("btw_nummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KlantenCategorieId");

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("Entiteiten.KlantenCategorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("StaffelKortingId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffelKortingTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffelKortingId");

                    b.HasIndex("StaffelKortingTypeId");

                    b.ToTable("KlantenCategories");
                });

            modelBuilder.Entity("Entiteiten.Locaties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocatieNaam")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Locaties");
                });

            modelBuilder.Entity("Entiteiten.Reservaties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("ArragementId")
                        .HasColumnType("int");

                    b.Property<int?>("EindLocatieId")
                        .HasColumnType("int");

                    b.Property<int>("KlantId")
                        .HasColumnType("int");

                    b.Property<decimal>("Korting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StartLocatieId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotaalBedrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotaalExclusiefBtw")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotaalInclusiefBtw")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("VoertuigId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArragementId");

                    b.HasIndex("EindLocatieId");

                    b.HasIndex("StartLocatieId");

                    b.HasIndex("VoertuigId");

                    b.ToTable("Reservatieses");
                });

            modelBuilder.Entity("Entiteiten.StaffelKorting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Aantal")
                        .HasColumnType("int");

                    b.Property<decimal>("Korting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("StaffelKortingTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffelKortingTypeId");

                    b.ToTable("StaffelKortings");
                });

            modelBuilder.Entity("Entiteiten.Voertuig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EersteUur")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Voertuigen");
                });

            modelBuilder.Entity("DataLayer.StaffelKortingType", b =>
                {
                    b.HasOne("DataLayer.StaffelKortingType", null)
                        .WithMany("StaffelKortingTypes")
                        .HasForeignKey("StaffelKortingTypeId");
                });

            modelBuilder.Entity("DataLayer.VoertuigPrijs", b =>
                {
                    b.HasOne("Entiteiten.Arragement", "Arragement")
                        .WithMany()
                        .HasForeignKey("ArragementId");

                    b.HasOne("Entiteiten.Voertuig", "Voertuig")
                        .WithMany()
                        .HasForeignKey("VoertuigId");
                });

            modelBuilder.Entity("Entiteiten.Klant", b =>
                {
                    b.HasOne("Entiteiten.KlantenCategorie", "KlantenCategorie")
                        .WithMany()
                        .HasForeignKey("KlantenCategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entiteiten.KlantenCategorie", b =>
                {
                    b.HasOne("Entiteiten.StaffelKorting", "StaffelKorting")
                        .WithMany()
                        .HasForeignKey("StaffelKortingId");

                    b.HasOne("DataLayer.StaffelKortingType", null)
                        .WithMany("KlantenCategories")
                        .HasForeignKey("StaffelKortingTypeId");
                });

            modelBuilder.Entity("Entiteiten.Reservaties", b =>
                {
                    b.HasOne("Entiteiten.Arragement", "Arragement")
                        .WithMany()
                        .HasForeignKey("ArragementId");

                    b.HasOne("Entiteiten.Locaties", "EindLocatie")
                        .WithMany()
                        .HasForeignKey("EindLocatieId");

                    b.HasOne("Entiteiten.Locaties", "StartLocatie")
                        .WithMany()
                        .HasForeignKey("StartLocatieId");

                    b.HasOne("Entiteiten.Voertuig", "Voertuig")
                        .WithMany()
                        .HasForeignKey("VoertuigId");
                });

            modelBuilder.Entity("Entiteiten.StaffelKorting", b =>
                {
                    b.HasOne("DataLayer.StaffelKortingType", "StaffelKortingType")
                        .WithMany()
                        .HasForeignKey("StaffelKortingTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
