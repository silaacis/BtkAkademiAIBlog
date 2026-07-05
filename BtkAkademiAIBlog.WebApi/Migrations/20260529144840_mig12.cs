using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtkAkademiAIBlog.WebApi.Migrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image300x300Url",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image300x300Url",
                table: "Articles");
        }
    }
}
