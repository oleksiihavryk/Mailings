using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressTo_MailingGroups_GroupId",
                table: "EmailAddressTo");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_MailingHistory_Id",
                table: "MailingGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "MailingHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailingHistory_GroupId",
                table: "MailingHistory",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressTo_MailingGroups_GroupId",
                table: "EmailAddressTo",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "EmailAddressFrom",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingHistory_MailingGroups_GroupId",
                table: "MailingHistory",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressTo_MailingGroups_GroupId",
                table: "EmailAddressTo");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingHistory_MailingGroups_GroupId",
                table: "MailingHistory");

            migrationBuilder.DropIndex(
                name: "IX_MailingHistory_GroupId",
                table: "MailingHistory");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "MailingHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressTo_MailingGroups_GroupId",
                table: "EmailAddressTo",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_MailingHistory_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "MailingHistory",
                principalColumn: "Id");
        }
    }
}
