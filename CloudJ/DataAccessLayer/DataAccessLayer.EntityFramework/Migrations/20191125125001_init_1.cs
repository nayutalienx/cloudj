using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class init_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Clouds_CloudId",
                table: "Solutions");

            migrationBuilder.AlterColumn<long>(
                name: "CloudId",
                table: "Solutions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Clouds_CloudId",
                table: "Solutions",
                column: "CloudId",
                principalTable: "Clouds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Clouds_CloudId",
                table: "Solutions");

            migrationBuilder.AlterColumn<long>(
                name: "CloudId",
                table: "Solutions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Clouds_CloudId",
                table: "Solutions",
                column: "CloudId",
                principalTable: "Clouds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
