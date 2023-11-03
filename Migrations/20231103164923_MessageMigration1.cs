using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messager.Migrations
{
    public partial class MessageMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reciverid",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Creatorid",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Creatorid",
                table: "Messages",
                column: "Creatorid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Reciverid",
                table: "Messages",
                column: "Reciverid");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_Creatorid",
                table: "Messages",
                column: "Creatorid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_Reciverid",
                table: "Messages",
                column: "Reciverid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_Creatorid",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_Reciverid",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Creatorid",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Reciverid",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "Reciverid",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Creatorid",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
