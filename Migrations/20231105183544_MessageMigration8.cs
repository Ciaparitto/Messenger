using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messager.Migrations
{
    public partial class MessageMigration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReciverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageList_AspNetUsers_ReciverId",
                        column: x => x.ReciverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageList_ReciverId",
                table: "MessageList",
                column: "ReciverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageList");
        }
    }
}
