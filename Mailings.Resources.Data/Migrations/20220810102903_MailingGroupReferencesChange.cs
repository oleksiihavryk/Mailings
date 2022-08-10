using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class MailingGroupReferencesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_MailingGroups_Id",
                table: "EmailSenders");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_Id",
                table: "MailingGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "MailId",
                table: "MailingGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
