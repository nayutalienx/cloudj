using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EntityFramework.Migrations
{
    public partial class docker_edited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "DockerImages",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "DockerImages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
