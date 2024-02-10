using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace messager.Migrations
{
    public partial class accountmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageList_Offer_UserId1",
                table: "ImageList");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "ImageList",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageList_AspNetUsers_UserId1",
                table: "ImageList",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageList_AspNetUsers_UserId1",
                table: "ImageList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "ImageList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AltLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelfLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ImageList_Offer_UserId1",
                table: "ImageList",
                column: "UserId1",
                principalTable: "Offer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
