using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddress_EmailAddressFrom_EmailAddressFromId",
                table: "EmailAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddress_EmailAddressTo_Id",
                table: "EmailAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_MailingHistory_HistoryNoteMailingGroupId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailingGroups_HistoryNoteMailingGroupId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddress_EmailAddressFromId",
                table: "EmailAddress");

            migrationBuilder.DropColumn(
                name: "HistoryNoteMailingGroupId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "EmailAddressFromId",
                table: "EmailAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "EmailAddressTo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "EmailAddressFrom",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddressTo_AddressId",
                table: "EmailAddressTo",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddressFrom_AddressId",
                table: "EmailAddressFrom",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_EmailAddress_AddressId",
                table: "EmailAddressFrom",
                column: "AddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressTo_EmailAddress_AddressId",
                table: "EmailAddressTo",
                column: "AddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_MailingHistory_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "MailingHistory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_EmailAddress_AddressId",
                table: "EmailAddressFrom");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressTo_EmailAddress_AddressId",
                table: "EmailAddressTo");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_MailingHistory_Id",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddressTo_AddressId",
                table: "EmailAddressTo");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddressFrom_AddressId",
                table: "EmailAddressFrom");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "EmailAddressTo");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "EmailAddressFrom");

            migrationBuilder.AddColumn<Guid>(
                name: "HistoryNoteMailingGroupId",
                table: "MailingGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmailAddressFromId",
                table: "EmailAddress",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_HistoryNoteMailingGroupId",
                table: "MailingGroups",
                column: "HistoryNoteMailingGroupId",
                unique: true,
                filter: "[HistoryNoteMailingGroupId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_EmailAddressFromId",
                table: "EmailAddress",
                column: "EmailAddressFromId",
                unique: true,
                filter: "[EmailAddressFromId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddress_EmailAddressFrom_EmailAddressFromId",
                table: "EmailAddress",
                column: "EmailAddressFromId",
                principalTable: "EmailAddressFrom",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddress_EmailAddressTo_Id",
                table: "EmailAddress",
                column: "Id",
                principalTable: "EmailAddressTo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "EmailAddressFrom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_MailingHistory_HistoryNoteMailingGroupId",
                table: "MailingGroups",
                column: "HistoryNoteMailingGroupId",
                principalTable: "MailingHistory",
                principalColumn: "Id");
        }
    }
}
