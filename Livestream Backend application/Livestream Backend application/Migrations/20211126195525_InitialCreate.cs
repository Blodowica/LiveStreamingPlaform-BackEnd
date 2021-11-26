using Microsoft.EntityFrameworkCore.Migrations;

namespace Livestream_Backend_application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    followers_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userd_id = table.Column<int>(nullable: false),
                    follower_id = table.Column<string>(fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.followers_id);
                });

            migrationBuilder.CreateTable(
                name: "Streams",
                columns: table => new
                {
                    stream_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    title = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    description = table.Column<string>(unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streams", x => x.stream_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    users_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    lastname = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    username = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    streamkey = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    role = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.users_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "Streams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
