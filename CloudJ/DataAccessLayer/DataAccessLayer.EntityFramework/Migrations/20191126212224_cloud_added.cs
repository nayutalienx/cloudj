using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class cloud_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CloudId",
                table: "DockerImages",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SolutionId",
                table: "Clouds",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DockerImages_CloudId",
                table: "DockerImages",
                column: "CloudId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clouds_SolutionId",
                table: "Clouds",
                column: "SolutionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clouds_Solutions_SolutionId",
                table: "Clouds",
                column: "SolutionId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DockerImages_Clouds_CloudId",
                table: "DockerImages",
                column: "CloudId",
                principalTable: "Clouds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clouds_Solutions_SolutionId",
                table: "Clouds");

            migrationBuilder.DropForeignKey(
                name: "FK_DockerImages_Clouds_CloudId",
                table: "DockerImages");

            migrationBuilder.DropIndex(
                name: "IX_DockerImages_CloudId",
                table: "DockerImages");

            migrationBuilder.DropIndex(
                name: "IX_Clouds_SolutionId",
                table: "Clouds");

            migrationBuilder.DropColumn(
                name: "CloudId",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "SolutionId",
                table: "Clouds");
        }
    }
}
