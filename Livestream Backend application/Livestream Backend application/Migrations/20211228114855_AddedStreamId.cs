using Microsoft.EntityFrameworkCore.Migrations;

namespace Livestream_Backend_application.Migrations
{
    public partial class AddedStreamId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreamId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreamId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StreamId",
                table: "AspNetUsers");
        }
    }
}
