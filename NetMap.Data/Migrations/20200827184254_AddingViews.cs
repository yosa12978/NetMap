using Microsoft.EntityFrameworkCore.Migrations;

namespace NetMap.Data.Migrations
{
    public partial class AddingViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "redirect_url",
                table: "posts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "views",
                table: "posts",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "redirect_url",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "views",
                table: "posts");
        }
    }
}
