using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class DogadjajMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DogadjajID",
                table: "Kategorije",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dogadjaji",
                columns: table => new
                {
                    DogadjajID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    DatumPocetka = table.Column<DateTime>(nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(nullable: false),
                    LokacijaMestoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadjaji", x => x.DogadjajID);
                    table.ForeignKey(
                        name: "FK_Dogadjaji_Mesta_LokacijaMestoID",
                        column: x => x.LokacijaMestoID,
                        principalTable: "Mesta",
                        principalColumn: "MestoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategorije_DogadjajID",
                table: "Kategorije",
                column: "DogadjajID");

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaji_LokacijaMestoID",
                table: "Dogadjaji",
                column: "LokacijaMestoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorije_Dogadjaji_DogadjajID",
                table: "Kategorije",
                column: "DogadjajID",
                principalTable: "Dogadjaji",
                principalColumn: "DogadjajID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorije_Dogadjaji_DogadjajID",
                table: "Kategorije");

            migrationBuilder.DropTable(
                name: "Dogadjaji");

            migrationBuilder.DropIndex(
                name: "IX_Kategorije_DogadjajID",
                table: "Kategorije");

            migrationBuilder.DropColumn(
                name: "DogadjajID",
                table: "Kategorije");
        }
    }
}
