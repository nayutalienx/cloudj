using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class photo_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Solutions_SolutionId",
                table: "Photos");

            migrationBuilder.AlterColumn<long>(
                name: "SolutionId",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Solutions_SolutionId",
                table: "Photos",
                column: "SolutionId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Solutions_SolutionId",
                table: "Photos");

            migrationBuilder.AlterColumn<long>(
                name: "SolutionId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Solutions_SolutionId",
                table: "Photos",
                column: "SolutionId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
