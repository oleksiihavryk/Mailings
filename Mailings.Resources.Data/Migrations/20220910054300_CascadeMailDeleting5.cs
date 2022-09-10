using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class CascadeMailDeleting5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups",
                column: "MailId",
                principalTable: "Mail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_Mail_MailId",
                table: "MailingGroups",
                column: "MailId",
                principalTable: "Mail",
                principalColumn: "Id");
        }
    }
}
