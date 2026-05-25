using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtkAkademiAIBlog.WebApi.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Articles");
        }
    }
}
