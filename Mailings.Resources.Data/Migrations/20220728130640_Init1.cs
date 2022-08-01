using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailAddressFrom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddressFrom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ByteContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StringContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailingHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    When = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSucceded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailingGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistoryNoteMailingGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailingGroups_EmailAddressFrom_Id",
                        column: x => x.Id,
                        principalTable: "EmailAddressFrom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MailingGroups_MailingHistory_HistoryNoteMailingGroupId",
                        column: x => x.HistoryNoteMailingGroupId,
                        principalTable: "MailingHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailAddressTo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddressTo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAddressTo_MailingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MailingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddressFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAddress_EmailAddressFrom_EmailAddressFromId",
                        column: x => x.EmailAddressFromId,
                        principalTable: "EmailAddressFrom",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailAddress_EmailAddressTo_Id",
                        column: x => x.Id,
                        principalTable: "EmailAddressTo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_EmailAddressFromId",
                table: "EmailAddress",
                column: "EmailAddressFromId",
                unique: true,
                filter: "[EmailAddressFromId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddressTo_GroupId",
                table: "EmailAddressTo",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_HistoryNoteMailingGroupId",
                table: "MailingGroups",
                column: "HistoryNoteMailingGroupId",
                unique: true,
                filter: "[HistoryNoteMailingGroupId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAddress");

            migrationBuilder.DropTable(
                name: "Mail");

            migrationBuilder.DropTable(
                name: "EmailAddressTo");

            migrationBuilder.DropTable(
                name: "MailingGroups");

            migrationBuilder.DropTable(
                name: "EmailAddressFrom");

            migrationBuilder.DropTable(
                name: "MailingHistory");
        }
    }
}
