using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Initialize2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_MailingGroups_GroupId",
                table: "EmailSenders");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailSenders_FromId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailingGroups_FromId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_EmailSenders_GroupId",
                table: "EmailSenders");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "EmailSenders");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSenders_MailingGroups_Id",
                table: "EmailSenders",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_MailingGroups_Id",
                table: "EmailSenders");

            migrationBuilder.AddColumn<Guid>(
                name: "FromId",
                table: "MailingGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "EmailSenders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MailingGroups_FromId",
                table: "MailingGroups",
                column: "FromId",
                unique: true,
                filter: "[FromId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSenders_GroupId",
                table: "EmailSenders",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSenders_MailingGroups_GroupId",
                table: "EmailSenders",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailSenders_FromId",
                table: "MailingGroups",
                column: "FromId",
                principalTable: "EmailSenders",
                principalColumn: "Id");
        }
    }
}
