using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtkAkademiAIBlog.WebApi.Migrations
{
    public partial class mig15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TradingVideos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingVideos_AppUserId",
                table: "TradingVideos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingVideos_AspNetUsers_AppUserId",
                table: "TradingVideos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradingVideos_AspNetUsers_AppUserId",
                table: "TradingVideos");

            migrationBuilder.DropIndex(
                name: "IX_TradingVideos_AppUserId",
                table: "TradingVideos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TradingVideos");
        }
    }
}
