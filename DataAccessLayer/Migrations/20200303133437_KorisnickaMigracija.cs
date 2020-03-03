using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class KorisnickaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Korisnici");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Korisnici",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Korisnici");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Korisnici",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
