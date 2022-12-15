using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dungeon.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Story2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomCatalog_RoomCatalogId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Story_RoomCatalog_RoomCatalogId",
                table: "Story");

            migrationBuilder.DropTable(
                name: "RoomCatalog");

            migrationBuilder.RenameColumn(
                name: "RoomCatalogId",
                table: "Story",
                newName: "EntranceRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Story_RoomCatalogId",
                table: "Story",
                newName: "IX_Story_EntranceRoomId");

            migrationBuilder.RenameColumn(
                name: "RoomCatalogId",
                table: "Room",
                newName: "StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_RoomCatalogId",
                table: "Room",
                newName: "IX_Room_StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Story_StoryId",
                table: "Room",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Room_EntranceRoomId",
                table: "Story",
                column: "EntranceRoomId",
                principalTable: "Room",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Story_StoryId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Story_Room_EntranceRoomId",
                table: "Story");

            migrationBuilder.RenameColumn(
                name: "EntranceRoomId",
                table: "Story",
                newName: "RoomCatalogId");

            migrationBuilder.RenameIndex(
                name: "IX_Story_EntranceRoomId",
                table: "Story",
                newName: "IX_Story_RoomCatalogId");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Room",
                newName: "RoomCatalogId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_StoryId",
                table: "Room",
                newName: "IX_Room_RoomCatalogId");

            migrationBuilder.CreateTable(
                name: "RoomCatalog",
                columns: table => new
                {
                    RoomCatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntranceRoomId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCatalog", x => x.RoomCatalogId);
                    table.ForeignKey(
                        name: "FK_RoomCatalog_Room_EntranceRoomId",
                        column: x => x.EntranceRoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomCatalog_EntranceRoomId",
                table: "RoomCatalog",
                column: "EntranceRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomCatalog_RoomCatalogId",
                table: "Room",
                column: "RoomCatalogId",
                principalTable: "RoomCatalog",
                principalColumn: "RoomCatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_RoomCatalog_RoomCatalogId",
                table: "Story",
                column: "RoomCatalogId",
                principalTable: "RoomCatalog",
                principalColumn: "RoomCatalogId");
        }
    }
}
