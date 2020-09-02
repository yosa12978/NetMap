using Microsoft.EntityFrameworkCore.Migrations;

namespace NetMap.Data.Migrations
{
    public partial class AddingPostHosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "host",
                table: "posts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "host",
                table: "posts");
        }
    }
}
