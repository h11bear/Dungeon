using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dungeon.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Narrative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "RoomExit",
                columns: table => new
                {
                    ExitRoomRoomId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomExit", x => new { x.ExitRoomRoomId, x.Id });
                    table.ForeignKey(
                        name: "FK_RoomExit_Room_ExitRoomRoomId",
                        column: x => x.ExitRoomRoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntranceRoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Story_Room_EntranceRoomId",
                        column: x => x.EntranceRoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Story_EntranceRoomId",
                table: "Story",
                column: "EntranceRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomExit");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
