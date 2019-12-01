using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class truncSol_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Collections_CollectionId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_CollectionId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Solutions");

            migrationBuilder.CreateTable(
                name: "TruncSolution",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SolutionId = table.Column<long>(nullable: false),
                    CollectionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruncSolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruncSolution_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TruncSolution_CollectionId",
                table: "TruncSolution",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruncSolution");

            migrationBuilder.AddColumn<long>(
                name: "CollectionId",
                table: "Solutions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_CollectionId",
                table: "Solutions",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Collections_CollectionId",
                table: "Solutions",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
