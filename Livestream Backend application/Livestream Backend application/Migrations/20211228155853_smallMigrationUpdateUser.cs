using Microsoft.EntityFrameworkCore.Migrations;

namespace Livestream_Backend_application.Migrations
{
    public partial class smallMigrationUpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Streams1_StreamsStreamId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Streams1");

            migrationBuilder.DropIndex(
                name: "IX_Users_StreamsStreamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StreamsStreamId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreamsStreamId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Streams1",
                columns: table => new
                {
                    StreamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streams1", x => x.StreamId);
                    table.ForeignKey(
                        name: "FK_Streams1_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StreamsStreamId",
                table: "Users",
                column: "StreamsStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_Streams1_AppUserId",
                table: "Streams1",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Streams1_StreamsStreamId",
                table: "Users",
                column: "StreamsStreamId",
                principalTable: "Streams1",
                principalColumn: "StreamId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
