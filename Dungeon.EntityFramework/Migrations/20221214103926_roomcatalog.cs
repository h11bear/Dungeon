using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dungeon.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class roomcatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomCatalogId",
                table: "Story",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomCatalogId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomCatalog",
                columns: table => new
                {
                    RoomCatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCatalog", x => x.RoomCatalogId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Story_RoomCatalogId",
                table: "Story",
                column: "RoomCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomCatalogId",
                table: "Room",
                column: "RoomCatalogId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomCatalog_RoomCatalogId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Story_RoomCatalog_RoomCatalogId",
                table: "Story");

            migrationBuilder.DropTable(
                name: "RoomCatalog");

            migrationBuilder.DropIndex(
                name: "IX_Story_RoomCatalogId",
                table: "Story");

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomCatalogId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomCatalogId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "RoomCatalogId",
                table: "Room");
        }
    }
}
