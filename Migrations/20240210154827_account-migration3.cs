using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messager.Migrations
{
    public partial class accountmigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageList_AspNetUsers_UserId1",
                table: "ImageList");

            migrationBuilder.DropIndex(
                name: "IX_ImageList_UserId1",
                table: "ImageList");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ImageList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ImageList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ImageList_UserId",
                table: "ImageList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageList_AspNetUsers_UserId",
                table: "ImageList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageList_AspNetUsers_UserId",
                table: "ImageList");

            migrationBuilder.DropIndex(
                name: "IX_ImageList_UserId",
                table: "ImageList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ImageList",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ImageList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageList_UserId1",
                table: "ImageList",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageList_AspNetUsers_UserId1",
                table: "ImageList",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
