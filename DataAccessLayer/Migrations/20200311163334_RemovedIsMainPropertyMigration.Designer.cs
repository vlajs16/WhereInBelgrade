﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(BeogradContext))]
    [Migration("20200311163334_RemovedIsMainPropertyMigration")]
    partial class RemovedIsMainPropertyMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Dogadjaj", b =>
                {
                    b.Property<int>("DogadjajID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumPocetka")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZavrsetka")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LokacijaMestoID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DogadjajID");

                    b.HasIndex("LokacijaMestoID");

                    b.ToTable("Dogadjaji");
                });

            modelBuilder.Entity("Model.Kategorija", b =>
                {
                    b.Property<int>("KategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategorijaID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("Model.KategorijaDogadjaj", b =>
                {
                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<int>("DogadjajID")
                        .HasColumnType("int");

                    b.HasKey("KategorijaID", "DogadjajID");

                    b.HasIndex("DogadjajID");

                    b.ToTable("KategorijeDogadjaji");
                });

            modelBuilder.Entity("Model.Komentar", b =>
                {
                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("DogadjajID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumVreme")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tekst")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikID", "DogadjajID");

                    b.HasIndex("DogadjajID");

                    b.ToTable("Komentari");
                });

            modelBuilder.Entity("Model.Korisnik", b =>
                {
                    b.Property<int>("KorisnikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Model.Mesto", b =>
                {
                    b.Property<int>("MestoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojStana")
                        .HasColumnType("int");

                    b.Property<string>("BrojUlice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sprat")
                        .HasColumnType("int");

                    b.Property<string>("Ulica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MestoID");

                    b.ToTable("Mesta");
                });

            modelBuilder.Entity("Model.Svidjanje", b =>
                {
                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("DogadjajID")
                        .HasColumnType("int");

                    b.HasKey("KorisnikID", "DogadjajID");

                    b.HasIndex("DogadjajID");

                    b.ToTable("Svidjanja");
                });

            modelBuilder.Entity("Model.Dogadjaj", b =>
                {
                    b.HasOne("Model.Mesto", "Lokacija")
                        .WithMany()
                        .HasForeignKey("LokacijaMestoID");
                });

            modelBuilder.Entity("Model.KategorijaDogadjaj", b =>
                {
                    b.HasOne("Model.Dogadjaj", "Dogadjaj")
                        .WithMany("KategorijeDogadjaji")
                        .HasForeignKey("DogadjajID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Kategorija", "Kategorija")
                        .WithMany("KategorijeDogadjaji")
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Komentar", b =>
                {
                    b.HasOne("Model.Dogadjaj", "Dogadjaj")
                        .WithMany("Komentari")
                        .HasForeignKey("DogadjajID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Svidjanje", b =>
                {
                    b.HasOne("Model.Dogadjaj", "Dogadjaj")
                        .WithMany()
                        .HasForeignKey("DogadjajID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Korisnik", null)
                        .WithMany("Svidjanja")
                        .HasForeignKey("KorisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}