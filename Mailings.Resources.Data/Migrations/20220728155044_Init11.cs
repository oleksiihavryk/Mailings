using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_FromId",
                table: "MailingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_Id",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailingGroups_FromId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "MailingGroups");

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
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "EmailAddressFrom",
                principalColumn: "Id");

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
                name: "FK_MailingGroups_EmailAddressFrom_Id",
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

            migrationBuilder.AddColumn<Guid>(
                name: "FromId",
                table: "MailingGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_FromId",
                table: "MailingGroups",
                column: "FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_FromId",
                table: "MailingGroups",
                column: "FromId",
                principalTable: "EmailAddressFrom",
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
