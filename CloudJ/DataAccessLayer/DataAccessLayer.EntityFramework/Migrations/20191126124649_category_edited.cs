using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class category_edited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LogoId",
                table: "Categories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LogoId",
                table: "Categories",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Photos_LogoId",
                table: "Categories",
                column: "LogoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Photos_LogoId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LogoId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Categories");
        }
    }
}
