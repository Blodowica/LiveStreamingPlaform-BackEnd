using Microsoft.EntityFrameworkCore.Migrations;

namespace Livestream_Backend_application.Migrations
{
    public partial class ChangedParentTableToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Streams_StreamId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StreamId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "Streams",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streams_user_id",
                table: "Streams",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Streams_AspNetUsers_user_id",
                table: "Streams",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streams_AspNetUsers_user_id",
                table: "Streams");

            migrationBuilder.DropIndex(
                name: "IX_Streams_user_id",
                table: "Streams");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "Streams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StreamId",
                table: "AspNetUsers",
                column: "StreamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Streams_StreamId",
                table: "AspNetUsers",
                column: "StreamId",
                principalTable: "Streams",
                principalColumn: "stream_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
