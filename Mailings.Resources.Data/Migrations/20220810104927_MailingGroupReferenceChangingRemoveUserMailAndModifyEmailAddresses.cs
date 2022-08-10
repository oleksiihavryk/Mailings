using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class MailingGroupReferenceChangingRemoveUserMailAndModifyEmailAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_MailingGroups_Id",
                table: "EmailSenders");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_Id",
                table: "MailingGroups");

            migrationBuilder.DropTable(
                name: "UserMails");

            migrationBuilder.AddColumn<Guid>(
                name: "MailId",
                table: "MailingGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EmailAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_MailId",
                table: "MailingGroups",
                column: "MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailSenders_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "EmailSenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups",
                column: "MailId",
                principalTable: "Mail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailSenders_Id",
                table: "MailingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailingGroups_MailId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "MailId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailAddress");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserMails_AddressId",
                table: "UserMails",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSenders_MailingGroups_Id",
                table: "EmailSenders",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_Mail_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "Mail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
