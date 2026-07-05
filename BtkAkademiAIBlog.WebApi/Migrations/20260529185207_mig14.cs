using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtkAkademiAIBlog.WebApi.Migrations
{
    public partial class mig14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TradingVideos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingVideos_CategoryId",
                table: "TradingVideos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingVideos_Categories_CategoryId",
                table: "TradingVideos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradingVideos_Categories_CategoryId",
                table: "TradingVideos");

            migrationBuilder.DropIndex(
                name: "IX_TradingVideos_CategoryId",
                table: "TradingVideos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TradingVideos");
        }
    }
}
