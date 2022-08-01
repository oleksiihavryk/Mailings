using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_MailingGroups_EmailAddressFrom_FromId",
                table: "MailingGroups",
                column: "FromId",
                principalTable: "EmailAddressFrom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_FromId",
                table: "MailingGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailingGroups_FromId",
                table: "MailingGroups");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "MailingGroups");
        }
    }
}
