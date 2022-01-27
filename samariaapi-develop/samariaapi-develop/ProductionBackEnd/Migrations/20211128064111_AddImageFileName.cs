using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductionBackEnd.Migrations
{
    public partial class AddImageFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitlePageImgName",
                table: "PlatformNews",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitlePageImgName",
                table: "PlatformNews");
        }
    }
}
