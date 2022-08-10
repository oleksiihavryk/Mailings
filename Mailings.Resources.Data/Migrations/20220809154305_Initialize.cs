using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress", x => x.Id);
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
                name: "UserMails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMails_EmailAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "EmailAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMails_EmailAddress_Id",
                        column: x => x.Id,
                        principalTable: "EmailAddress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Mail_MailId",
                        column: x => x.MailId,
                        principalTable: "Mail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailReceivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailReceivers_EmailAddress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "EmailAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailSenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PseudoName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailingGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailingGroups_EmailSenders_FromId",
                        column: x => x.FromId,
                        principalTable: "EmailSenders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MailingGroups_Mail_Id",
                        column: x => x.Id,
                        principalTable: "Mail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailingHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    When = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSucceded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailingHistory_MailingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MailingGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MailId",
                table: "Attachment",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailReceivers_AddressId",
                table: "EmailReceivers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailReceivers_GroupId",
                table: "EmailReceivers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSenders_GroupId",
                table: "EmailSenders",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_FromId",
                table: "MailingGroups",
                column: "FromId",
                unique: true,
                filter: "[FromId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MailingHistory_GroupId",
                table: "MailingHistory",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMails_AddressId",
                table: "UserMails",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSenders_MailingGroups_GroupId",
                table: "EmailSenders",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_Id",
                table: "MailingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_MailingGroups_GroupId",
                table: "EmailSenders");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "EmailReceivers");

            migrationBuilder.DropTable(
                name: "MailingHistory");

            migrationBuilder.DropTable(
                name: "UserMails");

            migrationBuilder.DropTable(
                name: "EmailAddress");

            migrationBuilder.DropTable(
                name: "Mail");

            migrationBuilder.DropTable(
                name: "MailingGroups");

            migrationBuilder.DropTable(
                name: "EmailSenders");
        }
    }
}
