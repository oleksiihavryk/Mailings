using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init65 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mail_MailingGroups_Id",
                table: "Mail");

            migrationBuilder.AddColumn<Guid>(
                name: "MailId",
                table: "MailingGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_MailId",
                table: "MailingGroups",
                column: "MailId",
                unique: true,
                filter: "[MailId] IS NOT NULL");

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
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailingGroups_MailId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "MailId",
                table: "MailingGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_MailingGroups_Id",
                table: "Mail",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
