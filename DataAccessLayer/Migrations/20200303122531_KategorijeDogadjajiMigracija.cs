using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class KategorijeDogadjajiMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KategorijeDogadjaji",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(nullable: false),
                    DogadjajID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijeDogadjaji", x => new { x.KategorijaID, x.DogadjajID });
                    table.ForeignKey(
                        name: "FK_KategorijeDogadjaji_Dogadjaji_DogadjajID",
                        column: x => x.DogadjajID,
                        principalTable: "Dogadjaji",
                        principalColumn: "DogadjajID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategorijeDogadjaji_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KategorijeDogadjaji_DogadjajID",
                table: "KategorijeDogadjaji",
                column: "DogadjajID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategorijeDogadjaji");
        }
    }
}
