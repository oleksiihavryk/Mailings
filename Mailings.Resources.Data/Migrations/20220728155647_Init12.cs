using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "EmailAddressFrom",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddressFrom_GroupId",
                table: "EmailAddressFrom",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_GroupId",
                table: "EmailAddressFrom",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_GroupId",
                table: "EmailAddressFrom");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddressFrom_GroupId",
                table: "EmailAddressFrom");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "EmailAddressFrom");
        }
    }
}
