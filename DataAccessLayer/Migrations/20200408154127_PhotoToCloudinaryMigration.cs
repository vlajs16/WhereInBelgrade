using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class PhotoToCloudinaryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Dogadjaji");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Dogadjaji",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Dogadjaji",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Dogadjaji");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Dogadjaji");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Dogadjaji",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
