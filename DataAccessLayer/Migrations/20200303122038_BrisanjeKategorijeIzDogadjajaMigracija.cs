using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class BrisanjeKategorijeIzDogadjajaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorije_Dogadjaji_DogadjajID",
                table: "Kategorije");

            migrationBuilder.DropIndex(
                name: "IX_Kategorije_DogadjajID",
                table: "Kategorije");

            migrationBuilder.DropColumn(
                name: "DogadjajID",
                table: "Kategorije");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DogadjajID",
                table: "Kategorije",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorije_DogadjajID",
                table: "Kategorije",
                column: "DogadjajID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorije_Dogadjaji_DogadjajID",
                table: "Kategorije",
                column: "DogadjajID",
                principalTable: "Dogadjaji",
                principalColumn: "DogadjajID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
