using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class otkat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Collections_CurrentCollectionId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_CurrentCollectionId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "CurrentCollectionId",
                table: "Solutions");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "CurrentCollectionId",
                table: "Solutions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_CurrentCollectionId",
                table: "Solutions",
                column: "CurrentCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Collections_CurrentCollectionId",
                table: "Solutions",
                column: "CurrentCollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
