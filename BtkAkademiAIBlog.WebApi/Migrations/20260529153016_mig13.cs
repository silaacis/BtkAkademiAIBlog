using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtkAkademiAIBlog.WebApi.Migrations
{
    public partial class mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeatureImageUrl",
                table: "TradingVideos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeature",
                table: "TradingVideos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureImageUrl",
                table: "TradingVideos");

            migrationBuilder.DropColumn(
                name: "IsFeature",
                table: "TradingVideos");
        }
    }
}
