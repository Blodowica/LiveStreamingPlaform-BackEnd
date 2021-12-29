using Microsoft.EntityFrameworkCore.Migrations;

namespace Livestream_Backend_application.Migrations
{
    public partial class AddedStreamUserRelatopnship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreamsStreamId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Streams1",
                columns: table => new
                {
                    StreamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
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
                name: "IX_AspNetUsers_StreamId",
                table: "AspNetUsers",
                column: "StreamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streams1_AppUserId",
                table: "Streams1",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Streams_StreamId",
                table: "AspNetUsers",
                column: "StreamId",
                principalTable: "Streams",
                principalColumn: "stream_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Streams1_StreamsStreamId",
                table: "Users",
                column: "StreamsStreamId",
                principalTable: "Streams1",
                principalColumn: "StreamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Streams_StreamId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Streams1_StreamsStreamId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Streams1");

            migrationBuilder.DropIndex(
                name: "IX_Users_StreamsStreamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StreamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StreamsStreamId",
                table: "Users");
        }
    }
}
