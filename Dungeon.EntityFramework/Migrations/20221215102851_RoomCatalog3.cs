using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dungeon.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RoomCatalog3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Room_EntranceRoomId",
                table: "Story");

            migrationBuilder.DropIndex(
                name: "IX_Story_EntranceRoomId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "EntranceRoomId",
                table: "Story");

            migrationBuilder.AddColumn<int>(
                name: "EntranceRoomId",
                table: "RoomCatalog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomCatalog_EntranceRoomId",
                table: "RoomCatalog",
                column: "EntranceRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomCatalog_Room_EntranceRoomId",
                table: "RoomCatalog",
                column: "EntranceRoomId",
                principalTable: "Room",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomCatalog_Room_EntranceRoomId",
                table: "RoomCatalog");

            migrationBuilder.DropIndex(
                name: "IX_RoomCatalog_EntranceRoomId",
                table: "RoomCatalog");

            migrationBuilder.DropColumn(
                name: "EntranceRoomId",
                table: "RoomCatalog");

            migrationBuilder.AddColumn<int>(
                name: "EntranceRoomId",
                table: "Story",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Story_EntranceRoomId",
                table: "Story",
                column: "EntranceRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Room_EntranceRoomId",
                table: "Story",
                column: "EntranceRoomId",
                principalTable: "Room",
                principalColumn: "RoomId");
        }
    }
}
