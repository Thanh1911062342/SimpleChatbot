using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleChatBot.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    KeyActive = table.Column<string>(type: "nvarchar(max)", unicode: false, nullable: true),
                    Ip = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    KeyDuration = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendMail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendMail_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SendMail_AccountId",
                table: "SendMail",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendMail");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
