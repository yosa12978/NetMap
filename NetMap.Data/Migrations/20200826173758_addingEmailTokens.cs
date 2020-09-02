using Microsoft.EntityFrameworkCore.Migrations;

namespace NetMap.Data.Migrations
{
    public partial class addingEmailTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "emailToken",
                table: "users",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailToken",
                table: "users");
        }
    }
}
