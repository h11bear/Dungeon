using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dungeon.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Story : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomCatalog");

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Story");

            migrationBuilder.CreateTable(
                name: "RoomCatalog",
                columns: table => new
                {
                    RoomCatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCatalog", x => x.RoomCatalogId);
                });
        }
    }
}
