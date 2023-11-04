using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messager.Migrations
{
    public partial class MessageMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatorId",
                table: "Messages",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_CreatorId",
                table: "Messages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_CreatorId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_CreatorId",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
